using NUnit.Framework;
using Demo3tiers.DAL.AI.Sample;
using Demo3tiers.DAL.TabularData;

namespace Demo3tiers.DAL.Tests
{
    public class TabularDataTests
    {
        static readonly string sampleRoot = @"..\..\..\..\..\Echantillons\";
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void BreastCancerDatTest()
        {
            string filename = sampleRoot + "breastw_train.dat";
            string[][] table = TabularDataReaderFactory.CreateDataReader(filename).Read(filename);
            Assert.AreEqual(343, table.Length);  // Rangées
            Assert.AreEqual(10, table[0].Length); // Colonnes
        }

        [Test]
        public void BreastCancerSampleTest()
        {
            string filename = sampleRoot+"breastw_train.dat";
            AISample[] ts = AISampleFactory.CreateFromFile("BreastCancerSample", filename);
            Assert.AreEqual(343,ts.Length );
        }
        [Test]
        public void DiabeteSampleTest()
        {
            string filename = sampleRoot + "pima_train.dat";
            AISample[] ts = AISampleFactory.CreateFromFile("DiabeteSample", filename);
            Assert.AreEqual(400, ts.Length);
        }
        [Test]
        public void SonarSampleTest()
        {
            string filename = sampleRoot + "sonar_train.dat";
            AISample[] ts = AISampleFactory.CreateFromFile("SonarSample", filename);
            Assert.AreEqual(105, ts.Length);
        }
        [Test]
        public void USVoteSampleTest()
        {
            string filename = sampleRoot + "USVotes_train.dat";
            AISample[] ts = AISampleFactory.CreateFromFile("USVoteSample", filename);
            Assert.AreEqual(235, ts.Length);
        }

        [Test]
        public void BreastCancerCSVTest()
        {
            string filename = sampleRoot + "breastw_test .csv";
            string[][] table = TabularDataReaderFactory.CreateDataReader(filename).Read(filename);
            Assert.AreEqual(340, table.Length);  // Rangées
            Assert.AreEqual(10, table[0].Length); // Colonnes
        }

        [Test]
        public void BreastCancerCSVSampleTest()
        {
            string filename = sampleRoot + "breastw_test .csv";
            AISample[] ts = AISampleFactory.CreateFromFile("BreastCancerSample", filename);
            Assert.AreEqual(340, ts.Length);
        }
    }
}