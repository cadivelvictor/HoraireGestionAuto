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
        private double distnacePANA = 230;       //Définit la distance entre le point du premier/dernier arret et l'étiquette du nom du premier/dernier arret


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
            //Affiche 21
            //MessageBox.Show(LesArrets.Count.ToString());

            //Line arcPrincipal = CreerAxe(0, (canvas.Height/2), canvas.Width, (canvas.Height/2), CouleurLigne, 2);
            Line axePrincipal = CreerAxe(0, (canvas.Height / 2), canvas.Width, (canvas.Height / 2), CouleurLigne, 2);
            canvas.Children.Add(axePrincipal);

            double xPAPremier = axePrincipal.X1 - (LongueurPointArret / 2);
            double yPAPremier = axePrincipal.Y1 - (HauteurPointArret / 2);

            double xPADernier = axePrincipal.X2 - (LongueurPointArret / 2);
            double yPADernier = axePrincipal.Y2 - (HauteurPointArret / 2);



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
                        etiquetteNomArret.FontSize = 20;

                        Canvas.SetLeft(pointArret, xPAPremier);
                        Canvas.SetTop(pointArret, yPAPremier);

                        Canvas.SetLeft(etiquetteNomArret, axePrincipal.X1);
                        Canvas.SetTop(etiquetteNomArret, (axePrincipal.Y1 - distnacePANA));

                        Canvas.SetLeft(etiquetteCorrespondanceArret, (xPAPremier - 2));
                        Canvas.SetTop(etiquetteCorrespondanceArret, (yPAPremier - 6));

                        Line axe1TermDebut = CreerAxe(axePrincipal.X1, (canvas.Height / 2), axePrincipal.X1, (Canvas.GetTop(etiquetteNomArret) + etiquetteNomArret.FontSize * 2), CouleurPoleEchange, 2);
                        canvas.Children.Add(axe1TermDebut);

                        Line axe2TermDebut = CreerAxe((axe1TermDebut.X2 - 1), axe1TermDebut.Y2, (etiquetteNomArret.ToString().Length * 2), axe1TermDebut.Y2, CouleurPoleEchange, 2);
                        canvas.Children.Add(axe2TermDebut);

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
                        etiquetteNomPoleEchange.FontSize = 20;

                        Canvas.SetLeft(pointPoleEchange, xPAPremier);
                        Canvas.SetTop(pointPoleEchange, yPAPremier);

                        Canvas.SetLeft(etiquetteNomPoleEchange, axePrincipal.X1);
                        Canvas.SetTop(etiquetteNomPoleEchange, (axePrincipal.Y1 - distnacePANA));

                        Canvas.SetLeft(etiquetteCorrespondancePoleEchange, (xPAPremier - 2));
                        Canvas.SetTop(etiquetteCorrespondancePoleEchange, (yPAPremier - 6));

                        Line axe1TermDebut = CreerAxe(axePrincipal.X1, (canvas.Height / 2), axePrincipal.X1, (Canvas.GetTop(etiquetteNomPoleEchange) + etiquetteNomPoleEchange.FontSize * 2), CouleurPoleEchange, 2);
                        canvas.Children.Add(axe1TermDebut);

                        Line axe2TermDebut = CreerAxe((axe1TermDebut.X2 - 1), axe1TermDebut.Y2, (etiquetteNomPoleEchange.ToString().Length * 2), axe1TermDebut.Y2, CouleurPoleEchange, 2);
                        canvas.Children.Add(axe2TermDebut);

                        canvas.Children.Add(pointPoleEchange);
                        canvas.Children.Add(etiquetteNomPoleEchange);
                        canvas.Children.Add(etiquetteCorrespondancePoleEchange);
                    }
                    else if (!premierArret.GetEstCorrespondance && !premierArret.GetEstPoleEchange)
                    {
                        Ellipse pointArret = CreerPointArret(LongueurPointArret, HauteurPointArret, CouleurLigne, EpaisseurTraitArret, premierArret.GetNom);
                        Label etiquetteNomArret = CreerEtiquetteNomArret(premierArret.GetNom, tpEtiquetteNomArret, premierArret.GetEstPoleEchange);

                        etiquetteNomArret.RenderTransform = rotationEtiquetteNomArretTerminus;
                        etiquetteNomArret.FontSize = 20;

                        Canvas.SetLeft(pointArret, xPAPremier);
                        Canvas.SetTop(pointArret, yPAPremier);

                        Canvas.SetLeft(etiquetteNomArret, axePrincipal.X1);
                        Canvas.SetTop(etiquetteNomArret, (axePrincipal.Y1 - distnacePANA));

                        Line axe1TermDebut = CreerAxe(axePrincipal.X1, (canvas.Height / 2), axePrincipal.X1, (Canvas.GetTop(etiquetteNomArret) + etiquetteNomArret.FontSize * 2), CouleurPoleEchange, 2);
                        canvas.Children.Add(axe1TermDebut);

                        Line axe2TermDebut = CreerAxe((axe1TermDebut.X2 - 1), axe1TermDebut.Y2, (etiquetteNomArret.ToString().Length * 2), axe1TermDebut.Y2, CouleurPoleEchange, 2);
                        canvas.Children.Add(axe2TermDebut);

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
                        etiquetteNomArret.FontSize = 20;

                        Canvas.SetLeft(pointArret, xPADernier);
                        Canvas.SetTop(pointArret, yPADernier);

                        Canvas.SetRight(etiquetteNomArret, axePrincipal.X1);
                        Canvas.SetTop(etiquetteNomArret, (axePrincipal.Y2 - distnacePANA)); 

                        Canvas.SetLeft(etiquetteCorrespondanceArret,(xPADernier - 2));
                        Canvas.SetTop(etiquetteCorrespondanceArret, (yPADernier - 6));

                        Line axe1TermFin = CreerAxe(axePrincipal.X2, (canvas.Height/2), axePrincipal.X2, (Canvas.GetTop(etiquetteNomArret) + etiquetteNomArret.FontSize * 2), CouleurPoleEchange, 2);
                        canvas.Children.Add(axe1TermFin);

                        Line axe2TermFin = CreerAxe((axe1TermFin.X2 + 1), axe1TermFin.Y2, (etiquetteNomArret.ToString().Length * 2), axe1TermFin.Y2, CouleurPoleEchange, 2);
                        canvas.Children.Add(axe2TermFin);

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
                        etiquetteNomPoleEchange.FontSize = 20;

                        Canvas.SetLeft(pointPoleEchange, xPADernier);
                        Canvas.SetTop(pointPoleEchange, yPADernier);

                        Canvas.SetRight(etiquetteNomPoleEchange, axePrincipal.X1);
                        Canvas.SetTop(etiquetteNomPoleEchange,(axePrincipal.Y2 - distnacePANA));

                        Canvas.SetLeft(etiquetteCorrespondancePoleEchange, (xPADernier - 2));
                        Canvas.SetTop(etiquetteCorrespondancePoleEchange, (yPADernier - 6));

                        canvas.Children.Add(pointPoleEchange);
                        canvas.Children.Add(etiquetteNomPoleEchange);
                        canvas.Children.Add(etiquetteCorrespondancePoleEchange);
                    }
                    else if(!dernierArret.GetEstCorrespondance && !dernierArret.GetEstPoleEchange)
                    {
                        Ellipse pointArret = CreerPointArret(LongueurPointArret, HauteurPointArret, CouleurLigne, EpaisseurTraitArret, dernierArret.GetNom);
                        Label etiquetteNomArret = CreerEtiquetteNomArret(dernierArret.GetNom, tpEtiquetteNomArret, dernierArret.GetEstPoleEchange);

                        etiquetteNomArret.RenderTransform = rotationEtiquetteNomArretTerminus;
                        etiquetteNomArret.FontSize = 20;

                        Canvas.SetLeft(pointArret, xPADernier);
                        Canvas.SetTop(pointArret, yPADernier);

                        Canvas.SetLeft(etiquetteNomArret, axePrincipal.X1);
                        Canvas.SetTop(etiquetteNomArret, (axePrincipal.Y2 - distnacePANA));

                        canvas.Children.Add(pointArret);
                        canvas.Children.Add(etiquetteNomArret);
                    }

                    LesArrets.RemoveAt(LesArrets.Count - 1);
                }                
            }


        }


        private Line CreerAxe(double x1, double y1, double x2, double y2, string couleur, double epaisseurTrait)
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
