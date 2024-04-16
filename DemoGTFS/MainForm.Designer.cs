/*
 * Crée par SharpDevelop.
 * Utilisateur: vcadivel
 * Date: 04/04/2024
 * Heure: 15:32
 * 
 * Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
 */
namespace DemoGTFS
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.DataGridView dataGridView_fichierGTFS;
		private System.Windows.Forms.Button button_choisir;
		private System.Windows.Forms.Label label_cheminFichierGTFS;
		private System.Windows.Forms.OpenFileDialog openFileDialog_fichierGTFS;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.dataGridView_fichierGTFS = new System.Windows.Forms.DataGridView();
			this.button_choisir = new System.Windows.Forms.Button();
			this.label_cheminFichierGTFS = new System.Windows.Forms.Label();
			this.openFileDialog_fichierGTFS = new System.Windows.Forms.OpenFileDialog();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView_fichierGTFS)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridView_fichierGTFS
			// 
			this.dataGridView_fichierGTFS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridView_fichierGTFS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView_fichierGTFS.Location = new System.Drawing.Point(10, 44);
			this.dataGridView_fichierGTFS.Name = "dataGridView_fichierGTFS";
			this.dataGridView_fichierGTFS.RowTemplate.Height = 24;
			this.dataGridView_fichierGTFS.Size = new System.Drawing.Size(795, 459);
			this.dataGridView_fichierGTFS.TabIndex = 0;
			// 
			// button_choisir
			// 
			this.button_choisir.Location = new System.Drawing.Point(12, 14);
			this.button_choisir.Name = "button_choisir";
			this.button_choisir.Size = new System.Drawing.Size(66, 24);
			this.button_choisir.TabIndex = 1;
			this.button_choisir.Text = "Choisir";
			this.button_choisir.UseVisualStyleBackColor = true;
			this.button_choisir.Click += new System.EventHandler(this.Button_choisirClick);
			// 
			// label_cheminFichierGTFS
			// 
			this.label_cheminFichierGTFS.Location = new System.Drawing.Point(84, 14);
			this.label_cheminFichierGTFS.Name = "label_cheminFichierGTFS";
			this.label_cheminFichierGTFS.Size = new System.Drawing.Size(720, 24);
			this.label_cheminFichierGTFS.TabIndex = 2;
			this.label_cheminFichierGTFS.Text = "label1";
			// 
			// openFileDialog_fichierGTFS
			// 
			this.openFileDialog_fichierGTFS.FileName = "openFileDialog1";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(816, 515);
			this.Controls.Add(this.label_cheminFichierGTFS);
			this.Controls.Add(this.button_choisir);
			this.Controls.Add(this.dataGridView_fichierGTFS);
			this.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "MainForm";
			this.Text = "DemoGTFS";
			this.Load += new System.EventHandler(this.MainFormLoad);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView_fichierGTFS)).EndInit();
			this.ResumeLayout(false);

		}
	}
}
