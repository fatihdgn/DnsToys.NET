using DnsToysNET.Parsers;

namespace DnsToysNET;

public class DefaultDnsToysEntryCompositeParser: DnsToysEntryCompositeParser
{
    public DefaultDnsToysEntryCompositeParser()
    {
        Register(new DnsToysHelpEntryParser());
        Register(new DnsToysTimeEntryParser());
        Register(new DnsToysIpEntryParser());
        Register(new DnsToysFxEntryParser());
        Register(new DnsToysUnitEntryParser());
        Register(new DnsToysWordsEntryParser());
        Register(new DnsToysCIDREntryParser());
        Register(new DnsToysWeatherEntryParser());
        Register(new DnsToysPIEntryParser());
        Register(new DnsToysBaseEntryParser());
    }
}
