using System.Text;
using System.Text.Json;
using SolarApp.Configs;
using SolarApp.Entities;

namespace SolarApp.Services;

public partial class IrradiationService
{
    private readonly IConfiguration _configuration;
    private readonly ContextConfig _contextConfig;

    public IrradiationService(IConfiguration configuration, ContextConfig contextConfig)
    {
        _configuration = configuration;
        _contextConfig = contextConfig;
    }

    static string NormalizeString(string input)
    {
        return input.Replace("á", "a").Replace("â", "a").Replace("ã", "a").Replace("é", "e").Replace("ê", "e").Replace("í", "i").Replace("ó", "o").Replace("ô", "o").Replace("õ", "o").Replace("ú", "u").Replace(" ", "").Replace(" ' ", "").ToLowerInvariant();
    }

    private Task<string> DefineProjectUrlRequest(string billCity)
    {
        var url = _configuration["IrradiationApi:url"];
        var city = NormalizeString(billCity);
        return Task.FromResult($"{url}/irradiation/{city}");
    }

    public async Task<CityIrradiation?> GetCityIrradiation(Project project)
    {
        var urlRequest = await DefineProjectUrlRequest(project.Location!.City!);

        var client = new HttpClient();
        HttpResponseMessage response;

        try
        {
            response = await client.GetAsync(urlRequest);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }

        var jsonResponse = await response.Content.ReadAsStreamAsync();
        var cityIrradiation = await JsonSerializer.DeserializeAsync<CityIrradiation>(jsonResponse, _jsonOptions);

        return cityIrradiation;
    }

    private readonly JsonSerializerOptions _jsonOptions =
            new() { PropertyNameCaseInsensitive = true };
}

