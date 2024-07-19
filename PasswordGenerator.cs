using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

public class PasswordGenerator
{
    private const string Uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string Lowercase = "abcdefghijklmnopqrstuvwxyz";
    private const string Numbers = "0123456789";
    private const string Symbols = "!@#$%^&*()-_=+[]{}|;:,.<>?";

    public static string Generate(int length, int word, int mayus, int minus, int number, int symbol, int alfa)
    {
        var result = new StringBuilder();

        // Fulfill minimum requirements
        result.Append(GenerateRandomString(Uppercase, Math.Max(mayus, word > 0 ? word - minus : 0)));
        result.Append(GenerateRandomString(Lowercase, Math.Max(minus, word > 0 ? word - mayus : 0)));
        result.Append(GenerateRandomString(Numbers, number));
        result.Append(GenerateRandomString(Symbols, symbol));

        // Fulfill alfa requirement
        var currentAlfa = result.ToString().Count(c => char.IsLetterOrDigit(c));
        if (currentAlfa < alfa)
        {
            result.Append(GenerateRandomString(Uppercase + Lowercase + Numbers, alfa - currentAlfa));
        }

        // Fill the rest with random characters
        var remainingLength = length - result.Length;
        var allChars = Uppercase + Lowercase + Numbers + Symbols;
        result.Append(GenerateRandomString(allChars, remainingLength));

        // Shuffle the result
        return ShuffleString(result.ToString());
    }

    private static string ShuffleString(string str)
    {
        var array = str.ToCharArray();
        var random = RandomNumberGenerator.Create();

        for (int i = array.Length - 1; i > 0; i--)
        {
            var randomByte = new byte[1];
            random.GetBytes(randomByte);
            int j = randomByte[0] % (i + 1);

            (array[i], array[j]) = (array[j], array[i]);
        }

        return new string(array);
    }

    private static string GenerateRandomString(string charSet, int length)
    {
        var result = new StringBuilder();
        var random = RandomNumberGenerator.Create();

        for (int i = 0; i < length; i++)
        {
            var randomByte = new byte[1];
            random.GetBytes(randomByte);
            result.Append(charSet[randomByte[0] % charSet.Length]);
        }

        return result.ToString();
    }
}