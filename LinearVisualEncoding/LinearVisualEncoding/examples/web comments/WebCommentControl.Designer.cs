namespace LinearVisualEncoding.examples.web_comments
{
    partial class WebCommentControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.singleDatasetWorker = new System.ComponentModel.BackgroundWorker();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.folderDatasetWorker = new System.ComponentModel.BackgroundWorker();
            this.diagramWidth_textbox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.heightValue_textbox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.numberOfTicks_textbox = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chooseSingleDataset_button = new System.Windows.Forms.Button();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.commentBrowsingOrder_comboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.desiredNoClicks_textbox = new System.Windows.Forms.TextBox();
            this.chooseDatasetForSequence_button = new System.Windows.Forms.Button();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.chooseFolder_button = new System.Windows.Forms.Button();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.heightType_comboBox = new System.Windows.Forms.ComboBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.sequenceDatasetWorker = new System.ComponentModel.BackgroundWorker();
            this.outputType_ComboBox = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // singleDatasetWorker
            // 
            this.singleDatasetWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.singleDatasetWorker_DoWork);
            this.singleDatasetWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.datasetWorker_RunWorkerCompleted);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "etm|*.etm.txt";
            // 
            // folderDatasetWorker
            // 
            this.folderDatasetWorker.WorkerReportsProgress = true;
            this.folderDatasetWorker.WorkerSupportsCancellation = true;
            this.folderDatasetWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.folderDatasetWorker_DoWork);
            this.folderDatasetWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.datasetWorker_ProgressChanged);
            this.folderDatasetWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.datasetWorker_RunWorkerCompleted);
            // 
            // diagramWidth_textbox
            // 
            this.diagramWidth_textbox.Location = new System.Drawing.Point(93, 17);
            this.diagramWidth_textbox.Name = "diagramWidth_textbox";
            this.diagramWidth_textbox.Size = new System.Drawing.Size(40, 20);
            this.diagramWidth_textbox.TabIndex = 0;
            this.diagramWidth_textbox.Text = "800";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Diagram Width";
            // 
            // heightValue_textbox
            // 
            this.heightValue_textbox.Location = new System.Drawing.Point(285, 16);
            this.heightValue_textbox.Name = "heightValue_textbox";
            this.heightValue_textbox.Size = new System.Drawing.Size(40, 20);
            this.heightValue_textbox.TabIndex = 2;
            this.heightValue_textbox.Text = "400";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(331, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "No. Ticks";
            // 
            // numberOfTicks_textbox
            // 
            this.numberOfTicks_textbox.Location = new System.Drawing.Point(390, 17);
            this.numberOfTicks_textbox.Name = "numberOfTicks_textbox";
            this.numberOfTicks_textbox.Size = new System.Drawing.Size(40, 20);
            this.numberOfTicks_textbox.TabIndex = 5;
            this.numberOfTicks_textbox.Text = "20";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.outputType_ComboBox);
            this.panel1.Controls.Add(this.chooseSingleDataset_button);
            this.panel1.Controls.Add(this.textBox4);
            this.panel1.Location = new System.Drawing.Point(10, 45);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(136, 96);
            this.panel1.TabIndex = 13;
            // 
            // chooseSingleDataset_button
            // 
            this.chooseSingleDataset_button.Location = new System.Drawing.Point(2, 68);
            this.chooseSingleDataset_button.Name = "chooseSingleDataset_button";
            this.chooseSingleDataset_button.Size = new System.Drawing.Size(129, 23);
            this.chooseSingleDataset_button.TabIndex = 14;
            this.chooseSingleDataset_button.Text = "Choose Dataset";
            this.chooseSingleDataset_button.UseVisualStyleBackColor = true;
            this.chooseSingleDataset_button.Click += new System.EventHandler(this.chooseSingleDataset_button_Click);
            // 
            // textBox4
            // 
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox4.Location = new System.Drawing.Point(3, 3);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(125, 43);
            this.textBox4.TabIndex = 13;
            this.textBox4.Text = "Generate a visual representation for a dataset";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.commentBrowsingOrder_comboBox);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.desiredNoClicks_textbox);
            this.panel2.Controls.Add(this.chooseDatasetForSequence_button);
            this.panel2.Controls.Add(this.textBox5);
            this.panel2.Location = new System.Drawing.Point(294, 45);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(136, 96);
            this.panel2.TabIndex = 15;
            // 
            // commentBrowsingOrder_comboBox
            // 
            this.commentBrowsingOrder_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.commentBrowsingOrder_comboBox.FormattingEnabled = true;
            this.commentBrowsingOrder_comboBox.Items.AddRange(new object[] {
            "Diagram Height",
            "Row Height"});
            this.commentBrowsingOrder_comboBox.Location = new System.Drawing.Point(4, 43);
            this.commentBrowsingOrder_comboBox.Name = "commentBrowsingOrder_comboBox";
            this.commentBrowsingOrder_comboBox.Size = new System.Drawing.Size(127, 21);
            this.commentBrowsingOrder_comboBox.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "comments ordered";
            // 
            // desiredNoClicks_textbox
            // 
            this.desiredNoClicks_textbox.Location = new System.Drawing.Point(7, 19);
            this.desiredNoClicks_textbox.Name = "desiredNoClicks_textbox";
            this.desiredNoClicks_textbox.Size = new System.Drawing.Size(23, 20);
            this.desiredNoClicks_textbox.TabIndex = 15;
            this.desiredNoClicks_textbox.Text = "5";
            // 
            // chooseDatasetForSequence_button
            // 
            this.chooseDatasetForSequence_button.Location = new System.Drawing.Point(2, 68);
            this.chooseDatasetForSequence_button.Name = "chooseDatasetForSequence_button";
            this.chooseDatasetForSequence_button.Size = new System.Drawing.Size(129, 23);
            this.chooseDatasetForSequence_button.TabIndex = 14;
            this.chooseDatasetForSequence_button.Text = "Choose Dataset";
            this.chooseDatasetForSequence_button.UseVisualStyleBackColor = true;
            this.chooseDatasetForSequence_button.Click += new System.EventHandler(this.chooseDatasetForSequence_button_Click);
            // 
            // textBox5
            // 
            this.textBox5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox5.Location = new System.Drawing.Point(3, 3);
            this.textBox5.Multiline = true;
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new System.Drawing.Size(125, 25);
            this.textBox5.TabIndex = 13;
            this.textBox5.Text = "Simulate a user browsing";
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.chooseFolder_button);
            this.panel3.Controls.Add(this.textBox6);
            this.panel3.Location = new System.Drawing.Point(152, 45);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(136, 96);
            this.panel3.TabIndex = 15;
            // 
            // chooseFolder_button
            // 
            this.chooseFolder_button.Location = new System.Drawing.Point(2, 68);
            this.chooseFolder_button.Name = "chooseFolder_button";
            this.chooseFolder_button.Size = new System.Drawing.Size(129, 23);
            this.chooseFolder_button.TabIndex = 14;
            this.chooseFolder_button.Text = "Choose Folder";
            this.chooseFolder_button.UseVisualStyleBackColor = true;
            this.chooseFolder_button.Click += new System.EventHandler(this.chooseFolder_button_Click);
            // 
            // textBox6
            // 
            this.textBox6.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox6.Location = new System.Drawing.Point(3, 3);
            this.textBox6.Multiline = true;
            this.textBox6.Name = "textBox6";
            this.textBox6.ReadOnly = true;
            this.textBox6.Size = new System.Drawing.Size(125, 54);
            this.textBox6.TabIndex = 13;
            this.textBox6.Text = "Generate a visual representation for each dataset in the folder";
            // 
            // heightType_comboBox
            // 
            this.heightType_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.heightType_comboBox.FormattingEnabled = true;
            this.heightType_comboBox.Items.AddRange(new object[] {
            "Diagram Height",
            "Row Height"});
            this.heightType_comboBox.Location = new System.Drawing.Point(152, 16);
            this.heightType_comboBox.Name = "heightType_comboBox";
            this.heightType_comboBox.Size = new System.Drawing.Size(127, 21);
            this.heightType_comboBox.TabIndex = 16;
            this.heightType_comboBox.SelectedIndexChanged += new System.EventHandler(this.heightType_comboBox_SelectedIndexChanged);
            // 
            // progressBar
            // 
            this.progressBar.Enabled = false;
            this.progressBar.Location = new System.Drawing.Point(10, 147);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(420, 14);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar.TabIndex = 17;
            this.progressBar.Visible = false;
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.heightType_comboBox);
            this.groupBox.Controls.Add(this.progressBar);
            this.groupBox.Controls.Add(this.panel3);
            this.groupBox.Controls.Add(this.panel2);
            this.groupBox.Controls.Add(this.panel1);
            this.groupBox.Controls.Add(this.numberOfTicks_textbox);
            this.groupBox.Controls.Add(this.label7);
            this.groupBox.Controls.Add(this.heightValue_textbox);
            this.groupBox.Controls.Add(this.label4);
            this.groupBox.Controls.Add(this.diagramWidth_textbox);
            this.groupBox.Location = new System.Drawing.Point(0, 0);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(440, 166);
            this.groupBox.TabIndex = 21;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "Web Comments";
            // 
            // sequenceDatasetWorker
            // 
            this.sequenceDatasetWorker.WorkerReportsProgress = true;
            this.sequenceDatasetWorker.WorkerSupportsCancellation = true;
            this.sequenceDatasetWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.sequenceDatasetWorker_DoWork);
            this.sequenceDatasetWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.datasetWorker_ProgressChanged);
            this.sequenceDatasetWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.datasetWorker_RunWorkerCompleted);
            // 
            // outputType_ComboBox
            // 
            this.outputType_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.outputType_ComboBox.FormattingEnabled = true;
            this.outputType_ComboBox.Items.AddRange(new object[] {
            "output HTML",
            "output png"});
            this.outputType_ComboBox.Location = new System.Drawing.Point(3, 43);
            this.outputType_ComboBox.Name = "outputType_ComboBox";
            this.outputType_ComboBox.Size = new System.Drawing.Size(127, 21);
            this.outputType_ComboBox.TabIndex = 17;
            // 
            // WebCommentControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox);
            this.Name = "WebCommentControl";
            this.Size = new System.Drawing.Size(443, 168);
            this.Load += new System.EventHandler(this.WebCommentControl_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.ComponentModel.BackgroundWorker singleDatasetWorker;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.ComponentModel.BackgroundWorker folderDatasetWorker;
        private System.Windows.Forms.TextBox diagramWidth_textbox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox heightValue_textbox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox numberOfTicks_textbox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button chooseSingleDataset_button;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button chooseDatasetForSequence_button;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button chooseFolder_button;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.ComboBox heightType_comboBox;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.GroupBox groupBox;
        private System.ComponentModel.BackgroundWorker sequenceDatasetWorker;
        private System.Windows.Forms.ComboBox commentBrowsingOrder_comboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox desiredNoClicks_textbox;
        private System.Windows.Forms.ComboBox outputType_ComboBox;
    }
}
