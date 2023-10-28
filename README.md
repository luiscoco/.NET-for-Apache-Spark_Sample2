# .NET-for-Apache-Spark_Sample2


## Create a JSON file as the application data input

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


## To run the application

```
C:\DataSourceSparkExample\DataSourceSparkExample\bin\Debug\net7.0>spark-submit ^
More? --class org.apache.spark.deploy.dotnet.DotnetRunner ^
More? --master local ^
More? microsoft-spark-3-2_2.12-2.1.1.jar ^
More? dotnet DataSourceSparkExample.dll C:\DataSourceSparkExample
```
