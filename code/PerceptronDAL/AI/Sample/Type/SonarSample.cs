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
namespace Demo3tiers.DAL.AI.Sample.Type
{
    /// <summary>
    /// Signaux de sonars permettant de distinguer les roches des mines
    /// </summary>
    /*
     * Structure de données d'une des lignes du fichier d'échantillons de données
     * 
     
    NAME: Sonar, Mines vs. Rocks

    Each pattern is a set of 60 numbers in the range 0.0 to 1.0.  Each number
    represents the energy within a particular frequency band, integrated over
    a certain period of time.  The integration aperture for higher frequencies
    occur later in time, since these frequencies are transmitted later during
    the chirp.

    @attribute 'attribute_1' real
    @attribute 'attribute_2' real
    @attribute 'attribute_3' real
    ...
    @attribute 'attribute_60' real
    @attribute 'Class' { R, M}

    "R" if the object is a rock and "M" if it is a mine (metal cylinder).
     */
    public class SonarSample : AISample
    {
        public static readonly string[] SonarSampleResultNames = {
            "Rock",
            "Mine"
        };

        public static readonly int SonarSampleAttributesCount = 60;

        public SonarSample(double[] attributes, int result) : base(attributes, result)
        {
            List<string> SonarAttributeNames = new List<string>();
            for (int i = 1; i <= 60; i++)
            {
                SonarAttributeNames.Add("attribute_" + i);
            }
            AttributeNames = SonarAttributeNames.ToArray();
        }
    }
}
