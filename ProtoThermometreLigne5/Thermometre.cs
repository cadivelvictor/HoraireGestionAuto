using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace ProtoThermometreLigne5
{
    public class Thermometre
    {
        private string CouleurLigne { get; set; }
        private string CouleurPoleEchange { get; set; }
        public static List<Arret> LesArrets { get; set; }
        private Dictionary<String, String> Lignes { get; set; }

        public Thermometre(string couleurLigne, string couleurPoleEchange, List<Arret> lesArrets, Dictionary<String, String> lignes) 
        {
            if (couleurLigne.Length != 7 || couleurPoleEchange.Length != 7)
            {
                throw new ArgumentException("Les codes couleur doivent avoir une longueur de 7 caractères.");
            }

            CouleurLigne = couleurLigne;
            CouleurPoleEchange = couleurPoleEchange;
            LesArrets = lesArrets;
            Lignes = lignes;
        }

        public void Construire()
        {

        }

        public void AjouterArret(string nomArret, double abscisse, double ordonnee, bool estCorrespondance)
        {

        }

        public void AjouterArc(string arretPrecedent, string arretSuivant)
        {

        }

        public void EstCorrespondance(string nomArret, double abscisse, double ordonnee, Ellipse arret, bool estCorrespondance)
        {

        }

        public void ExtensionItineraire(string nomArret, double abscisse, double ordonnee, Ellipse arret, Label etiquetteArret)
        { 
            
        }

        private FrameworkElement TrouverArret(string nomArret)
        {
            return null;
        }
    }
}
