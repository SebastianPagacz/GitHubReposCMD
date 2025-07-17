using GitHubReposCMD.Models;
using Newtonsoft.Json;

namespace GitHubReposCMD;

internal class Program
{
    static async Task Main(string[] args)
    {
        Console.Write("Type in github username: ");
        var username = Console.ReadLine();
        string url = $"https://api.github.com/users/{username}/repos";
        using (HttpClient client = new HttpClient())
        {
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.UserAgent.TryParseAdd("request");

            var result = await client.GetAsync(url);
            var json = await result.Content.ReadAsStringAsync();
            var deserializedData = JsonConvert.DeserializeObject<List<Request>>(json);
            var finalData = deserializedData.Where(d => d.Forks_Count == 0).ToList();

            for (int i = 0; i < finalData.Count; i++) 
            {
                Console.WriteLine($"{finalData[i].Name} : {finalData[i].Forks_Count}");
            }
        }
    }
}
