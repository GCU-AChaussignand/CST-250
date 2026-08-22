// Source: Grand Canyon University. (2025). Activity 6: Building an N-Layer Application With Advanced File I/O, LINQ, and Data Binding [Course activity guide].

using System.Text.RegularExpressions;
using FileIOAndLINQ.Models;
using FileIOAndLINQ.Services.BusinessLogicLayer;

namespace FileIOAndLINQ.PresentationLayer;

/// <summary>Main presentation layer for the Bible verse inventory.</summary>
public partial class FrmVerseList : Form
{
    private readonly List<Label> _errorLabels;
    private readonly VerseLogic _verseLogic;
    private readonly BindingSource _versesBindingSource;
    private readonly string _fileFilter = "Supported files (*.txt;*.json;*.csv;*.xml;*.xlsx)|*.txt;*.json;*.csv;*.xml;*.xlsx|Text files (*.txt)|*.txt|JSON files (*.json)|*.json|CSV files (*.csv)|*.csv|XML files (*.xml)|*.xml|Excel files (*.xlsx)|*.xlsx";
    private bool _isValidBook;
    private bool _isValidChapter;
    private bool _isValidVerse;
    private bool _isValidText;
    private bool _isValidMeaning;
    private bool _isValidImportance;
    private int _numToShow;

    public FrmVerseList()
    {
        InitializeComponent();
        _verseLogic = new VerseLogic();
        _versesBindingSource = new BindingSource();
        _errorLabels = new List<Label>();
        InitializeErrors();
        InitializeBooks();
        trbNumberToShow.Maximum = 0;
    }

    private void InitializeErrors()
    {
        _errorLabels.Clear();
        _errorLabels.AddRange(new[] { lblBookError, lblChapterError, lblVerseError, lblTextError, lblMeaningError, lblImportanceError });
        foreach (Label label in _errorLabels) label.Visible = false;
    }

    private void InitializeBooks()
    {
        string[] bibleBooks =
        {
            "Genesis", "Exodus", "Leviticus", "Numbers", "Deuteronomy", "Joshua", "Judges", "Ruth", "1 Samuel", "2 Samuel", "1 Kings", "2 Kings", "1 Chronicles", "2 Chronicles", "Ezra", "Nehemiah", "Esther", "Job", "Psalms", "Proverbs", "Ecclesiastes", "Song of Solomon", "Isaiah", "Jeremiah", "Lamentations", "Ezekiel", "Daniel", "Hosea", "Joel", "Amos", "Obadiah", "Jonah", "Micah", "Nahum", "Habakkuk", "Zephaniah", "Haggai", "Zechariah", "Malachi", "Matthew", "Mark", "Luke", "John", "Acts", "Romans", "1 Corinthians", "2 Corinthians", "Galatians", "Ephesians", "Philippians", "Colossians", "1 Thessalonians", "2 Thessalonians", "1 Timothy", "2 Timothy", "Titus", "Philemon", "Hebrews", "James", "1 Peter", "2 Peter", "1 John", "2 John", "3 John", "Jude", "Revelation"
        };
        cmbVerseBook.DataSource = bibleBooks.ToList();
        cmbVerseBook.SelectedIndex = -1;
        cmbVerseBook.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        cmbVerseBook.AutoCompleteSource = AutoCompleteSource.ListItems;
    }

    private void CmbVerseBookLeaveEH(object? sender, EventArgs e)
    {
        _isValidBook = cmbVerseBook.SelectedIndex >= 0;
        lblBookError.Visible = !_isValidBook;
    }

    private void TxtVerseChapterLeaveEH(object? sender, EventArgs e)
    {
        _isValidChapter = Regex.IsMatch(txtVerseChapter.Text.Trim(), "^[0-9]+$") && int.TryParse(txtVerseChapter.Text, out int chapter) && chapter > 0;
        lblChapterError.Visible = !_isValidChapter;
    }

    private void TxtVerseVerseLeaveEH(object? sender, EventArgs e)
    {
        _isValidVerse = Regex.IsMatch(txtVerseVerse.Text.Trim(), @"^\d+(?:-\d+)?$") && VerseLogic.CountVerseExpression(txtVerseVerse.Text.Trim()) > 0;
        lblVerseError.Visible = !_isValidVerse;
    }

    private void TxtVerseTextLeaveEH(object? sender, EventArgs e)
    {
        _isValidText = !string.IsNullOrWhiteSpace(txtVerseText.Text);
        lblTextError.Visible = !_isValidText;
    }

    private void TxtVerseMeaningLeaveEH(object? sender, EventArgs e)
    {
        _isValidMeaning = !string.IsNullOrWhiteSpace(txtVerseMeaning.Text);
        lblMeaningError.Visible = !_isValidMeaning;
    }

    private void NudVerseImportanceLeaveEH(object? sender, EventArgs e)
    {
        _isValidImportance = nudVerseImportance.Value is >= 1 and <= 10;
        lblImportanceError.Visible = !_isValidImportance;
    }

    private void BtnAddVerseClickEH(object? sender, EventArgs e)
    {
        ValidateAllFields();
        if (!(_isValidBook && _isValidChapter && _isValidVerse && _isValidText && _isValidMeaning && _isValidImportance))
        {
            MessageBox.Show("Correct the highlighted input fields before adding the verse.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        if (!int.TryParse(txtVerseChapter.Text.Trim(), out int chapter)) return;
        var request = new VerseRequestModel(cmbVerseBook.Text.Trim(), chapter, txtVerseVerse.Text.Trim(), txtVerseText.Text.Trim(), txtVerseMeaning.Text.Trim(), (int)nudVerseImportance.Value);
        _verseLogic.AddVerse(request);
        ClearInputFields();
        RefreshVersesDgv();
    }

    private void FrmVerseListLoadEH(object? sender, EventArgs e)
    {
        dgvVerseDisplay.DataSource = _versesBindingSource;
        RefreshVersesDgv();
    }

    private void RefreshVersesDgv()
    {
        List<VerseDisplayModel> verses = _verseLogic.GetAllVerses();
        _versesBindingSource.DataSource = verses;
        int count = verses.Count;
        trbNumberToShow.Maximum = count;
        if (trbNumberToShow.Value > count) trbNumberToShow.Value = count;
        _numToShow = trbNumberToShow.Value;
        lblTrackValue.Text = $"Number to show: {_numToShow}";
        lblTotalVerses.Text = $"Total verses represented: {_verseLogic.GetTotalVerseCount()}";
        FormatVersesDgv();
    }

    private void FormatVersesDgv()
    {
        dgvVerseDisplay.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvVerseDisplay.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        dgvVerseDisplay.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        if (dgvVerseDisplay.Columns.Count >= 4)
        {
            dgvVerseDisplay.Columns[nameof(VerseDisplayModel.Reference)].FillWeight = 30;
            dgvVerseDisplay.Columns[nameof(VerseDisplayModel.Text)].FillWeight = 55;
            dgvVerseDisplay.Columns[nameof(VerseDisplayModel.Meaning)].FillWeight = 55;
            dgvVerseDisplay.Columns[nameof(VerseDisplayModel.Importance)].FillWeight = 22;
        }
    }

    private void TsmSaveClickEH(object? sender, EventArgs e)
    {
        using var dialog = new SaveFileDialog { Title = "Save Bible Verses", Filter = _fileFilter, FileName = "Verses.json" };
        if (dialog.ShowDialog(this) != DialogResult.OK) return;
        string result = _verseLogic.WriteInventoryToFile(dialog.FileName);
        MessageBox.Show(result, "Save", MessageBoxButtons.OK, result.StartsWith("Saved") ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
    }

    private void TsmLoadClickEH(object? sender, EventArgs e)
    {
        using var dialog = new OpenFileDialog { Title = "Load Bible Verses", Filter = _fileFilter };
        if (dialog.ShowDialog(this) != DialogResult.OK) return;
        string result = _verseLogic.ReadVersesFromFile(dialog.FileName);
        MessageBox.Show(result, "Load", MessageBoxButtons.OK, result.StartsWith("Loaded") ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
        RefreshVersesDgv();
    }

    private void TrbNumberToShowScrollEH(object? sender, EventArgs e)
    {
        _numToShow = trbNumberToShow.Value;
        lblTrackValue.Text = $"Number to show: {_numToShow}";
        if (rdoShowLeastValuable.Checked) ShowFiltered(_verseLogic.GetLeastImportantVerses(_numToShow));
        if (rdoShowMostValuable.Checked) ShowFiltered(_verseLogic.GetMostImportantVerses(_numToShow));
    }

    private void RdoShowLeastImportantCheckedChangedEH(object? sender, EventArgs e)
    {
        if (rdoShowLeastValuable.Checked) ShowFiltered(_verseLogic.GetLeastImportantVerses(_numToShow));
    }

    private void RdoShowMostImportantCheckedChangedEH(object? sender, EventArgs e)
    {
        if (rdoShowMostValuable.Checked) ShowFiltered(_verseLogic.GetMostImportantVerses(_numToShow));
    }

    private void RdoShowAllCheckedChangedEH(object? sender, EventArgs e)
    {
        if (rdoShowAll.Checked) RefreshVersesDgv();
    }

    private void TxtSearchTextChangedEH(object? sender, EventArgs e)
    {
        if (txtSearch.TextLength == 0)
        {
            if (rdoShowLeastValuable.Checked)
            {
                ShowFiltered(_verseLogic.GetLeastImportantVerses(_numToShow));
            }
            else if (rdoShowMostValuable.Checked)
            {
                ShowFiltered(_verseLogic.GetMostImportantVerses(_numToShow));
            }
            else
            {
                RefreshVersesDgv();
            }

            return;
        }

        ShowFiltered(_verseLogic.SearchVerses(txtSearch.Text));
    }

    private void ShowFiltered(List<VerseDisplayModel> verses)
    {
        _versesBindingSource.DataSource = verses;
        FormatVersesDgv();
    }

    private void ValidateAllFields()
    {
        CmbVerseBookLeaveEH(null, EventArgs.Empty);
        TxtVerseChapterLeaveEH(null, EventArgs.Empty);
        TxtVerseVerseLeaveEH(null, EventArgs.Empty);
        TxtVerseTextLeaveEH(null, EventArgs.Empty);
        TxtVerseMeaningLeaveEH(null, EventArgs.Empty);
        NudVerseImportanceLeaveEH(null, EventArgs.Empty);
    }

    private void ClearInputFields()
    {
        cmbVerseBook.SelectedIndex = -1;
        foreach (TextBox textBox in new[] { txtVerseChapter, txtVerseVerse, txtVerseText, txtVerseMeaning }) textBox.Clear();
        nudVerseImportance.Value = 0;
        _isValidBook = _isValidChapter = _isValidVerse = _isValidText = _isValidMeaning = _isValidImportance = false;
        InitializeErrors();
    }
}
