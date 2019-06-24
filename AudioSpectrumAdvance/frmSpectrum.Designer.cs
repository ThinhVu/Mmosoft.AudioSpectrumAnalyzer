namespace AudioSpectrumAdvance
{
    partial class frmSpectrum
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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.horizontalSpectrumVisualizer1 = new AudioSpectrumAdvance.AudioSpectrumVisualizers.HorizontalSpectrumVisualizer();
            this.circleSpectrumVisualizer1 = new AudioSpectrumAdvance.CircleSpectrumVisualizer();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(12, 12);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(281, 21);
            this.comboBox1.TabIndex = 3;
            this.comboBox1.Visible = false;
            // 
            // horizontalSpectrumVisualizer1
            // 
            this.horizontalSpectrumVisualizer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.horizontalSpectrumVisualizer1.BackColor = System.Drawing.Color.Transparent;
            this.horizontalSpectrumVisualizer1.Location = new System.Drawing.Point(12, 625);
            this.horizontalSpectrumVisualizer1.Name = "horizontalSpectrumVisualizer1";
            this.horizontalSpectrumVisualizer1.Size = new System.Drawing.Size(539, 69);
            this.horizontalSpectrumVisualizer1.TabIndex = 5;
            this.horizontalSpectrumVisualizer1.Text = "horizontalSpectrumVisualizer1";
            // 
            // circleSpectrumVisualizer1
            // 
            this.circleSpectrumVisualizer1.BackColor = System.Drawing.Color.Transparent;
            this.circleSpectrumVisualizer1.Img = null;
            this.circleSpectrumVisualizer1.Location = new System.Drawing.Point(12, 12);
            this.circleSpectrumVisualizer1.Name = "circleSpectrumVisualizer1";
            this.circleSpectrumVisualizer1.Size = new System.Drawing.Size(500, 500);
            this.circleSpectrumVisualizer1.TabIndex = 4;
            this.circleSpectrumVisualizer1.Text = "circleSpectrumVisualizer1";
            // 
            // frmSpectrum
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(563, 706);
            this.Controls.Add(this.horizontalSpectrumVisualizer1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.circleSpectrumVisualizer1);
            this.Name = "frmSpectrum";
            this.Text = "Form1";
            this.Shown += new System.EventHandler(this.frmSpectrum_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private CircleSpectrumVisualizer circleSpectrumVisualizer1;
        private AudioSpectrumVisualizers.HorizontalSpectrumVisualizer horizontalSpectrumVisualizer1;
    }
}

