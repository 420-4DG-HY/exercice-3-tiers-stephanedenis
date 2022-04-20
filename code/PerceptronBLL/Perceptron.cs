using Demo3tiers.DAL.AI;
using Demo3tiers.DAL.AI.Sample;

using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            return value > 0 ? 1.0 : 0.0;
        }

        /// <summary>
        /// Permet l'apprentissage automatique du perceptron par rapport à une base de données d'observations (échantillons).
        /// </summary>
        /// <param name="samples">Échantillons de référence pour l'apprentissage</param>
        /// <param name="learningSpeed">Vitesse d'apprentissage (ratio d'intégration des erreurs détectées)</param>
        /// <param name="iterMax">Limite d'itérations</param>
        /// <param name="antiRegresstion">Évite les échantillons dont l'apprentissage fait augmenter le taux d'erreurs (expérimental, plus lent si activé)</param>
        /// <returns>Historique du nombre d'erreurs pendant l'apprentissage</returns>
        public int[] LearnFrom(AISample[] samples, double learningSpeed = 0.1, int iterMax = 1000, bool antiRegresstion = false)
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

            lock (this)
            {
                int originalErrors = BatchEvaluate(samples);
                int learningCycleErrors = originalErrors;
                bool improvement;
                PerceptronData prototype = synapse; ; // utilisé pour l'antirégression
                do
                {
                    improvement = false;
                    int candidateErrors = 0;
                    foreach (AISample subject in samples)
                    {
                        #region initialisation pour antiregression  si activée
                        if (antiRegresstion)
                        {
                            // prototype (et memento ;-),
                            // pour ne conserver que les meilleurs valeurs de synapses
                            // quand un échantillon biaise trop l'apprentissage
                            prototype = synapse;
                            synapse = prototype.DeepCopy();
                        }
                        #endregion

                        // Au cas où il y aurait des nuances (valeurs entre 0 et 1), on applique la même fonction de sortie
                        // au résultat de l'échantillon que ce qui est utilisé pour le résultat notre perceptron 
                        double target = Heaviside(subject.Result);

                        // Prédiction avec perceptron actuel
                        var prediction = Evaluate(subject);

                        // On compare avec le résultat officiel de l'échantillon
                        // et on applique une correction aux synapses au besoin
                        if (prediction != target)
                        {
                            candidateErrors++;
                            var error = target - prediction; // en Heaviside ça donne -1.0 ou +1.0

                            // mettre à jour les poids synaptiques
                            // avec la méthode de descente en gradient.
                            for (int i = 0; i < synapse.Length; i++)
                            {
                                var adjustment = error * learningSpeed; // * 0.1 par défaut, donc -0.1 ou +0.1
                                synapse[i] += adjustment * subject[i];
                            }

                        }

                        #region Traitement de l'antiregression si activée
                        if (antiRegresstion)
                        {
                            // On vérifie si on a amélioré le perceptron dans son ensemble
                            candidateErrors = BatchEvaluate(samples);
                            if (candidateErrors > learningCycleErrors)
                            {
                                // on a régressé... donc undo!
                                // un échantillon atypique nuit à la généralisation
                                synapse = prototype;
                                candidateErrors = learningCycleErrors;
                            }
                            else
                            {
                                // on peut continuer les ajustements avec les corrections de cet échantillon
                                learningCycleErrors = candidateErrors;
                                improvement = true;
                            }
                        }
                        else
                        #endregion

                        #region sans antirégression
                        {
                            if (candidateErrors <= learningCycleErrors)
                            {
                                improvement = true;
                            }
                            learningCycleErrors = candidateErrors;
                        }
                        #endregion
                    }
                    learningErrorHistory.Add(learningCycleErrors);
                }
                while (learningCycleErrors != 0 && improvement && learningErrorHistory.Count < iterMax);
            }
            return learningErrorHistory.ToArray();
        }

        public int[] LearnFrom(string type, string sourceFile, double learningSpeed = 0.1, int iterMax = 1000)
        {
            return LearnFrom(AISampleFactory.CreateFromFile(type, sourceFile), learningSpeed, iterMax);
        }

        /// <summary>
        /// Compte le nombre d'évaluations (prédictions) ayant un écart avec le résultat réel des échantillons
        /// </summary>
        /// <param name="type">Nom de la classe d'échantillons (voir AI.Sample.Type)</param>
        /// <param name="sourceFile">Fichier contenant une série d'échantillons</param>
        /// <returns>Nombre de prédictions erronées</returns>
        public int BatchEvaluate(string type, string sourceFile)
        {
            return BatchEvaluate(AISampleFactory.CreateFromFile(type, sourceFile));
        }

        /// <summary>
        /// Compte le nombre d'évaluations (prédictions) ayant un écart avec le résultat réel des échantillons
        /// </summary>
        /// <param name="samples">Série d'échantillon à évaluer</param>
        /// <returns>Nombre de prédictions erronées</returns>
        public int BatchEvaluate(AISample[] samples)
        {
            int errors = 0;
            lock (this)
            {
                foreach (AISample subject in samples)
                {
                    if (Evaluate(subject)  != Heaviside(subject.Result) )
                        errors++;
                }
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
            lock (this)
            {
                for (int i = 0; i < synapse.Length; i++)
                    sum += synapse[i] * subject[i];
            }
            return Heaviside(sum);
        }
    }
}
