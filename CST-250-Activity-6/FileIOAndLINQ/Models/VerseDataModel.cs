// Source: Grand Canyon University. (2025). Activity 6: Building an N-Layer Application With Advanced File I/O, LINQ, and Data Binding [Course activity guide].

namespace FileIOAndLINQ.Models;

/// <summary>Represents one internally stored Bible verse record.</summary>
public sealed class VerseDataModel
{
    public int Id { get; set; }
    public string Book { get; set; }
    public int Chapter { get; set; }
    public string Verse { get; set; }
    public string Text { get; set; }
    public string Meaning { get; set; }
    public int Importance { get; set; }

    public VerseDataModel()
    {
        Id = 0;
        Book = string.Empty;
        Chapter = 0;
        Verse = string.Empty;
        Text = string.Empty;
        Meaning = string.Empty;
        Importance = 0;
    }

    public VerseDataModel(int id, string book, int chapter, string verse, string text, string meaning, int importance)
    {
        Id = id;
        Book = book;
        Chapter = chapter;
        Verse = verse;
        Text = text;
        Meaning = meaning;
        Importance = importance;
    }

    public override string ToString()
    {
        return $"{Book}* {Chapter}* {Verse}* {Text}* {Meaning}* {Importance}";
    }
}
