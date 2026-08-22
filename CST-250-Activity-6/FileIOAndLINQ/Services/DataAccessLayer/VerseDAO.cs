// Source: Grand Canyon University. (2025). Activity 6: Building an N-Layer Application With Advanced File I/O, LINQ, and Data Binding [Course activity guide].

using System.Globalization;
using System.Xml.Serialization;
using FileIOAndLINQ.Models;
using OfficeOpenXml;
using ServiceStack.Text;

namespace FileIOAndLINQ.Services.DataAccessLayer;

/// <summary>Owns the verse inventory and all persistence operations.</summary>
public sealed class VerseDAO
{
    private readonly List<VerseDataModel> _verses;

    public VerseDAO()
    {
        _verses = new List<VerseDataModel>();
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
    }

    public int AddVerse(VerseRequestModel request)
    {
        ArgumentNullException.ThrowIfNull(request);
        int id = _verses.Count == 0 ? 1 : _verses.Max(v => v.Id) + 1;
        _verses.Add(new VerseDataModel(id, request.Book, request.Chapter, request.Verse, request.Text, request.Meaning, request.Importance));
        return id;
    }

    public List<VerseDataModel> GetAllVerses()
    {
        return _verses.Select(Clone).ToList();
    }

    public List<VerseDataModel> GetLeastImportantVerses(int numToFind)
    {
        int count = Math.Max(0, numToFind);
        var query = from verse in _verses
                    orderby verse.Importance ascending, verse.Book, verse.Chapter, verse.Verse
                    select verse;
        return query.Take(count).Select(Clone).ToList();
    }

    public List<VerseDataModel> GetMostImportantVerses(int numToFind)
    {
        int count = Math.Max(0, numToFind);
        return _verses
            .OrderByDescending(verse => verse.Importance)
            .ThenBy(verse => verse.Book)
            .ThenBy(verse => verse.Chapter)
            .Take(count)
            .Select(Clone)
            .ToList();
    }

    public string WriteVersesToFile(string fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName))
        {
            return "A file name is required.";
        }

        try
        {
            string extension = Path.GetExtension(fileName).ToLowerInvariant();
            switch (extension)
            {
                case ".txt":
                    File.WriteAllText(fileName, string.Join(Environment.NewLine, _verses.Select(v => v.ToString())));
                    break;
                case ".json":
                    File.WriteAllText(fileName, JsonSerializer.SerializeToString(_verses));
                    break;
                case ".csv":
                    File.WriteAllText(fileName, CsvSerializer.SerializeToString(_verses));
                    break;
                case ".xml":
                    WriteXml(fileName);
                    break;
                case ".xlsx":
                    WriteExcel(fileName);
                    break;
                default:
                    return "File not recognized. Use TXT, JSON, CSV, XML, or XLSX.";
            }

            return $"Saved {_verses.Count} verse record(s) to {Path.GetFileName(fileName)}.";
        }
        catch (Exception ex)
        {
            return $"The file could not be saved: {ex.Message}";
        }
    }

    public string ReadVersesFromFile(string fileName)
    {
        if (!File.Exists(fileName))
        {
            return "The selected file does not exist.";
        }

        try
        {
            List<VerseDataModel> loaded = Path.GetExtension(fileName).ToLowerInvariant() switch
            {
                ".txt" => ReadText(fileName),
                ".json" => JsonSerializer.DeserializeFromString<List<VerseDataModel>>(File.ReadAllText(fileName)) ?? new List<VerseDataModel>(),
                ".csv" => CsvSerializer.DeserializeFromString<List<VerseDataModel>>(File.ReadAllText(fileName)) ?? new List<VerseDataModel>(),
                ".xml" => ReadXml(fileName),
                ".xlsx" => ReadExcel(fileName),
                _ => throw new InvalidDataException("File not recognized. Use TXT, JSON, CSV, XML, or XLSX.")
            };

            _verses.Clear();
            int id = 1;
            foreach (VerseDataModel verse in loaded)
            {
                verse.Id = id++;
                _verses.Add(verse);
            }

            return $"Loaded {_verses.Count} verse record(s) from {Path.GetFileName(fileName)}.";
        }
        catch (Exception ex)
        {
            return $"The file could not be loaded: {ex.Message}";
        }
    }

    public VerseDataModel ConvertTxtToVerseDataModel(string line)
    {
        string[] values = line.Split("* ", StringSplitOptions.None);
        if (values.Length != 6)
        {
            throw new FormatException("A TXT verse record must contain six fields separated by '* '.");
        }

        if (!int.TryParse(values[1], NumberStyles.Integer, CultureInfo.InvariantCulture, out int chapter) ||
            !int.TryParse(values[5], NumberStyles.Integer, CultureInfo.InvariantCulture, out int importance))
        {
            throw new FormatException("TXT chapter and importance values must be whole numbers.");
        }

        return new VerseDataModel(0, values[0].Trim(), chapter, values[2].Trim(), values[3].Trim(), values[4].Trim(), importance);
    }

    private List<VerseDataModel> ReadText(string fileName)
    {
        var result = new List<VerseDataModel>();
        foreach (string line in File.ReadLines(fileName))
        {
            if (!string.IsNullOrWhiteSpace(line))
            {
                result.Add(ConvertTxtToVerseDataModel(line));
            }
        }
        return result;
    }

    private void WriteXml(string fileName)
    {
        var serializer = new XmlSerializer(typeof(List<VerseDataModel>));
        using FileStream stream = File.Create(fileName);
        serializer.Serialize(stream, _verses);
    }

    private static List<VerseDataModel> ReadXml(string fileName)
    {
        var serializer = new XmlSerializer(typeof(List<VerseDataModel>));
        using FileStream stream = File.OpenRead(fileName);
        return serializer.Deserialize(stream) as List<VerseDataModel> ?? new List<VerseDataModel>();
    }

    private void WriteExcel(string fileName)
    {
        using var package = new ExcelPackage();
        var sheet = package.Workbook.Worksheets.Add("Verses");
        string[] headers = { "Id", "Book", "Chapter", "Verse", "Text", "Meaning", "Importance" };
        for (int column = 0; column < headers.Length; column++)
        {
            sheet.Cells[1, column + 1].Value = headers[column];
        }

        for (int row = 0; row < _verses.Count; row++)
        {
            VerseDataModel verse = _verses[row];
            int excelRow = row + 2;
            sheet.Cells[excelRow, 1].Value = verse.Id;
            sheet.Cells[excelRow, 2].Value = verse.Book;
            sheet.Cells[excelRow, 3].Value = verse.Chapter;
            sheet.Cells[excelRow, 4].Value = verse.Verse;
            sheet.Cells[excelRow, 5].Value = verse.Text;
            sheet.Cells[excelRow, 6].Value = verse.Meaning;
            sheet.Cells[excelRow, 7].Value = verse.Importance;
        }

        if (sheet.Dimension is not null)
        {
            sheet.Cells[sheet.Dimension.Address].AutoFitColumns();
        }
        package.SaveAs(new FileInfo(fileName));
    }

    private static List<VerseDataModel> ReadExcel(string fileName)
    {
        using var package = new ExcelPackage(new FileInfo(fileName));
        var sheet = package.Workbook.Worksheets.FirstOrDefault() ?? throw new InvalidDataException("The workbook does not contain a worksheet.");
        var result = new List<VerseDataModel>();
        if (sheet.Dimension is null)
        {
            return result;
        }

        for (int row = 2; row <= sheet.Dimension.End.Row; row++)
        {
            string book = sheet.Cells[row, 2].Text.Trim();
            if (string.IsNullOrWhiteSpace(book))
            {
                continue;
            }
            if (!int.TryParse(sheet.Cells[row, 3].Text, out int chapter) || !int.TryParse(sheet.Cells[row, 7].Text, out int importance))
            {
                continue;
            }
            result.Add(new VerseDataModel(0, book, chapter, sheet.Cells[row, 4].Text.Trim(), sheet.Cells[row, 5].Text, sheet.Cells[row, 6].Text, importance));
        }
        return result;
    }

    private static VerseDataModel Clone(VerseDataModel verse)
    {
        return new VerseDataModel(verse.Id, verse.Book, verse.Chapter, verse.Verse, verse.Text, verse.Meaning, verse.Importance);
    }
}
