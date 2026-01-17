using System;

namespace RetroPhones.OldPhone.App
{
    public class App
    {
        public void Run()
        {
            Console.WriteLine("Enter text to convert to Old Phone Pad sequence. Type 'exit' to quit.");

            while (true)
            {
                Console.Write("> ");
                var input = Console.ReadLine();

                if (input?.ToLower() == "exit")
                {
                    break;
                }

                if (!string.IsNullOrEmpty(input))
                {
                    try
                    {
                        var output = OldPhonePad(input);
                        Console.WriteLine(output);
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
            }
        }
    }
}
