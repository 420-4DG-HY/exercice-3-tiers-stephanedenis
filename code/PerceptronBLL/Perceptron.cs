using Perceptron.DAL.AI;
using Perceptron.DAL.AI.Sample;

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
    /// Auteur :      Hugo St-Louis
    /// Description : Implémente la logique du perceptron.
    /// Date :        2021-02-17
    /// </summary>
    public class Perceptron
    {
        PerceptronData data;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="vitesseApp"></param>
        public Perceptron(double learningSpeed, int inputSize)
        {
            data = new PerceptronData(learningSpeed,inputSize);
        }

        public Perceptron(string name)
        {
            data = PerceptronData.RetreiveFromFile(name);
        }

        void initialize()
        {
            Random rdn = new Random();

            //Initialise les poids synaptiques à des valeurs aléatoire
            for (int i = 0; i < data.InputWeights.Length; i++)
                data.InputWeights[i] = rdn.NextDouble();
        }
        /// <summary>
        ///   Permet l'apprentissage automatique du perceptron par rapport à 
        ///   une base de données d'observations.
        /// </summary>
        /// <param name="samples">La base de données d'observations</param>
        /// <returns>Résultats intermédiaires de l'apprentissage automatique.</returns>
        public string Entrainement(AISample[] samples)
        {
            double dSum = 0;
            int iNbErreur = 0;
            int iNbIteration = 0;
            int iResultatEstime = 0;
            int iErreurLocal = 0;
            string sResultat = "";


            do
            {
                iNbErreur = 0;
                foreach (AISample sample in samples)
                {
                    //Évaluer une observation et de faire une prédiction.
                    dSum = data.InputWeights[0];
                    for (int j = 1; j < data.InputWeights.Length; j++)
                    {
                        dSum += data.InputWeights[j] * sample.GetAttributeValue( j - 1);
                    }
                    iResultatEstime = (dSum >= 0) ? 1 : 0;
                    iErreurLocal = sample.Result - iResultatEstime;

                    //Vérifier s'il y a eu une erreur avec l'observation
                    if (iErreurLocal != 0)
                    {
                        //Si on s'est trompé, alors mettre à jour les poids 
                        //synaptiques avec la méthode de descente en gradient.
                        data.InputWeights[0] += data.LearningSpeed * iErreurLocal;
                        for (int j = 1; j < data.InputWeights.Length; j++)
                        {
                            data.InputWeights[j] += data.LearningSpeed * iErreurLocal * sample.GetAttributeValue(j - 1);
                        }
                        iNbErreur++;
                    }
                }
                sResultat += string.Format("\r\nIteration {0} \t Erreur {1}", iNbIteration, iNbErreur);
                sResultat += string.Format("\r\nLe taux de succès est {0} %",
                                            ((double)(samples.Length- iNbErreur) / (double)(samples.Length)) * 100.00);

                iNbIteration++;
            }
            while (iNbErreur > 0 && iNbIteration < 10000);

            return sResultat;

        }

        /// <summary>
        /// Permet de tester un perceptron entrainé préalablement
        /// </summary>
        /// <param name="bd">L'ensemble d'entrainement</param>
        /// <returns>Les étapes intermédiaires du perceptron</returns>
        public string BatchEvaluate(string type, string sourceFile)
        {
            double dSum = 0;
            int iNbErreur = 0;

            int iResultatEstime = 0;
            int iErreurLocal = 0;
            string sResultat = "";

            AISample[] samples = AISampleFactory.CreateFromFile(type,sourceFile);


            foreach(AISample sample in samples)
            {
                //Évaluer une observation et de faire une prédiction.
                dSum = data.InputWeights[0];
                for (int j = 1; j < data.InputWeights.Length; j++)
                    dSum += data.InputWeights[j] * sample.GetAttributeValue(j - 1);
                iResultatEstime = (dSum >= 0) ? 1 : 0;
                iErreurLocal = sample.Result - iResultatEstime;

                //Vérifier s'il y a eu une erreur avec l'observation
                if (iErreurLocal != 0)
                    iNbErreur++;
            }
            sResultat += string.Format("\r\nLe percetron a fait {0} Erreur(s) sur l'échantillon de test", iNbErreur);
            sResultat += string.Format("\r\nLe taux de succès est {0} %",
                                        ((double)(samples.Length - iNbErreur) / (double)(samples.Length)) * 100.00);
            return sResultat;

        }

    }
}
