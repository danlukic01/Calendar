using System.Text.Json;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Calendar.Api.Models;

namespace Calendar.Api.Services;

public class AflFixtureService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<AflFixtureService> _logger;

    public AflFixtureService(HttpClient httpClient, ILogger<AflFixtureService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<List<AflGame>> GetCurrentRoundAsync()
    {
        var games = new List<AflGame>();
        try
        {
            var year = DateTime.UtcNow.Year;
            var url = $"https://api.squiggle.com.au/?q=games;year={year};next=1";
            using var res = await _httpClient.GetAsync(url);
            res.EnsureSuccessStatusCode();
            using var stream = await res.Content.ReadAsStreamAsync();
            using var doc = await JsonDocument.ParseAsync(stream);
            if (doc.RootElement.TryGetProperty("games", out var gamesEl))
            {
                var minRound = gamesEl.EnumerateArray()
                    .Select(g => g.GetProperty("round").GetInt32())
                    .Min();
                foreach (var g in gamesEl.EnumerateArray())
                {
                    if (g.GetProperty("round").GetInt32() == minRound)
                    {
                        games.Add(new AflGame
                        {
                            HomeTeam = g.GetProperty("hteam").GetString() ?? g.GetProperty("homeTeam").GetString() ?? string.Empty,
                            AwayTeam = g.GetProperty("ateam").GetString() ?? g.GetProperty("awayTeam").GetString() ?? string.Empty,
                            Date = DateTime.Parse(g.GetProperty("date").GetString() ?? string.Empty),
                            Round = minRound
                        });
                    }
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to fetch AFL fixtures");
        }
        return games;
    }
}
