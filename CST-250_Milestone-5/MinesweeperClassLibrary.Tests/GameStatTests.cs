using MinesweeperClassLibrary.BusinessLogicLayer;
using MinesweeperClassLibrary.DataAccessLayer;
using MinesweeperClassLibrary.Models;

namespace MinesweeperClassLibrary.Tests;

public class GameStatTests
{
    [Fact]
    public void GetNextId_ReturnsOneForEmptyList()
    {
        var logic = new GameStatLogic();
        Assert.Equal(1, logic.GetNextId(Array.Empty<GameStat>()));
    }

    [Fact]
    public void GetNextId_UsesMaximumExistingIdPlusOne()
    {
        var logic = new GameStatLogic();
        GameStat[] scores =
        {
            new() { Id = 2 },
            new() { Id = 8 },
            new() { Id = 4 }
        };

        Assert.Equal(9, logic.GetNextId(scores));
    }

    [Fact]
    public void SortByName_OrdersNamesAlphabetically()
    {
        var logic = new GameStatLogic();
        List<GameStat> result = logic.SortByName(CreateScores());

        Assert.Equal(new[] { "Aaron", "Jordan", "Taylor" }, result.Select(score => score.Name));
    }

    [Fact]
    public void SortByScore_OrdersHighestScoreFirst()
    {
        var logic = new GameStatLogic();
        List<GameStat> result = logic.SortByScore(CreateScores());

        Assert.Equal(new[] { 4100, 3200, 2800 }, result.Select(score => score.Score));
    }

    [Fact]
    public void SortByDate_OrdersNewestGameFirst()
    {
        var logic = new GameStatLogic();
        List<GameStat> result = logic.SortByDate(CreateScores());

        Assert.Equal(3, result[0].Id);
        Assert.Equal(1, result[^1].Id);
    }

    [Fact]
    public void Repository_SaveAndLoad_RoundTripsGameStats()
    {
        var repository = new GameStatRepository();
        string filePath = CreateTempPath();

        try
        {
            List<GameStat> expected = CreateScores();
            repository.Save(filePath, expected);
            List<GameStat> actual = repository.Load(filePath);

            Assert.Equal(expected.Count, actual.Count);
            Assert.Equal(expected[0].Id, actual[0].Id);
            Assert.Equal(expected[0].Name, actual[0].Name);
            Assert.Equal(expected[0].Score, actual[0].Score);
            Assert.Equal(expected[0].GameTime, actual[0].GameTime);
        }
        finally
        {
            DeleteIfExists(filePath);
        }
    }

    [Fact]
    public void Repository_Save_SanitizesPipeInWinnerName()
    {
        var repository = new GameStatRepository();
        string filePath = CreateTempPath();

        try
        {
            repository.Save(filePath, new[]
            {
                new GameStat { Id = 1, Name = "Aaron|Test", Score = 1000, GameTime = DateTime.Now }
            });

            List<GameStat> loaded = repository.Load(filePath);
            Assert.Single(loaded);
            Assert.Equal("Aaron/Test", loaded[0].Name);
        }
        finally
        {
            DeleteIfExists(filePath);
        }
    }

    [Fact]
    public void Repository_Load_IgnoresMalformedRows()
    {
        var repository = new GameStatRepository();
        string filePath = CreateTempPath();

        try
        {
            File.WriteAllLines(filePath, new[]
            {
                "1|Aaron|3100|2026-08-12T20:00:00.0000000Z",
                "bad row",
                "2||2200|2026-08-12T20:00:00.0000000Z",
                "3|Jordan|-1|2026-08-12T20:00:00.0000000Z"
            });

            List<GameStat> loaded = repository.Load(filePath);
            Assert.Single(loaded);
            Assert.Equal("Aaron", loaded[0].Name);
        }
        finally
        {
            DeleteIfExists(filePath);
        }
    }

    private static List<GameStat> CreateScores()
    {
        return new List<GameStat>
        {
            new() { Id = 1, Name = "Taylor", Score = 2800, GameTime = new DateTime(2026, 8, 10, 10, 0, 0, DateTimeKind.Local) },
            new() { Id = 2, Name = "Aaron", Score = 4100, GameTime = new DateTime(2026, 8, 11, 10, 0, 0, DateTimeKind.Local) },
            new() { Id = 3, Name = "Jordan", Score = 3200, GameTime = new DateTime(2026, 8, 12, 10, 0, 0, DateTimeKind.Local) }
        };
    }

    private static string CreateTempPath()
    {
        return Path.Combine(Path.GetTempPath(), $"minesweeper-scores-{Guid.NewGuid():N}.txt");
    }

    private static void DeleteIfExists(string filePath)
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }
}
