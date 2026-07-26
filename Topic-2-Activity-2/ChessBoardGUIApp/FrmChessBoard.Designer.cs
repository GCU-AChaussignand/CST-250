#nullable enable

namespace ChessBoardGUIApp
{
    partial class FrmChessBoard
    {
        private System.ComponentModel.IContainer? components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components is not null)
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            lblInstructions = new Label();
            pnlChessBoard = new Panel();
            tlpChessBoard = new TableLayoutPanel();
            grpOptions = new GroupBox();
            lblPieces = new Label();
            cmbChessPieces = new ComboBox();
            lblTheme = new Label();
            cmbTheme = new ComboBox();
            btnClearMoves = new Button();
            btnReset = new Button();
            lblStatus = new Label();
            btnSquare00 = new Button();
            btnSquare01 = new Button();
            btnSquare02 = new Button();
            btnSquare03 = new Button();
            btnSquare04 = new Button();
            btnSquare05 = new Button();
            btnSquare06 = new Button();
            btnSquare07 = new Button();
            btnSquare10 = new Button();
            btnSquare11 = new Button();
            btnSquare12 = new Button();
            btnSquare13 = new Button();
            btnSquare14 = new Button();
            btnSquare15 = new Button();
            btnSquare16 = new Button();
            btnSquare17 = new Button();
            btnSquare20 = new Button();
            btnSquare21 = new Button();
            btnSquare22 = new Button();
            btnSquare23 = new Button();
            btnSquare24 = new Button();
            btnSquare25 = new Button();
            btnSquare26 = new Button();
            btnSquare27 = new Button();
            btnSquare30 = new Button();
            btnSquare31 = new Button();
            btnSquare32 = new Button();
            btnSquare33 = new Button();
            btnSquare34 = new Button();
            btnSquare35 = new Button();
            btnSquare36 = new Button();
            btnSquare37 = new Button();
            btnSquare40 = new Button();
            btnSquare41 = new Button();
            btnSquare42 = new Button();
            btnSquare43 = new Button();
            btnSquare44 = new Button();
            btnSquare45 = new Button();
            btnSquare46 = new Button();
            btnSquare47 = new Button();
            btnSquare50 = new Button();
            btnSquare51 = new Button();
            btnSquare52 = new Button();
            btnSquare53 = new Button();
            btnSquare54 = new Button();
            btnSquare55 = new Button();
            btnSquare56 = new Button();
            btnSquare57 = new Button();
            btnSquare60 = new Button();
            btnSquare61 = new Button();
            btnSquare62 = new Button();
            btnSquare63 = new Button();
            btnSquare64 = new Button();
            btnSquare65 = new Button();
            btnSquare66 = new Button();
            btnSquare67 = new Button();
            btnSquare70 = new Button();
            btnSquare71 = new Button();
            btnSquare72 = new Button();
            btnSquare73 = new Button();
            btnSquare74 = new Button();
            btnSquare75 = new Button();
            btnSquare76 = new Button();
            btnSquare77 = new Button();
            pnlChessBoard.SuspendLayout();
            tlpChessBoard.SuspendLayout();
            grpOptions.SuspendLayout();
            SuspendLayout();
            // 
            // lblInstructions
            // 
            lblInstructions.AutoSize = true;
            lblInstructions.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblInstructions.Location = new Point(20, 20);
            lblInstructions.Name = "lblInstructions";
            lblInstructions.Size = new Size(487, 20);
            lblInstructions.TabIndex = 0;
            lblInstructions.Text = "Select a chess piece and a board square to display all legal moves.";
            // 
            // pnlChessBoard
            // 
            pnlChessBoard.BorderStyle = BorderStyle.FixedSingle;
            pnlChessBoard.Controls.Add(tlpChessBoard);
            pnlChessBoard.Location = new Point(20, 65);
            pnlChessBoard.Name = "pnlChessBoard";
            pnlChessBoard.Size = new Size(500, 500);
            pnlChessBoard.TabIndex = 1;
            // 
            // tlpChessBoard
            // 
            tlpChessBoard.ColumnCount = 8;
            tlpChessBoard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
            tlpChessBoard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
            tlpChessBoard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
            tlpChessBoard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
            tlpChessBoard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
            tlpChessBoard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
            tlpChessBoard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
            tlpChessBoard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
            tlpChessBoard.Dock = DockStyle.Fill;
            tlpChessBoard.Location = new Point(0, 0);
            tlpChessBoard.Margin = new Padding(0);
            tlpChessBoard.Name = "tlpChessBoard";
            tlpChessBoard.RowCount = 8;
            tlpChessBoard.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tlpChessBoard.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tlpChessBoard.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tlpChessBoard.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tlpChessBoard.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tlpChessBoard.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tlpChessBoard.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tlpChessBoard.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tlpChessBoard.Size = new Size(498, 498);
            tlpChessBoard.TabIndex = 0;
            // 
            // btnSquare00
            // 
            btnSquare00.Dock = DockStyle.Fill;
            btnSquare00.FlatStyle = FlatStyle.Flat;
            btnSquare00.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            btnSquare00.Location = new Point(0, 0);
            btnSquare00.Margin = new Padding(0);
            btnSquare00.Name = "btnSquare00";
            btnSquare00.Size = new Size(62, 62);
            btnSquare00.TabIndex = 0;
            btnSquare00.TabStop = false;
            btnSquare00.Text = "0, 0";
            btnSquare00.UseVisualStyleBackColor = false;
            tlpChessBoard.Controls.Add(btnSquare00, 0, 0);
            // 
            // btnSquare01
            // 
            btnSquare01.Dock = DockStyle.Fill;
            btnSquare01.FlatStyle = FlatStyle.Flat;
            btnSquare01.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            btnSquare01.Location = new Point(0, 0);
            btnSquare01.Margin = new Padding(0);
            btnSquare01.Name = "btnSquare01";
            btnSquare01.Size = new Size(62, 62);
            btnSquare01.TabIndex = 1;
            btnSquare01.TabStop = false;
            btnSquare01.Text = "0, 1";
            btnSquare01.UseVisualStyleBackColor = false;
            tlpChessBoard.Controls.Add(btnSquare01, 1, 0);
            // 
            // btnSquare02
            // 
            btnSquare02.Dock = DockStyle.Fill;
            btnSquare02.FlatStyle = FlatStyle.Flat;
            btnSquare02.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            btnSquare02.Location = new Point(0, 0);
            btnSquare02.Margin = new Padding(0);
            btnSquare02.Name = "btnSquare02";
            btnSquare02.Size = new Size(62, 62);
            btnSquare02.TabIndex = 2;
            btnSquare02.TabStop = false;
            btnSquare02.Text = "0, 2";
            btnSquare02.UseVisualStyleBackColor = false;
            tlpChessBoard.Controls.Add(btnSquare02, 2, 0);
            // 
            // btnSquare03
            // 
            btnSquare03.Dock = DockStyle.Fill;
            btnSquare03.FlatStyle = FlatStyle.Flat;
            btnSquare03.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            btnSquare03.Location = new Point(0, 0);
            btnSquare03.Margin = new Padding(0);
            btnSquare03.Name = "btnSquare03";
            btnSquare03.Size = new Size(62, 62);
            btnSquare03.TabIndex = 3;
            btnSquare03.TabStop = false;
            btnSquare03.Text = "0, 3";
            btnSquare03.UseVisualStyleBackColor = false;
            tlpChessBoard.Controls.Add(btnSquare03, 3, 0);
            // 
            // btnSquare04
            // 
            btnSquare04.Dock = DockStyle.Fill;
            btnSquare04.FlatStyle = FlatStyle.Flat;
            btnSquare04.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            btnSquare04.Location = new Point(0, 0);
            btnSquare04.Margin = new Padding(0);
            btnSquare04.Name = "btnSquare04";
            btnSquare04.Size = new Size(62, 62);
            btnSquare04.TabIndex = 4;
            btnSquare04.TabStop = false;
            btnSquare04.Text = "0, 4";
            btnSquare04.UseVisualStyleBackColor = false;
            tlpChessBoard.Controls.Add(btnSquare04, 4, 0);
            // 
            // btnSquare05
            // 
            btnSquare05.Dock = DockStyle.Fill;
            btnSquare05.FlatStyle = FlatStyle.Flat;
            btnSquare05.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            btnSquare05.Location = new Point(0, 0);
            btnSquare05.Margin = new Padding(0);
            btnSquare05.Name = "btnSquare05";
            btnSquare05.Size = new Size(62, 62);
            btnSquare05.TabIndex = 5;
            btnSquare05.TabStop = false;
            btnSquare05.Text = "0, 5";
            btnSquare05.UseVisualStyleBackColor = false;
            tlpChessBoard.Controls.Add(btnSquare05, 5, 0);
            // 
            // btnSquare06
            // 
            btnSquare06.Dock = DockStyle.Fill;
            btnSquare06.FlatStyle = FlatStyle.Flat;
            btnSquare06.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            btnSquare06.Location = new Point(0, 0);
            btnSquare06.Margin = new Padding(0);
            btnSquare06.Name = "btnSquare06";
            btnSquare06.Size = new Size(62, 62);
            btnSquare06.TabIndex = 6;
            btnSquare06.TabStop = false;
            btnSquare06.Text = "0, 6";
            btnSquare06.UseVisualStyleBackColor = false;
            tlpChessBoard.Controls.Add(btnSquare06, 6, 0);
            // 
            // btnSquare07
            // 
            btnSquare07.Dock = DockStyle.Fill;
            btnSquare07.FlatStyle = FlatStyle.Flat;
            btnSquare07.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            btnSquare07.Location = new Point(0, 0);
            btnSquare07.Margin = new Padding(0);
            btnSquare07.Name = "btnSquare07";
            btnSquare07.Size = new Size(62, 62);
            btnSquare07.TabIndex = 7;
            btnSquare07.TabStop = false;
            btnSquare07.Text = "0, 7";
            btnSquare07.UseVisualStyleBackColor = false;
            tlpChessBoard.Controls.Add(btnSquare07, 7, 0);
            // 
            // btnSquare10
            // 
            btnSquare10.Dock = DockStyle.Fill;
            btnSquare10.FlatStyle = FlatStyle.Flat;
            btnSquare10.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            btnSquare10.Location = new Point(0, 0);
            btnSquare10.Margin = new Padding(0);
            btnSquare10.Name = "btnSquare10";
            btnSquare10.Size = new Size(62, 62);
            btnSquare10.TabIndex = 8;
            btnSquare10.TabStop = false;
            btnSquare10.Text = "1, 0";
            btnSquare10.UseVisualStyleBackColor = false;
            tlpChessBoard.Controls.Add(btnSquare10, 0, 1);
            // 
            // btnSquare11
            // 
            btnSquare11.Dock = DockStyle.Fill;
            btnSquare11.FlatStyle = FlatStyle.Flat;
            btnSquare11.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            btnSquare11.Location = new Point(0, 0);
            btnSquare11.Margin = new Padding(0);
            btnSquare11.Name = "btnSquare11";
            btnSquare11.Size = new Size(62, 62);
            btnSquare11.TabIndex = 9;
            btnSquare11.TabStop = false;
            btnSquare11.Text = "1, 1";
            btnSquare11.UseVisualStyleBackColor = false;
            tlpChessBoard.Controls.Add(btnSquare11, 1, 1);
            // 
            // btnSquare12
            // 
            btnSquare12.Dock = DockStyle.Fill;
            btnSquare12.FlatStyle = FlatStyle.Flat;
            btnSquare12.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            btnSquare12.Location = new Point(0, 0);
            btnSquare12.Margin = new Padding(0);
            btnSquare12.Name = "btnSquare12";
            btnSquare12.Size = new Size(62, 62);
            btnSquare12.TabIndex = 10;
            btnSquare12.TabStop = false;
            btnSquare12.Text = "1, 2";
            btnSquare12.UseVisualStyleBackColor = false;
            tlpChessBoard.Controls.Add(btnSquare12, 2, 1);
            // 
            // btnSquare13
            // 
            btnSquare13.Dock = DockStyle.Fill;
            btnSquare13.FlatStyle = FlatStyle.Flat;
            btnSquare13.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            btnSquare13.Location = new Point(0, 0);
            btnSquare13.Margin = new Padding(0);
            btnSquare13.Name = "btnSquare13";
            btnSquare13.Size = new Size(62, 62);
            btnSquare13.TabIndex = 11;
            btnSquare13.TabStop = false;
            btnSquare13.Text = "1, 3";
            btnSquare13.UseVisualStyleBackColor = false;
            tlpChessBoard.Controls.Add(btnSquare13, 3, 1);
            // 
            // btnSquare14
            // 
            btnSquare14.Dock = DockStyle.Fill;
            btnSquare14.FlatStyle = FlatStyle.Flat;
            btnSquare14.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            btnSquare14.Location = new Point(0, 0);
            btnSquare14.Margin = new Padding(0);
            btnSquare14.Name = "btnSquare14";
            btnSquare14.Size = new Size(62, 62);
            btnSquare14.TabIndex = 12;
            btnSquare14.TabStop = false;
            btnSquare14.Text = "1, 4";
            btnSquare14.UseVisualStyleBackColor = false;
            tlpChessBoard.Controls.Add(btnSquare14, 4, 1);
            // 
            // btnSquare15
            // 
            btnSquare15.Dock = DockStyle.Fill;
            btnSquare15.FlatStyle = FlatStyle.Flat;
            btnSquare15.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            btnSquare15.Location = new Point(0, 0);
            btnSquare15.Margin = new Padding(0);
            btnSquare15.Name = "btnSquare15";
            btnSquare15.Size = new Size(62, 62);
            btnSquare15.TabIndex = 13;
            btnSquare15.TabStop = false;
            btnSquare15.Text = "1, 5";
            btnSquare15.UseVisualStyleBackColor = false;
            tlpChessBoard.Controls.Add(btnSquare15, 5, 1);
            // 
            // btnSquare16
            // 
            btnSquare16.Dock = DockStyle.Fill;
            btnSquare16.FlatStyle = FlatStyle.Flat;
            btnSquare16.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            btnSquare16.Location = new Point(0, 0);
            btnSquare16.Margin = new Padding(0);
            btnSquare16.Name = "btnSquare16";
            btnSquare16.Size = new Size(62, 62);
            btnSquare16.TabIndex = 14;
            btnSquare16.TabStop = false;
            btnSquare16.Text = "1, 6";
            btnSquare16.UseVisualStyleBackColor = false;
            tlpChessBoard.Controls.Add(btnSquare16, 6, 1);
            // 
            // btnSquare17
            // 
            btnSquare17.Dock = DockStyle.Fill;
            btnSquare17.FlatStyle = FlatStyle.Flat;
            btnSquare17.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            btnSquare17.Location = new Point(0, 0);
            btnSquare17.Margin = new Padding(0);
            btnSquare17.Name = "btnSquare17";
            btnSquare17.Size = new Size(62, 62);
            btnSquare17.TabIndex = 15;
            btnSquare17.TabStop = false;
            btnSquare17.Text = "1, 7";
            btnSquare17.UseVisualStyleBackColor = false;
            tlpChessBoard.Controls.Add(btnSquare17, 7, 1);
            // 
            // btnSquare20
            // 
            btnSquare20.Dock = DockStyle.Fill;
            btnSquare20.FlatStyle = FlatStyle.Flat;
            btnSquare20.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            btnSquare20.Location = new Point(0, 0);
            btnSquare20.Margin = new Padding(0);
            btnSquare20.Name = "btnSquare20";
            btnSquare20.Size = new Size(62, 62);
            btnSquare20.TabIndex = 16;
            btnSquare20.TabStop = false;
            btnSquare20.Text = "2, 0";
            btnSquare20.UseVisualStyleBackColor = false;
            tlpChessBoard.Controls.Add(btnSquare20, 0, 2);
            // 
            // btnSquare21
            // 
            btnSquare21.Dock = DockStyle.Fill;
            btnSquare21.FlatStyle = FlatStyle.Flat;
            btnSquare21.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            btnSquare21.Location = new Point(0, 0);
            btnSquare21.Margin = new Padding(0);
            btnSquare21.Name = "btnSquare21";
            btnSquare21.Size = new Size(62, 62);
            btnSquare21.TabIndex = 17;
            btnSquare21.TabStop = false;
            btnSquare21.Text = "2, 1";
            btnSquare21.UseVisualStyleBackColor = false;
            tlpChessBoard.Controls.Add(btnSquare21, 1, 2);
            // 
            // btnSquare22
            // 
            btnSquare22.Dock = DockStyle.Fill;
            btnSquare22.FlatStyle = FlatStyle.Flat;
            btnSquare22.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            btnSquare22.Location = new Point(0, 0);
            btnSquare22.Margin = new Padding(0);
            btnSquare22.Name = "btnSquare22";
            btnSquare22.Size = new Size(62, 62);
            btnSquare22.TabIndex = 18;
            btnSquare22.TabStop = false;
            btnSquare22.Text = "2, 2";
            btnSquare22.UseVisualStyleBackColor = false;
            tlpChessBoard.Controls.Add(btnSquare22, 2, 2);
            // 
            // btnSquare23
            // 
            btnSquare23.Dock = DockStyle.Fill;
            btnSquare23.FlatStyle = FlatStyle.Flat;
            btnSquare23.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            btnSquare23.Location = new Point(0, 0);
            btnSquare23.Margin = new Padding(0);
            btnSquare23.Name = "btnSquare23";
            btnSquare23.Size = new Size(62, 62);
            btnSquare23.TabIndex = 19;
            btnSquare23.TabStop = false;
            btnSquare23.Text = "2, 3";
            btnSquare23.UseVisualStyleBackColor = false;
            tlpChessBoard.Controls.Add(btnSquare23, 3, 2);
            // 
            // btnSquare24
            // 
            btnSquare24.Dock = DockStyle.Fill;
            btnSquare24.FlatStyle = FlatStyle.Flat;
            btnSquare24.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            btnSquare24.Location = new Point(0, 0);
            btnSquare24.Margin = new Padding(0);
            btnSquare24.Name = "btnSquare24";
            btnSquare24.Size = new Size(62, 62);
            btnSquare24.TabIndex = 20;
            btnSquare24.TabStop = false;
            btnSquare24.Text = "2, 4";
            btnSquare24.UseVisualStyleBackColor = false;
            tlpChessBoard.Controls.Add(btnSquare24, 4, 2);
            // 
            // btnSquare25
            // 
            btnSquare25.Dock = DockStyle.Fill;
            btnSquare25.FlatStyle = FlatStyle.Flat;
            btnSquare25.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            btnSquare25.Location = new Point(0, 0);
            btnSquare25.Margin = new Padding(0);
            btnSquare25.Name = "btnSquare25";
            btnSquare25.Size = new Size(62, 62);
            btnSquare25.TabIndex = 21;
            btnSquare25.TabStop = false;
            btnSquare25.Text = "2, 5";
            btnSquare25.UseVisualStyleBackColor = false;
            tlpChessBoard.Controls.Add(btnSquare25, 5, 2);
            // 
            // btnSquare26
            // 
            btnSquare26.Dock = DockStyle.Fill;
            btnSquare26.FlatStyle = FlatStyle.Flat;
            btnSquare26.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            btnSquare26.Location = new Point(0, 0);
            btnSquare26.Margin = new Padding(0);
            btnSquare26.Name = "btnSquare26";
            btnSquare26.Size = new Size(62, 62);
            btnSquare26.TabIndex = 22;
            btnSquare26.TabStop = false;
            btnSquare26.Text = "2, 6";
            btnSquare26.UseVisualStyleBackColor = false;
            tlpChessBoard.Controls.Add(btnSquare26, 6, 2);
            // 
            // btnSquare27
            // 
            btnSquare27.Dock = DockStyle.Fill;
            btnSquare27.FlatStyle = FlatStyle.Flat;
            btnSquare27.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            btnSquare27.Location = new Point(0, 0);
            btnSquare27.Margin = new Padding(0);
            btnSquare27.Name = "btnSquare27";
            btnSquare27.Size = new Size(62, 62);
            btnSquare27.TabIndex = 23;
            btnSquare27.TabStop = false;
            btnSquare27.Text = "2, 7";
            btnSquare27.UseVisualStyleBackColor = false;
            tlpChessBoard.Controls.Add(btnSquare27, 7, 2);
            // 
            // btnSquare30
            // 
            btnSquare30.Dock = DockStyle.Fill;
            btnSquare30.FlatStyle = FlatStyle.Flat;
            btnSquare30.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            btnSquare30.Location = new Point(0, 0);
            btnSquare30.Margin = new Padding(0);
            btnSquare30.Name = "btnSquare30";
            btnSquare30.Size = new Size(62, 62);
            btnSquare30.TabIndex = 24;
            btnSquare30.TabStop = false;
            btnSquare30.Text = "3, 0";
            btnSquare30.UseVisualStyleBackColor = false;
            tlpChessBoard.Controls.Add(btnSquare30, 0, 3);
            // 
            // btnSquare31
            // 
            btnSquare31.Dock = DockStyle.Fill;
            btnSquare31.FlatStyle = FlatStyle.Flat;
            btnSquare31.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            btnSquare31.Location = new Point(0, 0);
            btnSquare31.Margin = new Padding(0);
            btnSquare31.Name = "btnSquare31";
            btnSquare31.Size = new Size(62, 62);
            btnSquare31.TabIndex = 25;
            btnSquare31.TabStop = false;
            btnSquare31.Text = "3, 1";
            btnSquare31.UseVisualStyleBackColor = false;
            tlpChessBoard.Controls.Add(btnSquare31, 1, 3);
            // 
            // btnSquare32
            // 
            btnSquare32.Dock = DockStyle.Fill;
            btnSquare32.FlatStyle = FlatStyle.Flat;
            btnSquare32.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            btnSquare32.Location = new Point(0, 0);
            btnSquare32.Margin = new Padding(0);
            btnSquare32.Name = "btnSquare32";
            btnSquare32.Size = new Size(62, 62);
            btnSquare32.TabIndex = 26;
            btnSquare32.TabStop = false;
            btnSquare32.Text = "3, 2";
            btnSquare32.UseVisualStyleBackColor = false;
            tlpChessBoard.Controls.Add(btnSquare32, 2, 3);
            // 
            // btnSquare33
            // 
            btnSquare33.Dock = DockStyle.Fill;
            btnSquare33.FlatStyle = FlatStyle.Flat;
            btnSquare33.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            btnSquare33.Location = new Point(0, 0);
            btnSquare33.Margin = new Padding(0);
            btnSquare33.Name = "btnSquare33";
            btnSquare33.Size = new Size(62, 62);
            btnSquare33.TabIndex = 27;
            btnSquare33.TabStop = false;
            btnSquare33.Text = "3, 3";
            btnSquare33.UseVisualStyleBackColor = false;
            tlpChessBoard.Controls.Add(btnSquare33, 3, 3);
            // 
            // btnSquare34
            // 
            btnSquare34.Dock = DockStyle.Fill;
            btnSquare34.FlatStyle = FlatStyle.Flat;
            btnSquare34.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            btnSquare34.Location = new Point(0, 0);
            btnSquare34.Margin = new Padding(0);
            btnSquare34.Name = "btnSquare34";
            btnSquare34.Size = new Size(62, 62);
            btnSquare34.TabIndex = 28;
            btnSquare34.TabStop = false;
            btnSquare34.Text = "3, 4";
            btnSquare34.UseVisualStyleBackColor = false;
            tlpChessBoard.Controls.Add(btnSquare34, 4, 3);
            // 
            // btnSquare35
            // 
            btnSquare35.Dock = DockStyle.Fill;
            btnSquare35.FlatStyle = FlatStyle.Flat;
            btnSquare35.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            btnSquare35.Location = new Point(0, 0);
            btnSquare35.Margin = new Padding(0);
            btnSquare35.Name = "btnSquare35";
            btnSquare35.Size = new Size(62, 62);
            btnSquare35.TabIndex = 29;
            btnSquare35.TabStop = false;
            btnSquare35.Text = "3, 5";
            btnSquare35.UseVisualStyleBackColor = false;
            tlpChessBoard.Controls.Add(btnSquare35, 5, 3);
            // 
            // btnSquare36
            // 
            btnSquare36.Dock = DockStyle.Fill;
            btnSquare36.FlatStyle = FlatStyle.Flat;
            btnSquare36.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            btnSquare36.Location = new Point(0, 0);
            btnSquare36.Margin = new Padding(0);
            btnSquare36.Name = "btnSquare36";
            btnSquare36.Size = new Size(62, 62);
            btnSquare36.TabIndex = 30;
            btnSquare36.TabStop = false;
            btnSquare36.Text = "3, 6";
            btnSquare36.UseVisualStyleBackColor = false;
            tlpChessBoard.Controls.Add(btnSquare36, 6, 3);
            // 
            // btnSquare37
            // 
            btnSquare37.Dock = DockStyle.Fill;
            btnSquare37.FlatStyle = FlatStyle.Flat;
            btnSquare37.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            btnSquare37.Location = new Point(0, 0);
            btnSquare37.Margin = new Padding(0);
            btnSquare37.Name = "btnSquare37";
            btnSquare37.Size = new Size(62, 62);
            btnSquare37.TabIndex = 31;
            btnSquare37.TabStop = false;
            btnSquare37.Text = "3, 7";
            btnSquare37.UseVisualStyleBackColor = false;
            tlpChessBoard.Controls.Add(btnSquare37, 7, 3);
            // 
            // btnSquare40
            // 
            btnSquare40.Dock = DockStyle.Fill;
            btnSquare40.FlatStyle = FlatStyle.Flat;
            btnSquare40.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            btnSquare40.Location = new Point(0, 0);
            btnSquare40.Margin = new Padding(0);
            btnSquare40.Name = "btnSquare40";
            btnSquare40.Size = new Size(62, 62);
            btnSquare40.TabIndex = 32;
            btnSquare40.TabStop = false;
            btnSquare40.Text = "4, 0";
            btnSquare40.UseVisualStyleBackColor = false;
            tlpChessBoard.Controls.Add(btnSquare40, 0, 4);
            // 
            // btnSquare41
            // 
            btnSquare41.Dock = DockStyle.Fill;
            btnSquare41.FlatStyle = FlatStyle.Flat;
            btnSquare41.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            btnSquare41.Location = new Point(0, 0);
            btnSquare41.Margin = new Padding(0);
            btnSquare41.Name = "btnSquare41";
            btnSquare41.Size = new Size(62, 62);
            btnSquare41.TabIndex = 33;
            btnSquare41.TabStop = false;
            btnSquare41.Text = "4, 1";
            btnSquare41.UseVisualStyleBackColor = false;
            tlpChessBoard.Controls.Add(btnSquare41, 1, 4);
            // 
            // btnSquare42
            // 
            btnSquare42.Dock = DockStyle.Fill;
            btnSquare42.FlatStyle = FlatStyle.Flat;
            btnSquare42.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            btnSquare42.Location = new Point(0, 0);
            btnSquare42.Margin = new Padding(0);
            btnSquare42.Name = "btnSquare42";
            btnSquare42.Size = new Size(62, 62);
            btnSquare42.TabIndex = 34;
            btnSquare42.TabStop = false;
            btnSquare42.Text = "4, 2";
            btnSquare42.UseVisualStyleBackColor = false;
            tlpChessBoard.Controls.Add(btnSquare42, 2, 4);
            // 
            // btnSquare43
            // 
            btnSquare43.Dock = DockStyle.Fill;
            btnSquare43.FlatStyle = FlatStyle.Flat;
            btnSquare43.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            btnSquare43.Location = new Point(0, 0);
            btnSquare43.Margin = new Padding(0);
            btnSquare43.Name = "btnSquare43";
            btnSquare43.Size = new Size(62, 62);
            btnSquare43.TabIndex = 35;
            btnSquare43.TabStop = false;
            btnSquare43.Text = "4, 3";
            btnSquare43.UseVisualStyleBackColor = false;
            tlpChessBoard.Controls.Add(btnSquare43, 3, 4);
            // 
            // btnSquare44
            // 
            btnSquare44.Dock = DockStyle.Fill;
            btnSquare44.FlatStyle = FlatStyle.Flat;
            btnSquare44.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            btnSquare44.Location = new Point(0, 0);
            btnSquare44.Margin = new Padding(0);
            btnSquare44.Name = "btnSquare44";
            btnSquare44.Size = new Size(62, 62);
            btnSquare44.TabIndex = 36;
            btnSquare44.TabStop = false;
            btnSquare44.Text = "4, 4";
            btnSquare44.UseVisualStyleBackColor = false;
            tlpChessBoard.Controls.Add(btnSquare44, 4, 4);
            // 
            // btnSquare45
            // 
            btnSquare45.Dock = DockStyle.Fill;
            btnSquare45.FlatStyle = FlatStyle.Flat;
            btnSquare45.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            btnSquare45.Location = new Point(0, 0);
            btnSquare45.Margin = new Padding(0);
            btnSquare45.Name = "btnSquare45";
            btnSquare45.Size = new Size(62, 62);
            btnSquare45.TabIndex = 37;
            btnSquare45.TabStop = false;
            btnSquare45.Text = "4, 5";
            btnSquare45.UseVisualStyleBackColor = false;
            tlpChessBoard.Controls.Add(btnSquare45, 5, 4);
            // 
            // btnSquare46
            // 
            btnSquare46.Dock = DockStyle.Fill;
            btnSquare46.FlatStyle = FlatStyle.Flat;
            btnSquare46.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            btnSquare46.Location = new Point(0, 0);
            btnSquare46.Margin = new Padding(0);
            btnSquare46.Name = "btnSquare46";
            btnSquare46.Size = new Size(62, 62);
            btnSquare46.TabIndex = 38;
            btnSquare46.TabStop = false;
            btnSquare46.Text = "4, 6";
            btnSquare46.UseVisualStyleBackColor = false;
            tlpChessBoard.Controls.Add(btnSquare46, 6, 4);
            // 
            // btnSquare47
            // 
            btnSquare47.Dock = DockStyle.Fill;
            btnSquare47.FlatStyle = FlatStyle.Flat;
            btnSquare47.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            btnSquare47.Location = new Point(0, 0);
            btnSquare47.Margin = new Padding(0);
            btnSquare47.Name = "btnSquare47";
            btnSquare47.Size = new Size(62, 62);
            btnSquare47.TabIndex = 39;
            btnSquare47.TabStop = false;
            btnSquare47.Text = "4, 7";
            btnSquare47.UseVisualStyleBackColor = false;
            tlpChessBoard.Controls.Add(btnSquare47, 7, 4);
            // 
            // btnSquare50
            // 
            btnSquare50.Dock = DockStyle.Fill;
            btnSquare50.FlatStyle = FlatStyle.Flat;
            btnSquare50.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            btnSquare50.Location = new Point(0, 0);
            btnSquare50.Margin = new Padding(0);
            btnSquare50.Name = "btnSquare50";
            btnSquare50.Size = new Size(62, 62);
            btnSquare50.TabIndex = 40;
            btnSquare50.TabStop = false;
            btnSquare50.Text = "5, 0";
            btnSquare50.UseVisualStyleBackColor = false;
            tlpChessBoard.Controls.Add(btnSquare50, 0, 5);
            // 
            // btnSquare51
            // 
            btnSquare51.Dock = DockStyle.Fill;
            btnSquare51.FlatStyle = FlatStyle.Flat;
            btnSquare51.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            btnSquare51.Location = new Point(0, 0);
            btnSquare51.Margin = new Padding(0);
            btnSquare51.Name = "btnSquare51";
            btnSquare51.Size = new Size(62, 62);
            btnSquare51.TabIndex = 41;
            btnSquare51.TabStop = false;
            btnSquare51.Text = "5, 1";
            btnSquare51.UseVisualStyleBackColor = false;
            tlpChessBoard.Controls.Add(btnSquare51, 1, 5);
            // 
            // btnSquare52
            // 
            btnSquare52.Dock = DockStyle.Fill;
            btnSquare52.FlatStyle = FlatStyle.Flat;
            btnSquare52.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            btnSquare52.Location = new Point(0, 0);
            btnSquare52.Margin = new Padding(0);
            btnSquare52.Name = "btnSquare52";
            btnSquare52.Size = new Size(62, 62);
            btnSquare52.TabIndex = 42;
            btnSquare52.TabStop = false;
            btnSquare52.Text = "5, 2";
            btnSquare52.UseVisualStyleBackColor = false;
            tlpChessBoard.Controls.Add(btnSquare52, 2, 5);
            // 
            // btnSquare53
            // 
            btnSquare53.Dock = DockStyle.Fill;
            btnSquare53.FlatStyle = FlatStyle.Flat;
            btnSquare53.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            btnSquare53.Location = new Point(0, 0);
            btnSquare53.Margin = new Padding(0);
            btnSquare53.Name = "btnSquare53";
            btnSquare53.Size = new Size(62, 62);
            btnSquare53.TabIndex = 43;
            btnSquare53.TabStop = false;
            btnSquare53.Text = "5, 3";
            btnSquare53.UseVisualStyleBackColor = false;
            tlpChessBoard.Controls.Add(btnSquare53, 3, 5);
            // 
            // btnSquare54
            // 
            btnSquare54.Dock = DockStyle.Fill;
            btnSquare54.FlatStyle = FlatStyle.Flat;
            btnSquare54.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            btnSquare54.Location = new Point(0, 0);
            btnSquare54.Margin = new Padding(0);
            btnSquare54.Name = "btnSquare54";
            btnSquare54.Size = new Size(62, 62);
            btnSquare54.TabIndex = 44;
            btnSquare54.TabStop = false;
            btnSquare54.Text = "5, 4";
            btnSquare54.UseVisualStyleBackColor = false;
            tlpChessBoard.Controls.Add(btnSquare54, 4, 5);
            // 
            // btnSquare55
            // 
            btnSquare55.Dock = DockStyle.Fill;
            btnSquare55.FlatStyle = FlatStyle.Flat;
            btnSquare55.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            btnSquare55.Location = new Point(0, 0);
            btnSquare55.Margin = new Padding(0);
            btnSquare55.Name = "btnSquare55";
            btnSquare55.Size = new Size(62, 62);
            btnSquare55.TabIndex = 45;
            btnSquare55.TabStop = false;
            btnSquare55.Text = "5, 5";
            btnSquare55.UseVisualStyleBackColor = false;
            tlpChessBoard.Controls.Add(btnSquare55, 5, 5);
            // 
            // btnSquare56
            // 
            btnSquare56.Dock = DockStyle.Fill;
            btnSquare56.FlatStyle = FlatStyle.Flat;
            btnSquare56.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            btnSquare56.Location = new Point(0, 0);
            btnSquare56.Margin = new Padding(0);
            btnSquare56.Name = "btnSquare56";
            btnSquare56.Size = new Size(62, 62);
            btnSquare56.TabIndex = 46;
            btnSquare56.TabStop = false;
            btnSquare56.Text = "5, 6";
            btnSquare56.UseVisualStyleBackColor = false;
            tlpChessBoard.Controls.Add(btnSquare56, 6, 5);
            // 
            // btnSquare57
            // 
            btnSquare57.Dock = DockStyle.Fill;
            btnSquare57.FlatStyle = FlatStyle.Flat;
            btnSquare57.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            btnSquare57.Location = new Point(0, 0);
            btnSquare57.Margin = new Padding(0);
            btnSquare57.Name = "btnSquare57";
            btnSquare57.Size = new Size(62, 62);
            btnSquare57.TabIndex = 47;
            btnSquare57.TabStop = false;
            btnSquare57.Text = "5, 7";
            btnSquare57.UseVisualStyleBackColor = false;
            tlpChessBoard.Controls.Add(btnSquare57, 7, 5);
            // 
            // btnSquare60
            // 
            btnSquare60.Dock = DockStyle.Fill;
            btnSquare60.FlatStyle = FlatStyle.Flat;
            btnSquare60.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            btnSquare60.Location = new Point(0, 0);
            btnSquare60.Margin = new Padding(0);
            btnSquare60.Name = "btnSquare60";
            btnSquare60.Size = new Size(62, 62);
            btnSquare60.TabIndex = 48;
            btnSquare60.TabStop = false;
            btnSquare60.Text = "6, 0";
            btnSquare60.UseVisualStyleBackColor = false;
            tlpChessBoard.Controls.Add(btnSquare60, 0, 6);
            // 
            // btnSquare61
            // 
            btnSquare61.Dock = DockStyle.Fill;
            btnSquare61.FlatStyle = FlatStyle.Flat;
            btnSquare61.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            btnSquare61.Location = new Point(0, 0);
            btnSquare61.Margin = new Padding(0);
            btnSquare61.Name = "btnSquare61";
            btnSquare61.Size = new Size(62, 62);
            btnSquare61.TabIndex = 49;
            btnSquare61.TabStop = false;
            btnSquare61.Text = "6, 1";
            btnSquare61.UseVisualStyleBackColor = false;
            tlpChessBoard.Controls.Add(btnSquare61, 1, 6);
            // 
            // btnSquare62
            // 
            btnSquare62.Dock = DockStyle.Fill;
            btnSquare62.FlatStyle = FlatStyle.Flat;
            btnSquare62.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            btnSquare62.Location = new Point(0, 0);
            btnSquare62.Margin = new Padding(0);
            btnSquare62.Name = "btnSquare62";
            btnSquare62.Size = new Size(62, 62);
            btnSquare62.TabIndex = 50;
            btnSquare62.TabStop = false;
            btnSquare62.Text = "6, 2";
            btnSquare62.UseVisualStyleBackColor = false;
            tlpChessBoard.Controls.Add(btnSquare62, 2, 6);
            // 
            // btnSquare63
            // 
            btnSquare63.Dock = DockStyle.Fill;
            btnSquare63.FlatStyle = FlatStyle.Flat;
            btnSquare63.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            btnSquare63.Location = new Point(0, 0);
            btnSquare63.Margin = new Padding(0);
            btnSquare63.Name = "btnSquare63";
            btnSquare63.Size = new Size(62, 62);
            btnSquare63.TabIndex = 51;
            btnSquare63.TabStop = false;
            btnSquare63.Text = "6, 3";
            btnSquare63.UseVisualStyleBackColor = false;
            tlpChessBoard.Controls.Add(btnSquare63, 3, 6);
            // 
            // btnSquare64
            // 
            btnSquare64.Dock = DockStyle.Fill;
            btnSquare64.FlatStyle = FlatStyle.Flat;
            btnSquare64.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            btnSquare64.Location = new Point(0, 0);
            btnSquare64.Margin = new Padding(0);
            btnSquare64.Name = "btnSquare64";
            btnSquare64.Size = new Size(62, 62);
            btnSquare64.TabIndex = 52;
            btnSquare64.TabStop = false;
            btnSquare64.Text = "6, 4";
            btnSquare64.UseVisualStyleBackColor = false;
            tlpChessBoard.Controls.Add(btnSquare64, 4, 6);
            // 
            // btnSquare65
            // 
            btnSquare65.Dock = DockStyle.Fill;
            btnSquare65.FlatStyle = FlatStyle.Flat;
            btnSquare65.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            btnSquare65.Location = new Point(0, 0);
            btnSquare65.Margin = new Padding(0);
            btnSquare65.Name = "btnSquare65";
            btnSquare65.Size = new Size(62, 62);
            btnSquare65.TabIndex = 53;
            btnSquare65.TabStop = false;
            btnSquare65.Text = "6, 5";
            btnSquare65.UseVisualStyleBackColor = false;
            tlpChessBoard.Controls.Add(btnSquare65, 5, 6);
            // 
            // btnSquare66
            // 
            btnSquare66.Dock = DockStyle.Fill;
            btnSquare66.FlatStyle = FlatStyle.Flat;
            btnSquare66.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            btnSquare66.Location = new Point(0, 0);
            btnSquare66.Margin = new Padding(0);
            btnSquare66.Name = "btnSquare66";
            btnSquare66.Size = new Size(62, 62);
            btnSquare66.TabIndex = 54;
            btnSquare66.TabStop = false;
            btnSquare66.Text = "6, 6";
            btnSquare66.UseVisualStyleBackColor = false;
            tlpChessBoard.Controls.Add(btnSquare66, 6, 6);
            // 
            // btnSquare67
            // 
            btnSquare67.Dock = DockStyle.Fill;
            btnSquare67.FlatStyle = FlatStyle.Flat;
            btnSquare67.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            btnSquare67.Location = new Point(0, 0);
            btnSquare67.Margin = new Padding(0);
            btnSquare67.Name = "btnSquare67";
            btnSquare67.Size = new Size(62, 62);
            btnSquare67.TabIndex = 55;
            btnSquare67.TabStop = false;
            btnSquare67.Text = "6, 7";
            btnSquare67.UseVisualStyleBackColor = false;
            tlpChessBoard.Controls.Add(btnSquare67, 7, 6);
            // 
            // btnSquare70
            // 
            btnSquare70.Dock = DockStyle.Fill;
            btnSquare70.FlatStyle = FlatStyle.Flat;
            btnSquare70.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            btnSquare70.Location = new Point(0, 0);
            btnSquare70.Margin = new Padding(0);
            btnSquare70.Name = "btnSquare70";
            btnSquare70.Size = new Size(62, 62);
            btnSquare70.TabIndex = 56;
            btnSquare70.TabStop = false;
            btnSquare70.Text = "7, 0";
            btnSquare70.UseVisualStyleBackColor = false;
            tlpChessBoard.Controls.Add(btnSquare70, 0, 7);
            // 
            // btnSquare71
            // 
            btnSquare71.Dock = DockStyle.Fill;
            btnSquare71.FlatStyle = FlatStyle.Flat;
            btnSquare71.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            btnSquare71.Location = new Point(0, 0);
            btnSquare71.Margin = new Padding(0);
            btnSquare71.Name = "btnSquare71";
            btnSquare71.Size = new Size(62, 62);
            btnSquare71.TabIndex = 57;
            btnSquare71.TabStop = false;
            btnSquare71.Text = "7, 1";
            btnSquare71.UseVisualStyleBackColor = false;
            tlpChessBoard.Controls.Add(btnSquare71, 1, 7);
            // 
            // btnSquare72
            // 
            btnSquare72.Dock = DockStyle.Fill;
            btnSquare72.FlatStyle = FlatStyle.Flat;
            btnSquare72.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            btnSquare72.Location = new Point(0, 0);
            btnSquare72.Margin = new Padding(0);
            btnSquare72.Name = "btnSquare72";
            btnSquare72.Size = new Size(62, 62);
            btnSquare72.TabIndex = 58;
            btnSquare72.TabStop = false;
            btnSquare72.Text = "7, 2";
            btnSquare72.UseVisualStyleBackColor = false;
            tlpChessBoard.Controls.Add(btnSquare72, 2, 7);
            // 
            // btnSquare73
            // 
            btnSquare73.Dock = DockStyle.Fill;
            btnSquare73.FlatStyle = FlatStyle.Flat;
            btnSquare73.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            btnSquare73.Location = new Point(0, 0);
            btnSquare73.Margin = new Padding(0);
            btnSquare73.Name = "btnSquare73";
            btnSquare73.Size = new Size(62, 62);
            btnSquare73.TabIndex = 59;
            btnSquare73.TabStop = false;
            btnSquare73.Text = "7, 3";
            btnSquare73.UseVisualStyleBackColor = false;
            tlpChessBoard.Controls.Add(btnSquare73, 3, 7);
            // 
            // btnSquare74
            // 
            btnSquare74.Dock = DockStyle.Fill;
            btnSquare74.FlatStyle = FlatStyle.Flat;
            btnSquare74.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            btnSquare74.Location = new Point(0, 0);
            btnSquare74.Margin = new Padding(0);
            btnSquare74.Name = "btnSquare74";
            btnSquare74.Size = new Size(62, 62);
            btnSquare74.TabIndex = 60;
            btnSquare74.TabStop = false;
            btnSquare74.Text = "7, 4";
            btnSquare74.UseVisualStyleBackColor = false;
            tlpChessBoard.Controls.Add(btnSquare74, 4, 7);
            // 
            // btnSquare75
            // 
            btnSquare75.Dock = DockStyle.Fill;
            btnSquare75.FlatStyle = FlatStyle.Flat;
            btnSquare75.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            btnSquare75.Location = new Point(0, 0);
            btnSquare75.Margin = new Padding(0);
            btnSquare75.Name = "btnSquare75";
            btnSquare75.Size = new Size(62, 62);
            btnSquare75.TabIndex = 61;
            btnSquare75.TabStop = false;
            btnSquare75.Text = "7, 5";
            btnSquare75.UseVisualStyleBackColor = false;
            tlpChessBoard.Controls.Add(btnSquare75, 5, 7);
            // 
            // btnSquare76
            // 
            btnSquare76.Dock = DockStyle.Fill;
            btnSquare76.FlatStyle = FlatStyle.Flat;
            btnSquare76.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            btnSquare76.Location = new Point(0, 0);
            btnSquare76.Margin = new Padding(0);
            btnSquare76.Name = "btnSquare76";
            btnSquare76.Size = new Size(62, 62);
            btnSquare76.TabIndex = 62;
            btnSquare76.TabStop = false;
            btnSquare76.Text = "7, 6";
            btnSquare76.UseVisualStyleBackColor = false;
            tlpChessBoard.Controls.Add(btnSquare76, 6, 7);
            // 
            // btnSquare77
            // 
            btnSquare77.Dock = DockStyle.Fill;
            btnSquare77.FlatStyle = FlatStyle.Flat;
            btnSquare77.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            btnSquare77.Location = new Point(0, 0);
            btnSquare77.Margin = new Padding(0);
            btnSquare77.Name = "btnSquare77";
            btnSquare77.Size = new Size(62, 62);
            btnSquare77.TabIndex = 63;
            btnSquare77.TabStop = false;
            btnSquare77.Text = "7, 7";
            btnSquare77.UseVisualStyleBackColor = false;
            tlpChessBoard.Controls.Add(btnSquare77, 7, 7);
            // 
            // grpOptions
            // 
            grpOptions.Controls.Add(lblPieces);
            grpOptions.Controls.Add(cmbChessPieces);
            grpOptions.Controls.Add(lblTheme);
            grpOptions.Controls.Add(cmbTheme);
            grpOptions.Controls.Add(btnClearMoves);
            grpOptions.Controls.Add(btnReset);
            grpOptions.Controls.Add(lblStatus);
            grpOptions.Location = new Point(545, 65);
            grpOptions.Name = "grpOptions";
            grpOptions.Size = new Size(215, 500);
            grpOptions.TabIndex = 2;
            grpOptions.TabStop = false;
            grpOptions.Text = "Chessboard Options";
            // 
            // lblPieces
            // 
            lblPieces.AutoSize = true;
            lblPieces.Location = new Point(20, 38);
            lblPieces.Name = "lblPieces";
            lblPieces.Size = new Size(69, 15);
            lblPieces.TabIndex = 0;
            lblPieces.Text = "Chess Piece:";
            // 
            // cmbChessPieces
            // 
            cmbChessPieces.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbChessPieces.FormattingEnabled = true;
            cmbChessPieces.Items.AddRange(new object[] { "King", "Queen", "Bishop", "Knight", "Rook" });
            cmbChessPieces.Location = new Point(20, 60);
            cmbChessPieces.Name = "cmbChessPieces";
            cmbChessPieces.Size = new Size(175, 23);
            cmbChessPieces.TabIndex = 1;
            // 
            // lblTheme
            // 
            lblTheme.AutoSize = true;
            lblTheme.Location = new Point(20, 108);
            lblTheme.Name = "lblTheme";
            lblTheme.Size = new Size(46, 15);
            lblTheme.TabIndex = 2;
            lblTheme.Text = "Theme:";
            // 
            // cmbTheme
            // 
            cmbTheme.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTheme.FormattingEnabled = true;
            cmbTheme.Items.AddRange(new object[] { "Classic", "Cool", "Warm", "Neon", "Pastel", "Nature" });
            cmbTheme.Location = new Point(20, 130);
            cmbTheme.Name = "cmbTheme";
            cmbTheme.Size = new Size(175, 23);
            cmbTheme.TabIndex = 3;
            cmbTheme.SelectedIndexChanged += CmbTheme_SelectedIndexChanged;
            // 
            // btnClearMoves
            // 
            btnClearMoves.Location = new Point(20, 185);
            btnClearMoves.Name = "btnClearMoves";
            btnClearMoves.Size = new Size(175, 34);
            btnClearMoves.TabIndex = 4;
            btnClearMoves.Text = "Clear Piece and Moves";
            btnClearMoves.UseVisualStyleBackColor = true;
            btnClearMoves.Click += BtnClearMoves_Click;
            // 
            // btnReset
            // 
            btnReset.Location = new Point(20, 230);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(175, 34);
            btnReset.TabIndex = 5;
            btnReset.Text = "Reset Application";
            btnReset.UseVisualStyleBackColor = true;
            btnReset.Click += BtnReset_Click;
            // 
            // lblStatus
            // 
            lblStatus.BorderStyle = BorderStyle.FixedSingle;
            lblStatus.Location = new Point(20, 295);
            lblStatus.Name = "lblStatus";
            lblStatus.Padding = new Padding(8);
            lblStatus.Size = new Size(175, 160);
            lblStatus.TabIndex = 6;
            lblStatus.Text = "Choose a piece, theme, and board square.";
            lblStatus.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // FrmChessBoard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(780, 590);
            Controls.Add(grpOptions);
            Controls.Add(pnlChessBoard);
            Controls.Add(lblInstructions);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FrmChessBoard";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "CST-250 Activity 2 - Chess Board";
            pnlChessBoard.ResumeLayout(false);
            tlpChessBoard.ResumeLayout(false);
            grpOptions.ResumeLayout(false);
            grpOptions.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblInstructions = null!;
        private Panel pnlChessBoard = null!;
        private TableLayoutPanel tlpChessBoard = null!;
        private GroupBox grpOptions = null!;
        private Label lblPieces = null!;
        private ComboBox cmbChessPieces = null!;
        private Label lblTheme = null!;
        private ComboBox cmbTheme = null!;
        private Button btnClearMoves = null!;
        private Button btnReset = null!;
        private Label lblStatus = null!;
        private Button btnSquare00 = null!;
        private Button btnSquare01 = null!;
        private Button btnSquare02 = null!;
        private Button btnSquare03 = null!;
        private Button btnSquare04 = null!;
        private Button btnSquare05 = null!;
        private Button btnSquare06 = null!;
        private Button btnSquare07 = null!;
        private Button btnSquare10 = null!;
        private Button btnSquare11 = null!;
        private Button btnSquare12 = null!;
        private Button btnSquare13 = null!;
        private Button btnSquare14 = null!;
        private Button btnSquare15 = null!;
        private Button btnSquare16 = null!;
        private Button btnSquare17 = null!;
        private Button btnSquare20 = null!;
        private Button btnSquare21 = null!;
        private Button btnSquare22 = null!;
        private Button btnSquare23 = null!;
        private Button btnSquare24 = null!;
        private Button btnSquare25 = null!;
        private Button btnSquare26 = null!;
        private Button btnSquare27 = null!;
        private Button btnSquare30 = null!;
        private Button btnSquare31 = null!;
        private Button btnSquare32 = null!;
        private Button btnSquare33 = null!;
        private Button btnSquare34 = null!;
        private Button btnSquare35 = null!;
        private Button btnSquare36 = null!;
        private Button btnSquare37 = null!;
        private Button btnSquare40 = null!;
        private Button btnSquare41 = null!;
        private Button btnSquare42 = null!;
        private Button btnSquare43 = null!;
        private Button btnSquare44 = null!;
        private Button btnSquare45 = null!;
        private Button btnSquare46 = null!;
        private Button btnSquare47 = null!;
        private Button btnSquare50 = null!;
        private Button btnSquare51 = null!;
        private Button btnSquare52 = null!;
        private Button btnSquare53 = null!;
        private Button btnSquare54 = null!;
        private Button btnSquare55 = null!;
        private Button btnSquare56 = null!;
        private Button btnSquare57 = null!;
        private Button btnSquare60 = null!;
        private Button btnSquare61 = null!;
        private Button btnSquare62 = null!;
        private Button btnSquare63 = null!;
        private Button btnSquare64 = null!;
        private Button btnSquare65 = null!;
        private Button btnSquare66 = null!;
        private Button btnSquare67 = null!;
        private Button btnSquare70 = null!;
        private Button btnSquare71 = null!;
        private Button btnSquare72 = null!;
        private Button btnSquare73 = null!;
        private Button btnSquare74 = null!;
        private Button btnSquare75 = null!;
        private Button btnSquare76 = null!;
        private Button btnSquare77 = null!;
    }
}