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

namespace ProtoThermometre
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static readonly Dictionary<String, String> lignes = new Dictionary<string, string>
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

        public MainWindow()
        {
            InitializeComponent();
            main();
        }

        public static void main()
        {
            List<Ligne> lesLignes = new List<Ligne>();

            foreach (var paireCleValeur in lignes)
            {
                lesLignes.Add(new Ligne(paireCleValeur.Key, paireCleValeur.Value));
            }

            foreach (var ligne in lesLignes)
            {
                MessageBox.Show($"Numéro de ligne : {ligne.GetNumero}, Couleur : {ligne.GetCouleur}");
            }
        }

        /// <summary>
        /// Permet d'exporter le thermomètre en type image .png au format A4
        /// </summary>
        /// <param name="cheminFichier"></param>
        private void ExporterImage(string cheminFichier)
        {
            var largeur = 2480;
            var hauteur = 3508;
            var dpiX = 300;
            var dpiY = 300;
            RenderTargetBitmap renduFinal = new RenderTargetBitmap(largeur, hauteur, dpiX, dpiY, PixelFormats.Pbgra32);

            DrawingVisual conceptionVisuelle = new DrawingVisual();
            using (DrawingContext drawingContext = conceptionVisuelle.RenderOpen())
            {
                drawingContext.DrawRectangle(Brushes.White, null, new Rect(0, 0, largeur, hauteur));
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
                Process.Start(new ProcessStartInfo(saveFileDialog.FileName) { UseShellExecute = true });
            }
        }
    }
}