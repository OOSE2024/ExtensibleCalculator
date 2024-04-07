using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensibilityAPI {
    public interface IOperation {
        string Name { get; }
        double Calculate(double a, double b);
    }
}
