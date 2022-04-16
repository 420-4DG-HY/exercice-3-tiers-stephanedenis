using System;
using System.Collections.Generic;
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

using Perceptron.BLL;

namespace Perceptron.GUI
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
            txtNomFichier.Text = @"..\..\..\..\Echantillons\breastw_train.dat";
        }

        /// <summary>
        /// Lit et affiche le contenu d'un fichier(txtNomFichier)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLireFichier_Click(object sender, RoutedEventArgs e)
        {
            string sValeurs = "";
            string sNomFichier = txtNomFichier.Text;
            if (string.IsNullOrWhiteSpace(sNomFichier))
                //if (sNomFichier == null || sNomFichier.Trim() == "" )
                txtConsole.Text = "Vous devez entrer un nom de fichier valide";
            else
            {
                //sValeurs = _gestionMainWindow.LoadTrainingData(sNomFichier);
                txtConsole.Text = sValeurs;
            }
        }
    }
}
