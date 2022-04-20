using Demo3tiers.BLL;
using Demo3tiers.DAL.AI.Sample.Type;

using System;
using System.Linq;
using System.Windows;
using System.Windows.Shapes;

namespace Demo3tiers.GUI
{
    /// <summary>
    /// Auteur :      Hugo St-Louis
    /// Description : Logique d'interaction pour MainWindow.xaml
    /// Date :        2021-04-07
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            txtNomFichier.Text = @"..\..\..\..\..\Echantillons\usvotes_train.dat";

        }

        private void btnLireFichier_Click(object sender, RoutedEventArgs e)
        {

            string nomFichier = txtNomFichier.Text;
            if (!string.IsNullOrWhiteSpace(nomFichier))
            {
                string type = typeof(USVoteSample).Name;
                int size = USVoteSample.USVoteSampleAttributeNames.Length;
                Perceptron p = new Perceptron(size,type);
                //p.initializeWithRandomWeights();
                try
                {
                    txtConsole.Text = "Apprentissage en cours ...";
                    int[] learningCurve = p.LearnFrom(type, nomFichier);

                    txtConsole.Text = "Traçage ...";
                    int max = learningCurve.Max();
                    int min = learningCurve.Min();
                    lblGraphMax.Content = "Max\n" + max;
                    lblGraphMin.Content = min + "\nMin";

                    double width = graphCanvas.Width;
                    double height = graphCanvas.Height;
                    int index = 0;

                    foreach (int error in learningCurve)
                    {
                        double positionX = width * ((double)index / (double)learningCurve.Length);
                        double positionY =  height * ((double)(error - min) / (double)(max - min));
                        Line line = new Line();
                        line.Stroke = System.Windows.Media.Brushes.LightGreen;
                        line.StrokeThickness = width / learningCurve.Length;
                        line.X1 = line.X2 = positionX;
                        line.Y1 = height; // Car le zéro du canvas est en haut
                        line.Y2 = height - positionY;
                        graphCanvas.Children.Add(line);
                        index++;
                    }
                    txtConsole.Text = "Apprentissage terminé";
                }
                catch (Exception ex)
                {
                    txtConsole.Text = ex.Message;
                }

            }
        }
    }
}
