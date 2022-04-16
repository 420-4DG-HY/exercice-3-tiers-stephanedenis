using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Démonstration du patron architectural 3 tiers et de plusieurs patrons de conception
/// 
/// (CC) BY-SA Stéphane Denis et Hugo St-Louis, CEGEP de Saint-Hyacinthe
/// </summary>
namespace Perceptron.BLL
{
    /// <summary>
    /// Interface qui gère l'interfaction avec la MainWindow
    /// </summary>
    public interface IPerceptron
    {
        string LoadTrainingData(string sNomFichier);


    }
}
