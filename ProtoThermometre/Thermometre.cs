using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Media.Media3D;
using System.Windows.Media;

namespace ProtoThermometre
{
    public class Thermometre
    {
        /// <summary>
        /// Définit la représentation de la couleur de la ligne en système hexadecimal
        /// </summary>
        public static string? CouleurLigne;
        /// <summary>
        /// Définit la représentation de la couleur des pôles d'échange en système hexadecimal
        /// </summary>
        private static string? CouleurPoleEchange;
        /// <summary>
        /// Définit une liste d'objets de type Arret pour tous les arrêts de la ligne
        /// </summary>
        private static List<Arret>? LesArrets;
        /// <summary>
        /// Associe toutes les lignes du réseau à sa couleur hexadécimale
        /// </summary>
        private static List<Ligne>? LesLignes;

        /// <summary>
        /// Constructeur de la classe Thermometre
        /// </summary>
        /// <param name="couleurLigne">Couleur de ligne</param>
        /// <param name="couleurPoleEchange">Couleur du pole d'échange</param>
        /// <param name="lesArrets">Liste des arrêts</param>
        /// <param name="lesLignes">Liste des lignes</param>
        public Thermometre(string couleurLigne, string couleurPoleEchange, List<Arret> lesArrets, List<Ligne> lesLignes)
        { 
            CouleurLigne = couleurLigne;
            CouleurPoleEchange = couleurPoleEchange;
            LesArrets = lesArrets;
            LesLignes = lesLignes;
        }

        /// <summary>
        /// Retourne la couleur de la ligne du thermomètre
        /// </summary>
        /// <value>CouleurLigne</value>
        public string GetCouleurLigne
        {
            get
            {
                return CouleurLigne;
            }
        }

        /// <summary>
        /// Retourne la couleur de correspondance d'un pole d'échange
        /// </summary>
        /// <value>CouleurPoleEchange</value>
        public string GetCouleurPoleEchange
        {
            get
            {
                return CouleurPoleEchange;
            }
        }

        /// <summary>
        /// Retourne une liste d'objets de type Arret
        /// </summary>
        /// <value>LesArrets</value>
        public List<Arret> GetLesArrets
        {
            get
            {
                return LesArrets;
            }
        }

        /// <summary>
        /// Retourne une liste d'objets de type Ligne
        /// </summary>
        /// <value>LesLignes</value>
        public List<Ligne> GetLesLignes
        {
            get
            {
                return LesLignes;
            }
        }

        /// <summary>
        /// Permet la création du thermomètre
        /// </summary>
        public void Construire()
        {
            var abscisse = 0;
            var ordonnee = 250;
            
            foreach (var arret in LesArrets)
            {
                AjouterArret(arret.GetNom, arret.GetPositionX, arret.GetPositionY, arret.GetEstCorrespondance, arret.GetEstPoleEchange, arret.GetLesCorrespondances);
                abscisse = abscisse + 35;
            }

            // Ajout des arcs entre les arrêts
            for (int i=0; i<LesArrets.Count-1; i++)
            {
                AjouterArc(LesArrets[i].GetNom, LesArrets[i+1].GetNom);
            }

        }

        /// <summary>
        /// Permet de trouver le premier et le dernier arrêt du thermomètre
        /// </summary>
        /// <returns>Dictionary<string,bool></returns>
        private static Dictionary<string, bool> PremierDernierArret()
        {
            Dictionary<string, bool> premierDernierArret = new Dictionary<string, bool>();

            bool estPremierArret = false;
            bool estDernierArret = false;

            if (LesArrets.Count > 0)
            {
                estPremierArret = true;
                estDernierArret = true;

                foreach (var arret in LesArrets)
                {
                    string nomArret = (string)arret.GetNom;

                    if (arret == LesArrets.First())
                    {
                        premierDernierArret.Add(nomArret, true);
                    }
                    else
                    {
                        premierDernierArret.Add(nomArret, false);
                    }

                    if (arret == LesArrets.Last())
                    {
                        premierDernierArret[nomArret] = true;
                    }
                }
            }

            return premierDernierArret;
        }

        /// <summary>
        /// Permet d'ajouter un arrêt sur le graphe du thermomètre par ses coordonnées
        /// </summary>
        /// <param name="nomArret">Nom de l'arrêt</param>
        /// <param name="x">Abascisse de l'arrêt</param>
        /// <param name="y">Ordonnée de l'arrêt</param>
        private static void AjouterArret(string nomArret, double x, double y, bool estCorrespondance, bool estPoleEchange, List<string> lesCorrespondances)
        {
            //Création d'un point d'arrêt avec Ellipse
            Ellipse pointArret = new Ellipse
            {
                Width = 15,
                Height = 15,
                Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(CouleurLigne)),
                Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString(CouleurLigne)),
                StrokeThickness = 1,
                Tag = nomArret,
            };

            bool estPremierArret = false;
            bool estDernierArret = false;

            string nomArretEllipse = (string)pointArret.Tag;

            if (LesArrets.Count > 0)
            {
                estPremierArret = LesArrets[0].GetNom == nomArretEllipse;
                estDernierArret = LesArrets[LesArrets.Count - 1].GetNom == nomArretEllipse;
            }

            if (estPremierArret || estDernierArret)
            {
                pointArret.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(CouleurPoleEchange));
                pointArret.Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString(CouleurPoleEchange));
            }

            //Positionnement d'un point d'arrêt en abscisse et ordonnée
            Canvas.SetLeft(pointArret, x);
            Canvas.SetTop(pointArret, y);

            //Création d'une étiquette de correspondance avec Label
            Label etiquetteCorrespondance = new Label
            {
                Content = "C",
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };

            // Positionnement de l'étiquette de correspondance
            Canvas.SetLeft(etiquetteCorrespondance, x - 2);
            Canvas.SetTop(etiquetteCorrespondance, y - 6);

            //Création d'une étiquette pour le nom des arrêts avec un Label
            Label etiquetteNomArret = new Label
            {
                Content = nomArret,
                FontSize = 15,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center
            };

            double positionX = Canvas.GetLeft(pointArret) - 5;
            double positionY = Canvas.GetTop(pointArret) + 30;

            if (estCorrespondance)
            {
                etiquetteCorrespondance.Foreground = Brushes.White;

                foreach(var correspondance in lesCorrespondances)
                {
                    foreach(var ligne in LesLignes)
                    {
                        Rectangle modelisationLigne = new Rectangle
                        {
                            Width = 30,
                            Height = 20,
                            Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ligne.GetCouleur)),
                            Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ligne.GetCouleur)),
                            RadiusX = 2,
                            RadiusY = 2,
                            Tag = ligne.GetNumero,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center
                        };

                        Label numeroLigne = new Label
                        {
                            Width = 35,
                            Height = 25,
                            Content = ligne.GetNumero,
                            FontSize = 12,
                            FontWeight = FontWeights.Bold,
                            Foreground = Brushes.White,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center,
                            Tag = ligne.GetNumero
                        };

                        // Récupération des nuémros de lignes dessinés par un rectangle
                        var tag = modelisationLigne.Tag.ToString();

                        for(int i=0; i < LesArrets.Count; i++)
                        {
                            if(LesArrets[i].GetLesCorrespondances.Contains(tag))
                            {
                                if (tag.Length < 2)                                      //Si 1 caractère
                                {
                                    modelisationLigne.Height = 20;
                                    modelisationLigne.Width = 20;

                                    Canvas.SetLeft(numeroLigne, positionX + 3.5);
                                    Canvas.SetTop(numeroLigne, positionY - 0.5);
                                    //canvas.Children.Add(modelisationLigne);

                                    Canvas.SetLeft(modelisationLigne, positionX + 2);
                                    Canvas.SetTop(modelisationLigne, positionY + 3);
                                    //canvas.Children.Add(numeroLigne);

                                    positionY = positionY + 25;
                                }
                                else if (tag.Length < 3)                                 //Si 2 caractères
                                {
                                    modelisationLigne.Height = 20;
                                    modelisationLigne.Width = 20;

                                    Canvas.SetLeft(numeroLigne, positionX - 0.1);
                                    Canvas.SetTop(numeroLigne, positionY - 0.5);
                                    //canvas.Children.Add(modelisationLigne);

                                    Canvas.SetLeft(modelisationLigne, positionX + 2);
                                    Canvas.SetTop(modelisationLigne, positionY + 3);
                                    //canvas.Children.Add(numeroLigne);

                                    positionY = positionY + 25;
                                }
                                else if (tag.Length < 5)                                //Si 3 caractères
                                {
                                    modelisationLigne.Height = 20;
                                    modelisationLigne.Width = 30;

                                    Canvas.SetLeft(numeroLigne, positionX);
                                    Canvas.SetTop(numeroLigne, positionY - 0.5);
                                    //canvas.Children.Add(modelisationLigne);

                                    Canvas.SetLeft(modelisationLigne, positionX + 2);
                                    Canvas.SetTop(modelisationLigne, positionY + 3);
                                    //canvas.Children.Add(numeroLigne);

                                    positionY = positionY + 25;
                                }
                            }
                        }
                    }
                }
            }

            if (estPoleEchange && !estPremierArret && !estDernierArret)
            {
                etiquetteCorrespondance.Foreground = Brushes.Black;
                etiquetteNomArret.FontWeight = FontWeights.Bold;
            }


        }

        /// <summary>
        /// Permet d'ajouter un arc entre les arrêts du thermomètre
        /// </summary>
        /// <param name="nomArretPrecedent">Arrêt précédent</param>
        /// <param name="nomArretSuivant">Arrêt suivant</param>
        private static void AjouterArc(string nomArretPrecedent, string nomArretSuivant)
        {

        }

        /// <summary>
        /// Permet de trouver un arrêt spécifique au thermomètre
        /// </summary>
        /// <param name="nomArret">Nom de l'arrêt</param>
        /// <returns>FrameworkElement</returns>
        private FrameworkElement TrouverArret(string nomArret)
        {
            return new FrameworkElement();
        }
    }
}
