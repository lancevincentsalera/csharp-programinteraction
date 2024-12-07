using System.Diagnostics;

namespace ProgramInteraction;

public class MooExecutor
{
    private readonly string _arguments;

    public MooExecutor(string args)
    {
        _arguments = args;
    }

    public void Execute()
    {
        try
        {
            using Process process = new() { StartInfo = GetProcessStartInfo() };
            if (Console.IsInputRedirected) HandleInputRedirection(process);
            else process.Start();
            ReadAndOutputResults(process);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during execution: {ex.Message}");
        }

    }

    private ProcessStartInfo GetProcessStartInfo()
    {
        return new ProcessStartInfo()
        {
            FileName = "cowsay",
            Arguments = _arguments,
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };
    }

    private void HandleInputRedirection(Process process)
    {
        try
        {
            using StreamReader reader = new(Console.OpenStandardInput());
            string input = reader.ReadToEnd();
            if (!string.IsNullOrEmpty(input))
            {
                process.Start();
                process.StandardInput.Write(input);
                process.StandardInput.Close();
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to handle input redirection: {ex.Message}");
        }

    }

    private void ReadAndOutputResults(Process process)
    {
        try
        {
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            process.WaitForExit();

            Console.Write(output);

            if (!string.IsNullOrEmpty(error))
            {
                Console.Error.Write(error);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read and output results: {ex.Message}");
        }
    }
}