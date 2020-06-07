namespace Projekt_2
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
            this.pictureBoxFirst = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFirst)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxFirst
            // 
            this.pictureBoxFirst.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBoxFirst.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxFirst.Name = "pictureBoxFirst";
            this.pictureBoxFirst.Size = new System.Drawing.Size(1140, 745);
            this.pictureBoxFirst.TabIndex = 0;
            this.pictureBoxFirst.TabStop = false;
            this.pictureBoxFirst.Click += new System.EventHandler(this.pictureBoxFirst_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1140, 745);
            this.Controls.Add(this.pictureBoxFirst);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFirst)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxFirst;
    }
}

