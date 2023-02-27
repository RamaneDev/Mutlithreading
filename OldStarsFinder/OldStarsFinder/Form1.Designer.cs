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
            label1 = new Label();
            tbCount = new TextBox();
            ((System.ComponentModel.ISupportInitialize)picStarsBitmap).BeginInit();
            SuspendLayout();
            // 
            // picStarsBitmap
            // 
            picStarsBitmap.Image = (Image)resources.GetObject("picStarsBitmap.Image");
            picStarsBitmap.Location = new Point(55, 82);
            picStarsBitmap.Margin = new Padding(2, 1, 2, 1);
            picStarsBitmap.Name = "picStarsBitmap";
            picStarsBitmap.Size = new Size(548, 357);
            picStarsBitmap.TabIndex = 0;
            picStarsBitmap.TabStop = false;
            // 
            // butFindOldStars
            // 
            butFindOldStars.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            butFindOldStars.Location = new Point(70, 34);
            butFindOldStars.Margin = new Padding(2, 1, 2, 1);
            butFindOldStars.Name = "butFindOldStars";
            butFindOldStars.Size = new Size(132, 34);
            butFindOldStars.TabIndex = 1;
            butFindOldStars.Text = "Find old start";
            butFindOldStars.UseVisualStyleBackColor = true;
            butFindOldStars.Click += butFindOldStars_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Courier New", 18F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(345, 34);
            label1.Name = "label1";
            label1.Size = new Size(264, 27);
            label1.TabIndex = 2;
            label1.Text = "Old Starts Count :";
            // 
            // tbCount
            // 
            tbCount.Location = new Point(615, 39);
            tbCount.Name = "tbCount";
            tbCount.Size = new Size(74, 23);
            tbCount.TabIndex = 3;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(846, 540);
            Controls.Add(tbCount);
            Controls.Add(label1);
            Controls.Add(butFindOldStars);
            Controls.Add(picStarsBitmap);
            Margin = new Padding(2, 1, 2, 1);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)picStarsBitmap).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox picStarsBitmap;
        private Button butFindOldStars;
        private Label label1;
        private TextBox tbCount;
    }
}