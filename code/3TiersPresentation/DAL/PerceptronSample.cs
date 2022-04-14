using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3TiersPresentation.DAL
{
    /// <summary>
    /// Échantillon de données pouvant être utilisé de façon homogène dans un perceptron
    /// </summary>
    abstract class PerceptronSample
    {
        protected string[] AttributeNames;
        public string GetAttributeName(int index)
        {
            return AttributeNames[index];
        }
        public int AttributeDomainMaxValue { get; protected set; }
        public int AttributeDomainMinValue { get; protected set; }
        protected int[] Attributes;

        /// <summary>
        /// Permet de connaitre la valeur d'un des attributs
        /// </summary>
        /// <param name="index">position dans le tableau d'attributs</param>
        /// <returns></returns>
        public int GetAttributeValue(int index)
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

        public PerceptronSample(int[] attributes, int result)
        {
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
                if (attribute < AttributeDomainMaxValue || attribute > AttributeDomainMaxValue)
                {
                    throw new ArgumentException("Invalid attribute : " + attribute + " is out of domain values");
                }
            }

            if (Result < ResultDomainMinValue || Result > ResultDomainMaxValue)
            {
                throw new ArgumentException("Invalid result : "+ Result +" is out of domain values");
            }
        }
    }
}
