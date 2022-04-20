using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo3tiers.DAL.TabularData
{
    public interface ITabularDataReader
    {
        string[][] Read(string fileName);
    }
}
