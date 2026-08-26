using System.Text.Json;
using MinesweeperClassLibrary.Models;

namespace MinesweeperClassLibrary.DataAccessLayer;

/// <summary>
/// Persists SavedGameModel snapshots as JSON files.
/// </summary>
public sealed class SavedGameRepository
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true
    };

    public void Save(string fileName, SavedGameModel snapshot)
    {
        if (string.IsNullOrWhiteSpace(fileName))
        {
            throw new ArgumentException("A save file name is required.", nameof(fileName));
        }

        ArgumentNullException.ThrowIfNull(snapshot);
        string? directory = Path.GetDirectoryName(fileName);
        if (!string.IsNullOrWhiteSpace(directory))
        {
            Directory.CreateDirectory(directory);
        }

        string json = JsonSerializer.Serialize(snapshot, JsonOptions);
        File.WriteAllText(fileName, json);
    }

    public SavedGameModel Load(string fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName))
        {
            throw new ArgumentException("A save file name is required.", nameof(fileName));
        }

        string json = File.ReadAllText(fileName);
        SavedGameModel? snapshot = JsonSerializer.Deserialize<SavedGameModel>(json, JsonOptions);
        return snapshot ?? throw new InvalidDataException("The selected save file did not contain a valid game snapshot.");
    }
}
