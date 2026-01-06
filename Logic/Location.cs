using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AppointmentScheduler.Logic;

public class Location
{
    public async Task<Dictionary<string, string>> FindUser()
    {
        using var client = new HttpClient();
        string json = await client.GetStringAsync("http://ip-api.com/json/");

        using var doc = JsonDocument.Parse(json);
        var root = doc.RootElement;

        var location = new Dictionary<string, string>
        {
            ["City"] = root.GetProperty("city").GetString(),
            ["RegionName"] = root.GetProperty("regionName").GetString(),
            ["Country"] = root.GetProperty("country").GetString(),
            ["Timezone"] = root.GetProperty("timezone").GetString()
        };

        return location;
    }
}
