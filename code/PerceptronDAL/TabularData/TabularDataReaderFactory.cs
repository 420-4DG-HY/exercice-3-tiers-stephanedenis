using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perceptron.DAL.TabularData
{
    internal class TabularDataReaderFactory
    {
        public static ITabularDataReader CreateDataReader(string fileName)
        {
            if (fileName.EndsWith(".dat"))
            {
                return new DatReader();
            }
            if (fileName.EndsWith(".csv"))
            {
                return new CsvReader();
            }
            throw new InvalidOperationException(
                    "Le nom de fichier doit finir par .dat ou .csv");
        }
    }
}
