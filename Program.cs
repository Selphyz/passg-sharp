using System;
using System.CommandLine;

class Program
{
    static async Task<int> Main(string[] args)
    {
        var rootCommand = new RootCommand("Password generator CLI application");

        var copyOption = new Option<bool>(
            ["--copy", "-c"],
            description: "Copy the generated password to clipboard");

        var lengthOption = new Option<int>(
            ["--long", "-l"],
            getDefaultValue: () => 10,
            description: "Specify the total length of the password");

        var wordOption = new Option<int>(
            ["--word", "-w"],
            getDefaultValue: () => 0,
            description: "Minimum number of words (letters)");

        var mayusOption = new Option<int>(
            ["--mayus", "-M"],
            getDefaultValue: () => 0,
            description: "Minimum number of uppercase letters");

        var minusOption = new Option<int>(
            ["--minus", "-m"],
            getDefaultValue: () => 0,
            description: "Minimum number of lowercase letters");

        var numberOption = new Option<int>(
            ["--number", "-n"],
            getDefaultValue: () => 0,
            description: "Minimum number of numeric characters");

        var symbolOption = new Option<int>(
            ["--symbol", "-s"],
            getDefaultValue: () => 0,
            description: "Minimum number of symbol characters");

        var alfaOption = new Option<int>(
            ["--alfa", "-a"],
            getDefaultValue: () => 0,
            description: "Minimum number of alphanumeric characters");

        rootCommand.AddOption(copyOption);
        rootCommand.AddOption(lengthOption);
        rootCommand.AddOption(wordOption);
        rootCommand.AddOption(mayusOption);
        rootCommand.AddOption(minusOption);
        rootCommand.AddOption(numberOption);
        rootCommand.AddOption(symbolOption);
        rootCommand.AddOption(alfaOption);

        rootCommand.SetHandler((bool copy, int length, int word, int mayus, int minus, int number, int symbol, int alfa) =>
        {
            var password = PasswordGenerator.Generate(length, word, mayus, minus, number, symbol, alfa);
            Console.WriteLine($"Generated password: {password}");

            if (copy)
            {
                Console.WriteLine("Password copied to clipboard!");
            }
        }, copyOption, lengthOption, wordOption, mayusOption, minusOption, numberOption, symbolOption, alfaOption);

        return await rootCommand.InvokeAsync(args);
    }
}