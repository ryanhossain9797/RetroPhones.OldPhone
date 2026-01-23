using RetroPhones.OldPhone.Core.Models;

namespace RetroPhones.OldPhone.Core;

/// <summary>
/// Responsible for Lexical analysis of the keypad input string.
/// </summary>
internal static class Tokenizer
{
    /// <summary>
    /// Performs basic validation to ensure all characters are valid keypad buttons.
    /// </summary>
    /// <param name="input">The raw input string.</param>
    /// <exception cref="ArgumentException">Thrown if input is null, or contains invalid characters.</exception>
    private static void Validate(string input)
    {
        if (input is null)
        {
            throw new ArgumentException("Input cannot be null.", nameof(input));
        }

        if (input.Any(c => !char.IsDigit(c) && c != '*' && c != '#' && c != ' '))
        {
            throw new ArgumentException("Input contains invalid character.", nameof(input));
        }
    }

    /// <summary>
    /// Maps a single input character to its corresponding InputToken.
    /// </summary>
    private static InputToken ParseToToken(char c)
    {
        return c switch
        {
            var d when char.IsDigit(d) => new DigitToken((Digit)(d - '0')),
            '*' => new ControlKeyToken(ControlKey.Backspace),
            ' ' => new ControlKeyToken(ControlKey.Delay),
            '#' => new ControlKeyToken(ControlKey.Send),
            _ => throw new ArgumentException($"Invalid character: {c}") // Should not reach since already validated
        };
    }

    /// <summary>
    /// Tokenizes the raw input string into a list of InputTokens.
    /// </summary>
    /// <param name="input">The raw input string.</param>
    /// <returns>A list of resolved InputTokens.</returns>
    /// <exception cref="ArgumentException">Thrown if validation fails.</exception>
    internal static List<InputToken> Tokenize(string input)
    {
        Validate(input);

        return [.. input.Select(ParseToToken)];
    }
}
