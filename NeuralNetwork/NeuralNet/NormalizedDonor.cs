using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNet.NeuralNet
{
    class NormalizedDonor
    {

        public double Year { get; set; }
        public double OperationYear { get; set; }
        public double AxillaryNodes { get; set; }
        public double Status { get; set; }
        public NormalizedDonor(double year, double operationYear, double axillaryNodes, double status)
        {
            Year = year;
            OperationYear = operationYear;
            AxillaryNodes = axillaryNodes;
            Status = status;
        }
    }
}
