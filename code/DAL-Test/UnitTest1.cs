using NUnit.Framework;
using Perceptron.DAL.TrainingSample;

namespace DAL_Test
{
    public class Tests
    {
        static readonly string sampleRoot = @"..\..\..\..\..\Echantillons\";
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void BreastCancerSampleTest()
        {
            string filename = sampleRoot+"breastw_train.dat";
            TrainingSample[] ts = TrainingSampleFactory.CreateFromDatFile("BreastCancerSample", filename);
            Assert.AreEqual(343,ts.Length );

        }
        [Test]
        public void DiabeteSampleTest()
        {
            string filename = sampleRoot + "pima_train.dat";
            TrainingSample[] ts = TrainingSampleFactory.CreateFromDatFile("DiabeteSample", filename);
            Assert.AreEqual(400, ts.Length);

        }
        [Test]
        public void SonarSampleTest()
        {
            string filename = sampleRoot + "sonar_train.dat";
            TrainingSample[] ts = TrainingSampleFactory.CreateFromDatFile("SonarSample", filename);
            Assert.AreEqual(105, ts.Length);

        }
        [Test]
        public void USVoteSampleTest()
        {
            string filename = sampleRoot + "USVotes_train.dat";
            TrainingSample[] ts = TrainingSampleFactory.CreateFromDatFile("USVoteSample", filename);
            Assert.AreEqual(235, ts.Length);

        }
    }
}