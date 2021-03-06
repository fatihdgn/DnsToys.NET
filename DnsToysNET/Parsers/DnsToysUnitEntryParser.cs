using DnsToysNET.Models;
using System.Globalization;
using System.Text.RegularExpressions;

namespace DnsToysNET.Parsers;

public class DnsToysUnitEntryParser : IDnsToysUnitEntryParser
{
    private readonly static Regex ConversionParser = new Regex(@"(?'value'\d+[.]\d+)\s(?'unit'\w+)\s[(](?'symbol'\w+)[)]\s[=]\s(?'convertedValue'\d+[.]\d+)\s(?'convertedUnit'\w+)\s[(](?'convertedSymbol'\w+)[)]", RegexOptions.Compiled | RegexOptions.Singleline);
    public IDnsToysUnitEntry Parse(string[] rawValue)
    {
        if (rawValue.Length < 1)
            throw new ArgumentException("rawValue must contain at least two elements");
        var conversion = rawValue[0];
        var match = ConversionParser.Match(conversion);
        if (match is null || !match.Success) throw new InvalidDataException("Conversion value doesn't match the format");

        string value = match!.Groups![nameof(value)]?.Value ?? string.Empty;
        string unit = match!.Groups![nameof(unit)]?.Value ?? string.Empty;
        string symbol = match!.Groups![nameof(symbol)]?.Value ?? string.Empty;
        string convertedValue = match!.Groups![nameof(convertedValue)]?.Value ?? string.Empty;
        string convertedUnit = match!.Groups![nameof(convertedUnit)]?.Value ?? string.Empty;
        string convertedSymbol = match!.Groups![nameof(convertedSymbol)]?.Value ?? string.Empty;


        double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var dValue);
        double.TryParse(convertedValue, NumberStyles.Any, CultureInfo.InvariantCulture, out var dConvertedValue);
        return new DnsToysUnitEntry(unit, symbol, dValue, convertedUnit, convertedSymbol, dConvertedValue);
    }
}
