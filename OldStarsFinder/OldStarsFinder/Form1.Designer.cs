namespace OldStarsFinder
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            picStarsBitmap = new PictureBox();
            butFindOldStars = new Button();
            ((System.ComponentModel.ISupportInitialize)picStarsBitmap).BeginInit();
            SuspendLayout();
            // 
            // picStarsBitmap
            // 
            picStarsBitmap.Image = (Image)resources.GetObject("picStarsBitmap.Image");
            picStarsBitmap.Location = new Point(102, 175);
            picStarsBitmap.Name = "picStarsBitmap";
            picStarsBitmap.Size = new Size(1017, 761);
            picStarsBitmap.TabIndex = 0;
            picStarsBitmap.TabStop = false;
            // 
            // butFindOldStars
            // 
            butFindOldStars.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            butFindOldStars.Location = new Point(130, 73);
            butFindOldStars.Name = "butFindOldStars";
            butFindOldStars.Size = new Size(246, 72);
            butFindOldStars.TabIndex = 1;
            butFindOldStars.Text = "Find old start";
            butFindOldStars.UseVisualStyleBackColor = true;
            butFindOldStars.Click += butFindOldStars_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1474, 1129);
            Controls.Add(butFindOldStars);
            Controls.Add(picStarsBitmap);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)picStarsBitmap).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox picStarsBitmap;
        private Button butFindOldStars;
    }
}