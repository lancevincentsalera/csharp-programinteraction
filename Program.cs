namespace ProgramInteraction;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            string arguments = args.Length > 0 ? string.Join(" ", args) : string.Empty;
            MooExecutor moo = new(arguments);
            moo.Execute();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}