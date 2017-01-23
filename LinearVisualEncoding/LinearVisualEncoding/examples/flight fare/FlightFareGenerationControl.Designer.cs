namespace LinearVisualEncoding.examples.flight_fare
{
    partial class FlightFareGenerationControl
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.maxFare_textBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.minFare_textBox = new System.Windows.Forms.TextBox();
            this.isMainAirport_checkBox = new System.Windows.Forms.CheckBox();
            this.flightDuration_textBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.noFlights_textBox = new System.Windows.Forms.TextBox();
            this.airports_listBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.outgoingFlights_radioButton = new System.Windows.Forms.RadioButton();
            this.incomingFlights_radioButton = new System.Windows.Forms.RadioButton();
            this.textBox19 = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.maxCheapFare_textBox = new System.Windows.Forms.TextBox();
            this.minCheapFare_textBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.maxExpensiveFare_textBox = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.minExpensiveFare_textBox = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.outlierFareProbability_textBox = new System.Windows.Forms.TextBox();
            this.generate_button = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox1);
            this.groupBox3.Controls.Add(this.airports_listBox);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.outgoingFlights_radioButton);
            this.groupBox3.Controls.Add(this.incomingFlights_radioButton);
            this.groupBox3.Controls.Add(this.textBox19);
            this.groupBox3.Controls.Add(this.groupBox5);
            this.groupBox3.Controls.Add(this.generate_button);
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(440, 240);
            this.groupBox3.TabIndex = 23;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Flight Schedule Generator";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.maxFare_textBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.minFare_textBox);
            this.groupBox1.Controls.Add(this.isMainAirport_checkBox);
            this.groupBox1.Controls.Add(this.flightDuration_textBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label23);
            this.groupBox1.Controls.Add(this.noFlights_textBox);
            this.groupBox1.Location = new System.Drawing.Point(176, 66);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(118, 134);
            this.groupBox1.TabIndex = 32;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Airport Info";
            // 
            // maxFare_textBox
            // 
            this.maxFare_textBox.Location = new System.Drawing.Point(79, 104);
            this.maxFare_textBox.Name = "maxFare_textBox";
            this.maxFare_textBox.Size = new System.Drawing.Size(33, 20);
            this.maxFare_textBox.TabIndex = 41;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 42;
            this.label3.Text = "Max Fare";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 40;
            this.label4.Text = "Min Fare";
            // 
            // minFare_textBox
            // 
            this.minFare_textBox.Location = new System.Drawing.Point(79, 81);
            this.minFare_textBox.Name = "minFare_textBox";
            this.minFare_textBox.Size = new System.Drawing.Size(33, 20);
            this.minFare_textBox.TabIndex = 39;
            // 
            // isMainAirport_checkBox
            // 
            this.isMainAirport_checkBox.AutoSize = true;
            this.isMainAirport_checkBox.Location = new System.Drawing.Point(6, 19);
            this.isMainAirport_checkBox.Name = "isMainAirport_checkBox";
            this.isMainAirport_checkBox.Size = new System.Drawing.Size(90, 17);
            this.isMainAirport_checkBox.TabIndex = 33;
            this.isMainAirport_checkBox.Text = "is main airport";
            this.isMainAirport_checkBox.UseVisualStyleBackColor = true;
            this.isMainAirport_checkBox.Leave += new System.EventHandler(this.updateAirport_Leave);
            // 
            // flightDuration_textBox
            // 
            this.flightDuration_textBox.Location = new System.Drawing.Point(79, 59);
            this.flightDuration_textBox.Name = "flightDuration_textBox";
            this.flightDuration_textBox.Size = new System.Drawing.Size(33, 20);
            this.flightDuration_textBox.TabIndex = 37;
            this.flightDuration_textBox.Leave += new System.EventHandler(this.updateAirport_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 38;
            this.label2.Text = "Duration (hrs)";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(16, 39);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(57, 13);
            this.label23.TabIndex = 32;
            this.label23.Text = "No. Flights";
            // 
            // noFlights_textBox
            // 
            this.noFlights_textBox.Location = new System.Drawing.Point(79, 36);
            this.noFlights_textBox.Name = "noFlights_textBox";
            this.noFlights_textBox.Size = new System.Drawing.Size(33, 20);
            this.noFlights_textBox.TabIndex = 31;
            this.noFlights_textBox.Leave += new System.EventHandler(this.updateAirport_Leave);
            // 
            // airports_listBox
            // 
            this.airports_listBox.FormattingEnabled = true;
            this.airports_listBox.Location = new System.Drawing.Point(13, 66);
            this.airports_listBox.Name = "airports_listBox";
            this.airports_listBox.Size = new System.Drawing.Size(157, 134);
            this.airports_listBox.TabIndex = 36;
            this.airports_listBox.SelectedIndexChanged += new System.EventHandler(this.airports_listBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 212);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 13);
            this.label1.TabIndex = 32;
            this.label1.Text = "Flights from the main airport are";
            // 
            // outgoingFlights_radioButton
            // 
            this.outgoingFlights_radioButton.AutoSize = true;
            this.outgoingFlights_radioButton.Location = new System.Drawing.Point(231, 210);
            this.outgoingFlights_radioButton.Name = "outgoingFlights_radioButton";
            this.outgoingFlights_radioButton.Size = new System.Drawing.Size(66, 17);
            this.outgoingFlights_radioButton.TabIndex = 35;
            this.outgoingFlights_radioButton.TabStop = true;
            this.outgoingFlights_radioButton.Text = "outgoing";
            this.outgoingFlights_radioButton.UseVisualStyleBackColor = true;
            // 
            // incomingFlights_radioButton
            // 
            this.incomingFlights_radioButton.AutoSize = true;
            this.incomingFlights_radioButton.Checked = true;
            this.incomingFlights_radioButton.Location = new System.Drawing.Point(166, 210);
            this.incomingFlights_radioButton.Name = "incomingFlights_radioButton";
            this.incomingFlights_radioButton.Size = new System.Drawing.Size(67, 17);
            this.incomingFlights_radioButton.TabIndex = 34;
            this.incomingFlights_radioButton.TabStop = true;
            this.incomingFlights_radioButton.Text = "incoming";
            this.incomingFlights_radioButton.UseVisualStyleBackColor = true;
            // 
            // textBox19
            // 
            this.textBox19.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox19.Location = new System.Drawing.Point(13, 28);
            this.textBox19.Multiline = true;
            this.textBox19.Name = "textBox19";
            this.textBox19.ReadOnly = true;
            this.textBox19.Size = new System.Drawing.Size(418, 32);
            this.textBox19.TabIndex = 30;
            this.textBox19.Text = "Generate an flight schedule centred around a single airport. ";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.maxCheapFare_textBox);
            this.groupBox5.Controls.Add(this.minCheapFare_textBox);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.maxExpensiveFare_textBox);
            this.groupBox5.Controls.Add(this.label13);
            this.groupBox5.Controls.Add(this.minExpensiveFare_textBox);
            this.groupBox5.Controls.Add(this.label12);
            this.groupBox5.Controls.Add(this.outlierFareProbability_textBox);
            this.groupBox5.Location = new System.Drawing.Point(303, 66);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(128, 134);
            this.groupBox5.TabIndex = 28;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Outlier";
            // 
            // maxCheapFare_textBox
            // 
            this.maxCheapFare_textBox.Location = new System.Drawing.Point(51, 104);
            this.maxCheapFare_textBox.Name = "maxCheapFare_textBox";
            this.maxCheapFare_textBox.Size = new System.Drawing.Size(40, 20);
            this.maxCheapFare_textBox.TabIndex = 33;
            this.maxCheapFare_textBox.Text = "100";
            // 
            // minCheapFare_textBox
            // 
            this.minCheapFare_textBox.Location = new System.Drawing.Point(9, 104);
            this.minCheapFare_textBox.Name = "minCheapFare_textBox";
            this.minCheapFare_textBox.Size = new System.Drawing.Size(40, 20);
            this.minCheapFare_textBox.TabIndex = 32;
            this.minCheapFare_textBox.Text = "70";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 88);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 13);
            this.label5.TabIndex = 31;
            this.label5.Text = "Cheap (min,max)";
            // 
            // maxExpensiveFare_textBox
            // 
            this.maxExpensiveFare_textBox.Location = new System.Drawing.Point(51, 62);
            this.maxExpensiveFare_textBox.Name = "maxExpensiveFare_textBox";
            this.maxExpensiveFare_textBox.Size = new System.Drawing.Size(40, 20);
            this.maxExpensiveFare_textBox.TabIndex = 30;
            this.maxExpensiveFare_textBox.Text = "1200";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 43);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(103, 13);
            this.label13.TabIndex = 29;
            this.label13.Text = "Expensive (min,max)";
            // 
            // minExpensiveFare_textBox
            // 
            this.minExpensiveFare_textBox.Location = new System.Drawing.Point(9, 62);
            this.minExpensiveFare_textBox.Name = "minExpensiveFare_textBox";
            this.minExpensiveFare_textBox.Size = new System.Drawing.Size(40, 20);
            this.minExpensiveFare_textBox.TabIndex = 28;
            this.minExpensiveFare_textBox.Text = "800";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 19);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 13);
            this.label12.TabIndex = 27;
            this.label12.Text = "Pr. of Outlier";
            // 
            // outlierFareProbability_textBox
            // 
            this.outlierFareProbability_textBox.Location = new System.Drawing.Point(77, 16);
            this.outlierFareProbability_textBox.Name = "outlierFareProbability_textBox";
            this.outlierFareProbability_textBox.Size = new System.Drawing.Size(40, 20);
            this.outlierFareProbability_textBox.TabIndex = 26;
            this.outlierFareProbability_textBox.Text = "0.15";
            // 
            // generate_button
            // 
            this.generate_button.Location = new System.Drawing.Point(303, 207);
            this.generate_button.Name = "generate_button";
            this.generate_button.Size = new System.Drawing.Size(128, 23);
            this.generate_button.TabIndex = 18;
            this.generate_button.Text = "Generate";
            this.generate_button.UseVisualStyleBackColor = true;
            this.generate_button.Click += new System.EventHandler(this.generate_button_Click);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "csv|*.csv";
            // 
            // FlightFareGenerationControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox3);
            this.Name = "FlightFareGenerationControl";
            this.Size = new System.Drawing.Size(448, 252);
            this.Load += new System.EventHandler(this.FlightFareGenerationControl_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox isMainAirport_checkBox;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox noFlights_textBox;
        private System.Windows.Forms.TextBox textBox19;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox maxExpensiveFare_textBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox minExpensiveFare_textBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox outlierFareProbability_textBox;
        private System.Windows.Forms.Button generate_button;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton outgoingFlights_radioButton;
        private System.Windows.Forms.RadioButton incomingFlights_radioButton;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ListBox airports_listBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox flightDuration_textBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox maxFare_textBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox minFare_textBox;
        private System.Windows.Forms.TextBox maxCheapFare_textBox;
        private System.Windows.Forms.TextBox minCheapFare_textBox;
        private System.Windows.Forms.Label label5;
    }
}
