using FileIOAndLINQ.Models;
using FileIOAndLINQ.Services.BusinessLogicLayer;
using FileIOAndLINQ.Services.DataAccessLayer;

namespace FileIOAndLINQ.Tests;

public sealed class VerseLogicTests
{
    [Fact]
    public void CountVerseExpression_CountsSingleAndRanges()
    {
        Assert.Equal(1, VerseLogic.CountVerseExpression("7"));
        Assert.Equal(3, VerseLogic.CountVerseExpression("1-3"));
        Assert.Equal(0, VerseLogic.CountVerseExpression("4-2"));
    }

    [Fact]
    public void LeastAndMostImportant_ReturnCorrectOrder()
    {
        var logic = new VerseLogic(new VerseDAO());
        logic.AddVerse(new VerseRequestModel("John", 3, "16", "A", "A", 10));
        logic.AddVerse(new VerseRequestModel("Genesis", 1, "1-3", "B", "B", 2));
        logic.AddVerse(new VerseRequestModel("Romans", 8, "28", "C", "C", 8));

        Assert.Equal(2, logic.GetLeastImportantVerses(1)[0].Importance);
        Assert.Equal(10, logic.GetMostImportantVerses(1)[0].Importance);
        Assert.Equal(5, logic.GetTotalVerseCount());
    }

    [Fact]
    public void SearchVerses_SearchesDisplayProperties()
    {
        var logic = new VerseLogic(new VerseDAO());
        logic.AddVerse(new VerseRequestModel("Jeremiah", 29, "11", "plans", "hope", 9));
        logic.AddVerse(new VerseRequestModel("Acts", 2, "17", "Spirit", "prophecy", 10));
        Assert.Single(logic.SearchVerses("hope"));
        Assert.Single(logic.SearchVerses("Acts 2:17"));
    }

    [Fact]
    public void TextRoundTrip_PreservesVerseData()
    {
        var dao = new VerseDAO();
        dao.AddVerse(new VerseRequestModel("Psalm", 23, "1", "The Lord is my shepherd", "Trust", 10));
        string path = Path.Combine(Path.GetTempPath(), $"verses-{Guid.NewGuid():N}.txt");
        try
        {
            Assert.StartsWith("Saved", dao.WriteVersesToFile(path));
            var second = new VerseDAO();
            Assert.StartsWith("Loaded", second.ReadVersesFromFile(path));
            Assert.Equal("Psalm", second.GetAllVerses()[0].Book);
        }
        finally { if (File.Exists(path)) File.Delete(path); }
    }
}
