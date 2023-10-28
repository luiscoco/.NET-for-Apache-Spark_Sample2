# .NET-for-Apache-Spark_Sample2

## 0. Prerrequisites

See the prerrequisites to run a .NET Spark application in the repository:

https://github.com/luiscoco/.NET-for-Apache-Spark_Sample1

## 1. Create a new .NET C# console application

Run Visual Studio and create a new C# consolo application.

Load Nuget package **Microsoft.Spark**

![image](https://github.com/luiscoco/.NET-for-Apache-Spark_Sample2/assets/32194879/b769293f-37de-49cc-b00d-6ec9ad3f6494)

## 2. Application source code

```csharp
using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Spark.Sql;

namespace DataSourceSparkExample
{
    /// <summary>
    /// The example is taken/modified from spark/examples/src/main/python/sql/datasource.py
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.Error.WriteLine(
                    "Usage: Datasource <path to SPARK_HOME/examples/src/main/resources/>");

                Environment.Exit(1);
            }

            Console.WriteLine("Argumento: " + args[0]);

            string json = Path.Combine(args[0], "people.json");

            SparkSession spark = SparkSession
                .Builder()
                .AppName("SQL Datasource example using .NET for Apache Spark")
                .GetOrCreate();

            RunParquetExample(spark, json);

            spark.Stop();
        }

        static private void RunParquetExample(SparkSession spark, string json)
        {
            DataFrame peopleDf = spark.Read().Json(json);

            peopleDf.Write().Mode(SaveMode.Overwrite).Parquet("people.parquet");

            DataFrame parquetFile = spark.Read().Parquet("people.parquet");

            parquetFile.CreateTempView("parquet");

            DataFrame teenagers = spark.Sql(
                "SELECT name FROM parquet WHERE age >= 13 and age <= 30");

            teenagers.Show();
        }
    }
}
```

## 3. Create a JSON file as the application data input

```JSON
[
    {"name": "John", "age": 25},
    {"name": "Jane", "age": 18},
    {"name": "Bob", "age": 16},
    {"name": "Alice", "age": 22},
    {"name": "Charlie", "age": 17},
    {"name": "Diana", "age": 20}
]
```

Place the people.json file in the application folder

![image](https://github.com/luiscoco/.NET-for-Apache-Spark_Sample2/assets/32194879/81fd106b-2d32-42ce-81dc-448e52f7e568)

## 4. To run the application

```
C:\DataSourceSparkExample\DataSourceSparkExample\bin\Debug\net7.0>spark-submit ^
More? --class org.apache.spark.deploy.dotnet.DotnetRunner ^
More? --master local ^
More? microsoft-spark-3-2_2.12-2.1.1.jar ^
More? dotnet DataSourceSparkExample.dll C:\DataSourceSparkExample
```
