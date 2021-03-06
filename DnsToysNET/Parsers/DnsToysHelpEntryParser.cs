using DnsToysNET.Models;

namespace DnsToysNET.Parsers;

public class DnsToysHelpEntryParser : IDnsToysHelpEntryParser
{
    private static char[] SpaceArray = new char[] { ' ' };
    public IDnsToysHelpEntry Parse(string[] rawValue)
    {
        if (rawValue.Length < 2)
            throw new ArgumentException("rawValue must contain at least two elements");
        var description = rawValue[0];
        var sampleRequest = rawValue[1];
        sampleRequest = sampleRequest.Split(SpaceArray, StringSplitOptions.RemoveEmptyEntries).Skip(1).FirstOrDefault() ?? string.Empty;
        return new DnsToysHelpEntry(description, sampleRequest);
    }
}
