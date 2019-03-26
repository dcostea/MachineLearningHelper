# MachineLearningHelper

### Description

> A correlation matrix is a table showing correlation coefficients between variables.
> Each cell in the table shows the correlation between two variables.
> A correlation matrix is used as a way to summarize data, as an input into a more advanced analysis, and as a diagnostic for advanced analyses.

### Dependencies

MathNet.Numerics nuget package.
CsvHelper nuget package.

### How to use

Load data from csv file.
```
double[][] data = MachineLearningHelper.FetchRecords<T>("path/to/csv/file");
```

Calculate correlation matrix.
```sh
MathNet.Numerics.LinearAlgebra.Matrix<double> matrix = data.CorrelationMatrix();
```

Print to console the correlation matrix.
```sh
matrix.ToConsole();
```

Compact form.
```sh
MachineLearningHelper.FetchRecords<T>("path/to/csv/file").CorrelationMatrix().ToConsole();
```

### Example of use
```sh
MachineLearningHelper.FetchRecords<Record>("sample.csv").CorrelationMatrix().ToConsole();
```
where Record is a class for csv deserialization

```sh
class Record
{
    public double Lux { get; set; }
    public double Temp { get; set; }
    public double Infra { get; set; }
    public double Dist { get; set; }
    public string State { get; set; }
}
```
    
and csv file looks like

```sh
Lux,Temp,Infra,Dist,State
32.5,7.71,0,400,time
33.82,7.32,0,100,lighter
34.82,7.71,0,100,infra
32.02,7.91,0,400,infra
32.82,7.52,0,400,time
```

### ToDos
Exmplain optional params
