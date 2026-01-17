using System.Text;
using RetroPhones.OldPhone.Core.Models;

namespace RetroPhones.OldPhone.Core;

/// <summary>
/// Interprets sequences of keypad inputs into final texts.
/// </summary>
internal static class Interpreter
{
    /// <summary>
    /// Processes a list of tokens and maps them to their final string representation.
    /// </summary>
    /// <param name="tokens">The list of tokens to interpret.</param>
    /// <returns>The resulting decoded message.</returns>
    internal static string Interpret(List<InputToken> tokens)
    {
        var digitsAndCounts = ProcessTokens(tokens);
        var sb = new StringBuilder(digitsAndCounts.Count);

        foreach (var entry in digitsAndCounts)
        {
            sb.Append(entry.Digit.ToKeypadChar(entry.Count));
        }

        return sb.ToString();
    }

    /// <summary>
    /// Consolidates raw tokens into repetition counts and also handles special tokens.
    /// </summary>
    /// <param name="tokens">The list of raw input tokens.</param>
    /// <returns>A list of digit sequences and their repetition counts.</returns>
    /// <exception cref="ArgumentException">Thrown if '#' is not the terminal token or if it's completely missing.</exception>
    private static List<DigitAndCount> ProcessTokens(List<InputToken> tokens)
    {
        var results = new Stack<DigitAndCount>();
        DigitAndCount? current = null;

        void Commit()
        {
            if (current != null)
            {
                results.Push(current);
            }
            current = null;
        }

        foreach (var (i, token) in tokens.Index())
        {
            switch (token)
            {
                case DigitToken(Digit digit):
                    if (current != null && digit == current.Digit)
                    {
                        current = current with { Count = current.Count + 1 };
                    }
                    else
                    {
                        Commit();
                        current = new DigitAndCount(digit, 1);
                    }
                    break;

                case ControlKeyToken(ControlKey controlKey):
                    switch (controlKey)
                    {
                        case ControlKey.Delay:
                            Commit();
                            break;

                        case ControlKey.Backspace:
                            Commit();
                            if (results.Count > 0)
                            {
                                results.Pop();
                            }
                            break;

                        case ControlKey.Send:
                            if (i != tokens.Count - 1)
                            {
                                throw new ArgumentException("Input must contain '#' only at the end.", nameof(tokens));
                            }
                            Commit();
                            return [.. results.Reverse()];
                    }
                    break;
            }
        }

        throw new ArgumentException("Input must end with '#' character.", nameof(tokens));
    }
}
