# AI Prompting Documentation

## Potential Oneshot AI Prompt
We could potentially use Claude Code, Cursor, GPT, or Antigravity.
I would likely use Claude Code in VSCode if I wanted to oneshot or try to reach a 90% done solution.

Most of them have a planning mode these days, I would use that to see their plan first and ask for adjustments.

`Prompt:`
```
You are an engineer building a dotnet project from scratch in C#.
Your project is a library that simulates the keypad of old phones with numbers and special controls like backspace and send.
It will take in an input string like "227*#" and return the corresponding output "B".

The full requirements are in TASK.md (or I could paste the requirements in the prompt if not in an IDE context)

ToDo:
-  Make two projects Core and Tests
-  Add a console app project that uses the library in a loop to test it manually.
-  Split the logic into a `Tokenizer` (lexical validation) and an `Interpreter` (message construction).
-  The Tokenizer should convert the input characters into input tokens.
-  The Interpreter should take the input tokens and convert them into the output string.
-  Tokens should be modeled as below, Add the Pause as a ControlKeyToken too
    internal abstract record InputToken;
    internal record DigitToken(Digit Digit) : InputToken;
    internal record ControlKeyToken(ControlKey ControlKey) : InputToken;

   Digit can be Zero -> Nine and ControlKey can be Backspace, Pause, or Pound.
-  Interpreter should be further broken down into two steps
    - A method that takes the input sequence, handles Pause and Backspace, and returns a sequence of DigitAndCount tuples.
    - A method that takes the sequence of DigitAndCount tuples and returns the output string.
-  Use a Stack-based approach in the Interpreter to handle backspacing (`*`) correctly across character boundaries.
-  Make sure to implement character cycling like a real phone.
-  Handle numerical digits (0-9) and basic punctuation on key '1' by including them at the end of character cycles. Research real phones if needed.
-  Use modern C# features like records for the Token cases, switch expressions etc.
-  Make sure to fail on invalid characters on Tokenizer, but input sequence validity like # at the end should be handled by the Interpreter as that is not the Tokenizer's responsibility.
-  Add tests
   - High-level Integration tests for various cases, especially the examples in the task.
   - Granular Unit tests for the Tokenizer and Interpreter.
   - Edge cases like multiple backspaces, redundant backspaces, invalid input strings and anything else you can think of.
-  All internal logic should be private with XML docs. The entry point should be `OldPhone.OldPhonePad(string)` like the task dictates.
-  Try to follow best practices for C# code. But keep in mind that this is a very small library and avoid turning it into Enterprise bloat.
-  Make sure all tests pass.
```

That should likely get me to a working solution as the AI will try to run the tests and fix as needed.
Potential adjustments would probably be names of Methods, the AI writing code in older style,
missed test cases along with unnecessary test cases, Some missed behavior etc

---

## How I Actually Used AI
AI was used in this project mostly as a pair-programming partner to:
- Generate Boilerplate: Rapidly generating models and unit test skeletons.
- Generate Docs: Asked AI to generate XML docs for the code.
- Refine Architecture and Large Scale Changes: Renaming, Moving, Restructring things quickly.
- Brainstroming: Consulting AI for best practice and standard dotnet project structure etc.
- Final Checks: Asked a few different models to criticize the Files and Project strcuture

The AI ultimately didn't choose any business logic, those were all handled by me. The AI played the role of a Critic and a Typist.