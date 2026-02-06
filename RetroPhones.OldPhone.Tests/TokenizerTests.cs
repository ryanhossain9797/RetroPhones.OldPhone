using RetroPhones.OldPhone.Core;
using RetroPhones.OldPhone.Core.Models;

namespace RetroPhones.OldPhone.Tests;

public class TokenizerTests
{
    [Fact]
    public void Tokenize_ParsesDigitsCorrecty()
    {
        var tokens = Tokenizer.Tokenize("2#");
        Assert.Equal(Digit.Two, ((InputToken.DigitToken)tokens[0]).Digit);
    }

    [Fact]
    public void Tokenize_ParsesControlKeysCorrectly()
    {
        var tokens = Tokenizer.Tokenize("* #");
        Assert.Equal(ControlKey.Backspace, ((InputToken.ControlKeyToken)tokens[0]).ControlKey);
        Assert.Equal(ControlKey.Delay, ((InputToken.ControlKeyToken)tokens[1]).ControlKey);
        Assert.Equal(ControlKey.Send, ((InputToken.ControlKeyToken)tokens[2]).ControlKey);
    }

    [Fact]
    public void Tokenize_ReturnsEmpty_ForEmptyInput()
    {
        var tokens = Tokenizer.Tokenize("");
        Assert.Empty(tokens);
    }

    [Fact]
    public void Tokenize_ThrowsException_ForNullInput()
    {
        Assert.Throws<ArgumentException>(() => Tokenizer.Tokenize(null!));
    }

    [Fact]
    public void Tokenize_ThrowsException_ForInvalidCharacters()
    {
        Assert.Throws<ArgumentException>(() => Tokenizer.Tokenize("22A#"));
    }
}
