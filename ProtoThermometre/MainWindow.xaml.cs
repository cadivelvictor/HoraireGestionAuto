using Microsoft.Win32;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace ProtoThermometre
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            WindowState = WindowState.Minimized;

            canvas.Width = FenetrePrincipale.Width - 2 * 50;
            canvas.Height = FenetrePrincipale.Height - 2 * 50;

            List<Ligne> lesLignes = new List<Ligne>();

            Dictionary<string,string> lignes = new Dictionary<string,string>
            {
                {"1" , "#008000"}, {"5" , "#E30613"}, {"6" , "#4F80BD"}, {"7" , "#76B82A"}, {"8" , "#F39200"}, {"10", "#FFDD00"}, {"11", "#A85E24"},
                {"12", "#EA5297"}, {"12A","#EA5297"}, {"12B","#EA5297"}, {"13", "#00A68B"}, {"14", "#A2A628"}, {"15", "#ED925F"}, {"16", "#BFD243"},
                {"19", "#A61D81"}, {"20", "#2DAFE6"}, {"21", "#76B88E"}, {"21A","#76B88E"}, {"22", "#F3953F"}, {"22A","#F3953F"}, {"23", "#8577B6"},
                {"23A","#8577B6"}, {"23B","#8577B6"}, {"23C","#8577B6"}, {"23D","#8577B6"}, {"23E","#8577B6"}, {"24", "#004F9F"}, {"24A","#004F9F"},
                {"24B","#004F9F"}, {"25", "#72787A"}, {"25A","#72787A"}, {"26", "#00823F"}, {"26A","#00823F"}, {"27", "#009FE3"}, {"27A","#009FE3"},
                {"28", "#EE758F"}, {"29", "#BD2B0B"}, {"29A","#BD2B0B"}, {"30", "#BC731E"}, {"31", "#B14D97"}, {"32", "#7E4900"}, {"33", "#C8D400"},
                {"34", "#C6C6C6"}, {"36", "#FFCC07"}, {"37", "#AFCA13"}, {"38", "#0A9CA5"}, {"39", "#706F6F"}, {"40", "#13A538"}, {"41", "#F39200"},
                {"42", "#BA4E97"}, {"44A","#73858E"}, {"46", "#E4032E"}, {"47", "#276499"}, {"48", "#5BC5F2"}, {"48A","#5BC5F2"}, {"49", "#A98F00"},
                {"50", "#FFCC00"}, {"51", "#009BA4"}, {"51A","#009BA4"}, {"53", "#12A33A"}, {"54", "#DE85B6"}, {"60", "#08A339"}, {"61", "#DF3439"},
                {"62", "#EA8C36"}, {"63", "#1D9DD9"}, {"64", "#C8D400"}, {"65", "#7D8386"}, {"67", "#7C6EB0"}, {"68", "#F39200"}, {"69", "#006878"}
            };

            List<Arret> lesArrets = new List<Arret>
            {
                new Arret("STADE DE L’EST", true, false, new List<string>{"15", "26", "27", "27A", "31", "33"}),
                new Arret("Parc des Expositions", true, false, new List<string> { "27", "27A", "33" }),
                new Arret("ZEC du Chaudron", true, false, new List <string> { "27", "27A", "33" }),
                new Arret("Manès", true, false, null),
                new Arret("Pierre Aubert", true, false, new List<string> { "27", "27A", "33" }),
                new Arret("Roger Payet", true, false, new List<string> { "27", "27A", "31", "33" }),
                new Arret("Station Chaudron", false, true, new List <string> { "1", "24", "24A", "25", "26", "27", "27A", "28", "29A", "31", "32", "33", "40" }),
                new Arret("Mail du Chaudron", false, true, new List<string> { "1", "6", "8" }),
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
                new Arret("HÔTEL DE VILLE DE SAINT-DENIS", true, false, new List<string> { "6", "7", "8", "10", "11", "12", "13", "14", "16", "19", "21", "22", "22A", "23" })
            };            

            foreach (var paireCleValeur in lignes)
            {
                lesLignes.Add(new Ligne(paireCleValeur.Key, paireCleValeur.Value));
            }

            Thermometre thermometre = new Thermometre("#E30613", "#ACC32B", lesArrets, lesLignes);
            thermometre.Construire(canvas);
        }

        /// <summary>
        /// Permet d'exporter le thermomètre en type image .png au format A4
        /// </summary>
        /// <param name="cheminFichier"></param>
        private void ExporterImage(string cheminFichier, int longueur, int hauteur, double dpiX, double dpiY)
        {
            RenderTargetBitmap renduFinal = new RenderTargetBitmap(longueur, hauteur, dpiX, dpiY, PixelFormats.Pbgra32);

            DrawingVisual drawingVisual = new DrawingVisual();

            using (DrawingContext drawingContext = drawingVisual.RenderOpen())
            {
                drawingContext.DrawRectangle(Brushes.White, null, new Rect(0, 0, longueur, hauteur));
            }

            renduFinal.Render(drawingVisual);
            renduFinal.Render(canvas);

            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(renduFinal));

            using (FileStream stream = new FileStream(cheminFichier, FileMode.Create))
            {
                encoder.Save(stream);
            }
        }

        private void FenetrePrincipale_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double marge = 50;
            double largeurCanvas = FenetrePrincipale.Width - 2 * marge;
            double hauteurCanvas = FenetrePrincipale.Height - 2 * marge;

            canvas.Width = largeurCanvas;
            canvas.Height = hauteurCanvas;
        }

        private void FenetrePrincipale_Loaded(object sender, RoutedEventArgs e)
        {
            string nomFichierExport = $"GrapheItineraireLigne5_{DateTime.Now:yyyyMMdd_HHmmss}.png";
            string cheminFichierExport = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "ExportImage", nomFichierExport);
            ExporterImage(cheminFichierExport, 3507, 2480, 300, 300);
            Process.Start(new ProcessStartInfo(cheminFichierExport) { UseShellExecute = true });

            this.Close();
        }
    }
}