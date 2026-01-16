namespace RetroPhones.OldPhone.Core;

public static class OldPhone
{
    /// <summary>
    /// Decodes a string of old phone pad key presses into a text.
    /// </summary>
    /// <param name="input">The input string containing sequence of key presses. Must end with '#'.</param>
    /// <returns>The decoded string message.</returns>
    /// <exception cref="ArgumentException">Thrown when input is invalid (null, empty, invalid chars, or missing '#').</exception>
    public static string OldPhonePad(string input)
    {
        var tokens = Tokenizer.Tokenize(input);
        var result = Interpreter.Interpret(tokens);

        return result;
    }
}

