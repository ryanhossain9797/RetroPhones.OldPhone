using RetroPhones.OldPhone.Core;

namespace RetroPhones.OldPhone.Tests;

public class InterpreterTests
{
    [Fact]
    public void Interpret_ConvertsTokensToString()
    {
        var tokens = Tokenizer.Tokenize("22#");
        var result = Interpreter.Interpret(tokens);
        Assert.Equal("B", result);
    }

    [Fact]
    public void Interpret_HandlesSpacesAsSeparators()
    {
        var tokens = Tokenizer.Tokenize("2 2#");
        var result = Interpreter.Interpret(tokens);
        Assert.Equal("AA", result);
    }

    [Fact]
    public void Interpret_HandlesBackspaces()
    {
        var tokens = Tokenizer.Tokenize("22*3#");
        var result = Interpreter.Interpret(tokens);
        Assert.Equal("D", result); // "BB" -> backspace -> empty -> "D"
    }

    [Fact]
    public void Interpret_HandlesCycling()
    {
        var tokens = Tokenizer.Tokenize("22222#");
        Assert.Equal("A", Interpreter.Interpret(tokens));
    }

    [Fact]
    public void Interpret_HandlesMultipleBackspaces()
    {
        Assert.Equal("", Interpreter.Interpret(Tokenizer.Tokenize("22 22**#")));
    }

    [Fact]
    public void Interpret_HandlesRedundantBackspaces()
    {
        Assert.Equal("", Interpreter.Interpret(Tokenizer.Tokenize("22 33***#")));
        Assert.Equal("", Interpreter.Interpret(Tokenizer.Tokenize("******#")));
    }

    [Fact]
    public void Interpret_ThrowsException_IfSendIsNotLast()
    {
        var tokens = Tokenizer.Tokenize("#2");
        var ex = Assert.Throws<ArgumentException>(() => Interpreter.Interpret(tokens));
        Assert.Contains("'#' only at the end", ex.Message);
    }

    [Fact]
    public void Interpret_HandlesRedundantDelays()
    {
        Assert.Equal("", Interpreter.Interpret(Tokenizer.Tokenize("    #")));
        Assert.Equal("", Interpreter.Interpret(Tokenizer.Tokenize("  * #")));
    }

    [Fact]
    public void Interpret_ThrowsException_IfNoSendToken()
    {
        var tokens = Tokenizer.Tokenize("2");
        var ex = Assert.Throws<ArgumentException>(() => Interpreter.Interpret(tokens));
        Assert.Contains("must end with '#'", ex.Message);
    }
}
