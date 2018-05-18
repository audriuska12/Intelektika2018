using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNet.NeuralNet
{
    class Donor
    {
        public int Year { get; set; }
        public int OperationYear { get; set; }
        public int AxillaryNodes { get; set; }
        public int Status { get; set; }

        public Donor(int year, int operationYear, int axillaryNodes, int status)
        {
            Year = year;
            OperationYear = operationYear;
            AxillaryNodes = axillaryNodes;
            Status = status;
        }
    }
}
