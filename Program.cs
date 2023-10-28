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
                "SELECT name FROM parquet WHERE age >= 13 and age <= 19");

            teenagers.Show();
        }
    }
}