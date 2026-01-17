# Old Phone Pad Simulation

## Overview

This project is a static library that converts sequences of phone keypad presses (digits `0-9`, backspace `*` and send `#`) into their intended text messages. Pressing the same button multiple times cycles through its assigned characters. A delay will commit the current character.

### Key Features
- Realistic Multi-tap: Includes support for characters, digits, punctuation.
- Architectural Separation: Divided into Tokenization and Interpretation layers for maintainability.
- Strict Modeling: Uses rigid Enums, abstract classes, and concrete children to model the keypad layout and prevent invalid states.

---

## Architecture

Built with compiler like terminology:

1.  Tokenizer: performs lexical analysis, converting the raw `string` into a sequence of `InputToken` objects (Digits or Special characters). 
2.  Interpreter: performs syntactic analysis. It consolidates repeating digit tokens into character assignments, handles the "pause" (space) behavior, and applies backspace operations using a stack.
3.  Models/DigitMapping: Centralizes the keypad layout. It utilizes `FrozenDictionary` for high-performance, immutable character lookups.

---

## Building

This project is built with the latest .net 10 SDK.

From the root directory:
```bash
dotnet build
```

---

## Testing

The project includes a test suite using xUnit:

- Integration Tests: In `PhonePad.OldPhonePad`, Tests against a variety of examples.
- Unit Tests:
  - `TokenizerTests`: Validates lexical rules and individual character parsing.
  - `InterpreterTests`: Validates complex logic like character cycling, redundant backspaces, and structural requirements.

From the root directory:
```bash
dotnet test
```

---

## Console App

A simple console application is included to demonstrate the library's functionality. It provides a REPL (Read-Eval-Print Loop) to interact with the `OldPhonePad` converter.

### How to Run
From the root directory:
```bash
dotnet run --project RetroPhones.OldPhone.App
```

---

## Engineering Standards

- Encapsulation: Core logic and models are marked as `internal` to prevent leaking implementation details to library consumers.
- Single Responsibility Principle: The separation between `Tokenizer` and `Interpreter` ensures each class has one clear job.
- Robust Error Handling: Descriptive `ArgumentException` messages for null/empty inputs, invalid characters, or structural errors.
- Efficiency: Uses `StringBuilder` for message construction and `FrozenDictionary` for static character mappings.

---

# Design Decisions
- Why are no Interfaces provided or any DI used?

  Based on the nature of the task and the static entrypoint in the Task Prompt,
  It's rational to assume this is a static library, so providing an interface felt redundant.

  Similarly the console app is meant to minimally demonstrate usage and does not rely on any DI.

- Why model Buttons/Characters as Enums?
  
  It may seem a little overkill for the simple use case, but the code was written assuming it's part of a larger library
  If so, having Type safe representations of the Phone's KeyPad (InputToken), and it's subcategories like Digit and ControlKey
  Will prove more valuable in the long run.
  
- Why `abstract InputToken` with `DigitToken` and `ControlKeyToken` as children?

  It's a replication of F#/rust style dsicriminated unions
  ```fsharp
  type InputToken =
  | DigitToken of Digit
  | ControlKeyToken of ControlKey
  ```
  It allows us to have contexts where both `Digit`s and `ControlKey`s are valid like Tokenizer output,
  while restricting to only `Digit` in contexts like `DigitMapping` or `DigitAndCount`.
  We could have used a common enum and runtime validation to ensure ControlKeys don't leak into places where only Digits are valid, but this implementation is simple enough and type safe.

- Why Tokenizer and Interpreter terminology?

  While they may seem a bit academic for this use case... alternative terms like Convert, Parse, Process etc seemed too open ended.
  Tokenizer and Interpreter are more accurate terms for the two stages of the process.
---

# Assumptions

For many ambiguous cases, we have a source of truth.
Since we are simulating a real device... we can refer to those for behavior not provided in the example.

Assumed behaviors:
- I have chosen to treat multiple 'space' characters in the input as `VALID`, like `"2   22#"`. It may be arguable that it's an invalid input. But I have chosen to ignore it for this simulation.
- `"2 2#1 11#"` could be interpreted as two seperate inputs `"2 2#"` and `"1 11#"`. But based on the output signature in the task I have treated it as `INVALID` input.

Behaviors derived from real life old phone:
- The `1` key has many more punctuation characters than pictured.
- All keys have the digit itself after the pictured characters.
- The input cycles back to the first character after the last character.
- Pressing backspace even when all characters are gone is `VALID` and does nothing. It does not throw an error.