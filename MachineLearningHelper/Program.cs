using static System.Console;

namespace MachineLearningHelper
{
    class Program
    {
        static void Main(string[] args)
        {
            //MachineLearningHelper.Configuration.Delimiter = "\t";

            MachineLearningHelper.FetchRecords<Record>("sample.csv").CorrelationMatrix().ToConsole(true);

            double[][] data = MachineLearningHelper.FetchRecords<Record>("sample.csv");

            MathNet.Numerics.LinearAlgebra.Matrix<double> matrix = data.CorrelationMatrix();

            matrix.ToConsole();

            ReadKey();
        }
    }
}
