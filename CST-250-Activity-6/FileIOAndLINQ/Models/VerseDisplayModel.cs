// Source: Grand Canyon University. (2025). Activity 6: Building an N-Layer Application With Advanced File I/O, LINQ, and Data Binding [Course activity guide].

namespace FileIOAndLINQ.Models;

/// <summary>Contains display-ready data sent back to the presentation layer.</summary>
public sealed class VerseDisplayModel
{
    public string Reference { get; set; }
    public string Text { get; set; }
    public string Meaning { get; set; }
    public int Importance { get; set; }

    public VerseDisplayModel()
    {
        Reference = string.Empty;
        Text = string.Empty;
        Meaning = string.Empty;
        Importance = 0;
    }

    public VerseDisplayModel(string reference, string text, string meaning, int importance)
    {
        Reference = reference;
        Text = text;
        Meaning = meaning;
        Importance = importance;
    }
}
