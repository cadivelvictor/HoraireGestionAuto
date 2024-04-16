/*
 * Crée par SharpDevelop.
 * Utilisateur: vcadivel
 * Date: 04/04/2024
 * Heure: 15:32
 * 
 * Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.ComponentModel;
using System.Threading.Tasks;
using System.IO;

namespace DemoGTFS
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		/**
		 * Cette fonction permet de lire un fichier GTFS
		 * <param>Repertoire et nom du fichier GTFS</param>
		 * <returns>Un tableau de données </returns>
		 */
		
		public DataTable lectureGTFS(string nomFichier) 
		{
			DataTable tableDonnees = new DataTable("Data");
			using(OleDbConnection connexion = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=\"" + Path.GetDirectoryName(nomFichier) + "\";Extended Properties='text;HDR=yes;FMT=Delimited(,);CharacterSet=65001;'"))
			{
				using(OleDbCommand commande = new OleDbCommand(string.Format("SELECT * FROM [{0}]", new FileInfo(nomFichier).Name), connexion))
				{
					connexion.Open();
					using(OleDbDataAdapter adapter = new OleDbDataAdapter(commande))
					{
						adapter.Fill(tableDonnees);
					}
				}
			}
			
			return tableDonnees;
		}
		
		void MainFormLoad(object sender, EventArgs e)
		{
			this.label_cheminFichierGTFS.Text = "";
		}
		
		void Button_choisirClick(object sender, EventArgs e)
		{
			try
			{
				using(OpenFileDialog boiteDialogueOuvertureFichier = new OpenFileDialog() {Filter = "Fichiers GTFS|*.txt", ValidateNames = true, Multiselect = false })
				{
					if(boiteDialogueOuvertureFichier.ShowDialog() == DialogResult.OK) 
					{
						this.label_cheminFichierGTFS.Text = boiteDialogueOuvertureFichier.FileName;
						dataGridView_fichierGTFS.DataSource = lectureGTFS(boiteDialogueOuvertureFichier.FileName);
					}
				}
			} 
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			
		}
	}
}
