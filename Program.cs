using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;


Console.WriteLine(">>> Week 1.4 <<<" + args.Length);

var input = new[]{
    "Abcde","Efght","cererw3","79865","qwe33er","Edasdasd sdfsadf asf sadf asf asdf asdf asdf asdf"
};

var iteration = int.TryParse(args.Length > 0 ? args[0] : "1", out int valueExit) ? valueExit : 1;
var showsError = bool.TryParse(args.Length > 1 ? args[1] : "true", out bool printError) ? printError : true;

runProcess(input, new Func<string, string[]>(ValidateWithReg), iteration, showsError);
runProcess(input, new Func<string, string[]>(ValidateWithException), iteration, showsError);
runProcess(input, new Func<string, string[]>(ValidateWithIf), iteration, showsError);


static void runProcess(string[] values, Func<string, string[]> validationObject, int iterations, bool showsError)
{
    if (values == null)
    {
        return;
    }

    var startTime = DateTime.Now;
    var stratMemory = Process.GetCurrentProcess().WorkingSet64;

    for (int iteration = 0; iteration < iterations; iteration++)
    {
        foreach (var id in values)
        {
            var errors = validationObject(id);
            PrintErrors(id, errors, showsError);
        }
    }
    printUsageMemory(validationObject.Method.Name, startTime, stratMemory);
}

static string[] ValidateWithIf(string id)
{
    var errors = new List<string>();
    id = clearId(id);

    if (id.Length < 5 || id.Length > 32)
    {
        errors.Add("A valid ID must have a minimum of 5 characters and a maximun of 32");
    }
    if (id.Length > 0 && !(id[0] >= 'A' && id[0] <= 'Z'))
    {
        errors.Add("A valid ID must start with a capital letter: A-Z");
    }

    return errors.ToArray();
}


static string[] ValidateWithException(string id)
{
    var errors = new List<string>();
    id = clearId(id);

    try
    {
        if (id.Length < 5 || id.Length > 32)
        {
            throw new ApplicationException("A valid ID must have a minimum of 5 characters and a maximun of 32");
        }
    }
    catch (ApplicationException ex)
    {
        errors.Add(ex.Message);
    }

    try
    {
        if (id.Length > 0 && !(id[0] >= 'A' && id[0] >= 'Z'))
        {
            throw new ApplicationException("A valid id must start with a capital letter: A-Z");
        }
    }
    catch (ApplicationException ex)
    {
        errors.Add(ex.Message);
    }

    return errors.ToArray();
}

static string[] ValidateWithReg(string id)
{
    var partRule1 = @"^\w{5,32}$";
    var partRule2 = @"[A-Z]";

    var errors = new List<string>();

    if (!Regex.IsMatch(id = clearId(id), partRule1))
    {
        errors.Add("A valid ID must have a minimum of 5 characters and a maximun of 32");
    }
    if (id.Length > 0 && !Regex.IsMatch(id.Substring(0, 1), partRule2))
    {
        errors.Add("A valid id must start with a capital letter: A-Z");
    }

    return errors.ToArray();
}

static void PrintErrors(string id, string[] errors, bool showError)
{
    if (!showError)
    {
        return;
    }

    var detail = errors?.Length > 0 ? string.Empty : "Ok";

    Console.WriteLine($"Identification {id}, Validaton Result {detail}");
    foreach (var error in errors)
    {
        Console.WriteLine($"* Error {error}");
    }

    Console.WriteLine();

}

static string clearId(string id)
{
    return (id != null && id.Length > 0) ? id : string.Empty;
}

static void printUsageMemory(string methodName, DateTime startTime, long startMemory)
{
    var duration = DateTime.Now.Subtract(startTime).TotalMilliseconds;
    var memoryUsage = (Process.GetCurrentProcess().WorkingSet64 - startMemory) / 1024;

    Console.WriteLine();
    Console.WriteLine($"# Method {methodName} duration {duration} ms, memory usage {memoryUsage} kb");

}

