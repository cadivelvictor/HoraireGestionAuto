using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;
using Label = System.Windows.Controls.Label;
using System.Windows;

namespace ProtoThermometre
{
    public class ThermometreV2
    {
        private string CouleurLigne;
        private string CouleurPoleEchange;
        private List<Arret> LesArrets;
        private List<Ligne> LesLignes;
        private double LongueurSommet = 15;
        private double HauteurSommet = 15;
        private double EpaisseurTraitSommet = 2;
        private double tpEtiquetteNomArret = 15;
        private double tpEtiquetteCorrespondance = 12;
        private RotateTransform rotationEtiquetteNomArretTerminus = new RotateTransform(0);


        public ThermometreV2(string couleurLigne, string couleurPoleEchange, List<Arret> lesArrets, List<Ligne> lesLignes)
        {
            this.CouleurLigne = couleurLigne;
            this.CouleurPoleEchange = couleurPoleEchange;
            this.LesArrets = lesArrets;
            this.LesLignes = lesLignes;
        }
        public string GetCouleurLigne
        {
            get
            {
                return CouleurLigne;
            }
        }

        public string GetCouleurPoleEchange
        {
            get
            {
                return CouleurPoleEchange;
            }
        }

        public List<Arret> GetLesArrets
        {
            get
            {
                return LesArrets;
            }
        }

        public List<Ligne> GetLesLignes
        {
            get
            {
                return LesLignes;
            }
        }

        public void Construire(Canvas canvas)
        {
            Line arcPrincipal = CreerArc(0, 0, 250, 0, CouleurLigne, 2);
            canvas.Children.Add(arcPrincipal);

            double abscisseDebut = arcPrincipal.X1;
            double ordonneeDebut = arcPrincipal.Y1;

            double abscissFin = arcPrincipal.X2;
            double ordonneeFin = arcPrincipal.Y2; 

            if(LesArrets.Count > 0)
            {
                foreach(var arret in LesArrets)
                {
                    if(arret == LesArrets.First())
                    {
                        if (arret.GetEstCorrespondance)
                        {
                            Ellipse pointArret = CreerSommet(LongueurSommet, HauteurSommet, CouleurLigne, EpaisseurTraitSommet, arret.GetNom);
                            Label etiquetteNomArret = CreerEtiquetteNomArret(arret.GetNom, tpEtiquetteNomArret, arret.GetEstPoleEchange);
                            Label etiquetteCorrespondanceArret = CreerEtiquetteCorrespondance(tpEtiquetteCorrespondance, arret.GetEstPoleEchange);

                            etiquetteNomArret.RenderTransform = rotationEtiquetteNomArretTerminus;

                            Canvas.SetLeft(pointArret, abscisseDebut);
                            Canvas.SetTop(pointArret, ordonneeDebut);

                            Canvas.SetLeft(etiquetteNomArret, abscisseDebut + 5);
                            Canvas.SetTop(etiquetteNomArret, ordonneeDebut - 230);

                            Canvas.SetLeft(etiquetteCorrespondanceArret, abscisseDebut - 2);
                            Canvas.SetTop(etiquetteCorrespondanceArret, ordonneeDebut - 6);

                            canvas.Children.Add(pointArret);
                            canvas.Children.Add(etiquetteNomArret);
                            canvas.Children.Add(etiquetteCorrespondanceArret);

                        }

                        if (arret.GetEstPoleEchange)
                        {
                            Ellipse pointPoleEchange = CreerSommet(LongueurSommet, HauteurSommet, CouleurPoleEchange, EpaisseurTraitSommet, arret.GetNom);
                            Label etiquetteNomPoleEchange = CreerEtiquetteNomArret(arret.GetNom, tpEtiquetteNomArret, arret.GetEstPoleEchange);
                            Label etiquetteCorrespondancePoleEchange = CreerEtiquetteCorrespondance(tpEtiquetteCorrespondance, arret.GetEstPoleEchange);

                            etiquetteNomPoleEchange.RenderTransform = rotationEtiquetteNomArretTerminus;

                            Canvas.SetLeft(pointPoleEchange, abscisseDebut);
                            Canvas.SetTop(pointPoleEchange, ordonneeDebut);

                            Canvas.SetLeft(etiquetteNomPoleEchange, abscisseDebut + 5);
                            Canvas.SetTop(etiquetteNomPoleEchange, ordonneeDebut - 230);

                            Canvas.SetLeft(etiquetteCorrespondancePoleEchange, abscisseDebut - 2);
                            Canvas.SetTop(etiquetteCorrespondancePoleEchange, ordonneeDebut - 6);

                            canvas.Children.Add(pointPoleEchange);
                            canvas.Children.Add(etiquetteNomPoleEchange);
                            canvas.Children.Add(etiquetteCorrespondancePoleEchange);
                        }

                        if(!arret.GetEstCorrespondance && !arret.GetEstPoleEchange)
                        {
                            Ellipse pointArret = CreerSommet(LongueurSommet, HauteurSommet, CouleurLigne, EpaisseurTraitSommet, arret.GetNom);
                            Label etiquetteNomArret = CreerEtiquetteNomArret(arret.GetNom, tpEtiquetteNomArret, arret.GetEstPoleEchange);

                            etiquetteNomArret.RenderTransform = rotationEtiquetteNomArretTerminus;

                            Canvas.SetLeft(pointArret, abscisseDebut);
                            Canvas.SetTop(pointArret, ordonneeDebut);

                            Canvas.SetLeft(etiquetteNomArret, abscisseDebut + 5);
                            Canvas.SetTop(etiquetteNomArret, ordonneeDebut - 230);

                            canvas.Children.Add(pointArret);
                            canvas.Children.Add(etiquetteNomArret);
                        }
                    }
                }
            }
        }


        private Line CreerArc(double x1, double y1, double x2, double y2, string couleur, double epaisseurTrait)
        {
            return new Line
            {
                X1 = x1,
                Y1 = y1,
                X2 = x2,
                Y2 = y2,
                Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString(couleur)),
                StrokeThickness = epaisseurTrait
            };
        }

        private Ellipse CreerSommet(double longueur, double hauteur, string couleur, double epaisseurTrait, string etiquette)
        {
            return new Ellipse
            {
                Width = longueur,
                Height = hauteur,
                Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(couleur)),
                Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString(couleur)),
                StrokeThickness = epaisseurTrait,
                Tag = etiquette
            };
        }

        private Label CreerEtiquetteNomArret(string nom, double taillePolice, bool estPoleEchange)
        {
            if(estPoleEchange)
            {
                return new Label
                {
                    Content = nom,
                    FontSize = taillePolice,
                    HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center,
                    VerticalAlignment = System.Windows.VerticalAlignment.Center,
                    FontWeight = FontWeights.Bold
                };
            }
            else
            {
                return new Label
                {
                    Content = nom,
                    FontSize = taillePolice,
                    HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center,
                    VerticalAlignment = System.Windows.VerticalAlignment.Center,
                };
            }
        }

        private Label CreerEtiquetteCorrespondance(double taillePolice, bool estPoleEchange)
        {
            if (estPoleEchange)
            {
                return new Label
                {
                    Content = "C",
                    FontSize = taillePolice,
                    FontWeight = FontWeights.Bold,
                    Foreground = Brushes.Black,
                };
            }
            else
            {
                return new Label
                {
                    Content = "C",
                    FontSize = taillePolice,
                    FontWeight = FontWeights.Bold,
                    Foreground = Brushes.White,
                };
            }
        }

        private Rectangle CreerRectangleNoLigne(double longueur, double hauteur, string couleur, double bordureX, double bordureY, string etiquette) 
        {
            return new Rectangle
            {
                Width = longueur,
                Height = hauteur,
                Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(couleur)),
                Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString(couleur)),
                RadiusX = bordureX,
                RadiusY = bordureY,
                Tag = etiquette,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
        }

        private Label CreerEtiquetteNoLigne(double longueur, double hauteur, string contenu, double taillePolice, string etiquette)
        {
            return new Label
            {
                Width = longueur,
                Height = hauteur,
                Content = contenu,
                FontSize = taillePolice,
                FontWeight = FontWeights.Bold,
                Foreground = Brushes.White,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Tag = etiquette
            };
        }

    }
}
