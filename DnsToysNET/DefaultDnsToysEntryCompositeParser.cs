﻿using DnsToysNET.Parsers;

namespace DnsToysNET;

public class DefaultDnsToysEntryCompositeParser: DnsToysEntryCompositeParser
{
    public DefaultDnsToysEntryCompositeParser()
    {
        Register(new DnsToysHelpEntryParser());
        Register(new DnsToysTimeEntryParser());
    }
}
