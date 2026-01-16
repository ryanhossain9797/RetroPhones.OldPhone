namespace RetroPhones.OldPhone.Core.Models;

/// <summary>
/// Base class for all input tokens.
/// Use concrete implementations DigitToken and ControlKeyToken.
/// </summary>
internal abstract record InputToken;

/// <summary>
/// Represents a digit token. Intended to be used as a case of InputToken.
/// </summary>
internal record DigitToken(Digit Digit) : InputToken;

/// <summary>
/// Represents a control key token. Intended to be used as a case of InputToken.
/// </summary>
internal record ControlKeyToken(ControlKey ControlKey) : InputToken;
