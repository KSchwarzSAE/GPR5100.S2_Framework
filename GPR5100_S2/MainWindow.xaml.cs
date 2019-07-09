using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GPR5100_S2
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        // Menüleiste
        // File
        // File->New => Grid geleert werden
        // File->Save => SaveFileDialog
        // File->Load => OpenFileDialog
        // Toolbox hinzufügen (Empfehle StackPanel mit Images)
        // Statusbar soll letzte Aktion enthalten ("Datei xy wurde gespeichert")...
        //** Statusbar KANN eine Progressbar enthalten, welche den Ladestatus angibt (LoadImages async) **/
        // Das Programm darf unter keinen Umständen abstürzen.
        // Es sollen (falls notwendig) klare Fehlermeldungen angezeigt werden (z.B. MessageBox)
        // Im optimalfall können keine Fehler auftreten.
        // Tipp zum Laden:
        // Grid.GetColumn(image);
        // Grid.GetRow(image);

        private List<BitmapSource> bitmapSources;

        public MainWindow()
        {
            InitializeComponent();

            // alle bilder laden
            bitmapSources = LoadImages();

            // dynamisch 10 * 10 zellen erstellen
            for (int i = 0; i < 10; ++i)
            {
                sceneGrid.ColumnDefinitions.Add(new ColumnDefinition());
                sceneGrid.RowDefinitions.Add(new RowDefinition());
            }
            
            for(int x = 0; x < 10; ++x)
                for(int y = 0; y < 10; ++y)
                {
                    Image image = new Image();
                    image.Source = bitmapSources.First();
                    Grid.SetColumn(image, x);
                    Grid.SetRow(image, y);
                    sceneGrid.Children.Add(image);

                    image.MouseUp += Image_MouseUp;
                }
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            // das bild auf dem image ändern
        }

        // Muss noch gegen fehler abgesichert werden.
        private List<BitmapSource> LoadImages()
        {
            List<BitmapSource> bitmapImages = new List<BitmapSource>();

            // alle jpegs durchgehen
            foreach(string file in Directory.GetFiles("./images", "*.jpg"))
            {
                // die datei zum laden
                using (var stream = File.OpenRead(file))
                {
                    var decoder = BitmapDecoder.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);

                    bitmapImages.Add(decoder.Frames.First());
                }
            }

            return bitmapImages;
        }
    }
}
