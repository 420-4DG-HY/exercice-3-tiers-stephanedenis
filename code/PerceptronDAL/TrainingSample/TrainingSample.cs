/// <summary>
/// Démonstration du patron architectural 3 tiers et de plusieurs patrons de conception
/// 
/// (CC) BY-SA Stéphane Denis et Hugo St-Louis, CEGEP de Saint-Hyacinthe
/// </summary>
namespace Perceptron.DAL.TrainingSample
{
    /// <summary>
    /// Échantillon de données pouvant être utilisé de façon homogène dans un perceptron
    /// </summary>
    public abstract class TrainingSample
    {
        protected string[] AttributeNames;
        public string GetAttributeName(int index)
        {
            return AttributeNames[index];
        }
        public float AttributeDomainMaxValue { get; protected set; }
        public float AttributeDomainMinValue { get; protected set; }
        protected float[] Attributes;

        /// <summary>
        /// Permet de connaitre la valeur d'un des attributs
        /// </summary>
        /// <param name="index">position dans le tableau d'attributs</param>
        /// <returns></returns>
        public float GetAttributeValue(int index)
        {
            return Attributes[index];
        }

        /// <summary>
        /// Permet de connaitre la valeur d'un des attributs
        /// </summary>
        /// <param name="index">position dans le tableau d'attributs</param>
        /// <returns></returns>
        public string GetAttributeNameAndValue(int index)
        {
            return AttributeNames[index]+" : "+Attributes[index];
        }

        protected string[] ResultNames;
        public string GetResultNames(int index)
        {
            return ResultNames[index];
        }

        /// <summary>
        /// Permet de connaitre la taille des données en entrée
        /// </summary>
        /// <returns>nombre d'attributs en entrée du perceptron</returns>
        public int GetAttributeCount()
        {
            return Attributes.Length;
        }

        public int ResultDomainMaxValue { get; protected set; }
        public int ResultDomainMinValue { get; protected set; }
        int Result;

        public TrainingSample(float[] attributes, int result)
        {
            AttributeNames = new string[0];
            ResultNames = new string[0];
            Attributes = attributes;
            Result = result;
        }

        protected void validateData()
        {
            if (Attributes.Length != AttributeNames.Length)
            {
                throw new ArgumentException("Invalid attributes number");
            }

            foreach (var attribute in Attributes)
            {
                if (attribute < AttributeDomainMinValue || attribute > AttributeDomainMaxValue)
                {
                    throw new ArgumentException("Invalid attribute : out of domain values ("+ AttributeDomainMinValue + " <= " + Result + " <= " + AttributeDomainMaxValue+")");
                }
            }

            if (Result < ResultDomainMinValue || Result > ResultDomainMaxValue)
            {
                throw new ArgumentException("Invalid result : out of domain values (" + ResultDomainMinValue + " <= " + Result + " <= "+ ResultDomainMaxValue + ")");
            }
        }
    }
}
