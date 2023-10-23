using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;


/*
This code provides functionality to parse JSON data containing an array of test cases, each with properties Name, Status, ExecutionTime, and Timestamp. It can then export it to a CSV file and calculate metrics and display them in the console.
The Name is expected to be a string representing the name of the test case.
The Status is expected to be either "pass" or "fail".
The ExecutionTime is expected to be a number representing the execution time in seconds.
The Timestamp is expected to be a string representing a date and time in the format "yyyy-MM-ddTHH:mm:ss".

This code uses the Newtonsoft.Json library for JSON parsing.
*/
namespace testResultAnalysis{
    
public class TestCase
{
    private string _name = "DataError";
    private string _status = "DataError";
    private double _executionTime = -1;
    private DateTime _timestamp = DateTime.MinValue;

    public string Name
    {
        get { return _name ?? "DataError"; }
        set { _name = value; }
    }

    public string Status
    {
        get { return _status ?? "DataError"; }
        set { _status = value; }
    }

    public double ExecutionTime
    {
        get { return _executionTime; }
        set { _executionTime = value < 0 ? 0 : value; }
    }

    public DateTime Timestamp
    {
        get { return _timestamp; }
        set { _timestamp = value; }
    }
}
public class TestResult
{
    public List<TestCase> TestCases { get; set; }

    public TestResult()
    {
        TestCases = new List<TestCase>();
    }
    /*
    Parses the specified JSON data into a list of test cases.
    @param json: The JSON data to parse.
    */
    public void ParseJson(string json)
    {
        TestCases = JsonConvert.DeserializeObject<List<TestCase>>(json) ?? new List<TestCase>();
    }
    /*
    Exports the list of test cases to a CSV file at the specified file path.
    @param filePath: The file path to export the CSV file to.
    */
    public void ExportToCsv(string filePath)
    {
        var csv = new List<string> { "Name,Status,ExecutionTime,Timestamp" };
        csv.AddRange(TestCases.Select(tc => $"{tc.Name},{tc.Status},{tc.ExecutionTime},{tc.Timestamp}"));
        File.WriteAllLines(filePath, csv);
    }
    /*
    Calculates and returns the metrics for the list of test cases.
    @return The metrics for the list of test cases.
    */
    public void CalculateMetrics()
    {
        Console.WriteLine($"Total number of test cases executed: {TestCases.Count}.");
        Console.WriteLine($"Number of test cases passed: {TestCases.Count(tc => tc.Status == "pass")}.");
        Console.WriteLine($"Number of test cases failed: {TestCases.Count(tc => tc.Status == "fail")}.");
        Console.WriteLine($"Average execution time for all test cases: {TestCases.Average(tc => tc.ExecutionTime)} seconds.");
        Console.WriteLine($"Minimum execution time among all test cases: {TestCases.Min(tc => tc.ExecutionTime)} seconds.");
        Console.WriteLine($"Maximum execution time among all test cases: {TestCases.Max(tc => tc.ExecutionTime)} seconds.");
    }
}


/*
To run this program, you need to pass two command line arguments: The path of the input JSON file and the path of the output CSV file. For example: "dotnet run .\mockData.json .\output.csv"
This will read the JSON data from mockData.json, convert it to CSV, export the CSV data to output.csv, and display the metrics in the console.
A working .NET development enviroment is required to run this program.
*/
class Program
{
    static void Main(string[] args)
    {
        if (args.Length != 2)
        {
            Console.WriteLine("Usage: testResultAnalysis <inputJsonFilePath> <outputCsvFilePath>");
            return;
        }

        var inputJsonFilePath = args[0];
        var outputCsvFilePath = args[1];

        if (!File.Exists(inputJsonFilePath))
        {
            Console.WriteLine($"File not found: {inputJsonFilePath}");
            return;
        }

        var json = File.ReadAllText(inputJsonFilePath);

        var testResult = new TestResult();
        testResult.ParseJson(json);
        testResult.ExportToCsv(outputCsvFilePath);
        testResult.CalculateMetrics();
    }
}

}