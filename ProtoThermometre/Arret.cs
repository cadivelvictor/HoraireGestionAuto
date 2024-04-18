using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtoThermometre
{
    public class Arret
    {
        /// <summary>
        /// Définit le nom d'un arrêt pour une ligne
        /// </summary>
        private string Nom;
        /// <summary>
        /// Définit si un arrêt est une correspondance
        /// </summary>
        private bool EstCorrespondance;
        /// <summary>
        /// Définit si un arrêt est un pôle d'échange
        /// </summary>
        private bool EstPoleEchange;
        /// <summary>
        /// Définit si un arrêt possède des correspondances avec d'autres lignes
        /// </summary>
        private List<string> LesCorrespondances;

        /// <summary>
        /// Constructeur de la classe Arret
        /// </summary>
        /// <param name="nom">Nom de l'arrêt</param>
        /// <param name="estCorrespondance">L'arrêt est une correspondance : true (oui) ou false (non)</param>
        /// <param name="estPoleEchange"><L'arrêt est un pôle d'échange : true (oui) ou false (non)/param>
        /// <param name="lesCorrespondances">Lignes de correspondances pour l'arrêt</param>
        public Arret(string nom,bool estCorrespondance, bool estPoleEchange, List<string> lesCorrespondances)
        {
            this.Nom = nom;
            this.EstCorrespondance = estCorrespondance;
            this.EstPoleEchange = estPoleEchange;
            this.LesCorrespondances = lesCorrespondances;
        }

        /// <summary>
        /// Retourne le nom de l'arrêt
        /// </summary>
        /// <value>this.Nom</value>
        public string GetNom
        {
            get 
            { 
                return this.Nom; 
            }
        }

        /// <summary>
        /// Retourne vrai ou faux si l'arrêt est ou n'est pas une correspondance
        /// </summary>
        /// <value>this.EstCorrespondance</value>
        public bool GetEstCorrespondance
        {
            get
            {
                return this.EstCorrespondance;
            }
        }

        /// <summary>
        /// Retourne vrai ou faux si l'arrêt est ou n'est pas un pôle d'échange
        /// </summary>
        /// <value>this.EstPoleEchange</value>
        public bool GetEstPoleEchange
        {
            get
            {
                return this.EstPoleEchange;
            }
        }

        /// <summary>
        /// Retourne la liste des correspondances de l'arrêt
        /// </summary>
        /// <value>this.LesCorrespondances</value>
        public List<string> GetLesCorrespondances
        {
            get
            {
                return this.LesCorrespondances;
            }
        }
    }
}