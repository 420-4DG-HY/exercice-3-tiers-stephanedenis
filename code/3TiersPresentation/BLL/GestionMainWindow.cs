using _3TiersPresentation.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3TiersPresentation.BLL
{
    /// <summary>
    /// Auteur :      Hugo St-Louis
    /// Description : Gère l'interaction du MainWindow
    /// Date :        2021-04-07
    /// </summary>
    public class GestionMainWindow : IGestionMainWindow
    {
        private ILectureFichier _lectureFichier = new GestionFichierTexte();

        /// <summary>
        /// Lecture d'un fichier dont le nom est passé en paramètres
        /// </summary>
        /// <param name="sNomFichier"></param>
        /// <returns></returns>
        public string LireFichier(string sNomFichier)
        {
            string sValeur;
            //return _lectureFichier.LireFichierTexte(sNomFichier);
            // Utiliser une variable plutot que de retourner le contenu 
            // de la fonction.
            sValeur = _lectureFichier.LireFichierTexte(sNomFichier);
            return sValeur;
        }
    }
}
