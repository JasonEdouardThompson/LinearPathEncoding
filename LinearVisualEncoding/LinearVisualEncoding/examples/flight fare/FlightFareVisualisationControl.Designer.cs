namespace LinearVisualEncoding.examples.flight_fare
{
    partial class FlightFareVisualisationControl
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.heightType_comboBox = new System.Windows.Forms.ComboBox();
            this.generate_button = new System.Windows.Forms.Button();
            this.heightValue_textbox = new System.Windows.Forms.TextBox();
            this.depictionInterval_ComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.colourScheme_comboBox = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.maxTime_dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.minTime_dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.maxPrice_textBox = new System.Windows.Forms.TextBox();
            this.minPrice_textBox = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.airports_checkedListBox = new System.Windows.Forms.CheckedListBox();
            this.label14 = new System.Windows.Forms.Label();
            this.chooseDataset_button = new System.Windows.Forms.Button();
            this.noTicks_textBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.diagramWidth_textBox = new System.Windows.Forms.TextBox();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.outputType_ComboBox = new System.Windows.Forms.ComboBox();
            this.airportThickness_textBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.airportThickness_textBox);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.outputType_ComboBox);
            this.groupBox2.Controls.Add(this.heightType_comboBox);
            this.groupBox2.Controls.Add(this.generate_button);
            this.groupBox2.Controls.Add(this.heightValue_textbox);
            this.groupBox2.Controls.Add(this.depictionInterval_ComboBox);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.colourScheme_comboBox);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.chooseDataset_button);
            this.groupBox2.Controls.Add(this.noTicks_textBox);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.diagramWidth_textBox);
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(440, 202);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Flight Schedule Visual Encoding";
            // 
            // heightType_comboBox
            // 
            this.heightType_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.heightType_comboBox.FormattingEnabled = true;
            this.heightType_comboBox.Items.AddRange(new object[] {
            "Diagram Height",
            "Row Height"});
            this.heightType_comboBox.Location = new System.Drawing.Point(116, 19);
            this.heightType_comboBox.Name = "heightType_comboBox";
            this.heightType_comboBox.Size = new System.Drawing.Size(99, 21);
            this.heightType_comboBox.TabIndex = 25;
            this.heightType_comboBox.SelectedIndexChanged += new System.EventHandler(this.heightType_comboBox_SelectedIndexChanged);
            // 
            // generate_button
            // 
            this.generate_button.Location = new System.Drawing.Point(297, 173);
            this.generate_button.Name = "generate_button";
            this.generate_button.Size = new System.Drawing.Size(137, 23);
            this.generate_button.TabIndex = 17;
            this.generate_button.Text = "Generate";
            this.generate_button.UseVisualStyleBackColor = true;
            this.generate_button.Click += new System.EventHandler(this.generate_button_Click);
            // 
            // heightValue_textbox
            // 
            this.heightValue_textbox.Location = new System.Drawing.Point(221, 19);
            this.heightValue_textbox.Name = "heightValue_textbox";
            this.heightValue_textbox.Size = new System.Drawing.Size(25, 20);
            this.heightValue_textbox.TabIndex = 24;
            this.heightValue_textbox.Text = "400";
            // 
            // depictionInterval_ComboBox
            // 
            this.depictionInterval_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.depictionInterval_ComboBox.FormattingEnabled = true;
            this.depictionInterval_ComboBox.Location = new System.Drawing.Point(100, 47);
            this.depictionInterval_ComboBox.Name = "depictionInterval_ComboBox";
            this.depictionInterval_ComboBox.Size = new System.Drawing.Size(121, 21);
            this.depictionInterval_ComboBox.TabIndex = 23;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Depiction Interval";
            // 
            // colourScheme_comboBox
            // 
            this.colourScheme_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.colourScheme_comboBox.FormattingEnabled = true;
            this.colourScheme_comboBox.Location = new System.Drawing.Point(313, 47);
            this.colourScheme_comboBox.Name = "colourScheme_comboBox";
            this.colourScheme_comboBox.Size = new System.Drawing.Size(121, 21);
            this.colourScheme_comboBox.TabIndex = 21;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(229, 50);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(79, 13);
            this.label15.TabIndex = 20;
            this.label15.Text = "Colour Scheme";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.maxTime_dateTimePicker);
            this.groupBox4.Controls.Add(this.minTime_dateTimePicker);
            this.groupBox4.Controls.Add(this.label19);
            this.groupBox4.Controls.Add(this.label20);
            this.groupBox4.Controls.Add(this.maxPrice_textBox);
            this.groupBox4.Controls.Add(this.minPrice_textBox);
            this.groupBox4.Controls.Add(this.label18);
            this.groupBox4.Controls.Add(this.airports_checkedListBox);
            this.groupBox4.Controls.Add(this.label14);
            this.groupBox4.Location = new System.Drawing.Point(6, 72);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(428, 99);
            this.groupBox4.TabIndex = 19;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Filter";
            // 
            // maxTime_dateTimePicker
            // 
            this.maxTime_dateTimePicker.CustomFormat = "\"yyyy.MM.dd HH:mm\"";
            this.maxTime_dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.maxTime_dateTimePicker.Location = new System.Drawing.Point(65, 68);
            this.maxTime_dateTimePicker.Name = "maxTime_dateTimePicker";
            this.maxTime_dateTimePicker.ShowUpDown = true;
            this.maxTime_dateTimePicker.Size = new System.Drawing.Size(150, 20);
            this.maxTime_dateTimePicker.TabIndex = 27;
            // 
            // minTime_dateTimePicker
            // 
            this.minTime_dateTimePicker.CustomFormat = "\"yyyy.MM.dd HH:mm\"";
            this.minTime_dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.minTime_dateTimePicker.Location = new System.Drawing.Point(65, 42);
            this.minTime_dateTimePicker.Name = "minTime_dateTimePicker";
            this.minTime_dateTimePicker.ShowUpDown = true;
            this.minTime_dateTimePicker.Size = new System.Drawing.Size(150, 20);
            this.minTime_dateTimePicker.TabIndex = 26;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(4, 72);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(56, 13);
            this.label19.TabIndex = 12;
            this.label19.Text = "Max. Time";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(6, 45);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(53, 13);
            this.label20.TabIndex = 11;
            this.label20.Text = "Min. Time";
            // 
            // maxPrice_textBox
            // 
            this.maxPrice_textBox.Location = new System.Drawing.Point(175, 16);
            this.maxPrice_textBox.Name = "maxPrice_textBox";
            this.maxPrice_textBox.Size = new System.Drawing.Size(40, 20);
            this.maxPrice_textBox.TabIndex = 10;
            // 
            // minPrice_textBox
            // 
            this.minPrice_textBox.Location = new System.Drawing.Point(66, 16);
            this.minPrice_textBox.Name = "minPrice_textBox";
            this.minPrice_textBox.Size = new System.Drawing.Size(40, 20);
            this.minPrice_textBox.TabIndex = 9;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(112, 19);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(57, 13);
            this.label18.TabIndex = 6;
            this.label18.Text = "Max. Price";
            // 
            // airports_checkedListBox
            // 
            this.airports_checkedListBox.FormattingEnabled = true;
            this.airports_checkedListBox.Location = new System.Drawing.Point(221, 12);
            this.airports_checkedListBox.Name = "airports_checkedListBox";
            this.airports_checkedListBox.Size = new System.Drawing.Size(201, 79);
            this.airports_checkedListBox.TabIndex = 4;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 19);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(54, 13);
            this.label14.TabIndex = 0;
            this.label14.Text = "Min. Price";
            // 
            // chooseDataset_button
            // 
            this.chooseDataset_button.Location = new System.Drawing.Point(154, 173);
            this.chooseDataset_button.Name = "chooseDataset_button";
            this.chooseDataset_button.Size = new System.Drawing.Size(137, 23);
            this.chooseDataset_button.TabIndex = 16;
            this.chooseDataset_button.Text = "Choose Dataset";
            this.chooseDataset_button.UseVisualStyleBackColor = true;
            this.chooseDataset_button.Click += new System.EventHandler(this.chooseDataset_button_Click);
            // 
            // noTicks_textBox
            // 
            this.noTicks_textBox.Location = new System.Drawing.Point(311, 19);
            this.noTicks_textBox.Name = "noTicks_textBox";
            this.noTicks_textBox.Size = new System.Drawing.Size(24, 20);
            this.noTicks_textBox.TabIndex = 11;
            this.noTicks_textBox.Text = "20";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(252, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 13);
            this.label8.TabIndex = 10;
            this.label8.Text = "No. Ticks";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 22);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 13);
            this.label10.TabIndex = 7;
            this.label10.Text = "Diagram Width";
            // 
            // diagramWidth_textBox
            // 
            this.diagramWidth_textBox.Location = new System.Drawing.Point(80, 19);
            this.diagramWidth_textBox.Name = "diagramWidth_textBox";
            this.diagramWidth_textBox.Size = new System.Drawing.Size(32, 20);
            this.diagramWidth_textBox.TabIndex = 6;
            this.diagramWidth_textBox.Text = "800";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "csv|*.csv";
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "csv|*.csv";
            // 
            // outputType_ComboBox
            // 
            this.outputType_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.outputType_ComboBox.FormattingEnabled = true;
            this.outputType_ComboBox.Location = new System.Drawing.Point(6, 175);
            this.outputType_ComboBox.Name = "outputType_ComboBox";
            this.outputType_ComboBox.Size = new System.Drawing.Size(142, 21);
            this.outputType_ComboBox.TabIndex = 26;
            // 
            // airportThickness_textBox
            // 
            this.airportThickness_textBox.Location = new System.Drawing.Point(404, 19);
            this.airportThickness_textBox.Name = "airportThickness_textBox";
            this.airportThickness_textBox.Size = new System.Drawing.Size(24, 20);
            this.airportThickness_textBox.TabIndex = 28;
            this.airportThickness_textBox.Text = "25";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(341, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 27;
            this.label2.Text = "Airport";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(339, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 29;
            this.label3.Text = "Thickness";
            // 
            // FlightFareVisualisationControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Name = "FlightFareVisualisationControl";
            this.Size = new System.Drawing.Size(450, 210);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox colourScheme_comboBox;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox maxPrice_textBox;
        private System.Windows.Forms.TextBox minPrice_textBox;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.CheckedListBox airports_checkedListBox;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button generate_button;
        private System.Windows.Forms.Button chooseDataset_button;
        private System.Windows.Forms.TextBox noTicks_textBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox diagramWidth_textBox;
        private System.Windows.Forms.ComboBox depictionInterval_ComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox heightType_comboBox;
        private System.Windows.Forms.TextBox heightValue_textbox;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.DateTimePicker maxTime_dateTimePicker;
        private System.Windows.Forms.DateTimePicker minTime_dateTimePicker;
        private System.Windows.Forms.ComboBox outputType_ComboBox;
        private System.Windows.Forms.TextBox airportThickness_textBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}
