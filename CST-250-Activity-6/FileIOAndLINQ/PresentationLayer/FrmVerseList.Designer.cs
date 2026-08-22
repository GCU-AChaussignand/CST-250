// Source: Grand Canyon University. (2025). Activity 6: Building an N-Layer Application With Advanced File I/O, LINQ, and Data Binding [Course activity guide].

namespace FileIOAndLINQ.PresentationLayer;

partial class FrmVerseList
{
    private System.ComponentModel.IContainer? components;
    private MenuStrip mnsFileActions = null!;
    private ToolStripMenuItem tsmFile = null!;
    private ToolStripMenuItem tsmSave = null!;
    private ToolStripMenuItem tsmLoad = null!;
    private ToolStripMenuItem tsmExit = null!;
    private ComboBox cmbVerseBook = null!;
    private TextBox txtVerseChapter = null!;
    private TextBox txtVerseVerse = null!;
    private TextBox txtVerseText = null!;
    private TextBox txtVerseMeaning = null!;
    private NumericUpDown nudVerseImportance = null!;
    private Button btnAddVerse = null!;
    private Label lblBookError = null!;
    private Label lblChapterError = null!;
    private Label lblVerseError = null!;
    private Label lblTextError = null!;
    private Label lblMeaningError = null!;
    private Label lblImportanceError = null!;
    private RadioButton rdoShowAll = null!;
    private RadioButton rdoShowLeastValuable = null!;
    private RadioButton rdoShowMostValuable = null!;
    private TrackBar trbNumberToShow = null!;
    private Label lblTrackValue = null!;
    private TextBox txtSearch = null!;
    private Label lblTotalVerses = null!;
    private DataGridView dgvVerseDisplay = null!;

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        Text = "Bible Verses";
        StartPosition = FormStartPosition.CenterScreen;
        MinimumSize = new Size(1040, 700);
        ClientSize = new Size(1180, 760);
        Font = new Font("Segoe UI", 10F);
        BackColor = Color.FromArgb(245, 247, 250);

        mnsFileActions = new MenuStrip();
        tsmFile = new ToolStripMenuItem("File");
        tsmSave = new ToolStripMenuItem("Save");
        tsmLoad = new ToolStripMenuItem("Load");
        tsmExit = new ToolStripMenuItem("Exit");
        tsmFile.DropDownItems.AddRange(new ToolStripItem[] { tsmSave, tsmLoad, new ToolStripSeparator(), tsmExit });
        mnsFileActions.Items.Add(tsmFile);
        MainMenuStrip = mnsFileActions;

        var title = new Label { Text = "BIBLE VERSE LIBRARY", Font = new Font("Segoe UI Semibold", 22F), AutoSize = true, ForeColor = Color.FromArgb(30, 56, 82), Dock = DockStyle.Top };
        var subtitle = new Label { Text = "N-layer file I/O, LINQ filtering, data binding, search, and multi-format persistence", AutoSize = true, ForeColor = Color.DimGray, Dock = DockStyle.Top };

        var inputGroup = new GroupBox { Text = "Add A Bible Verse", Dock = DockStyle.Fill, Padding = new Padding(14) };
        var input = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 13 };
        input.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 95));
        input.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        cmbVerseBook = new ComboBox { Name = "cmbVerseBook", Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDown };
        txtVerseChapter = new TextBox { Name = "txtVerseChapter", Dock = DockStyle.Fill };
        txtVerseVerse = new TextBox { Name = "txtVerseVerse", Dock = DockStyle.Fill };
        txtVerseText = new TextBox { Name = "txtVerseText", Dock = DockStyle.Fill, Multiline = true, Height = 70, ScrollBars = ScrollBars.Vertical };
        txtVerseMeaning = new TextBox { Name = "txtVerseMeaning", Dock = DockStyle.Fill, Multiline = true, Height = 70, ScrollBars = ScrollBars.Vertical };
        nudVerseImportance = new NumericUpDown { Name = "nudVerseImportance", Minimum = 0, Maximum = 10, Dock = DockStyle.Left, Width = 90 };
        btnAddVerse = new Button { Name = "btnAddVerse", Text = "Add Verse", Height = 40, Dock = DockStyle.Fill, BackColor = Color.FromArgb(43, 111, 166), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
        btnAddVerse.FlatAppearance.BorderSize = 0;
        lblBookError = ErrorLabel("Select a valid book.");
        lblChapterError = ErrorLabel("Enter a chapter number.");
        lblVerseError = ErrorLabel("Enter a verse such as 3 or 1-3.");
        lblTextError = ErrorLabel("Verse text cannot be blank.");
        lblMeaningError = ErrorLabel("Meaning cannot be blank.");
        lblImportanceError = ErrorLabel("Importance must be 1-10.");
        AddInputRow(input, 0, "Book:", cmbVerseBook, lblBookError);
        AddInputRow(input, 2, "Chapter:", txtVerseChapter, lblChapterError);
        AddInputRow(input, 4, "Verse:", txtVerseVerse, lblVerseError);
        AddInputRow(input, 6, "Text:", txtVerseText, lblTextError);
        AddInputRow(input, 8, "Meaning:", txtVerseMeaning, lblMeaningError);
        AddInputRow(input, 10, "Importance:", nudVerseImportance, lblImportanceError);
        input.Controls.Add(btnAddVerse, 1, 12);
        inputGroup.Controls.Add(input);

        var filterGroup = new GroupBox { Text = "Filter And Sort", Dock = DockStyle.Fill, Padding = new Padding(14) };
        var filters = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 8, ColumnCount = 1 };
        rdoShowAll = new RadioButton { Name = "rdoShowAll", Text = "Show All", Checked = true, AutoSize = true };
        rdoShowLeastValuable = new RadioButton { Name = "rdoShowLeastValuable", Text = "Show Least Important", AutoSize = true };
        rdoShowMostValuable = new RadioButton { Name = "rdoShowMostValuable", Text = "Show Most Important", AutoSize = true };
        trbNumberToShow = new TrackBar { Name = "trbNumberToShow", Minimum = 0, Maximum = 0, Dock = DockStyle.Top, TickStyle = TickStyle.BottomRight };
        lblTrackValue = new Label { Text = "Number to show: 0", AutoSize = true };
        txtSearch = new TextBox { Name = "txtSearch", Dock = DockStyle.Top, PlaceholderText = "Search reference, text, meaning, importance..." };
        lblTotalVerses = new Label { Name = "lblTotalVerses", Text = "Total verses represented: 0", AutoSize = true, Font = new Font("Segoe UI Semibold", 10F) };
        filters.Controls.Add(rdoShowAll);
        filters.Controls.Add(rdoShowLeastValuable);
        filters.Controls.Add(rdoShowMostValuable);
        filters.Controls.Add(lblTrackValue);
        filters.Controls.Add(trbNumberToShow);
        filters.Controls.Add(new Label { Text = "Real-time search", AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) });
        filters.Controls.Add(txtSearch);
        filters.Controls.Add(lblTotalVerses);
        filterGroup.Controls.Add(filters);

        dgvVerseDisplay = new DataGridView { Name = "dgvVerseDisplay", Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false, AllowUserToDeleteRows = false, AutoGenerateColumns = true, BackgroundColor = Color.White, BorderStyle = BorderStyle.FixedSingle, SelectionMode = DataGridViewSelectionMode.FullRowSelect, RowHeadersVisible = false };

        var left = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 2, ColumnCount = 1 };
        left.RowStyles.Add(new RowStyle(SizeType.Percent, 68));
        left.RowStyles.Add(new RowStyle(SizeType.Percent, 32));
        left.Controls.Add(inputGroup, 0, 0);
        left.Controls.Add(filterGroup, 0, 1);

        var body = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 1 };
        body.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 370));
        body.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        body.Controls.Add(left, 0, 0);
        body.Controls.Add(dgvVerseDisplay, 1, 0);

        var root = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(18), RowCount = 3, ColumnCount = 1 };
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 48));
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 34));
        root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        root.Controls.Add(title, 0, 0);
        root.Controls.Add(subtitle, 0, 1);
        root.Controls.Add(body, 0, 2);
        Controls.Add(root);
        Controls.Add(mnsFileActions);

        tsmSave.Click += TsmSaveClickEH;
        tsmLoad.Click += TsmLoadClickEH;
        tsmExit.Click += (_, _) => Close();
        cmbVerseBook.Leave += CmbVerseBookLeaveEH;
        txtVerseChapter.Leave += TxtVerseChapterLeaveEH;
        txtVerseVerse.Leave += TxtVerseVerseLeaveEH;
        txtVerseText.Leave += TxtVerseTextLeaveEH;
        txtVerseMeaning.Leave += TxtVerseMeaningLeaveEH;
        nudVerseImportance.Leave += NudVerseImportanceLeaveEH;
        btnAddVerse.Click += BtnAddVerseClickEH;
        Load += FrmVerseListLoadEH;
        trbNumberToShow.Scroll += TrbNumberToShowScrollEH;
        rdoShowLeastValuable.CheckedChanged += RdoShowLeastImportantCheckedChangedEH;
        rdoShowMostValuable.CheckedChanged += RdoShowMostImportantCheckedChangedEH;
        rdoShowAll.CheckedChanged += RdoShowAllCheckedChangedEH;
        txtSearch.TextChanged += TxtSearchTextChangedEH;
    }

    private static Label ErrorLabel(string text) => new() { Text = text, ForeColor = Color.Firebrick, AutoSize = true };

    private static void AddInputRow(TableLayoutPanel panel, int row, string labelText, Control control, Label error)
    {
        panel.Controls.Add(new Label { Text = labelText, AutoSize = true, Anchor = AnchorStyles.Left }, 0, row);
        panel.Controls.Add(control, 1, row);
        panel.Controls.Add(error, 1, row + 1);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing) components?.Dispose();
        base.Dispose(disposing);
    }
}
