using MinesweeperClassLibrary.BusinessLogicLayer;
using MinesweeperClassLibrary.DataAccessLayer;
using MinesweeperClassLibrary.Models;

namespace MinesweeperClassLibrary.Tests;

public class Milestone6FeatureTests
{
    [Fact]
    public void ThemeLogic_ReturnsConcreteThemesThroughBaseType()
    {
        var logic = new ThemeLogic();
        IReadOnlyList<GameTheme> themes = logic.GetThemes();

        Assert.True(themes.Count >= 3);
        Assert.Contains(themes, theme => theme is ClassicGameTheme);
        Assert.Contains(themes, theme => theme is MidnightGameTheme);
        Assert.Contains(themes, theme => theme is HighContrastGameTheme);
    }

    [Fact]
    public void ThemeLogic_UnknownNameFallsBackToClassic()
    {
        var logic = new ThemeLogic();

        GameTheme theme = logic.FindByName("Not A Theme");

        Assert.IsType<ClassicGameTheme>(theme);
    }

    [Fact]
    public void CreateSnapshotAndRestoreBoard_PreservesCellState()
    {
        var board = new BoardModel(5, 10);
        IBoardLogic boardLogic = new BoardLogic(700);
        boardLogic.InitializeBoard(board);
        CellModel safe = board.Cells.Cast<CellModel>().First(cell => !cell.IsBomb);
        safe.IsVisited = true;
        CellModel hidden = board.Cells.Cast<CellModel>().First(cell => !cell.IsVisited && cell != safe);
        hidden.IsFlagged = true;
        var saveLogic = new GameSaveLogic();

        SavedGameModel snapshot = saveLogic.CreateSnapshot(board, TimeSpan.FromSeconds(42), "Midnight");
        BoardModel restored = saveLogic.RestoreBoard(snapshot);

        Assert.Equal(board.Size, restored.Size);
        Assert.Equal(board.DifficultyPercent, restored.DifficultyPercent);
        Assert.Equal(42.0, snapshot.ElapsedSeconds, 3);
        Assert.Equal("Midnight", snapshot.ThemeName);
        for (int row = 0; row < board.Size; row++)
        {
            for (int column = 0; column < board.Size; column++)
            {
                CellModel original = board.Cells[row, column];
                CellModel copy = restored.Cells[row, column];
                Assert.Equal(original.IsBomb, copy.IsBomb);
                Assert.Equal(original.IsVisited, copy.IsVisited);
                Assert.Equal(original.IsFlagged, copy.IsFlagged);
                Assert.Equal(original.NumberOfBombNeighbors, copy.NumberOfBombNeighbors);
            }
        }
    }

    [Fact]
    public void SavedGameRepository_JsonRoundTrip_PreservesSnapshot()
    {
        var board = new BoardModel(5, 10);
        IBoardLogic boardLogic = new BoardLogic(701);
        boardLogic.InitializeBoard(board);
        var saveLogic = new GameSaveLogic();
        SavedGameModel expected = saveLogic.CreateSnapshot(board, TimeSpan.FromSeconds(19), "High Contrast");
        var repository = new SavedGameRepository();
        string fileName = Path.Combine(Path.GetTempPath(), $"minesweeper-{Guid.NewGuid():N}.json");

        try
        {
            repository.Save(fileName, expected);
            SavedGameModel actual = repository.Load(fileName);

            Assert.Equal(expected.Size, actual.Size);
            Assert.Equal(expected.DifficultyPercent, actual.DifficultyPercent);
            Assert.Equal(expected.ThemeName, actual.ThemeName);
            Assert.Equal(expected.Cells.Count, actual.Cells.Count);
            Assert.Equal(expected.Cells.Count(cell => cell.IsBomb), actual.Cells.Count(cell => cell.IsBomb));
        }
        finally
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
        }
    }

    [Fact]
    public void RestoreBoard_DuplicateCoordinates_ThrowsInvalidDataException()
    {
        var board = new BoardModel(5, 10);
        IBoardLogic boardLogic = new BoardLogic(702);
        boardLogic.InitializeBoard(board);
        var saveLogic = new GameSaveLogic();
        SavedGameModel snapshot = saveLogic.CreateSnapshot(board, TimeSpan.Zero, "Classic");
        snapshot.Cells[1].Row = snapshot.Cells[0].Row;
        snapshot.Cells[1].Column = snapshot.Cells[0].Column;

        Assert.Throws<InvalidDataException>(() => saveLogic.RestoreBoard(snapshot));
    }

    [Fact]
    public void ThemeLogic_FindByName_IsCaseInsensitive()
    {
        var logic = new ThemeLogic();

        GameTheme theme = logic.FindByName("mIdNiGhT");

        Assert.IsType<MidnightGameTheme>(theme);
        Assert.Equal("Midnight", theme.Name);
    }

    [Fact]
    public void CreateSnapshot_NegativeElapsedTime_ClampsToZero()
    {
        var board = new BoardModel(5, 10);
        IBoardLogic boardLogic = new BoardLogic(703);
        boardLogic.InitializeBoard(board);
        var saveLogic = new GameSaveLogic();

        SavedGameModel snapshot = saveLogic.CreateSnapshot(board, TimeSpan.FromSeconds(-15), "Classic");

        Assert.Equal(0, snapshot.ElapsedSeconds);
    }

    [Fact]
    public void RestoreBoard_UnsupportedSize_ThrowsInvalidDataException()
    {
        SavedGameModel snapshot = CreateValidSnapshot(seed: 704);
        snapshot.Size = 4;

        var saveLogic = new GameSaveLogic();
        Assert.Throws<InvalidDataException>(() => saveLogic.RestoreBoard(snapshot));
    }

    [Fact]
    public void RestoreBoard_UnsupportedDifficulty_ThrowsInvalidDataException()
    {
        SavedGameModel snapshot = CreateValidSnapshot(seed: 705);
        snapshot.DifficultyPercent = 40;

        var saveLogic = new GameSaveLogic();
        Assert.Throws<InvalidDataException>(() => saveLogic.RestoreBoard(snapshot));
    }

    [Fact]
    public void RestoreBoard_IncorrectCellCount_ThrowsInvalidDataException()
    {
        SavedGameModel snapshot = CreateValidSnapshot(seed: 706);
        snapshot.Cells.RemoveAt(snapshot.Cells.Count - 1);

        var saveLogic = new GameSaveLogic();
        Assert.Throws<InvalidDataException>(() => saveLogic.RestoreBoard(snapshot));
    }

    [Fact]
    public void SavedGameRepository_Save_BlankFileName_ThrowsArgumentException()
    {
        SavedGameModel snapshot = CreateValidSnapshot(seed: 707);
        var repository = new SavedGameRepository();

        Assert.Throws<ArgumentException>(() => repository.Save("   ", snapshot));
    }

    [Fact]
    public void SavedGameRepository_Load_MissingFile_ThrowsFileNotFoundException()
    {
        var repository = new SavedGameRepository();
        string fileName = Path.Combine(Path.GetTempPath(), $"missing-minesweeper-{Guid.NewGuid():N}.json");

        if (File.Exists(fileName))
        {
            File.Delete(fileName);
        }

        Assert.Throws<FileNotFoundException>(() => repository.Load(fileName));
    }

    private static SavedGameModel CreateValidSnapshot(int seed)
    {
        var board = new BoardModel(5, 10);
        IBoardLogic boardLogic = new BoardLogic(seed);
        boardLogic.InitializeBoard(board);
        return new GameSaveLogic().CreateSnapshot(board, TimeSpan.FromSeconds(10), "Classic");
    }
}
