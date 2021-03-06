using DnsClient;
using DnsToysNET.Models;
using System.Globalization;
using System.Net;

namespace DnsToysNET;

public class DnsToys : IDnsToys
{
    private readonly IDnsToysRawRequester _requester;
    private readonly IDnsToysEntryCompositeParser _compositeParser;

    public DnsToys(string? host = null, IDnsToysEntryCompositeParser? compositeParser = null) : this(new DnsToysRawRequester(host), compositeParser) { }
    public DnsToys(IPAddress hostIpAddress, IDnsToysEntryCompositeParser? compositeParser = null) : this(new DnsToysRawRequester(hostIpAddress), compositeParser) { }
    public DnsToys(ILookupClient client, IDnsToysEntryCompositeParser? compositeParser = null) : this(new DnsToysRawRequester(client), compositeParser) { }
    public DnsToys(IDnsToysRawRequester requester, IDnsToysEntryCompositeParser? compositeParser = null)
    {
        _requester = requester ?? throw new ArgumentNullException(nameof(requester));
        _compositeParser = compositeParser ?? new DefaultDnsToysEntryCompositeParser();
    }

    protected async Task<IEnumerable<TIDnsToysEntry>> GetAsync<TIDnsToysEntry>(string command)
        where TIDnsToysEntry : IDnsToysEntry
    {
        var response = await _requester.RequestAsync(command);
        return response.Select(_compositeParser.Parse<TIDnsToysEntry>);
    }

    protected async Task<TIDnsToysEntry> GetFirstAsync<TIDnsToysEntry>(string command)
        where TIDnsToysEntry : IDnsToysEntry
    => (await GetAsync<TIDnsToysEntry>(command)).First();

    private const string HelpRequest = "help";
    public async Task<IEnumerable<IDnsToysHelpEntry>> HelpAsync() => await GetAsync<IDnsToysHelpEntry>(HelpRequest);

    private const string TimeRequestFormat = "{0}.time";
    public async Task<IDnsToysTimeEntry> TimeAsync(string city) => await GetFirstAsync<IDnsToysTimeEntry>(string.Format(CultureInfo.InvariantCulture, TimeRequestFormat, city));

    private const string IpRequest = "ip";
    public async Task<IDnsToysIpEntry> IpAsync() => await GetFirstAsync<IDnsToysIpEntry>(IpRequest);

    private const string FxRequestFormat = "{0}{1}-{2}.fx";
    public async Task<IDnsToysFxEntry> FxAsync(double rate, string fromCurrencyCode, string toCurrencyCode) => await GetFirstAsync<IDnsToysFxEntry>(string.Format(CultureInfo.InvariantCulture, FxRequestFormat, rate, fromCurrencyCode, toCurrencyCode));

    private const string UnitRequestFormat = "{0}{1}-{2}.unit";
    public async Task<IDnsToysUnitEntry> UnitAsync(double unit, string fromSymbol, string toSymbol) => await GetFirstAsync<IDnsToysUnitEntry>(string.Format(CultureInfo.InvariantCulture, UnitRequestFormat, unit, fromSymbol, toSymbol));

    private const string WordsRequestFormat = "{0}.words";
    public async Task<IDnsToysWordsEntry> WordsAsync(int numbers) => await GetFirstAsync<IDnsToysWordsEntry>(string.Format(CultureInfo.InvariantCulture, WordsRequestFormat, numbers));

    private const string CIDRRequestFormat = "{0}.cidr";
    public async Task<IDnsToysCIDREntry> CIDRAsync(string cidr) => await GetFirstAsync<IDnsToysCIDREntry>(string.Format(CultureInfo.InvariantCulture, CIDRRequestFormat, cidr));
    public async Task<IDnsToysCIDREntry> CIDRAsync(string ipAddress, byte bits) => await CIDRAsync($"{ipAddress}/{bits}");

    private const string WeatherRequestFormat = "{0}.weather";
    public async Task<IEnumerable<IDnsToysWeatherEntry>> WeatherAsync(string city) => await GetAsync<IDnsToysWeatherEntry>(string.Format(CultureInfo.InvariantCulture, WeatherRequestFormat, city));

    private const string PIRequest = "pi";
    public async Task<IDnsToysPIEntry> PIAsync() => await GetFirstAsync<IDnsToysPIEntry>(PIRequest);

    private const string BaseRequestFormat = "{0}{1}-{2}.base";
    public async Task<IDnsToysBaseEntry> BaseAsync(double value, string fromBase, string toBase) => await GetFirstAsync<IDnsToysBaseEntry>(string.Format(CultureInfo.InvariantCulture, BaseRequestFormat, value, fromBase, toBase));
}