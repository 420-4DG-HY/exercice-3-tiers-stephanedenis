using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows;

namespace _3TiersPresentation.DAL
{
    /// <summary>
    /// Échantillon de cellules potentiellement cancéreuses
    /// L'échantillon contient 9 caractéristiques observées (Attributes) et le diagnostic (Result) associé.
    /// </summary>
    /*
     * Structure de données d'une des lignes du fichier d'échantillons de données
     * 
     * La première colonne est une référence à un numéro d'éprouvette (colonne retirée dans l'exemple fourni par Hugo)
     * Les 9 autres sont des caractéristiques observées en laboratoire
     * La dernière colonne contient le diagnostic résultant de l'observation des 9 caractéristiques précédentes
     * 
           #  Attribute                     Domain
           -- -----------------------------------------
           1. Sample code number            id number (cet attribut a été enlevé des fichiers .dat)
           2. Clump Thickness               1 - 10
           3. Uniformity of Cell Size       1 - 10
           4. Uniformity of Cell Shape      1 - 10
           5. Marginal Adhesion             1 - 10
           6. Single Epithelial Cell Size   1 - 10
           7. Bare Nuclei                   1 - 10
           8. Bland Chromatin               1 - 10
           9. Normal Nucleoli               1 - 10
          10. Mitoses                       1 - 10
          11. Class:                        (2 for benign, 4 for malignant)

     */
    internal class BreastCancerSample : PerceptronSample
    {
        static readonly string[] BreastCancerSampleAttributeNames = {
           "Clump Thickness",
           "Uniformity of Cell Size",
           "Uniformity of Cell Shape",
           "Marginal Adhesion",
           "Single Epithelial Cell Size",
           "Bare Nuclei",
           "Bland Chromatin",
           "Normal Nucleoli",
           "Mitoses"
        };

        static readonly string[] BreastCancerSampleResultNames = {
           "Benign",
           "Malignant"
        };

        public BreastCancerSample(int[] attributes, int result) : base(attributes, result)
        {
            AttributeNames = BreastCancerSampleAttributeNames;
            ResultNames = BreastCancerSampleResultNames;
            AttributeDomainMaxValue = 10;
            AttributeDomainMinValue = 1;
            ResultDomainMaxValue = 4;
            ResultDomainMinValue = 2;

            validateData();
            if (result == 3) // Validation extra pour données discontinues
            {
                throw new ArgumentException("Invalid result value");
            }
        }

    }
}
