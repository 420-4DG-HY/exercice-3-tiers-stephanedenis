using Demo3tiers.DAL.AI;
using Demo3tiers.DAL.AI.Sample;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

/// <summary>
/// Démonstration du patron architectural 3 tiers et de plusieurs patrons de conception
/// 
/// (CC) BY-SA Stéphane Denis et Hugo St-Louis, CEGEP de Saint-Hyacinthe
/// </summary>
namespace Demo3tiers.BLL
{

    /// <summary>
    /// Implémentation de l'algorithme de prédiction et d'apprentissage du perceptron
    /// https://fr.wikipedia.org/wiki/Perceptron
    /// </summary>
    public class Perceptron
    {
        PerceptronData synapse;

        /// <summary>
        /// Construit un perceptron vierge
        /// </summary>
        /// <param name="inputSize">Nombre d'entrées des données d'un échantillon</param>
        public Perceptron(int inputSize, string type)
        {
            synapse = new PerceptronData(inputSize, type);
        }

        /// <summary>
        /// Récupère un perceptron déjà enregistré dans un fichier
        /// </summary>
        /// <param name="name">Nom du fichier (sans extension)</param>
        public Perceptron(string fileName)
        {
            synapse = PerceptronData.RetreiveFromFile(fileName);
        }

        /// <summary>
        /// Enregistre le perceptron dans un fichier
        /// </summary>
        /// <param name="fileName">Nom du fichier (sans extension)</param>
        public void SavePerceptron(string fileName)
        {
            synapse.SaveToFile(fileName);
        }

        public void initializeWithRandomWeights()
        {
            Random rdn = new Random();

            //Initialise les poids synaptiques à des valeurs aléatoire
            for (int i = 0; i < synapse.Length; i++)
                synapse[i] = rdn.NextDouble();
        }

        /// <summary>
        /// Pour normaliser le résultat, on réduit la prédiction au domaine 0..1
        /// ici sans nuance : https://fr.wikipedia.org/wiki/Fonction_de_Heaviside
        /// Option alternative : https://fr.wikipedia.org/wiki/Tangente_hyperbolique
        /// </summary>
        /// <param name="value">entrée</param>
        /// <returns>valeur de 0 à 1 (ici, 0 ou 1)</returns>
        double Heaviside(double value)
        {
            return value > 0 ? 1.0 : 0.0 ;
        }

        /// <summary>
        /// Permet l'apprentissage automatique du perceptron par rapport à une base de données d'observations (échantillons).
        /// </summary>
        /// <param name="samples">Échantillons de référence pour l'apprentissage</param>
        /// <param name="learningSpeed">Vitesse d'apprentissage (ratio d'intégration des erreurs détectées)</param>
        /// <param name="iterMax">Limite d'itérations</param>
        /// <returns>Historique du nombre d'erreurs pendant l'apprentissage</returns>
        public int[] LearnFrom(AISample[] samples, double learningSpeed = 0.1, int iterMax = 10000)
        {
            #region validation des arguments
            if (learningSpeed <= 0 || learningSpeed >= 1)
            {
                throw new ArgumentException("learningSpeed doit être entre 0 et 1");
            }
            if (samples[0].Length != synapse.Length)
            {
                throw new Exception("Le nombre d'attributs des échantillons ne correspond pas au nombre de synapses du perceptron (" + samples[0].Length + "/" + synapse.Length + ")");
            }
            #endregion

            // Historique retourné en résultat : Utile pour étudier la courbe d'apprentissage
            List<int> learningErrorHistory = new List<int>();

            int errors;
            do
            {
                errors = 0;
                foreach (AISample subject in samples)
                {
                    // Au cas où il y aurait des nuances (valeurs entre 0 et 1), on applique la même fonction de sortie
                    // au résultat de l'échantillon que ce qui est utilisé pour le résultat notre perceptron 
                    double target = Heaviside(subject.Result);

                    // Prédiction avec perceptron actuel
                    var prediction = Evaluate(subject);

                    // On compare avec le résultat officiel de l'échantillon
                    // et on applique une correction aux synapses au besoin
                    if (prediction != target)
                    {
                        var error = target - prediction; // en Heaviside ça donne -1.0 ou +1.0

                        // mettre à jour les poids synaptiques
                        // avec la méthode de descente en gradient.
                        for (int i = 0; i < synapse.Length; i++)
                        {
                            var adjustment = error * learningSpeed; // * 0.1 par défaut, donc -0.1 ou +0.1
                            synapse[i] += adjustment * subject[i];
                        }
                        errors++;
                    }
                }
                learningErrorHistory.Add(errors);
            }
            while (errors > 0 && learningErrorHistory.Count < iterMax);

            return learningErrorHistory.ToArray();
        }

        public int[] LearnFrom(string type, string sourceFile, double learningSpeed = 0.1, int iterMax = 10000)
        {
            return LearnFrom(AISampleFactory.CreateFromFile(type, sourceFile), learningSpeed, iterMax);
        }

        /// <summary>
        /// Compte le nombre d'évaluations (prédictions) ayant un écart avec le résultat réel des échantillons
        /// </summary>
        /// <param name="type">Nom de la classe d'échantillons (voir AI.Sample.Type)</param>
        /// <param name="sourceFile">Fichier contenant une série d'échantillons</param>
        /// <returns></returns>
        public int BatchEvaluate(string type, string sourceFile)
        {
            int errors = 0;

            AISample[] samples = AISampleFactory.CreateFromFile(type, sourceFile);

            foreach (AISample subject in samples)
            {
                if (Evaluate(subject)>0 != subject.Result > 0)
                    errors++;
            }
            return errors;
        }

        /// <summary>
        /// Évalue les entrées du perceptron et prédit un résultat
        /// </summary>
        /// <param name="subject">Échantillon à évaluer</param>
        /// <returns>Résultat prédit par le perceptron</returns>
        public double Evaluate(AISample subject)
        {
            var sum = 0.0;
            for (int i = 0; i < synapse.Length; i++)
                sum += synapse[i] * subject[i];
            return Heaviside(sum);
        }
    }
}
