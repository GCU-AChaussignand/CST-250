namespace MinesweeperClassLibrary.Models;

/// <summary>
/// Abstract visual theme contract. The class library stores colors as hex values so
/// presentation code can interpret them without coupling the business layer to WinForms.
/// </summary>
public abstract class GameTheme
{
    protected GameTheme(
        string name,
        string windowBackgroundHex,
        string panelBackgroundHex,
        string coveredCellHex,
        string revealedCellHex,
        string accentHex,
        string flagHex,
        string bombHex,
        string textHex)
    {
        Name = name;
        WindowBackgroundHex = windowBackgroundHex;
        PanelBackgroundHex = panelBackgroundHex;
        CoveredCellHex = coveredCellHex;
        RevealedCellHex = revealedCellHex;
        AccentHex = accentHex;
        FlagHex = flagHex;
        BombHex = bombHex;
        TextHex = textHex;
    }

    public string Name { get; }
    public string WindowBackgroundHex { get; }
    public string PanelBackgroundHex { get; }
    public string CoveredCellHex { get; }
    public string RevealedCellHex { get; }
    public string AccentHex { get; }
    public string FlagHex { get; }
    public string BombHex { get; }
    public string TextHex { get; }
}

/// <summary>Classic blue-gray Minesweeper theme.</summary>
public sealed class ClassicGameTheme : GameTheme
{
    public ClassicGameTheme()
        : base("Classic", "#F3F6FA", "#FFFFFF", "#657786", "#E6EBEF", "#286FA8", "#2B9CA6", "#CC4C4C", "#193046")
    {
    }
}

/// <summary>Dark theme for lower-light play.</summary>
public sealed class MidnightGameTheme : GameTheme
{
    public MidnightGameTheme()
        : base("Midnight", "#111827", "#1F2937", "#374151", "#D1D5DB", "#7C3AED", "#06B6D4", "#EF4444", "#F9FAFB")
    {
    }
}

/// <summary>High-contrast theme emphasizing board state differences.</summary>
public sealed class HighContrastGameTheme : GameTheme
{
    public HighContrastGameTheme()
        : base("High Contrast", "#FFFFFF", "#FFFFFF", "#111111", "#F2F2F2", "#005FCC", "#008A00", "#C00000", "#000000")
    {
    }
}
