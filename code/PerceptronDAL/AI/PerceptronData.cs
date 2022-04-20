using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Demo3tiers.DAL.AI
{
    public class PerceptronData
    {
        // Pour comprendre à quoi correspondent les poids des synapses
        // Voir AI.Sample.Type
        public string Type
        {
            get;
            set;
        }

        public double[] InputWeights
        {
            get;
            set; // Utilisé par constructeur et désérialisation seulement
        }

        public double this[int i]
        {
            get { return InputWeights[i]; }
            set { InputWeights[i] = value; }
        }

        [JsonIgnore]
        public int Length { get => InputWeights.Length; }

        public PerceptronData(int inputSize, string type)
        {
            InputWeights = new double[inputSize];
            Type = type;

        }

        [JsonConstructor]
        public PerceptronData()
        {
            // Utilisé par la désérialisation seulement
        }

        public PerceptronData DeepCopy()
        {
            PerceptronData perceptronData = new PerceptronData();
            perceptronData.Type = Type;
            perceptronData.InputWeights = (double[])InputWeights.Clone();
            return perceptronData;
        }

        public static PerceptronData RetreiveFromFile(string name)
        {
            string fileName = name + ".perceptron.json";
            string jsonString = File.ReadAllText(fileName);
            // TODO gérer les exceptions de ReadAllText

            PerceptronData? pd =
               JsonSerializer.Deserialize<PerceptronData>(jsonString);

            if (pd == null)
            {
                throw new Exception("Erreur d'interprétation du fichier " + fileName);
            }
            return pd;
        }

        public void SaveToFile(string name)
        {
            string fileName = name + ".perceptron.json";
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize<PerceptronData>(this, options);
            File.WriteAllText(fileName, jsonString);
            // TODO gérer les exceptions de WriteAllText
        }
    }
}
