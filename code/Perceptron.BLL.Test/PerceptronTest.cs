using NUnit.Framework;
using System;
using Demo3tiers.BLL;
using System.IO;

namespace Demo3tiers.BLL.Test
{
    public class PerceptronTest
    {
        static readonly string sampleRoot = @"..\..\..\..\..\Echantillons\";
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void BreastCancerLearningTest()
        {
            string filename = sampleRoot + "breastw_train.dat";
            string type = "BreastCancerSample";
            Perceptron perceptron = new Perceptron(9,type);
            int[] learningCurve = perceptron.LearnFrom(type,filename);
            Assert.AreEqual(1000, learningCurve.Length);
            int discrepancies = perceptron.BatchEvaluate(type,sampleRoot + "breastw_test .csv");
            Assert.AreEqual(49, discrepancies); // 49/340 = 14,4% d'erreurs
        }

        [Test]
        public void DiabeteLearningTest()
        {
            string filename = sampleRoot + "pima_train.dat";
            string type = "DiabeteSample";
            Perceptron perceptron = new Perceptron(8,type);
            int[] learningCurve = perceptron.LearnFrom(type, filename,0.1,100000);
            Assert.AreEqual(100000, learningCurve.Length);
            int discrepancies = perceptron.BatchEvaluate(type, sampleRoot + "pima_test.dat");
            Assert.AreEqual(128, discrepancies); // 128/368 = 34,8% d'erreurs
        }

        [Test]
        public void SonarLearningTest()
        {
            string filename = sampleRoot + "sonar_train.dat";
            string type = "SonarSample";
            Perceptron perceptron = new Perceptron(60,type);
            int[] learningCurve = perceptron.LearnFrom(type, filename, 0.1, 10000);
            Assert.AreEqual(9166, learningCurve.Length);  // Converge avant la limite !
            int discrepancies = perceptron.BatchEvaluate(type, sampleRoot + "sonar_test.dat");
            Assert.AreEqual(21, discrepancies); // 21/103 = 20% d'erreurs
        }

        [Test]
        public void USVotesLearningTest()
        {
            string filename = sampleRoot + "USvotes_train.dat";
            string type = "USVoteSample";
            Perceptron perceptron = new Perceptron(16,type);
            int[] learningCurve = perceptron.LearnFrom(type, filename);
            Assert.AreEqual(1000, learningCurve.Length);  // Converge avant la limite
            int discrepancies = perceptron.BatchEvaluate(type, sampleRoot + "USvotes_test.dat");
            Assert.AreEqual(15, discrepancies); // 15/200 = 7.5% d'erreurs
        }

        [Test]
        public void USVotesSaveAndRetreiveLearningTest()
        {
            string filename = sampleRoot + "USvotes_train.dat";
             string type = "USVoteSample";
           Perceptron perceptron = new Perceptron(16,type);
            int[] learningCurve = perceptron.LearnFrom(type, filename);

            // Save and retreive
            string file = sampleRoot + "UnitTest";
            File.Delete(file);
            perceptron.SavePerceptron(file);
            perceptron= new Perceptron(file);
            File.Delete(file);

            int discrepancies = perceptron.BatchEvaluate(type, sampleRoot + "USvotes_test.dat");
            Assert.AreEqual(15, discrepancies); // 15/200 = 7.5% d'erreurs
        }
    }
}