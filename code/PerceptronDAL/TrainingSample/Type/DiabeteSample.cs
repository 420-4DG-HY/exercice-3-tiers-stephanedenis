using Perceptron.DAL.TrainingSample;

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
namespace Perceptron.DAL.TrainingSample.Type
{
    /// <summary>
    /// Données permettant de prédire un diabète à partir d'une étude sur des autochtones
    /// </summary>
    /*
     * Structure de données d'une des lignes du fichier d'échantillons de données
     * 
     
    Title: Pima Indians Diabetes Database

    Attribute: (all numeric-valued)

    1. Number of times pregnant
    2. Plasma glucose concentration a 2 hours in an oral glucose tolerance test
    3. Diastolic blood pressure (mm Hg)
    4. Triceps skin fold thickness (mm)
    5. 2-Hour serum insulin (mu U/ml)
    6. Body mass index (weight in kg/(height in m)^2)
    7. Diabetes pedigree function
    8. Age (years)
    9. Class variable (0 or 1)

    class value 1 is interpreted as "tested positive for diabetes"

    */
    internal class DiabeteSample : TrainingSample
    {
        static readonly string[] DiabeteSampleAttributeNames = {
            "Number of times pregnant",
            "Plasma glucose concentration a 2 hours in an oral glucose tolerance test",
            "Diastolic blood pressure (mm Hg)",
            "Triceps skin fold thickness (mm)",
            "2-Hour serum insulin (mu U/ml)",
            "Body mass index (weight in kg/(height in m)^2)",
            "Diabetes pedigree function",
            "Age"
        };

        static readonly string[] DiabeteSampleResultNames = {
            "tested NEGATIVE for diabetes",
            "tested POSITIVE for diabetes"
        };

        public DiabeteSample(float[] attributes, int result) : base(attributes, result)
        {
            AttributeNames = DiabeteSampleAttributeNames;
            ResultNames = DiabeteSampleResultNames;
            AttributeDomainMaxValue = 1000;
            AttributeDomainMinValue = 0;
            ResultDomainMaxValue = 1;
            ResultDomainMinValue = 0;

            validateData();
        }
    }
}
