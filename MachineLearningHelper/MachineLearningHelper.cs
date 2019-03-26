using CsvHelper;
using CsvHelper.Configuration;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.Statistics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using static System.Console;

namespace MachineLearningHelper
{
    public static class MachineLearningHelper
    {
        public enum Algorithm
        {
            Spearman,
            Pearson
        }

        /// <summary>
        /// Csv configuration (compatible with CsvHelper.Configuration).
        /// </summary>
        public static Configuration Configuration { get; set; }

        static MachineLearningHelper()
        {
            Configuration = new Configuration() { HasHeaderRecord = true };
        }

        /// <summary>
        /// Fetches an enumeration of records from csv file.
        /// </summary>
        /// <typeparam name="T">Type of record.</typeparam>
        /// <param name="csvFile">Full path to csv file.</param>
        /// <returns>Returns an enumeration of records of type T.</returns>
        public static double[][] FetchRecords<T>(string csvFile) where T : new()
        {
            IList<T> records;

            using (TextReader reader = File.OpenText(csvFile))
            {
                using (var csv = new CsvReader(reader, Configuration))
                {
                    records = csv.GetRecords<T>().ToList();
                    Header = csv.Context.HeaderRecord;
                }
            }

            T record = new T();
            Type t = record.GetType();
            PropertyInfo[] props = t.GetProperties();

            var a = records.Select(r => (double)props[0].GetValue(r)).ToArray();
            var b = records.Select(r => (double)props[1].GetValue(r)).ToArray();
            var c = records.Select(r => (double)props[2].GetValue(r)).ToArray();
            var d = records.Select(r => (double)props[3].GetValue(r)).ToArray();

            return new double[][] { a, b, c, d };
        }

        /// <summary>
        /// Header data property as arrays of strings.
        /// </summary>
        public static string[] Header { get; set; }

        /// <summary>
        /// Builds a correlation matrix for an array of columns
        /// </summary>
        /// <param name="columns">Array of columns for correlation</param>
        /// <param name="algorithm">Algorithm enumeration of Pearson and Spearman (default)</param>
        /// <returns>Returns the correlation matrix</returns>
        public static Matrix<double> CorrelationMatrix(this double[][] columns, Algorithm algorithm = Algorithm.Spearman)
        {
            switch (algorithm)
            {
                case Algorithm.Pearson:
                    return Correlation.PearsonMatrix(columns);

                case Algorithm.Spearman:
                    return Correlation.SpearmanMatrix(columns);

                default:
                    return Correlation.SpearmanMatrix(columns);
            }
        }

        /// <summary>
        /// Prints to console the correlation matrix.
        /// </summary>
        /// <exception cref="ArgumentNullException">Header property must be set. Call FetchRecords first.</exception>
        /// <param name="matrix">Correlation matrix.</param>
        /// <param name="showLegend">Show legend.</param>
        public static void ToConsole(this Matrix<double> matrix, bool showLegend = false)
        {
            Write("C-MATRIX");

            for (int i = 0; i < Header.Length - 1; i++)
            {
                Write($"{Header[i].Substring(0, Math.Min(Header[i].Length, 8)),8}");
            }

            for (int i = 0; i < matrix.ColumnCount; i++)
            {
                WriteLine();
                Write($"{Header[i].Substring(0, Math.Min(Header[i].Length, 8)),8}");

                for (int j = 0; j < matrix.RowCount; j++)
                {
                    switch (matrix[i, j])
                    {
                        case double n when (n >= -1 && n < -0.8):
                            ForegroundColor = ConsoleColor.Red;
                            break;

                        case double n when (n >= -0.8 && n < -0.6):
                            ForegroundColor = ConsoleColor.Yellow;
                            break;

                        case double n when (n >= -0.6 && n < -0.4):
                            ForegroundColor = ConsoleColor.Green;
                            break;

                        case double n when (n >= -0.4 && n < -0.2):
                            ForegroundColor = ConsoleColor.Blue;
                            break;

                        case double n when (n >= -0.2 && n < 0):
                            ForegroundColor = ConsoleColor.Gray;
                            break;

                        case double n when (n >= 0 && n < 0.2):
                            ForegroundColor = ConsoleColor.Gray;
                            BackgroundColor = ConsoleColor.Black;
                            break;

                        case double n when (n >= 0.2 && n < 0.4):
                            ForegroundColor = ConsoleColor.Blue;
                            BackgroundColor = ConsoleColor.DarkBlue;
                            break;

                        case double n when (n >= 0.4 && n < 0.6):
                            ForegroundColor = ConsoleColor.Green;
                            BackgroundColor = ConsoleColor.DarkGreen;
                            break;

                        case double n when (n >= 0.6 && n < 0.8):
                            ForegroundColor = ConsoleColor.Yellow;
                            BackgroundColor = ConsoleColor.DarkYellow;
                            break;

                        case double n when (n >= 0.8 && n < 1):
                            ForegroundColor = ConsoleColor.Red;
                            BackgroundColor = ConsoleColor.DarkRed;
                            break;
                    }

                    if (i == j)
                    {
                        ForegroundColor = ConsoleColor.DarkGray;
                        BackgroundColor = ConsoleColor.DarkGray;
                    }

                    Write($"{matrix[i, j],8:F2}");
                    ResetColor();
                }
            }

            WriteLine();

            if (showLegend)
            {
                WriteLine(Environment.NewLine + "Legend:");

                ForegroundColor = ConsoleColor.Gray;
                BackgroundColor = ConsoleColor.Black;
                Write("████████");
                ResetColor();
                WriteLine(" 0.0 : 0.2");

                ForegroundColor = ConsoleColor.DarkBlue;
                BackgroundColor = ConsoleColor.Black;
                Write("████████");
                ResetColor();
                WriteLine(" 0.2 : 0.4");

                ForegroundColor = ConsoleColor.DarkGreen;
                BackgroundColor = ConsoleColor.Black;
                Write("████████");
                ResetColor();
                WriteLine(" 0.4 : 0.6");

                ForegroundColor = ConsoleColor.DarkYellow;
                BackgroundColor = ConsoleColor.Black;
                Write("████████");
                ResetColor();
                WriteLine(" 0.6 : 0.8");

                ForegroundColor = ConsoleColor.DarkRed;
                BackgroundColor = ConsoleColor.Black;
                Write("████████");
                ResetColor();
                WriteLine(" 0.8 : 1.0");
            }

            WriteLine();
        }
    }
}
