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
    public class Thermometre
    {
        private string CouleurLigne;
        private string CouleurPoleEchange;
        private List<Arret> LesArrets;
        private List<Ligne> LesLignes;
        private double LongueurPointArret = 15;
        private double HauteurPointArret = 15;
        private double EpaisseurTraitArret = 2;
        private double tpEtiquetteNomArret = 15;
        private double tpEtiquetteCorrespondance = 12;
        private double tpEtiquetteNoLigne = 12;
        private RotateTransform rotationEtiquetteNomArretTerminus = new RotateTransform(0);


        public Thermometre(string couleurLigne, string couleurPoleEchange, List<Arret> lesArrets, List<Ligne> lesLignes)
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
            ;
            double abscisseArc = canvas.ActualWidth / 2;
            double ordonneeArc = canvas.ActualHeight / 2;

            Line arcPrincipal = CreerArc(0, 0, 500, 0, CouleurLigne, 2);
            canvas.Children.Add(arcPrincipal);

            double xPAPremier = arcPrincipal.X1 - LongueurPointArret / 2;
            double yPAPremier = arcPrincipal.Y1 - HauteurPointArret / 2;

            double xPADernier = arcPrincipal.X2 - LongueurPointArret / 2;
            double yPADernier = arcPrincipal.Y2 - HauteurPointArret / 2;

            if (LesArrets.Count > 0)
            {
                Arret premierArret = LesArrets.First();
                Arret dernierArret = LesArrets.Last();

                if(premierArret != null)
                {
                    if (premierArret.GetEstCorrespondance)
                    {
                        Ellipse pointArret = CreerPointArret(LongueurPointArret, HauteurPointArret, CouleurPoleEchange, EpaisseurTraitArret, premierArret.GetNom);
                        Label etiquetteNomArret = CreerEtiquetteNomArret(premierArret.GetNom, tpEtiquetteNomArret, premierArret.GetEstPoleEchange);
                        Label etiquetteCorrespondanceArret = CreerEtiquetteCorrespondance(tpEtiquetteCorrespondance, premierArret.GetEstPoleEchange);

                        etiquetteNomArret.RenderTransform = rotationEtiquetteNomArretTerminus;

                        Canvas.SetLeft(pointArret, xPAPremier);
                        Canvas.SetTop(pointArret, yPAPremier);

                        Canvas.SetLeft(etiquetteNomArret, xPAPremier + 5);
                        Canvas.SetTop(etiquetteNomArret, yPAPremier - 230);

                        Canvas.SetLeft(etiquetteCorrespondanceArret, xPAPremier - 2);
                        Canvas.SetTop(etiquetteCorrespondanceArret, yPAPremier - 6);

                        canvas.Children.Add(pointArret);
                        canvas.Children.Add(etiquetteNomArret);
                        canvas.Children.Add(etiquetteCorrespondanceArret);

                    }
                    else if (premierArret.GetEstPoleEchange)
                    {
                        Ellipse pointPoleEchange = CreerPointArret(LongueurPointArret, HauteurPointArret, CouleurPoleEchange, EpaisseurTraitArret, premierArret.GetNom);
                        Label etiquetteNomPoleEchange = CreerEtiquetteNomArret(premierArret.GetNom, tpEtiquetteNomArret, premierArret.GetEstPoleEchange);
                        Label etiquetteCorrespondancePoleEchange = CreerEtiquetteCorrespondance(tpEtiquetteCorrespondance, premierArret.GetEstPoleEchange);

                        etiquetteNomPoleEchange.RenderTransform = rotationEtiquetteNomArretTerminus;

                        Canvas.SetLeft(pointPoleEchange, xPAPremier);
                        Canvas.SetTop(pointPoleEchange, yPAPremier);

                        Canvas.SetLeft(etiquetteNomPoleEchange, xPAPremier + 5);
                        Canvas.SetTop(etiquetteNomPoleEchange, yPAPremier - 230);

                        Canvas.SetLeft(etiquetteCorrespondancePoleEchange, xPAPremier - 2);
                        Canvas.SetTop(etiquetteCorrespondancePoleEchange, yPAPremier - 6);

                        canvas.Children.Add(pointPoleEchange);
                        canvas.Children.Add(etiquetteNomPoleEchange);
                        canvas.Children.Add(etiquetteCorrespondancePoleEchange);
                    }
                    else if (!premierArret.GetEstCorrespondance && !premierArret.GetEstPoleEchange)
                    {
                        Ellipse pointArret = CreerPointArret(LongueurPointArret, HauteurPointArret, CouleurLigne, EpaisseurTraitArret, premierArret.GetNom);
                        Label etiquetteNomArret = CreerEtiquetteNomArret(premierArret.GetNom, tpEtiquetteNomArret, premierArret.GetEstPoleEchange);

                        etiquetteNomArret.RenderTransform = rotationEtiquetteNomArretTerminus;

                        Canvas.SetLeft(pointArret, xPAPremier);
                        Canvas.SetTop(pointArret, yPAPremier);

                        Canvas.SetLeft(etiquetteNomArret, xPAPremier + 5);
                        Canvas.SetTop(etiquetteNomArret, yPAPremier - 230);

                        canvas.Children.Add(pointArret);
                        canvas.Children.Add(etiquetteNomArret);
                    }

                    LesArrets.RemoveAt(0);
                }

                if(dernierArret != null)
                {
                    if(dernierArret.GetEstCorrespondance)
                    {
                        Ellipse pointArret = CreerPointArret(LongueurPointArret, HauteurPointArret, CouleurPoleEchange, EpaisseurTraitArret, dernierArret.GetNom);
                        Label etiquetteNomArret = CreerEtiquetteNomArret(dernierArret.GetNom, tpEtiquetteNomArret, dernierArret.GetEstPoleEchange);
                        Label etiquetteCorrespondanceArret = CreerEtiquetteCorrespondance(tpEtiquetteCorrespondance, dernierArret.GetEstPoleEchange);

                        etiquetteNomArret.RenderTransform = rotationEtiquetteNomArretTerminus;

                        Canvas.SetLeft(pointArret, xPADernier);
                        Canvas.SetTop(pointArret, yPADernier);

                        Canvas.SetLeft(etiquetteNomArret, xPADernier + 5);
                        Canvas.SetTop(etiquetteNomArret, yPADernier - 230);

                        Canvas.SetLeft(etiquetteCorrespondanceArret, xPADernier - 2);
                        Canvas.SetTop(etiquetteCorrespondanceArret, yPADernier - 6);

                        canvas.Children.Add(pointArret);
                        canvas.Children.Add(etiquetteNomArret);
                        canvas.Children.Add(etiquetteCorrespondanceArret);

                    }
                    else if(dernierArret.GetEstPoleEchange)
                    {
                        Ellipse pointPoleEchange = CreerPointArret(LongueurPointArret, HauteurPointArret, CouleurPoleEchange, EpaisseurTraitArret, dernierArret.GetNom);
                        Label etiquetteNomPoleEchange = CreerEtiquetteNomArret(dernierArret.GetNom, tpEtiquetteNomArret, dernierArret.GetEstPoleEchange);
                        Label etiquetteCorrespondancePoleEchange = CreerEtiquetteCorrespondance(tpEtiquetteCorrespondance, dernierArret.GetEstPoleEchange);

                        etiquetteNomPoleEchange.RenderTransform = rotationEtiquetteNomArretTerminus;

                        Canvas.SetLeft(pointPoleEchange, xPADernier);
                        Canvas.SetTop(pointPoleEchange, yPADernier);

                        Canvas.SetLeft(etiquetteNomPoleEchange, xPADernier + 5);
                        Canvas.SetTop(etiquetteNomPoleEchange, yPADernier - 230);

                        Canvas.SetLeft(etiquetteCorrespondancePoleEchange, xPADernier - 2);
                        Canvas.SetTop(etiquetteCorrespondancePoleEchange, yPADernier - 6);

                        canvas.Children.Add(pointPoleEchange);
                        canvas.Children.Add(etiquetteNomPoleEchange);
                        canvas.Children.Add(etiquetteCorrespondancePoleEchange);
                    }
                    else if(!dernierArret.GetEstCorrespondance && !dernierArret.GetEstPoleEchange)
                    {
                        Ellipse pointArret = CreerPointArret(LongueurPointArret, HauteurPointArret, CouleurLigne, EpaisseurTraitArret, dernierArret.GetNom);
                        Label etiquetteNomArret = CreerEtiquetteNomArret(dernierArret.GetNom, tpEtiquetteNomArret, dernierArret.GetEstPoleEchange);

                        etiquetteNomArret.RenderTransform = rotationEtiquetteNomArretTerminus;

                        Canvas.SetLeft(pointArret, xPADernier);
                        Canvas.SetTop(pointArret, yPADernier);

                        Canvas.SetLeft(etiquetteNomArret, xPADernier + 5);
                        Canvas.SetTop(etiquetteNomArret, yPADernier - 230);

                        canvas.Children.Add(pointArret);
                        canvas.Children.Add(etiquetteNomArret);
                    }

                    LesArrets.RemoveAt(LesArrets.Count - 1);
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

        private Ellipse CreerPointArret(double longueur, double hauteur, string couleur, double epaisseurTrait, string etiquette)
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
            if (estPoleEchange)
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
