# TestResultsAnalysisApp
This code provides functionality to parse JSON data containing an array of test cases, each with properties Name, Status, ExecutionTime, and Timestamp. It can then export it to a CSV file and calculate metrics and display them in the console.
The Name is expected to be a string representing the name of the test case.
The Status is expected to be either "pass" or "fail".
The ExecutionTime is expected to be a number representing the execution time in seconds.
The Timestamp is expected to be a string representing a date and time in the format "yyyy-MM-ddTHH:mm:ss".

To run this program, you need to pass two command line arguments: The path of the input JSON file and the path of the output CSV file. For example: 
```
dotnet run .\mockData.json .\output.csv
```
This will read the JSON data from mockData.json, convert it to CSV, export the CSV data to output.csv, and display the metrics in the console.
A working .NET development enviroment is required to run this program.

This code uses the Newtonsoft.Json library for JSON parsing.

This code uses .NET version 7.0 wich can be obtained from microsoft's official site:
https://dotnet.microsoft.com/en-us/download
