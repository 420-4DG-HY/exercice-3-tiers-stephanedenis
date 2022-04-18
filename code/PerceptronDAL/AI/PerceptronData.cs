using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Perceptron.DAL.AI
{
    public class PerceptronData
    {
        public double LearningSpeed { get; private set; }
        public double[] InputWeights { get; private set; }

        public PerceptronData(double learningSpeed, int inputSize)
        {
            LearningSpeed = learningSpeed;
            InputWeights = new double[inputSize];
        }

        public static PerceptronData RetreiveFromFile(string name)
        {
            string fileName = name + ".perceptron.json";
            string jsonString = File.ReadAllText(fileName);
            // TODO gérer les exceptions de ReadAllText

            PerceptronData? pd =
               JsonSerializer.Deserialize<PerceptronData>(jsonString);

            if(pd == null)
            {
                throw new Exception("Erreur d'interprétation du fichier " + fileName);
            }
            return pd;
        }

        public void SaveToFile(string name)
        {
            string fileName = name + ".perceptron.json";
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize<PerceptronData>(this,options);
            File.WriteAllText(fileName, jsonString);
            // TODO gérer les exceptions de WriteAllText
        }
    }
}
