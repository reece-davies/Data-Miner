namespace DataMiner
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.MainMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.panelChart = new System.Windows.Forms.Panel();
            this.progressBarLoading = new System.Windows.Forms.ProgressBar();
            this.labelMidYAxis = new System.Windows.Forms.Label();
            this.labelYAxisHeight = new System.Windows.Forms.Label();
            this.comboBoxDataToDisplay = new System.Windows.Forms.ComboBox();
            this.buttonChange = new System.Windows.Forms.Button();
            this.textBoxOutput = new System.Windows.Forms.TextBox();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.groupBoxOption = new System.Windows.Forms.GroupBox();
            this.radioButtonCustomer = new System.Windows.Forms.RadioButton();
            this.radioButtonInvoice = new System.Windows.Forms.RadioButton();
            this.labelRecordNumber = new System.Windows.Forms.Label();
            this.menuStripMain.SuspendLayout();
            this.panelChart.SuspendLayout();
            this.groupBoxOption.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStripMain
            // 
            this.menuStripMain.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainMenu});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(1227, 24);
            this.menuStripMain.TabIndex = 1;
            this.menuStripMain.Text = "menuStrip1";
            // 
            // MainMenu
            // 
            this.MainMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem});
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.MainMenu.Size = new System.Drawing.Size(37, 20);
            this.MainMenu.Text = "&File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            this.openFileDialog.Filter = "csv | *.csv";
            // 
            // panelChart
            // 
            this.panelChart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelChart.BackColor = System.Drawing.Color.White;
            this.panelChart.Controls.Add(this.progressBarLoading);
            this.panelChart.Controls.Add(this.labelMidYAxis);
            this.panelChart.Controls.Add(this.labelYAxisHeight);
            this.panelChart.Location = new System.Drawing.Point(12, 83);
            this.panelChart.Name = "panelChart";
            this.panelChart.Size = new System.Drawing.Size(1207, 512);
            this.panelChart.TabIndex = 2;
            this.panelChart.Paint += new System.Windows.Forms.PaintEventHandler(this.panelChart_Paint);
            this.panelChart.Resize += new System.EventHandler(this.panelChart_Resize);
            // 
            // progressBarLoading
            // 
            this.progressBarLoading.Location = new System.Drawing.Point(405, 236);
            this.progressBarLoading.Margin = new System.Windows.Forms.Padding(2);
            this.progressBarLoading.MarqueeAnimationSpeed = 12;
            this.progressBarLoading.Maximum = 13;
            this.progressBarLoading.Name = "progressBarLoading";
            this.progressBarLoading.Size = new System.Drawing.Size(351, 31);
            this.progressBarLoading.Step = 1;
            this.progressBarLoading.TabIndex = 31;
            // 
            // labelMidYAxis
            // 
            this.labelMidYAxis.AutoSize = true;
            this.labelMidYAxis.Location = new System.Drawing.Point(3, 147);
            this.labelMidYAxis.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelMidYAxis.Name = "labelMidYAxis";
            this.labelMidYAxis.Size = new System.Drawing.Size(49, 13);
            this.labelMidYAxis.TabIndex = 30;
            this.labelMidYAxis.Text = "midYAxis";
            // 
            // labelYAxisHeight
            // 
            this.labelYAxisHeight.AutoSize = true;
            this.labelYAxisHeight.Location = new System.Drawing.Point(18, 14);
            this.labelYAxisHeight.Name = "labelYAxisHeight";
            this.labelYAxisHeight.Size = new System.Drawing.Size(36, 13);
            this.labelYAxisHeight.TabIndex = 17;
            this.labelYAxisHeight.Text = "Y-Axis";
            // 
            // comboBoxDataToDisplay
            // 
            this.comboBoxDataToDisplay.FormattingEnabled = true;
            this.comboBoxDataToDisplay.Items.AddRange(new object[] {
            "Total quantity of items sold per calendar month",
            "Total value, in pounds of items sold per calendar month",
            "Total number of unique customers per month",
            "Total number of invoices generated per month",
            "Average number of items per invoice per calendar month",
            "Average spend, in pounds, per customer per month",
            "Average spend, in pounds, per invoice per calendar month"});
            this.comboBoxDataToDisplay.Location = new System.Drawing.Point(12, 26);
            this.comboBoxDataToDisplay.Name = "comboBoxDataToDisplay";
            this.comboBoxDataToDisplay.Size = new System.Drawing.Size(307, 21);
            this.comboBoxDataToDisplay.TabIndex = 4;
            this.comboBoxDataToDisplay.Text = "Choose what to display";
            this.comboBoxDataToDisplay.SelectedIndexChanged += new System.EventHandler(this.comboBoxDataToDisplay_SelectedIndexChanged);
            // 
            // buttonChange
            // 
            this.buttonChange.Location = new System.Drawing.Point(335, 25);
            this.buttonChange.Margin = new System.Windows.Forms.Padding(2);
            this.buttonChange.Name = "buttonChange";
            this.buttonChange.Size = new System.Drawing.Size(125, 21);
            this.buttonChange.TabIndex = 5;
            this.buttonChange.Text = "Click to search data";
            this.buttonChange.UseVisualStyleBackColor = true;
            this.buttonChange.Click += new System.EventHandler(this.buttonChange_Click);
            // 
            // textBoxOutput
            // 
            this.textBoxOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxOutput.Location = new System.Drawing.Point(11, 88);
            this.textBoxOutput.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxOutput.Multiline = true;
            this.textBoxOutput.Name = "textBoxOutput";
            this.textBoxOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxOutput.Size = new System.Drawing.Size(1205, 497);
            this.textBoxOutput.TabIndex = 31;
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Location = new System.Drawing.Point(583, 27);
            this.textBoxSearch.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(109, 20);
            this.textBoxSearch.TabIndex = 32;
            this.textBoxSearch.Text = "Search data here";
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(702, 29);
            this.buttonSearch.Margin = new System.Windows.Forms.Padding(2);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(87, 19);
            this.buttonSearch.TabIndex = 33;
            this.buttonSearch.Text = "Search";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // groupBoxOption
            // 
            this.groupBoxOption.Controls.Add(this.radioButtonCustomer);
            this.groupBoxOption.Controls.Add(this.radioButtonInvoice);
            this.groupBoxOption.Location = new System.Drawing.Point(479, 27);
            this.groupBoxOption.Margin = new System.Windows.Forms.Padding(2);
            this.groupBoxOption.Name = "groupBoxOption";
            this.groupBoxOption.Padding = new System.Windows.Forms.Padding(2);
            this.groupBoxOption.Size = new System.Drawing.Size(91, 51);
            this.groupBoxOption.TabIndex = 34;
            this.groupBoxOption.TabStop = false;
            this.groupBoxOption.Text = "Search options";
            // 
            // radioButtonCustomer
            // 
            this.radioButtonCustomer.AutoSize = true;
            this.radioButtonCustomer.Location = new System.Drawing.Point(4, 36);
            this.radioButtonCustomer.Margin = new System.Windows.Forms.Padding(2);
            this.radioButtonCustomer.Name = "radioButtonCustomer";
            this.radioButtonCustomer.Size = new System.Drawing.Size(83, 17);
            this.radioButtonCustomer.TabIndex = 1;
            this.radioButtonCustomer.TabStop = true;
            this.radioButtonCustomer.Text = "Customer ID";
            this.radioButtonCustomer.UseVisualStyleBackColor = true;
            // 
            // radioButtonInvoice
            // 
            this.radioButtonInvoice.AutoSize = true;
            this.radioButtonInvoice.Location = new System.Drawing.Point(4, 16);
            this.radioButtonInvoice.Margin = new System.Windows.Forms.Padding(2);
            this.radioButtonInvoice.Name = "radioButtonInvoice";
            this.radioButtonInvoice.Size = new System.Drawing.Size(74, 17);
            this.radioButtonInvoice.TabIndex = 0;
            this.radioButtonInvoice.TabStop = true;
            this.radioButtonInvoice.Text = "Invoice ID";
            this.radioButtonInvoice.UseVisualStyleBackColor = true;
            // 
            // labelRecordNumber
            // 
            this.labelRecordNumber.AutoSize = true;
            this.labelRecordNumber.Location = new System.Drawing.Point(13, 50);
            this.labelRecordNumber.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelRecordNumber.Name = "labelRecordNumber";
            this.labelRecordNumber.Size = new System.Drawing.Size(92, 13);
            this.labelRecordNumber.TabIndex = 35;
            this.labelRecordNumber.Text = "Record number: 0";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1227, 621);
            this.Controls.Add(this.labelRecordNumber);
            this.Controls.Add(this.groupBoxOption);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.textBoxSearch);
            this.Controls.Add(this.textBoxOutput);
            this.Controls.Add(this.buttonChange);
            this.Controls.Add(this.comboBoxDataToDisplay);
            this.Controls.Add(this.panelChart);
            this.Controls.Add(this.menuStripMain);
            this.MainMenuStrip = this.menuStripMain;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Menu";
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.panelChart.ResumeLayout(false);
            this.panelChart.PerformLayout();
            this.groupBoxOption.ResumeLayout(false);
            this.groupBoxOption.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem MainMenu;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Panel panelChart;
        private System.Windows.Forms.ComboBox comboBoxDataToDisplay;
        private System.Windows.Forms.Label labelYAxisHeight;
        private System.Windows.Forms.Button buttonChange;
        private System.Windows.Forms.Label labelMidYAxis;
        private System.Windows.Forms.TextBox textBoxOutput;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.GroupBox groupBoxOption;
        private System.Windows.Forms.RadioButton radioButtonCustomer;
        private System.Windows.Forms.RadioButton radioButtonInvoice;
        private System.Windows.Forms.Label labelRecordNumber;
        private System.Windows.Forms.ProgressBar progressBarLoading;
    }
}

