using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3TiersPresentation.DAL
{
    /// <summary>
    /// Auteur :      Hugo St-Louis
    /// Description : Interface qui interagit avec le disque la 
    ///               gestion de fichiers.
    /// Date:         2021-04-07              
    /// </summary>
    public interface ILectureFichier
    {
        string LireFichierTexte(string sNomFichier);

    }
}
