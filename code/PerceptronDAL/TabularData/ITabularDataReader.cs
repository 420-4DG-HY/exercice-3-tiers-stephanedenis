using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perceptron.DAL.TabularData
{
    public interface ITabularDataReader
    {
        string[][] Read(string fileName);
    }
}
