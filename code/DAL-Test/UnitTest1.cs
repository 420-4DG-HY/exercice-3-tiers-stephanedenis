using NUnit.Framework;
using Perceptron.DAL.AI.Sample;

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
            AISample[] ts = AISampleFactory.CreateFromDatFile("BreastCancerSample", filename);
            Assert.AreEqual(343,ts.Length );

        }
        [Test]
        public void DiabeteSampleTest()
        {
            string filename = sampleRoot + "pima_train.dat";
            AISample[] ts = AISampleFactory.CreateFromDatFile("DiabeteSample", filename);
            Assert.AreEqual(400, ts.Length);

        }
        [Test]
        public void SonarSampleTest()
        {
            string filename = sampleRoot + "sonar_train.dat";
            AISample[] ts = AISampleFactory.CreateFromDatFile("SonarSample", filename);
            Assert.AreEqual(105, ts.Length);

        }
        [Test]
        public void USVoteSampleTest()
        {
            string filename = sampleRoot + "USVotes_train.dat";
            AISample[] ts = AISampleFactory.CreateFromDatFile("USVoteSample", filename);
            Assert.AreEqual(235, ts.Length);

        }
    }
}