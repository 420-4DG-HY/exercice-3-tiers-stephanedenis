using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo3tiers.DAL.TabularData
{
    public class TabularDataReaderFactory
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
