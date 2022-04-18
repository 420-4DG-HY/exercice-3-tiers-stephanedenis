using System.Diagnostics.Contracts;
using System.Reflection;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System;
/// <summary>
/// Démonstration du patron architectural 3 tiers et de plusieurs patrons de conception
/// 
/// (CC) BY-SA Stéphane Denis et Hugo St-Louis, CEGEP de Saint-Hyacinthe
/// </summary>
namespace Perceptron.DAL.AI.Sample.Type
{
    /// <summary>
    /// Données permettants de prédire si un électeur votera Démocrate ou Républicain 
    /// aux élections présidentielles des États-Unis
    /// </summary>
    /*
     * Structure de données d'une des lignes du fichier d'échantillons de données
     * 
     
        Title: 1984 United States Congressional Voting Records Database

        +1 signfie "yea", -1 signifie "nay", 0 signifie "unknown"

        Attribute Information:
        1. Class Name: 2 (democrat, republican) (je l'ai mis comme dernier attribut; 1 signifie "republican"))
        2. handicapped-infants: 2 (y,n)
        3. water-project-cost-sharing: 2 (y,n)
        4. adoption-of-the-budget-resolution: 2 (y,n)
        5. physician-fee-freeze: 2 (y,n)
        6. el-salvador-aid: 2 (y,n)
        7. religious-groups-in-schools: 2 (y,n)
        8. anti-satellite-test-ban: 2 (y,n)
        9. aid-to-nicaraguan-contras: 2 (y,n)
        10. mx-missile: 2 (y,n)
        11. immigration: 2 (y,n)
        12. synfuels-corporation-cutback: 2 (y,n)
        13. education-spending: 2 (y,n)
        14. superfund-right-to-sue: 2 (y,n)
        15. crime: 2 (y,n)
        16. duty-free-exports: 2 (y,n)
        17. export-administration-act-south-africa: 2 (y,n)
    */
    internal class USVoteSample : AISample
    {
        static readonly string[] USVoteSampleAttributeNames = {
            "handicapped-infants",
            "water-project-cost-sharing",
            "adoption-of-the-budget-resolution",
            "physician-fee-freeze",
            "el-salvador-aid",
            "religious-groups-in-schools",
            "anti-satellite-test-ban",
            "aid-to-nicaraguan-contras",
            "mx-missile",
            "immigration",
            "synfuels-corporation-cutback",
            "education-spending",
            "superfund-right-to-sue",
            "crime",
            "duty-free-exports",
            "export-administration-act-south-africa"
        };

        static readonly string[] USVoteSampleResultNames = {
            "democrat",
            "republican"
        };
        public USVoteSample(double[] attributes, int result) : base(attributes, result)
        {
            AttributeNames = USVoteSampleAttributeNames;
            ResultNames = USVoteSampleResultNames;
        }
    }
}
