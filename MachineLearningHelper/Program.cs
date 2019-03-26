using static System.Console;

namespace MachineLearningHelper
{
    class Program
    {
        static void Main(string[] args)
        {
            //MachineLearningHelper.Configuration.Delimiter = "\t";

            MachineLearningHelper.FetchRecords<Record>("sample.csv").CorrelationMatrix().ToConsole();
            MachineLearningHelper.FetchRecords<Record>("sample.csv").CorrelationMatrix().ToConsole();

            ReadKey();
        }
    }
}
