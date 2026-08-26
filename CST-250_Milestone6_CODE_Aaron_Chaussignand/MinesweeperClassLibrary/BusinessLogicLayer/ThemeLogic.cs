using MinesweeperClassLibrary.Models;

namespace MinesweeperClassLibrary.BusinessLogicLayer;

/// <summary>
/// Provides the available visual themes through the abstract GameTheme type.
/// Concrete themes can be added without changing the presentation contract.
/// </summary>
public sealed class ThemeLogic
{
    private readonly IReadOnlyList<GameTheme> _themes = new GameTheme[]
    {
        new ClassicGameTheme(),
        new MidnightGameTheme(),
        new HighContrastGameTheme()
    };

    public IReadOnlyList<GameTheme> GetThemes() => _themes;

    public GameTheme FindByName(string? name)
    {
        return _themes.FirstOrDefault(theme =>
                   string.Equals(theme.Name, name, StringComparison.OrdinalIgnoreCase))
               ?? _themes[0];
    }
}
