/*
 * Crée par SharpDevelop.
 * Utilisateur: vcadivel
 * Date: 04/04/2024
 * Heure: 17:07
 * 
 * Prototypage de création du thermomètre de la ligne 5
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

using System.Windows.Shapes;
using System.IO;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.Diagnostics;
using System.Windows.Ink;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Linq;


namespace ProtoThermometreLigne5
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class Window1 : Window
	{
        public string couleurLigne = "#E30613";
        public string couleurPoleEchange = "#ACC32B";
        public List<Arret> lesArrets = new List<Arret>
        {
            new Arret("STADE DE L’EST", true, false, new List<string>{"15", "26", "27", "27A", "31", "33"}),
            new Arret("Parc des Expositions", true, false, new List<string> { "27", "27A", "33" }),
            new Arret("ZEC du Chaudron", true, false, new List <string> { "27", "27A", "33" }),
            new Arret("Manès", true, false, null),
            new Arret("Pierre Aubert", true, false, new List<string> { "27", "27A", "33" }),
            new Arret("Roger Payet", true, false, new List<string> { "27", "27A", "31", "33" }),
            new Arret("Station Chaudron", true, true, new List <string> { "1", "24", "24A", "25", "26", "27", "27A", "28", "29A", "31", "32", "33", "40" }),
            new Arret("Mail du Chaudron", true, true, new List<string> { "1", "6", "8" }),
            new Arret("Lacroix", true, false, new List<string> { "1", "6", "8" }),
            new Arret("Sainte-Clotilde Centre", true, false, new List<string> { "1", "6", "7", "8" }),
            new Arret("Banian", true, false, new List<string> { "1", "6", "7", "8" }),
            new Arret("Deux Canons", true, false, new List<string> { "1", "6", "7", "8", "15" }),
            new Arret("Parc Aquatique", true, true, new List<string> { "1", "6", "7", "8", "10", "15", "19" }),
            new Arret("Butor", true, false, new List<string> { "1", "6", "7", "8" }),
            new Arret("Hôtel des Impôts", true, false, new List<string> { "1", "6", "7", "8" }),
            new Arret("Camp Jacquot", true, false, new List<string> { "1", "6", "7", "8" }),
            new Arret("Saint-Jacques", true, false, new List<string> { "1", "6", "7", "8" }),
            new Arret("Petit Marché", true, false, new List<string> { "6", "7", "8", "13", "14" }),
            new Arret("École Centrale", true, false, new List<string> { "6", "7", "8", "13", "14" }),
            new Arret("Rieul", true, false, new List<string> { "6", "7", "8", "13", "14" }),
            new Arret("HÔTEL DE VILLE DE SAINT-DENIS", true, true, new List<string> { "6", "7", "8", "10", "11", "12", "13", "14", "16", "19", "21", "22", "22A", "23" })
        };


        public readonly Dictionary<String, String> lignes = new Dictionary<string, string>
        {
            {"1", "#008000"}, {"5", "#E30613"}, {"6", "#4F80BD"}, {"7", "#76B82A"}, {"8", "#F39200"}, {"10", "#FFDD00"}, {"11", "#A85E24"},
            {"12", "#EA5297"}, {"12A", "#EA5297"},{"12B", "#EA5297"}, {"13", "#00A68B"}, {"14", "#A2A628"}, {"15", "#ED925F"}, {"16", "#BFD243"},
            {"19", "#A61D81"}, {"20", "#2DAFE6"}, {"21", "#76B88E"}, {"21A", "#76B88E"}, {"22", "#F3953F"}, {"22A", "#F3953F"}, {"23", "#8577B6"},
            {"23A", "#8577B6"},{"23B", "#8577B6"}, {"23C", "#8577B6"}, {"23D", "#8577B6"}, {"23E", "#8577B6"}, {"24", "#004F9F"}, {"24A", "#004F9F"},
            {"24B", "#004F9F"},{"25", "#72787A"}, {"25A", "#72787A"}, {"26", "#00823F"}, {"26A", "#00823F"}, {"27", "#009FE3"}, {"27A", "#009FE3"},
            {"28", "#EE758F"}, {"29", "#BD2B0B"}, {"29A", "#BD2B0B"}, {"30", "#BC731E"}, {"31", "#B14D97"}, {"32", "#7E4900"}, {"33", "#C8D400"},
            {"34", "#C6C6C6"}, {"36", "#FFCC07"}, {"37", "#AFCA13"}, {"38", "#0A9CA5"}, {"39", "#706F6F"}, {"40", "#13A538"}, {"41", "#F39200"},
            {"42", "#BA4E97"}, {"44A", "#73858E"},{"46", "#E4032E"}, {"47", "#276499"}, {"48", "#5BC5F2"}, {"48A", "#5BC5F2"}, {"49", "#A98F00"},
            {"50", "#FFCC00"}, {"51", "#009BA4"}, {"51A", "#009BA4"}, {"53", "#12A33A"},{"54", "#DE85B6"}, {"60", "#08A339"}, {"61", "#DF3439"},
            {"62", "#EA8C36"}, {"63", "#1D9DD9"}, {"64", "#C8D400"}, {"65", "#7D8386"}, {"67", "#7C6EB0"}, {"68", "#F39200"}, {"69", "#006878"}
        };

        public Window1()
		{
			InitializeComponent();
			ConstruireThermometre();

            WindowState = WindowState.Maximized;
        }

        private void ConstruireThermometre()
        {
            var abscisse = 0;
            var ordonnee = 250;

            foreach (var arret in lesArrets)
            {
                AjouterArret(arret.Nom, abscisse, ordonnee, arret.EstCorrespondance, arret.EstPoleEchange, lesArrets);
                abscisse = abscisse + 35;
            }

            // Ajout des arcs entre les arrêts
            for (int i = 0; i < lesArrets.Count - 1; i++)
            {
                AjouterArc(lesArrets[i].Nom, lesArrets[i + 1].Nom);
            }
        }

        private void AjouterArret(string nomArret, double x, double y, bool estCorrespondance, bool estPoleEchange, List<Arret> lesArrets)
        {
            //Création d'un arrêt avec Ellipse
            var arret = new Ellipse
            {
                Width = 15,
                Height = 15,
                Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(couleurLigne)),
                Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString(couleurLigne)),
                StrokeThickness = 2,
                Tag = nomArret,
            };

            //Positionnement d'un arrêt en abscisse et ordonnée
            Canvas.SetLeft(arret, x);
            Canvas.SetTop(arret, y);

            bool estPremierArret = false;
            bool estDernierArret = false;

            string nomArretEllipse = (string)arret.Tag;

            if (lesArrets.Count > 0)
            {
                estPremierArret = lesArrets[0].Nom == nomArretEllipse;
                estDernierArret = lesArrets[lesArrets.Count - 1].Nom == nomArretEllipse;
            }

            if (estPremierArret || estDernierArret)
            {
                arret.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(couleurPoleEchange));
                arret.Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString(couleurPoleEchange));
            }

            canvas.Children.Add(arret);

            //Création d'un nom d'arrêt avec un Label
            var etiquetteArret = new Label
            {
                Content = nomArret,
                FontSize = 15,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center
            };

            //Positionnement d'un nom d'arrêt en abscisse et ordonnée
            Canvas.SetLeft(etiquetteArret, x - 10);
            Canvas.SetTop(etiquetteArret, y - 20);

            //Définition d'un angle de rotation pour le positionnement des noms d'arrêts
            RotateTransform angleRotation = new RotateTransform(-35);
            etiquetteArret.RenderTransform = angleRotation;

            //Ajout d'un nom d'arrêt sur le graphe
            canvas.Children.Add(etiquetteArret);

            ExtensionItineraire(nomArret, x, y, etiquetteArret, arret);

            EstCorrespondance(nomArret, x, y, arret, estCorrespondance, lesArrets);

            EstPoleEchange(arret, etiquetteArret, estPoleEchange);
        }

        public void EstPoleEchange(Ellipse arret, Label etiquetteArret, bool estPoleEchange)
        {
            bool estPremierArret = false;
            bool estDernierArret = false;

            string nomArretEllipse = (string)arret.Tag;

            if (lesArrets.Count > 0)
            {
                estPremierArret = lesArrets[0].Nom == nomArretEllipse;
                estDernierArret = lesArrets[lesArrets.Count - 1].Nom == nomArretEllipse;
            }

            if (estPoleEchange && !estPremierArret && !estDernierArret)
            {
                arret.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(couleurPoleEchange));
                arret.Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString(couleurPoleEchange));
                etiquetteArret.FontWeight = FontWeights.Bold;
            }
        }

        public void EstCorrespondance(string nomArret, double x, double y, Ellipse arret, bool estCorrespondance, List<Arret> lesArrets)
        {
            if (estCorrespondance)
            {
                var etiquetteCorrespondance = new Label
                {
                    Content = "C",
                    FontSize = 12,
                    FontWeight = FontWeights.Bold,
                    Foreground = Brushes.White
                };

                if (nomArret.Equals("Station Chaudron") || nomArret.Equals("Mail du Chaudron") || nomArret.Equals("Parc Aquatique"))
                {
                    etiquetteCorrespondance.Foreground = Brushes.Black;
                }

                // Positionnement de l'étiquette de correspondance
                Canvas.SetLeft(etiquetteCorrespondance, x - 2);
                Canvas.SetTop(etiquetteCorrespondance, y - 6);

                canvas.Children.Add(etiquetteCorrespondance);

                double positionX = Canvas.GetLeft(arret) - 5;
                double positionY = Canvas.GetTop(arret) + 30;


                var corrStadeDeLEst = new List<string> { "15", "26", "27", "27A", "31", "33" };
                var corrParcExposition = new List<string> { "27", "27A", "33" };
                var corrZECChaudron = new List<string> { "27", "27A", "33" };
                var corrPierreAubert = new List<string> { "27", "27A", "33" };
                var corrRogerPayet = new List<string> { "27", "27A", "31", "33" };
                var corrSationChaudron = new List<string> { "1", "24", "24A", "25", "26", "27", "27A", "28", "29A", "31", "32", "33", "40" };
                var corrMailChaudron = new List<string> { "1", "6", "8" };
                var corrLaCroix = new List<string> { "1", "6", "8" };
                var corrSteCloCentre = new List<string> { "1", "6", "7", "8" };
                var corrBanian = new List<string> { "1", "6", "7", "8" };
                var corrDeuxCanons = new List<string> { "1", "6", "7", "8", "15" };
                var corrParcAquatique = new List<string> { "1", "6", "7", "8", "10", "15", "19" };
                var corrButor = new List<string> { "1", "6", "7", "8" };
                var corrHotImpots = new List<string> { "1", "6", "7", "8" };
                var corrCampJacquot = new List<string> { "1", "6", "7", "8" };
                var corrStJacques = new List<string> { "1", "6", "7", "8" };
                var corrPetitMarche = new List<string> { "6", "7", "8", "13", "14" };
                var corrEcoleCentrale = new List<string> { "6", "7", "8", "13", "14" };
                var corrRieul = new List<string> { "6", "7", "8", "13", "14" };
                var corrHdvStDenis = new List<string> { "6", "7", "8", "10", "11", "12", "13", "14", "16", "19", "21", "22", "22A", "23" };


                foreach (var ligne in lignes)
                {
                    var modelisationLigne = new Rectangle
                    {
                        Width = 30,
                        Height = 20,
                        Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ligne.Value)),
                        Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ligne.Value)),
                        RadiusX = 2,
                        RadiusY = 2,
                        Tag = ligne.Key,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    };

                    var numLigne = new Label
                    {
                        Width = 35,
                        Height = 25,
                        Content = ligne.Key,
                        FontSize = 12,
                        FontWeight = FontWeights.Bold,
                        Foreground = Brushes.White,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Tag = ligne.Key
                    };

                    if (numLigne.Tag.ToString().Equals("10"))
                    {
                        numLigne.Foreground = Brushes.Black;
                    }

                    if (numLigne.Tag.ToString().Equals("16"))
                    {
                        numLigne.Foreground = Brushes.Black;
                    }


                    //positionY = positionY + 25;

                    // Récupération des nuémros de lignes dessinés par un rectangle
                    var tag = modelisationLigne.Tag.ToString();

                    if (nomArret.Equals("STADE DE L’EST") && corrStadeDeLEst.Contains(tag))
                    {
                        if (tag.Length < 2)                                      //Si 1 caractère
                        {
                            modelisationLigne.Height = 20;
                            modelisationLigne.Width = 20;

                            Canvas.SetLeft(numLigne, positionX + 3.5);
                            Canvas.SetTop(numLigne, positionY - 0.5);
                            canvas.Children.Add(modelisationLigne);

                            Canvas.SetLeft(modelisationLigne, positionX + 2);
                            Canvas.SetTop(modelisationLigne, positionY + 3);
                            canvas.Children.Add(numLigne);

                            positionY = positionY + 25;
                        }
                        else if (tag.Length < 3)                                 //Si 2 caractères
                        {
                            modelisationLigne.Height = 20;
                            modelisationLigne.Width = 20;

                            Canvas.SetLeft(numLigne, positionX - 0.1);
                            Canvas.SetTop(numLigne, positionY - 0.5);
                            canvas.Children.Add(modelisationLigne);

                            Canvas.SetLeft(modelisationLigne, positionX + 2);
                            Canvas.SetTop(modelisationLigne, positionY + 3);
                            canvas.Children.Add(numLigne);

                            positionY = positionY + 25;
                        }
                        else if (tag.Length < 5)                                //Si 3 caractères
                        {
                            modelisationLigne.Height = 20;
                            modelisationLigne.Width = 30;

                            Canvas.SetLeft(numLigne, positionX);
                            Canvas.SetTop(numLigne, positionY - 0.5);
                            canvas.Children.Add(modelisationLigne);

                            Canvas.SetLeft(modelisationLigne, positionX + 2);
                            Canvas.SetTop(modelisationLigne, positionY + 3);
                            canvas.Children.Add(numLigne);

                            positionY = positionY + 25;
                        }


                        /*                        Canvas.SetLeft(numLigne, positionX);
                                                                        Canvas.SetTop(numLigne, positionY);
                                                                        canvas.Children.Add(modelisationLigne);

                                                                        Canvas.SetLeft(modelisationLigne, positionX + 2);
                                                                        Canvas.SetTop(modelisationLigne, positionY + 3);
                                                                        canvas.Children.Add(numLigne);

                                                                        positionY = positionY + 25;*/

                    }

                    if (nomArret.Equals("Parc des Expositions") && corrParcExposition.Contains(tag))
                    {
                        if (tag.Length < 2)                                      //Si 1 caractère
                        {
                            modelisationLigne.Height = 20;
                            modelisationLigne.Width = 20;

                            Canvas.SetLeft(numLigne, positionX + 3.5);
                            Canvas.SetTop(numLigne, positionY - 0.5);
                            canvas.Children.Add(modelisationLigne);

                            Canvas.SetLeft(modelisationLigne, positionX + 2);
                            Canvas.SetTop(modelisationLigne, positionY + 3);
                            canvas.Children.Add(numLigne);

                            positionY = positionY + 25;
                        }
                        else if (tag.Length < 3)                                 //Si 2 caractères
                        {
                            modelisationLigne.Height = 20;
                            modelisationLigne.Width = 20;

                            Canvas.SetLeft(numLigne, positionX - 0.1);
                            Canvas.SetTop(numLigne, positionY - 0.5);
                            canvas.Children.Add(modelisationLigne);

                            Canvas.SetLeft(modelisationLigne, positionX + 2);
                            Canvas.SetTop(modelisationLigne, positionY + 3);
                            canvas.Children.Add(numLigne);

                            positionY = positionY + 25;
                        }
                        else if (tag.Length < 5)                                //Si 3 caractères
                        {
                            modelisationLigne.Height = 20;
                            modelisationLigne.Width = 30;

                            Canvas.SetLeft(numLigne, positionX);
                            Canvas.SetTop(numLigne, positionY - 0.5);
                            canvas.Children.Add(modelisationLigne);

                            Canvas.SetLeft(modelisationLigne, positionX + 2);
                            Canvas.SetTop(modelisationLigne, positionY + 3);
                            canvas.Children.Add(numLigne);

                            positionY = positionY + 25;
                        }
                    }

                    if (nomArret.Equals("ZEC du Chaudron") && corrZECChaudron.Contains(tag))
                    {
                        if (tag.Length < 2)                                      //Si 1 caractère
                        {
                            modelisationLigne.Height = 20;
                            modelisationLigne.Width = 20;

                            Canvas.SetLeft(numLigne, positionX + 3.5);
                            Canvas.SetTop(numLigne, positionY - 0.5);
                            canvas.Children.Add(modelisationLigne);

                            Canvas.SetLeft(modelisationLigne, positionX + 2);
                            Canvas.SetTop(modelisationLigne, positionY + 3);
                            canvas.Children.Add(numLigne);

                            positionY = positionY + 25;
                        }
                        else if (tag.Length < 3)                                 //Si 2 caractères
                        {
                            modelisationLigne.Height = 20;
                            modelisationLigne.Width = 20;

                            Canvas.SetLeft(numLigne, positionX - 0.1);
                            Canvas.SetTop(numLigne, positionY - 0.5);
                            canvas.Children.Add(modelisationLigne);

                            Canvas.SetLeft(modelisationLigne, positionX + 2);
                            Canvas.SetTop(modelisationLigne, positionY + 3);
                            canvas.Children.Add(numLigne);

                            positionY = positionY + 25;
                        }
                        else if (tag.Length < 5)                                //Si 3 caractères
                        {
                            modelisationLigne.Height = 20;
                            modelisationLigne.Width = 30;

                            Canvas.SetLeft(numLigne, positionX);
                            Canvas.SetTop(numLigne, positionY - 0.5);
                            canvas.Children.Add(modelisationLigne);

                            Canvas.SetLeft(modelisationLigne, positionX + 2);
                            Canvas.SetTop(modelisationLigne, positionY + 3);
                            canvas.Children.Add(numLigne);

                            positionY = positionY + 25;
                        }
                    }

                    if (nomArret.Equals("Pierre Aubert") && corrPierreAubert.Contains(tag))
                    {
                        if (tag.Length < 2)                                      //Si 1 caractère
                        {
                            modelisationLigne.Height = 20;
                            modelisationLigne.Width = 20;

                            Canvas.SetLeft(numLigne, positionX + 3.5);
                            Canvas.SetTop(numLigne, positionY - 0.5);
                            canvas.Children.Add(modelisationLigne);

                            Canvas.SetLeft(modelisationLigne, positionX + 2);
                            Canvas.SetTop(modelisationLigne, positionY + 3);
                            canvas.Children.Add(numLigne);

                            positionY = positionY + 25;
                        }
                        else if (tag.Length < 3)                                 //Si 2 caractères
                        {
                            modelisationLigne.Height = 20;
                            modelisationLigne.Width = 20;

                            Canvas.SetLeft(numLigne, positionX - 0.1);
                            Canvas.SetTop(numLigne, positionY - 0.5);
                            canvas.Children.Add(modelisationLigne);

                            Canvas.SetLeft(modelisationLigne, positionX + 2);
                            Canvas.SetTop(modelisationLigne, positionY + 3);
                            canvas.Children.Add(numLigne);

                            positionY = positionY + 25;
                        }
                        else if (tag.Length < 5)                                //Si 3 caractères
                        {
                            modelisationLigne.Height = 20;
                            modelisationLigne.Width = 30;

                            Canvas.SetLeft(numLigne, positionX);
                            Canvas.SetTop(numLigne, positionY - 0.5);
                            canvas.Children.Add(modelisationLigne);

                            Canvas.SetLeft(modelisationLigne, positionX + 2);
                            Canvas.SetTop(modelisationLigne, positionY + 3);
                            canvas.Children.Add(numLigne);

                            positionY = positionY + 25;
                        }
                    }

                    if (nomArret.Equals("Roger Payet") && corrRogerPayet.Contains(tag))
                    {
                        if (tag.Length < 2)                                      //Si 1 caractère
                        {
                            modelisationLigne.Height = 20;
                            modelisationLigne.Width = 20;

                            Canvas.SetLeft(numLigne, positionX + 3.5);
                            Canvas.SetTop(numLigne, positionY - 0.5);
                            canvas.Children.Add(modelisationLigne);

                            Canvas.SetLeft(modelisationLigne, positionX + 2);
                            Canvas.SetTop(modelisationLigne, positionY + 3);
                            canvas.Children.Add(numLigne);

                            positionY = positionY + 25;
                        }
                        else if (tag.Length < 3)                                 //Si 2 caractères
                        {
                            modelisationLigne.Height = 20;
                            modelisationLigne.Width = 20;

                            Canvas.SetLeft(numLigne, positionX - 0.1);
                            Canvas.SetTop(numLigne, positionY - 0.5);
                            canvas.Children.Add(modelisationLigne);

                            Canvas.SetLeft(modelisationLigne, positionX + 2);
                            Canvas.SetTop(modelisationLigne, positionY + 3);
                            canvas.Children.Add(numLigne);

                            positionY = positionY + 25;
                        }
                        else if (tag.Length < 5)                                //Si 3 caractères
                        {
                            modelisationLigne.Height = 20;
                            modelisationLigne.Width = 30;

                            Canvas.SetLeft(numLigne, positionX);
                            Canvas.SetTop(numLigne, positionY - 0.5);
                            canvas.Children.Add(modelisationLigne);

                            Canvas.SetLeft(modelisationLigne, positionX + 2);
                            Canvas.SetTop(modelisationLigne, positionY + 3);
                            canvas.Children.Add(numLigne);

                            positionY = positionY + 25;
                        }
                    }

                    if (nomArret.Equals("Station Chaudron") && corrSationChaudron.Contains(tag))
                    {
                        if (tag.Length < 2)                                      //Si 1 caractère
                        {
                            modelisationLigne.Height = 20;
                            modelisationLigne.Width = 20;

                            Canvas.SetLeft(numLigne, positionX + 3.5);
                            Canvas.SetTop(numLigne, positionY - 0.5);
                            canvas.Children.Add(modelisationLigne);

                            Canvas.SetLeft(modelisationLigne, positionX + 2);
                            Canvas.SetTop(modelisationLigne, positionY + 3);
                            canvas.Children.Add(numLigne);

                            positionY = positionY + 25;
                        }
                        else if (tag.Length < 3)                                 //Si 2 caractères
                        {
                            modelisationLigne.Height = 20;
                            modelisationLigne.Width = 20;

                            Canvas.SetLeft(numLigne, positionX - 0.1);
                            Canvas.SetTop(numLigne, positionY - 0.5);
                            canvas.Children.Add(modelisationLigne);

                            Canvas.SetLeft(modelisationLigne, positionX + 2);
                            Canvas.SetTop(modelisationLigne, positionY + 3);
                            canvas.Children.Add(numLigne);

                            positionY = positionY + 25;
                        }
                        else if (tag.Length < 5)                                //Si 3 caractères
                        {
                            modelisationLigne.Height = 20;
                            modelisationLigne.Width = 30;

                            Canvas.SetLeft(numLigne, positionX);
                            Canvas.SetTop(numLigne, positionY - 0.5);
                            canvas.Children.Add(modelisationLigne);

                            Canvas.SetLeft(modelisationLigne, positionX + 2);
                            Canvas.SetTop(modelisationLigne, positionY + 3);
                            canvas.Children.Add(numLigne);

                            positionY = positionY + 25;
                        }
                    }

                    if (nomArret.Equals("Mail du Chaudron") && corrMailChaudron.Contains(tag))
                    {
                        if (tag.Length < 2)                                      //Si 1 caractère
                        {
                            modelisationLigne.Height = 20;
                            modelisationLigne.Width = 20;

                            Canvas.SetLeft(numLigne, positionX + 3.5);
                            Canvas.SetTop(numLigne, positionY - 0.5);
                            canvas.Children.Add(modelisationLigne);

                            Canvas.SetLeft(modelisationLigne, positionX + 2);
                            Canvas.SetTop(modelisationLigne, positionY + 3);
                            canvas.Children.Add(numLigne);

                            positionY = positionY + 25;
                        }
                        else if (tag.Length < 3)                                 //Si 2 caractères
                        {
                            modelisationLigne.Height = 20;
                            modelisationLigne.Width = 20;

                            Canvas.SetLeft(numLigne, positionX - 0.1);
                            Canvas.SetTop(numLigne, positionY - 0.5);
                            canvas.Children.Add(modelisationLigne);

                            Canvas.SetLeft(modelisationLigne, positionX + 2);
                            Canvas.SetTop(modelisationLigne, positionY + 3);
                            canvas.Children.Add(numLigne);

                            positionY = positionY + 25;
                        }
                        else if (tag.Length < 5)                                //Si 3 caractères
                        {
                            modelisationLigne.Height = 20;
                            modelisationLigne.Width = 30;

                            Canvas.SetLeft(numLigne, positionX);
                            Canvas.SetTop(numLigne, positionY - 0.5);
                            canvas.Children.Add(modelisationLigne);

                            Canvas.SetLeft(modelisationLigne, positionX + 2);
                            Canvas.SetTop(modelisationLigne, positionY + 3);
                            canvas.Children.Add(numLigne);

                            positionY = positionY + 25;
                        }
                    }

                    if (nomArret.Equals("Lacroix") && corrLaCroix.Contains(tag))
                    {
                        if (tag.Length < 2)                                      //Si 1 caractère
                        {
                            modelisationLigne.Height = 20;
                            modelisationLigne.Width = 20;

                            Canvas.SetLeft(numLigne, positionX + 3.5);
                            Canvas.SetTop(numLigne, positionY - 0.5);
                            canvas.Children.Add(modelisationLigne);

                            Canvas.SetLeft(modelisationLigne, positionX + 2);
                            Canvas.SetTop(modelisationLigne, positionY + 3);
                            canvas.Children.Add(numLigne);

                            positionY = positionY + 25;
                        }
                        else if (tag.Length < 3)                                 //Si 2 caractères
                        {
                            modelisationLigne.Height = 20;
                            modelisationLigne.Width = 20;

                            Canvas.SetLeft(numLigne, positionX - 0.1);
                            Canvas.SetTop(numLigne, positionY - 0.5);
                            canvas.Children.Add(modelisationLigne);

                            Canvas.SetLeft(modelisationLigne, positionX + 2);
                            Canvas.SetTop(modelisationLigne, positionY + 3);
                            canvas.Children.Add(numLigne);

                            positionY = positionY + 25;
                        }
                        else if (tag.Length < 5)                                //Si 3 caractères
                        {
                            modelisationLigne.Height = 20;
                            modelisationLigne.Width = 30;

                            Canvas.SetLeft(numLigne, positionX);
                            Canvas.SetTop(numLigne, positionY - 0.5);
                            canvas.Children.Add(modelisationLigne);

                            Canvas.SetLeft(modelisationLigne, positionX + 2);
                            Canvas.SetTop(modelisationLigne, positionY + 3);
                            canvas.Children.Add(numLigne);

                            positionY = positionY + 25;
                        }
                    }

                    if (nomArret.Equals("Sainte-Clotilde Centre") && corrSteCloCentre.Contains(tag))
                    {
                        if (tag.Length < 2)                                      //Si 1 caractère
                        {
                            modelisationLigne.Height = 20;
                            modelisationLigne.Width = 20;

                            Canvas.SetLeft(numLigne, positionX + 3.5);
                            Canvas.SetTop(numLigne, positionY - 0.5);
                            canvas.Children.Add(modelisationLigne);

                            Canvas.SetLeft(modelisationLigne, positionX + 2);
                            Canvas.SetTop(modelisationLigne, positionY + 3);
                            canvas.Children.Add(numLigne);

                            positionY = positionY + 25;
                        }
                        else if (tag.Length < 3)                                 //Si 2 caractères
                        {
                            modelisationLigne.Height = 20;
                            modelisationLigne.Width = 20;

                            Canvas.SetLeft(numLigne, positionX - 0.1);
                            Canvas.SetTop(numLigne, positionY - 0.5);
                            canvas.Children.Add(modelisationLigne);

                            Canvas.SetLeft(modelisationLigne, positionX + 2);
                            Canvas.SetTop(modelisationLigne, positionY + 3);
                            canvas.Children.Add(numLigne);

                            positionY = positionY + 25;
                        }
                        else if (tag.Length < 5)                                //Si 3 caractères
                        {
                            modelisationLigne.Height = 20;
                            modelisationLigne.Width = 30;

                            Canvas.SetLeft(numLigne, positionX);
                            Canvas.SetTop(numLigne, positionY - 0.5);
                            canvas.Children.Add(modelisationLigne);

                            Canvas.SetLeft(modelisationLigne, positionX + 2);
                            Canvas.SetTop(modelisationLigne, positionY + 3);
                            canvas.Children.Add(numLigne);

                            positionY = positionY + 25;
                        }
                    }

                    if (nomArret.Equals("Banian") && corrBanian.Contains(tag))
                    {
                        if (tag.Length < 2)                                      //Si 1 caractère
                        {
                            modelisationLigne.Height = 20;
                            modelisationLigne.Width = 20;

                            Canvas.SetLeft(numLigne, positionX + 3.5);
                            Canvas.SetTop(numLigne, positionY - 0.5);
                            canvas.Children.Add(modelisationLigne);

                            Canvas.SetLeft(modelisationLigne, positionX + 2);
                            Canvas.SetTop(modelisationLigne, positionY + 3);
                            canvas.Children.Add(numLigne);

                            positionY = positionY + 25;
                        }
                        else if (tag.Length < 3)                                 //Si 2 caractères
                        {
                            modelisationLigne.Height = 20;
                            modelisationLigne.Width = 20;

                            Canvas.SetLeft(numLigne, positionX - 0.1);
                            Canvas.SetTop(numLigne, positionY - 0.5);
                            canvas.Children.Add(modelisationLigne);

                            Canvas.SetLeft(modelisationLigne, positionX + 2);
                            Canvas.SetTop(modelisationLigne, positionY + 3);
                            canvas.Children.Add(numLigne);

                            positionY = positionY + 25;
                        }
                        else if (tag.Length < 5)                                //Si 3 caractères
                        {
                            modelisationLigne.Height = 20;
                            modelisationLigne.Width = 30;

                            Canvas.SetLeft(numLigne, positionX);
                            Canvas.SetTop(numLigne, positionY - 0.5);
                            canvas.Children.Add(modelisationLigne);

                            Canvas.SetLeft(modelisationLigne, positionX + 2);
                            Canvas.SetTop(modelisationLigne, positionY + 3);
                            canvas.Children.Add(numLigne);

                            positionY = positionY + 25;
                        }
                    }

                    if (nomArret.Equals("Deux Canons") && corrDeuxCanons.Contains(tag))
                    {
                        if (tag.Length < 2)                                      //Si 1 caractère
                        {
                            modelisationLigne.Height = 20;
                            modelisationLigne.Width = 20;

                            Canvas.SetLeft(numLigne, positionX + 3.5);
                            Canvas.SetTop(numLigne, positionY - 0.5);
                            canvas.Children.Add(modelisationLigne);

                            Canvas.SetLeft(modelisationLigne, positionX + 2);
                            Canvas.SetTop(modelisationLigne, positionY + 3);
                            canvas.Children.Add(numLigne);

                            positionY = positionY + 25;
                        }
                        else if (tag.Length < 3)                                 //Si 2 caractères
                        {
                            modelisationLigne.Height = 20;
                            modelisationLigne.Width = 20;

                            Canvas.SetLeft(numLigne, positionX - 0.1);
                            Canvas.SetTop(numLigne, positionY - 0.5);
                            canvas.Children.Add(modelisationLigne);

                            Canvas.SetLeft(modelisationLigne, positionX + 2);
                            Canvas.SetTop(modelisationLigne, positionY + 3);
                            canvas.Children.Add(numLigne);

                            positionY = positionY + 25;
                        }
                        else if (tag.Length < 5)                                //Si 3 caractères
                        {
                            modelisationLigne.Height = 20;
                            modelisationLigne.Width = 30;

                            Canvas.SetLeft(numLigne, positionX);
                            Canvas.SetTop(numLigne, positionY - 0.5);
                            canvas.Children.Add(modelisationLigne);

                            Canvas.SetLeft(modelisationLigne, positionX + 2);
                            Canvas.SetTop(modelisationLigne, positionY + 3);
                            canvas.Children.Add(numLigne);

                            positionY = positionY + 25;
                        }
                    }

                    if (nomArret.Equals("Parc Aquatique") && corrParcAquatique.Contains(tag))
                    {
                        if (tag.Length < 2)                                      //Si 1 caractère
                        {
                            modelisationLigne.Height = 20;
                            modelisationLigne.Width = 20;

                            Canvas.SetLeft(numLigne, positionX + 3.5);
                            Canvas.SetTop(numLigne, positionY - 0.5);
                            canvas.Children.Add(modelisationLigne);

                            Canvas.SetLeft(modelisationLigne, positionX + 2);
                            Canvas.SetTop(modelisationLigne, positionY + 3);
                            canvas.Children.Add(numLigne);

                            positionY = positionY + 25;
                        }
                        else if (tag.Length < 3)                                 //Si 2 caractères
                        {
                            modelisationLigne.Height = 20;
                            modelisationLigne.Width = 20;

                            Canvas.SetLeft(numLigne, positionX - 0.1);
                            Canvas.SetTop(numLigne, positionY - 0.5);
                            canvas.Children.Add(modelisationLigne);

                            Canvas.SetLeft(modelisationLigne, positionX + 2);
                            Canvas.SetTop(modelisationLigne, positionY + 3);
                            canvas.Children.Add(numLigne);

                            positionY = positionY + 25;
                        }
                        else if (tag.Length < 5)                                //Si 3 caractères
                        {
                            modelisationLigne.Height = 20;
                            modelisationLigne.Width = 30;

                            Canvas.SetLeft(numLigne, positionX);
                            Canvas.SetTop(numLigne, positionY - 0.5);
                            canvas.Children.Add(modelisationLigne);

                            Canvas.SetLeft(modelisationLigne, positionX + 2);
                            Canvas.SetTop(modelisationLigne, positionY + 3);
                            canvas.Children.Add(numLigne);

                            positionY = positionY + 25;
                        }
                    }

                    if (nomArret.Equals("Butor") && corrButor.Contains(tag))
                    {
                        if (tag.Length < 2)                                      //Si 1 caractère
                        {
                            modelisationLigne.Height = 20;
                            modelisationLigne.Width = 20;

                            Canvas.SetLeft(numLigne, positionX + 3.5);
                            Canvas.SetTop(numLigne, positionY - 0.5);
                            canvas.Children.Add(modelisationLigne);

                            Canvas.SetLeft(modelisationLigne, positionX + 2);
                            Canvas.SetTop(modelisationLigne, positionY + 3);
                            canvas.Children.Add(numLigne);

                            positionY = positionY + 25;
                        }
                        else if (tag.Length < 3)                                 //Si 2 caractères
                        {
                            modelisationLigne.Height = 20;
                            modelisationLigne.Width = 20;

                            Canvas.SetLeft(numLigne, positionX - 0.1);
                            Canvas.SetTop(numLigne, positionY - 0.5);
                            canvas.Children.Add(modelisationLigne);

                            Canvas.SetLeft(modelisationLigne, positionX + 2);
                            Canvas.SetTop(modelisationLigne, positionY + 3);
                            canvas.Children.Add(numLigne);

                            positionY = positionY + 25;
                        }
                        else if (tag.Length < 5)                                //Si 3 caractères
                        {
                            modelisationLigne.Height = 20;
                            modelisationLigne.Width = 30;

                            Canvas.SetLeft(numLigne, positionX);
                            Canvas.SetTop(numLigne, positionY - 0.5);
                            canvas.Children.Add(modelisationLigne);

                            Canvas.SetLeft(modelisationLigne, positionX + 2);
                            Canvas.SetTop(modelisationLigne, positionY + 3);
                            canvas.Children.Add(numLigne);

                            positionY = positionY + 25;
                        }
                    }

                    if (nomArret.Equals("Hôtel des Impôts") && corrHotImpots.Contains(tag))
                    {
                        if (tag.Length < 2)                                      //Si 1 caractère
                        {
                            modelisationLigne.Height = 20;
                            modelisationLigne.Width = 20;

                            Canvas.SetLeft(numLigne, positionX + 3.5);
                            Canvas.SetTop(numLigne, positionY - 0.5);
                            canvas.Children.Add(modelisationLigne);

                            Canvas.SetLeft(modelisationLigne, positionX + 2);
                            Canvas.SetTop(modelisationLigne, positionY + 3);
                            canvas.Children.Add(numLigne);

                            positionY = positionY + 25;
                        }
                        else if (tag.Length < 3)                                 //Si 2 caractères
                        {
                            modelisationLigne.Height = 20;
                            modelisationLigne.Width = 20;

                            Canvas.SetLeft(numLigne, positionX - 0.1);
                            Canvas.SetTop(numLigne, positionY - 0.5);
                            canvas.Children.Add(modelisationLigne);

                            Canvas.SetLeft(modelisationLigne, positionX + 2);
                            Canvas.SetTop(modelisationLigne, positionY + 3);
                            canvas.Children.Add(numLigne);

                            positionY = positionY + 25;
                        }
                        else if (tag.Length < 5)                                //Si 3 caractères
                        {
                            modelisationLigne.Height = 20;
                            modelisationLigne.Width = 30;

                            Canvas.SetLeft(numLigne, positionX);
                            Canvas.SetTop(numLigne, positionY - 0.5);
                            canvas.Children.Add(modelisationLigne);

                            Canvas.SetLeft(modelisationLigne, positionX + 2);
                            Canvas.SetTop(modelisationLigne, positionY + 3);
                            canvas.Children.Add(numLigne);

                            positionY = positionY + 25;
                        }
                    }

                    if (nomArret.Equals("Camp Jacquot") && corrCampJacquot.Contains(tag))
                    {
                        if (tag.Length < 2)                                      //Si 1 caractère
                        {
                            modelisationLigne.Height = 20;
                            modelisationLigne.Width = 20;

                            Canvas.SetLeft(numLigne, positionX + 3.5);
                            Canvas.SetTop(numLigne, positionY - 0.5);
                            canvas.Children.Add(modelisationLigne);

                            Canvas.SetLeft(modelisationLigne, positionX + 2);
                            Canvas.SetTop(modelisationLigne, positionY + 3);
                            canvas.Children.Add(numLigne);

                            positionY = positionY + 25;
                        }
                        else if (tag.Length < 3)                                 //Si 2 caractères
                        {
                            modelisationLigne.Height = 20;
                            modelisationLigne.Width = 20;

                            Canvas.SetLeft(numLigne, positionX - 0.1);
                            Canvas.SetTop(numLigne, positionY - 0.5);
                            canvas.Children.Add(modelisationLigne);

                            Canvas.SetLeft(modelisationLigne, positionX + 2);
                            Canvas.SetTop(modelisationLigne, positionY + 3);
                            canvas.Children.Add(numLigne);

                            positionY = positionY + 25;
                        }
                        else if (tag.Length < 5)                                //Si 3 caractères
                        {
                            modelisationLigne.Height = 20;
                            modelisationLigne.Width = 30;

                            Canvas.SetLeft(numLigne, positionX);
                            Canvas.SetTop(numLigne, positionY - 0.5);
                            canvas.Children.Add(modelisationLigne);

                            Canvas.SetLeft(modelisationLigne, positionX + 2);
                            Canvas.SetTop(modelisationLigne, positionY + 3);
                            canvas.Children.Add(numLigne);

                            positionY = positionY + 25;
                        }
                    }

                    if (nomArret.Equals("Saint-Jacques") && corrStJacques.Contains(tag))
                    {
                        if (tag.Length < 2)                                      //Si 1 caractère
                        {
                            modelisationLigne.Height = 20;
                            modelisationLigne.Width = 20;

                            Canvas.SetLeft(numLigne, positionX + 3.5);
                            Canvas.SetTop(numLigne, positionY - 0.5);
                            canvas.Children.Add(modelisationLigne);

                            Canvas.SetLeft(modelisationLigne, positionX + 2);
                            Canvas.SetTop(modelisationLigne, positionY + 3);
                            canvas.Children.Add(numLigne);

                            positionY = positionY + 25;
                        }
                        else if (tag.Length < 3)                                 //Si 2 caractères
                        {
                            modelisationLigne.Height = 20;
                            modelisationLigne.Width = 20;

                            Canvas.SetLeft(numLigne, positionX - 0.1);
                            Canvas.SetTop(numLigne, positionY - 0.5);
                            canvas.Children.Add(modelisationLigne);

                            Canvas.SetLeft(modelisationLigne, positionX + 2);
                            Canvas.SetTop(modelisationLigne, positionY + 3);
                            canvas.Children.Add(numLigne);

                            positionY = positionY + 25;
                        }
                        else if (tag.Length < 5)                                //Si 3 caractères
                        {
                            modelisationLigne.Height = 20;
                            modelisationLigne.Width = 30;

                            Canvas.SetLeft(numLigne, positionX);
                            Canvas.SetTop(numLigne, positionY - 0.5);
                            canvas.Children.Add(modelisationLigne);

                            Canvas.SetLeft(modelisationLigne, positionX + 2);
                            Canvas.SetTop(modelisationLigne, positionY + 3);
                            canvas.Children.Add(numLigne);

                            positionY = positionY + 25;
                        }
                    }

                    if (nomArret.Equals("Petit Marché") && corrPetitMarche.Contains(tag))
                    {
                        if (tag.Length < 2)                                      //Si 1 caractère
                        {
                            modelisationLigne.Height = 20;
                            modelisationLigne.Width = 20;

                            Canvas.SetLeft(numLigne, positionX + 3.5);
                            Canvas.SetTop(numLigne, positionY - 0.5);
                            canvas.Children.Add(modelisationLigne);

                            Canvas.SetLeft(modelisationLigne, positionX + 2);
                            Canvas.SetTop(modelisationLigne, positionY + 3);
                            canvas.Children.Add(numLigne);

                            positionY = positionY + 25;
                        }
                        else if (tag.Length < 3)                                 //Si 2 caractères
                        {
                            modelisationLigne.Height = 20;
                            modelisationLigne.Width = 20;

                            Canvas.SetLeft(numLigne, positionX - 0.1);
                            Canvas.SetTop(numLigne, positionY - 0.5);
                            canvas.Children.Add(modelisationLigne);

                            Canvas.SetLeft(modelisationLigne, positionX + 2);
                            Canvas.SetTop(modelisationLigne, positionY + 3);
                            canvas.Children.Add(numLigne);

                            positionY = positionY + 25;
                        }
                        else if (tag.Length < 5)                                //Si 3 caractères
                        {
                            modelisationLigne.Height = 20;
                            modelisationLigne.Width = 30;

                            Canvas.SetLeft(numLigne, positionX);
                            Canvas.SetTop(numLigne, positionY - 0.5);
                            canvas.Children.Add(modelisationLigne);

                            Canvas.SetLeft(modelisationLigne, positionX + 2);
                            Canvas.SetTop(modelisationLigne, positionY + 3);
                            canvas.Children.Add(numLigne);

                            positionY = positionY + 25;
                        }
                    }

                    if (nomArret.Equals("École Centrale") && corrEcoleCentrale.Contains(tag))
                    {
                        if (tag.Length < 2)                                      //Si 1 caractère
                        {
                            modelisationLigne.Height = 20;
                            modelisationLigne.Width = 20;

                            Canvas.SetLeft(numLigne, positionX + 3.5);
                            Canvas.SetTop(numLigne, positionY - 0.5);
                            canvas.Children.Add(modelisationLigne);

                            Canvas.SetLeft(modelisationLigne, positionX + 2);
                            Canvas.SetTop(modelisationLigne, positionY + 3);
                            canvas.Children.Add(numLigne);

                            positionY = positionY + 25;
                        }
                        else if (tag.Length < 3)                                 //Si 2 caractères
                        {
                            modelisationLigne.Height = 20;
                            modelisationLigne.Width = 20;

                            Canvas.SetLeft(numLigne, positionX - 0.1);
                            Canvas.SetTop(numLigne, positionY - 0.5);
                            canvas.Children.Add(modelisationLigne);

                            Canvas.SetLeft(modelisationLigne, positionX + 2);
                            Canvas.SetTop(modelisationLigne, positionY + 3);
                            canvas.Children.Add(numLigne);

                            positionY = positionY + 25;
                        }
                        else if (tag.Length < 5)                                //Si 3 caractères
                        {
                            modelisationLigne.Height = 20;
                            modelisationLigne.Width = 30;

                            Canvas.SetLeft(numLigne, positionX);
                            Canvas.SetTop(numLigne, positionY - 0.5);
                            canvas.Children.Add(modelisationLigne);

                            Canvas.SetLeft(modelisationLigne, positionX + 2);
                            Canvas.SetTop(modelisationLigne, positionY + 3);
                            canvas.Children.Add(numLigne);

                            positionY = positionY + 25;
                        }
                    }

                    if (nomArret.Equals("Rieul") && corrRieul.Contains(tag))
                    {
                        if (tag.Length < 2)                                      //Si 1 caractère
                        {
                            modelisationLigne.Height = 20;
                            modelisationLigne.Width = 20;

                            Canvas.SetLeft(numLigne, positionX + 3.5);
                            Canvas.SetTop(numLigne, positionY - 0.5);
                            canvas.Children.Add(modelisationLigne);

                            Canvas.SetLeft(modelisationLigne, positionX + 2);
                            Canvas.SetTop(modelisationLigne, positionY + 3);
                            canvas.Children.Add(numLigne);

                            positionY = positionY + 25;
                        }
                        else if (tag.Length < 3)                                 //Si 2 caractères
                        {
                            modelisationLigne.Height = 20;
                            modelisationLigne.Width = 20;

                            Canvas.SetLeft(numLigne, positionX - 0.1);
                            Canvas.SetTop(numLigne, positionY - 0.5);
                            canvas.Children.Add(modelisationLigne);

                            Canvas.SetLeft(modelisationLigne, positionX + 2);
                            Canvas.SetTop(modelisationLigne, positionY + 3);
                            canvas.Children.Add(numLigne);

                            positionY = positionY + 25;
                        }
                        else if (tag.Length < 5)                                //Si 3 caractères
                        {
                            modelisationLigne.Height = 20;
                            modelisationLigne.Width = 30;

                            Canvas.SetLeft(numLigne, positionX);
                            Canvas.SetTop(numLigne, positionY - 0.5);
                            canvas.Children.Add(modelisationLigne);

                            Canvas.SetLeft(modelisationLigne, positionX + 2);
                            Canvas.SetTop(modelisationLigne, positionY + 3);
                            canvas.Children.Add(numLigne);

                            positionY = positionY + 25;
                        }
                    }

                    if (nomArret.Equals("HÔTEL DE VILLE DE SAINT-DENIS") && corrHdvStDenis.Contains(tag))
                    {
                        if (tag.Length < 2)                                      //Si 1 caractère
                        {
                            modelisationLigne.Height = 20;
                            modelisationLigne.Width = 20;

                            Canvas.SetLeft(numLigne, positionX + 3.5);
                            Canvas.SetTop(numLigne, positionY - 0.5);
                            canvas.Children.Add(modelisationLigne);

                            Canvas.SetLeft(modelisationLigne, positionX + 2);
                            Canvas.SetTop(modelisationLigne, positionY + 3);
                            canvas.Children.Add(numLigne);

                            positionY = positionY + 25;
                        }
                        else if (tag.Length < 3)                                 //Si 2 caractères
                        {
                            modelisationLigne.Height = 20;
                            modelisationLigne.Width = 20;

                            Canvas.SetLeft(numLigne, positionX - 0.1);
                            Canvas.SetTop(numLigne, positionY - 0.5);
                            canvas.Children.Add(modelisationLigne);

                            Canvas.SetLeft(modelisationLigne, positionX + 2);
                            Canvas.SetTop(modelisationLigne, positionY + 3);
                            canvas.Children.Add(numLigne);

                            positionY = positionY + 25;
                        }
                        else if (tag.Length < 5)                                //Si 3 caractères
                        {
                            modelisationLigne.Height = 20;
                            modelisationLigne.Width = 30;

                            Canvas.SetLeft(numLigne, positionX);
                            Canvas.SetTop(numLigne, positionY - 0.5);
                            canvas.Children.Add(modelisationLigne);

                            Canvas.SetLeft(modelisationLigne, positionX + 2);
                            Canvas.SetTop(modelisationLigne, positionY + 3);
                            canvas.Children.Add(numLigne);

                            positionY = positionY + 25;
                        }
                    }
                }
            }
        }

        public void ExtensionItineraire(string nomArret, double x, double y, Label etiquetteArret, Ellipse arret)
        {
            if (nomArret.Equals("STADE DE L’EST") || nomArret.Equals("HÔTEL DE VILLE DE SAINT-DENIS"))
            {
                etiquetteArret.FontSize = 20;
                arret.Width = 15;
                arret.Height = 15;
                //arret.StrokeThickness = 20;

                RotateTransform rotationHorizontale = new RotateTransform(0);
                etiquetteArret.RenderTransform = rotationHorizontale;

                if (nomArret.Equals("STADE DE L’EST"))
                {
                    Canvas.SetLeft(etiquetteArret, x + 5);
                    Canvas.SetTop(etiquetteArret, y - 230);
                }
                else if (nomArret.Equals("HÔTEL DE VILLE DE SAINT-DENIS"))
                {
                    Canvas.SetLeft(etiquetteArret, x - 225);
                    Canvas.SetTop(etiquetteArret, y - 230);
                }

                var arretStadeDeLEst = TrouverArret("STADE DE L’EST");

                if (arretStadeDeLEst != null)
                {
                    //Premier arc d'extension
                    var extension1depart = new Line
                    {
                        X1 = Canvas.GetLeft(arretStadeDeLEst) + arretStadeDeLEst.Width / 2,
                        Y1 = Canvas.GetTop(arretStadeDeLEst) + arretStadeDeLEst.Height - 15,

                        X2 = Canvas.GetLeft(arretStadeDeLEst) + arretStadeDeLEst.Width / 2,
                        Y2 = 55,

                        Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString(couleurPoleEchange)),
                        StrokeThickness = 2
                    };

                    canvas.Children.Add(extension1depart);

                    //Deuxième arc d'extension
                    var extension2depart = new Line
                    {
                        X1 = extension1depart.X2 - 1,
                        Y1 = extension1depart.Y2,

                        X2 = 90,
                        Y2 = 55,

                        Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString(couleurPoleEchange)),
                        StrokeThickness = 2
                    };

                    canvas.Children.Add(extension2depart);
                }

                var arretHdvStDenis = TrouverArret("HÔTEL DE VILLE DE SAINT-DENIS");

                if (arretHdvStDenis != null)
                {
                    var extension1terminus = new Line
                    {
                        X1 = Canvas.GetLeft(arretHdvStDenis) + arretHdvStDenis.Width,
                        Y1 = Canvas.GetTop(arretHdvStDenis) + arretHdvStDenis.Height / 2,

                        X2 = 1075,
                        Y2 = Canvas.GetTop(arretHdvStDenis) + arretHdvStDenis.Height / 2,

                        Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString(couleurPoleEchange)),
                        StrokeThickness = 2
                    };

                    canvas.Children.Add(extension1terminus);

                    var extension2terminus = new Line
                    {
                        X1 = extension1terminus.X2,
                        Y1 = extension1terminus.Y2 + 1,

                        X2 = extension1terminus.X2,
                        Y2 = 55,

                        Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString(couleurPoleEchange)),
                        StrokeThickness = 2
                    };

                    canvas.Children.Add(extension2terminus);

                    var extension3terminus = new Line
                    {
                        X1 = extension1terminus.X1 - 25,
                        Y1 = 55,

                        X2 = extension2terminus.X2 + 1,
                        Y2 = extension2terminus.Y2,

                        Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString(couleurPoleEchange)),
                        StrokeThickness = 2
                    };

                    canvas.Children.Add(extension3terminus);
                }
            }
        }

        private void AjouterArc(string nomArretPrecedent, string nomArretSuivant)
        {
            //Définition des arrêts (sommets) précédents et suivants
            var arretPrecedent = TrouverArret(nomArretPrecedent);
            var arretSuivant = TrouverArret(nomArretSuivant);

            if (arretPrecedent == null || arretSuivant == null)
            {
                return;
            }

            //Création d'une ligne (arc) entre les arrêts
            var arc = new Line
            {
                /*                //Positionnement du début de la ligne en abscisse et ordonnée
                                X1 = Canvas.GetLeft(arretPrecedent) + arretPrecedent.Width / 1,
                                Y1 = Canvas.GetTop(arretPrecedent) + arretPrecedent.Height / 2,

                                //Positionnement de la fin de la ligne en abscisse et ordonnée
                                X2 = Canvas.GetLeft(arretSuivant) + arretSuivant.Width / 50,
                                Y2 = Canvas.GetTop(arretSuivant) + arretSuivant.Height / 2,*/

                //Positionnement du début de la ligne en abscisse et ordonnée
                X1 = Canvas.GetLeft(arretPrecedent) + arretPrecedent.Width,
                Y1 = Canvas.GetTop(arretPrecedent) + arretPrecedent.Height / 2,

                //Positionnement de la fin de la ligne en abscisse et ordonnée
                X2 = Canvas.GetLeft(arretSuivant) + arretSuivant.Width - 15,
                Y2 = Canvas.GetTop(arretSuivant) + arretSuivant.Height / 2,


                //Coloration et définition de la bordure de la ligne
                Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString(couleurLigne)),
                //Stroke = Brushes.Black,
                StrokeThickness = 2
            };

            //Ajout de la ligne au graphe
            canvas.Children.Add(arc);
        }

		private FrameworkElement TrouverArret(string nomArret)
		{
		    foreach (var child in canvas.Children)
		    {
		        if (child is Ellipse)
		        {
		            var ellipse = child as Ellipse;
		            if (ellipse.Tag.ToString() == nomArret)
                    {
                        return ellipse as FrameworkElement;
                    }
                }
		    }
		
		    return null;
		}

        private void ExporterImage(string cheminFichier)
        {
            var largeur = 2480;
            var hauteur = 3508;
            var dpiX = 300;
            var dpiY = 300;
            RenderTargetBitmap renduFinal = new RenderTargetBitmap(
                /*                                (int)canvas.ActualWidth,
                                                (int)canvas.ActualHeight,*/
                largeur,
                hauteur,
                dpiX,
                dpiY,
                PixelFormats.Pbgra32);

            DrawingVisual conceptionVisuelle = new DrawingVisual();
            using (DrawingContext drawingContext = conceptionVisuelle.RenderOpen())
            {
                drawingContext.DrawRectangle(Brushes.White, null, new Rect(0, 0, largeur, hauteur));
                //drawingContext.DrawRectangle(Brushes.White, null, new Rect(0, 0, canvas.ActualWidth, canvas.ActualHeight));
            }

            renduFinal.Render(conceptionVisuelle);
            renduFinal.Render(canvas);

            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(renduFinal));

            using (FileStream stream = new FileStream(cheminFichier, FileMode.Create))
            {
                encoder.Save(stream);
            }
        }

        private void Button_exportImage_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Fichiers d'image (*.png)|*.png";
            saveFileDialog.FileName = $"GrapheItineraireLigne5_{DateTime.Now:yyyyMMdd_HHmmss}.png";

            // Afficher la boîte de dialogue
            if (saveFileDialog.ShowDialog() == true)
            {
                ExporterImage(saveFileDialog.FileName);
                Process.Start(saveFileDialog.FileName);
            }
        }
    }
}