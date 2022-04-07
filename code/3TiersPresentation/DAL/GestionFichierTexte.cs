using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3TiersPresentation.DAL
{
    /// <summary>
    /// Auteur :       Hugo St-Louis
    /// Description :  Classe concrète qui gère un fichier texte.
    /// Date :         2021-04-07
    /// </summary>
    public class GestionFichierTexte : ILectureFichier
    {
        /// <summary>
        /// Lit et retourne le contenu d'un fichier texte pour 
        /// lequel le nom est passé en paramètre.
        /// </summary>
        /// <param name="sNomFichier"></param>
        /// <returns></returns>
        public string LireFichierTexte(string sNomFichier)
        {
            bool bFichierExist = File.Exists(sNomFichier);
            string sContenuFichier = "";
            StreamReader sr = null;

            if (bFichierExist == false)
                return "Le fichier n'existe pas...";

            try
            {
                sr = new StreamReader(sNomFichier);
                sContenuFichier = sr.ReadToEnd();
                return sContenuFichier;
            }
            catch (Exception ex)
            {

                return "Erreur lors de la lecture du fichier " + sNomFichier + " \r\n " +
                    ex.Message;
            
            }

        }
    }
}
