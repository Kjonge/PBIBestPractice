namespace PBIBestPractice
{
    partial class BestPracticeAnalyzer
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
            this.btnExit = new System.Windows.Forms.Button();
            this.btnAnalyzeModel = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(1020, 519);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(153, 39);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnAnalyzeModel
            // 
            this.btnAnalyzeModel.Location = new System.Drawing.Point(12, 519);
            this.btnAnalyzeModel.Name = "btnAnalyzeModel";
            this.btnAnalyzeModel.Size = new System.Drawing.Size(218, 39);
            this.btnAnalyzeModel.TabIndex = 4;
            this.btnAnalyzeModel.Text = "Analyze model";
            this.btnAnalyzeModel.UseVisualStyleBackColor = true;
            this.btnAnalyzeModel.Click += new System.EventHandler(this.btnAnalyzeModel_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 12);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(1173, 484);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = "";
            // 
            // BestPracticeAnalyzer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1195, 592);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnAnalyzeModel);
            this.Controls.Add(this.richTextBox1);
            this.Name = "BestPracticeAnalyzer";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnAnalyzeModel;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}

