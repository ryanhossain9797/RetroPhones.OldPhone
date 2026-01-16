namespace RetroPhones.OldPhone.Core.Models;

/// <summary>
/// Represents control key input tokens on the phone keypad.
/// Control keys perform operations rather than produce characters.
/// </summary>
internal enum ControlKey
{
    /// <summary>
    /// Backspace operation, triggered by '*' key.
    /// </summary>
    Backspace,

    /// <summary>
    /// Delay/pause operation, triggered by ' ' (space character).
    /// Used to separate consecutive presses of the same digit.
    /// </summary>
    Delay,

    /// <summary>
    /// Send operation, triggered by '#' key.
    /// Marks the end of input and triggers message submission.
    /// Should only appear at the end of the input.
    /// </summary>
    Send
}
