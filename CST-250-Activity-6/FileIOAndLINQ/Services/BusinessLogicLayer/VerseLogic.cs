// Source: Grand Canyon University. (2025). Activity 6: Building an N-Layer Application With Advanced File I/O, LINQ, and Data Binding [Course activity guide].

using FileIOAndLINQ.Models;
using FileIOAndLINQ.Services.DataAccessLayer;

namespace FileIOAndLINQ.Services.BusinessLogicLayer;

/// <summary>Coordinates verse rules between the presentation layer and DAO.</summary>
public sealed class VerseLogic
{
    private readonly VerseDAO _verseDAO;

    public VerseLogic() : this(new VerseDAO()) { }

    public VerseLogic(VerseDAO verseDAO)
    {
        _verseDAO = verseDAO ?? throw new ArgumentNullException(nameof(verseDAO));
    }

    public int AddVerse(VerseRequestModel request)
    {
        ValidateRequest(request);
        return _verseDAO.AddVerse(request);
    }

    public List<VerseDisplayModel> GetAllVerses()
    {
        return ConvertVerseDataToDisplay(_verseDAO.GetAllVerses());
    }

    public string WriteInventoryToFile(string fileName)
    {
        return _verseDAO.WriteVersesToFile(fileName);
    }

    public string ReadVersesFromFile(string fileName)
    {
        return _verseDAO.ReadVersesFromFile(fileName);
    }

    public VerseDataModel ConvertTxtToVerseDataModel(string line)
    {
        return _verseDAO.ConvertTxtToVerseDataModel(line);
    }

    public List<VerseDisplayModel> GetLeastImportantVerses(int numToFind)
    {
        return ConvertVerseDataToDisplay(_verseDAO.GetLeastImportantVerses(numToFind));
    }

    public List<VerseDisplayModel> GetMostImportantVerses(int numToFind)
    {
        return ConvertVerseDataToDisplay(_verseDAO.GetMostImportantVerses(numToFind));
    }

    public List<VerseDisplayModel> ConvertVerseDataToDisplay(List<VerseDataModel> dataVerses)
    {
        return dataVerses.Select(verse => new VerseDisplayModel(
            $"{verse.Book} {verse.Chapter}:{verse.Verse}",
            verse.Text,
            verse.Meaning,
            verse.Importance)).ToList();
    }

    public List<VerseDisplayModel> SearchVerses(string searchText)
    {
        IEnumerable<VerseDisplayModel> verses = GetAllVerses();
        string term = searchText?.Trim() ?? string.Empty;
        if (term.Length == 0)
        {
            return verses.ToList();
        }

        return verses.Where(verse =>
            verse.Reference.Contains(term, StringComparison.OrdinalIgnoreCase) ||
            verse.Text.Contains(term, StringComparison.OrdinalIgnoreCase) ||
            verse.Meaning.Contains(term, StringComparison.OrdinalIgnoreCase) ||
            verse.Importance.ToString().Contains(term, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public int GetTotalVerseCount()
    {
        int total = 0;
        foreach (VerseDataModel verse in _verseDAO.GetAllVerses())
        {
            total += CountVerseExpression(verse.Verse);
        }
        return total;
    }

    public static int CountVerseExpression(string verseExpression)
    {
        if (string.IsNullOrWhiteSpace(verseExpression))
        {
            return 0;
        }

        string[] parts = verseExpression.Split('-', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length == 1 && int.TryParse(parts[0], out int single) && single > 0)
        {
            return 1;
        }

        if (parts.Length == 2 && int.TryParse(parts[0], out int start) && int.TryParse(parts[1], out int end) && start > 0 && end >= start)
        {
            return end - start + 1;
        }

        return 0;
    }

    private static void ValidateRequest(VerseRequestModel request)
    {
        ArgumentNullException.ThrowIfNull(request);
        if (string.IsNullOrWhiteSpace(request.Book)) throw new ArgumentException("Book is required.", nameof(request));
        if (request.Chapter <= 0) throw new ArgumentException("Chapter must be positive.", nameof(request));
        if (CountVerseExpression(request.Verse) == 0) throw new ArgumentException("Verse must be a positive number or ascending range.", nameof(request));
        if (string.IsNullOrWhiteSpace(request.Text)) throw new ArgumentException("Verse text is required.", nameof(request));
        if (string.IsNullOrWhiteSpace(request.Meaning)) throw new ArgumentException("Meaning is required.", nameof(request));
        if (request.Importance is < 1 or > 10) throw new ArgumentException("Importance must be between 1 and 10.", nameof(request));
    }
}
