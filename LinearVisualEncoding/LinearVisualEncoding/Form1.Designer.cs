namespace LinearVisualEncoding
{
    partial class Form1
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
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog2 = new System.Windows.Forms.SaveFileDialog();
            this.flightFareGenerationControl1 = new LinearVisualEncoding.examples.flight_fare.FlightFareGenerationControl();
            this.flightFareVisualisationControl1 = new LinearVisualEncoding.examples.flight_fare.FlightFareVisualisationControl();
            this.webCommentControl1 = new LinearVisualEncoding.examples.web_comments.WebCommentControl();
            this.SuspendLayout();
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "image|*.bmp";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "etm|*.etm.txt";
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.FileName = "openFileDialog2";
            this.openFileDialog2.Filter = "comma seperated values|*.csv";
            // 
            // saveFileDialog2
            // 
            this.saveFileDialog2.Filter = "csv|*.csv";
            // 
            // flightFareGenerationControl1
            // 
            this.flightFareGenerationControl1.Location = new System.Drawing.Point(8, 189);
            this.flightFareGenerationControl1.Name = "flightFareGenerationControl1";
            this.flightFareGenerationControl1.Size = new System.Drawing.Size(444, 250);
            this.flightFareGenerationControl1.TabIndex = 25;
            // 
            // flightFareVisualisationControl1
            // 
            this.flightFareVisualisationControl1.Location = new System.Drawing.Point(8, 445);
            this.flightFareVisualisationControl1.Name = "flightFareVisualisationControl1";
            this.flightFareVisualisationControl1.Size = new System.Drawing.Size(444, 219);
            this.flightFareVisualisationControl1.TabIndex = 24;
            // 
            // webCommentControl1
            // 
            this.webCommentControl1.Location = new System.Drawing.Point(8, 12);
            this.webCommentControl1.Name = "webCommentControl1";
            this.webCommentControl1.Size = new System.Drawing.Size(448, 171);
            this.webCommentControl1.TabIndex = 23;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 657);
            this.Controls.Add(this.flightFareGenerationControl1);
            this.Controls.Add(this.flightFareVisualisationControl1);
            this.Controls.Add(this.webCommentControl1);
            this.Name = "Form1";
            this.Text = "Linear Path Encoding Examples";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.SaveFileDialog saveFileDialog2;
        private examples.web_comments.WebCommentControl webCommentControl1;
        private examples.flight_fare.FlightFareVisualisationControl flightFareVisualisationControl1;
        private examples.flight_fare.FlightFareGenerationControl flightFareGenerationControl1;
    }
}

