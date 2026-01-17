using System.Collections.Frozen;

namespace RetroPhones.OldPhone.Core.Models;

/// <summary>
/// Provides mappings and extension methods for converting keypad digits into characters.
/// </summary>
internal static class DigitExtensions
{
    /// <summary>
    /// The canonical mapping of digits to their respective character sequences.
    /// sequences include digits at the end to support realistic multi-tap behavior.
    /// </summary>
    private static readonly FrozenDictionary<Digit, char[]> CharsByDigit = new Dictionary<Digit, char[]>
    {
        { Digit.One,   ['&', '\'', '(', ')', '.', ',', '?', '!', '1'] },
        { Digit.Two,   ['A', 'B', 'C', '2'] },
        { Digit.Three, ['D', 'E', 'F', '3'] },
        { Digit.Four,  ['G', 'H', 'I', '4'] },
        { Digit.Five,  ['J', 'K', 'L', '5'] },
        { Digit.Six,   ['M', 'N', 'O', '6'] },
        { Digit.Seven, ['P', 'Q', 'R', 'S', '7'] },
        { Digit.Eight, ['T', 'U', 'V', '8'] },
        { Digit.Nine,  ['W', 'X', 'Y', 'Z', '9'] },
        { Digit.Zero,  [' ', '0'] }
    }.ToFrozenDictionary();

    /// <summary>
    /// Converts a digit and its repetition count into the corresponding character.
    /// </summary>
    /// <param name="digit">The keypad digit.</param>
    /// <param name="count">The number of times the digit was consecutively pressed.</param>
    /// <returns>The resolved character.</returns>
    internal static char ToKeypadChar(this Digit digit, int count)
    {
        var charsOfDigit = CharsByDigit[digit];
        return charsOfDigit[(count - 1) % charsOfDigit.Length];
    }
}
