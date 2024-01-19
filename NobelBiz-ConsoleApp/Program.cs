using System.Configuration;
using System.Text.Json;
using NobelBiz_ConsoleApp.Models;

class Program
{
    static async Task Main()
    {
        try
        {
            string ApiUrl = ConfigurationManager.AppSettings[nameof(ApiUrl)]!.ToString();

            List<string> topics = await FetchAndExtractTopicsAsync(ApiUrl);

            Console.WriteLine("Unique Topics:\n");
            topics.ForEach(Console.WriteLine);
            Console.WriteLine("---------------");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    private static async Task<List<string>> FetchAndExtractTopicsAsync(string apiUrl)
    {
        if (string.IsNullOrWhiteSpace(apiUrl)) throw new ArgumentNullException(nameof(apiUrl));

        using HttpClient client = new();
        string jsonResponse = await client.GetStringAsync(apiUrl);

        if (string.IsNullOrWhiteSpace(jsonResponse)) throw new ArgumentNullException(nameof(jsonResponse));

        return ExtractTopics(jsonResponse);
    }

    private static List<string> ExtractTopics(string jsonData)
    {
        var codingResourceList = JsonSerializer.Deserialize<List<CodingResourceResponse>>(jsonData);

        if (!codingResourceList!.Any()) throw new ArgumentNullException(nameof(codingResourceList));

        return codingResourceList!.SelectMany(x => x.Topics).DistinctBy(x => x).ToList();
    }
}
