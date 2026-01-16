using RetroPhones.OldPhone.Core;

namespace RetroPhones.OldPhone.Tests;

public class OldPhonePadTests
{
    [Theory]
    [InlineData("33#", "E")]
    [InlineData("227*#", "B")]
    [InlineData("4433555 555666#", "HELLO")]
    [InlineData("222 2 22#", "CAB")]
    [InlineData("111 2 1111#", "(A)")]
    public void OldPhonePad_ReturnsCorrectOutput_ForValidInput(string input, string expected)
    {
        var result = Core.OldPhone.OldPhonePad(input);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("8 88777444666*664#", "TURING")]
    public void OldPhonePad_HandlesComplexSequences(string input, string expected)
    {
        var result = Core.OldPhone.OldPhonePad(input);
        Assert.Equal(expected, result);
    }
}
