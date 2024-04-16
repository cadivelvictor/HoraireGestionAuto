using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtoThermometre
{
    public class Ligne
    {
        /// <summary>
        /// Définit le numéro d'une ligne
        /// </summary>
        private string Numero;
        /// <summary>
        /// Définit la couleur d'une ligne en système hexadécimal
        /// </summary>
        private string Couleur;

        /// <summary>
        /// Constructeur de la classe Ligne
        /// </summary>
        /// <param name="numero">Numéro de la ligne</param>
        /// <param name="couleur">Couleur hexadécimale de la ligne</param>
        public Ligne(string numero, string couleur)
        {
            Numero = numero;
            Couleur = couleur;
        }

        /// <summary>
        /// Retourne le numéro de la ligne
        /// </summary>
        /// <value>this.Numeroe</value>
        public string GetNumero
        {
            get 
            { 
                return this.Numero; 
            }
        }

        /// <summary>
        /// Retourne la couleur de la ligne
        /// </summary>
        /// <value>this.Couleur</value>
        public string GetCouleur
        {
            get 
            { 
                return this.Couleur; 
            }
        }
    }
}
