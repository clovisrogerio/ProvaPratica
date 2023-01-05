using Newtonsoft.Json.Linq;
using Questao2;

public class Program
{
    public static void Main()
    {
        string teamName = "Paris Saint-Germain";
        int year = 2013;
        int totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }

    public static int getTotalScoredGoals(string team, int year)
    {
        var client = new HttpClient();
        var totalPages = GetTotalPages(client, team, year);

        var homeMatchesData = new List<FootballMatchesData>();
        var awayMatchesData = new List<FootballMatchesData>();

        for (int i = 1; i <= totalPages; i++)
        {
            homeMatchesData.AddRange(GetFootballMatchesDatas("team1", i, team, year, client));
            awayMatchesData.AddRange(GetFootballMatchesDatas("team2", i, team, year, client));
        }

        var homeMatchesGoals = homeMatchesData.Sum(x => x.Team1Goals);
        var awayMatchesGoals = awayMatchesData.Sum(x => x.Team2Goals);

        return homeMatchesGoals + awayMatchesGoals;
    }

    private static List<FootballMatchesData> GetFootballMatchesDatas(string team, int pageNumber, string teamName, int year, HttpClient httpClient)
    {
        var urlString = new Uri($"https://jsonmock.hackerrank.com/api/football_matches?year={year}&{team}={teamName}&page={pageNumber}");
        var response = httpClient.GetAsync(urlString).Result;
        if (response.IsSuccessStatusCode)
        {
            var responseData = response.Content.ReadAsStringAsync().Result;
            JObject result = JObject.Parse(responseData);
            var matchesArray = result["data"].Value<JArray>();
            return matchesArray.ToObject<List<FootballMatchesData>>();
        }
        return new List<FootballMatchesData>();
    }

    private static int GetTotalPages(HttpClient httpClient, string teamName, int year)
    {
        var totalPages = 0;
        var urlString = new Uri($"https://jsonmock.hackerrank.com/api/football_matches?year={year}&team1={teamName}");
        var response = httpClient.GetAsync(urlString).Result;

        if (response.IsSuccessStatusCode)
        {
            var responseData = response.Content.ReadAsStringAsync().Result;
            JObject result = JObject.Parse(responseData);
            totalPages = result["total_pages"].Value<int>();
        }

        return totalPages;
    }

}