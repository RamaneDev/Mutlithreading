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
            label2 = new Label();
            tbTasks = new TextBox();
            ((System.ComponentModel.ISupportInitialize)picStarsBitmap).BeginInit();
            SuspendLayout();
            // 
            // picStarsBitmap
            // 
            picStarsBitmap.Image = (Image)resources.GetObject("picStarsBitmap.Image");
            picStarsBitmap.Location = new Point(164, 256);
            picStarsBitmap.Margin = new Padding(4, 2, 4, 2);
            picStarsBitmap.Name = "picStarsBitmap";
            picStarsBitmap.Size = new Size(1018, 764);
            picStarsBitmap.TabIndex = 0;
            picStarsBitmap.TabStop = false;
            // 
            // butFindOldStars
            // 
            butFindOldStars.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            butFindOldStars.Location = new Point(130, 73);
            butFindOldStars.Margin = new Padding(4, 2, 4, 2);
            butFindOldStars.Name = "butFindOldStars";
            butFindOldStars.Size = new Size(245, 73);
            butFindOldStars.TabIndex = 1;
            butFindOldStars.Text = "Find old start";
            butFindOldStars.UseVisualStyleBackColor = true;
            butFindOldStars.Click += butFindOldStars_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Courier New", 18F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(454, 73);
            label1.Margin = new Padding(6, 0, 6, 0);
            label1.Name = "label1";
            label1.Size = new Size(863, 50);
            label1.TabIndex = 2;
            label1.Text = "Number of bitmaps to divide :";
            // 
            // tbCount
            // 
            tbCount.Location = new Point(1014, 169);
            tbCount.Margin = new Padding(6);
            tbCount.Name = "tbCount";
            tbCount.Size = new Size(134, 39);
            tbCount.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Courier New", 18F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(467, 161);
            label2.Margin = new Padding(6, 0, 6, 0);
            label2.Name = "label2";
            label2.Size = new Size(544, 50);
            label2.TabIndex = 4;
            label2.Text = "Old Starts Count :";
            // 
            // tbTasks
            // 
            tbTasks.Location = new Point(1314, 81);
            tbTasks.Margin = new Padding(6);
            tbTasks.Name = "tbTasks";
            tbTasks.Size = new Size(134, 39);
            tbTasks.TabIndex = 5;
            tbTasks.Text = "8";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1493, 1163);
            Controls.Add(tbTasks);
            Controls.Add(label2);
            Controls.Add(tbCount);
            Controls.Add(label1);
            Controls.Add(butFindOldStars);
            Controls.Add(picStarsBitmap);
            Margin = new Padding(4, 2, 4, 2);
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
        private Label label2;
        private TextBox tbTasks;
    }
}