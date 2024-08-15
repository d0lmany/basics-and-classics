namespace winform
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.outUser = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.outPost = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.outComment = new System.Windows.Forms.ListBox();
            this.start = new System.Windows.Forms.Button();
            this.outPhoto = new System.Windows.Forms.PictureBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.outPhotoList = new System.Windows.Forms.ListBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.left = new System.Windows.Forms.Button();
            this.right = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.outPhoto)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.outUser);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(269, 223);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "юзеры";
            // 
            // outUser
            // 
            this.outUser.FormattingEnabled = true;
            this.outUser.ItemHeight = 16;
            this.outUser.Location = new System.Drawing.Point(6, 21);
            this.outUser.Name = "outUser";
            this.outUser.Size = new System.Drawing.Size(257, 196);
            this.outUser.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.outPost);
            this.groupBox2.Location = new System.Drawing.Point(287, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(269, 223);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "посты";
            // 
            // outPost
            // 
            this.outPost.FormattingEnabled = true;
            this.outPost.ItemHeight = 16;
            this.outPost.Location = new System.Drawing.Point(6, 21);
            this.outPost.Name = "outPost";
            this.outPost.Size = new System.Drawing.Size(257, 196);
            this.outPost.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.outComment);
            this.groupBox3.Location = new System.Drawing.Point(562, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(269, 223);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "коменты";
            // 
            // outComment
            // 
            this.outComment.FormattingEnabled = true;
            this.outComment.ItemHeight = 16;
            this.outComment.Location = new System.Drawing.Point(6, 21);
            this.outComment.Name = "outComment";
            this.outComment.Size = new System.Drawing.Size(257, 196);
            this.outComment.TabIndex = 0;
            // 
            // start
            // 
            this.start.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.start.Location = new System.Drawing.Point(562, 241);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(269, 111);
            this.start.TabIndex = 3;
            this.start.Text = "fetch!";
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // outPhoto
            // 
            this.outPhoto.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.outPhoto.Location = new System.Drawing.Point(6, 21);
            this.outPhoto.Name = "outPhoto";
            this.outPhoto.Size = new System.Drawing.Size(257, 196);
            this.outPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.outPhoto.TabIndex = 4;
            this.outPhoto.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.outPhotoList);
            this.groupBox4.Location = new System.Drawing.Point(12, 241);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(269, 223);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "фотки";
            // 
            // outPhotoList
            // 
            this.outPhotoList.FormattingEnabled = true;
            this.outPhotoList.ItemHeight = 16;
            this.outPhotoList.Location = new System.Drawing.Point(6, 21);
            this.outPhotoList.Name = "outPhotoList";
            this.outPhotoList.Size = new System.Drawing.Size(257, 196);
            this.outPhotoList.TabIndex = 0;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.outPhoto);
            this.groupBox5.Location = new System.Drawing.Point(287, 241);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(269, 223);
            this.groupBox5.TabIndex = 6;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "фото";
            // 
            // left
            // 
            this.left.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.left.Location = new System.Drawing.Point(562, 358);
            this.left.Name = "left";
            this.left.Size = new System.Drawing.Size(134, 106);
            this.left.TabIndex = 7;
            this.left.Text = "<";
            this.left.UseVisualStyleBackColor = true;
            this.left.Click += new System.EventHandler(this.left_Click);
            // 
            // right
            // 
            this.right.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.right.Location = new System.Drawing.Point(697, 358);
            this.right.Name = "right";
            this.right.Size = new System.Drawing.Size(134, 106);
            this.right.TabIndex = 8;
            this.right.Text = ">";
            this.right.UseVisualStyleBackColor = true;
            this.right.Click += new System.EventHandler(this.right_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 479);
            this.Controls.Add(this.right);
            this.Controls.Add(this.left);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.start);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.outPhoto)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button start;
        private System.Windows.Forms.ListBox outPost;
        private System.Windows.Forms.ListBox outComment;
        private System.Windows.Forms.ListBox outUser;
        private System.Windows.Forms.PictureBox outPhoto;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ListBox outPhotoList;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button left;
        private System.Windows.Forms.Button right;
    }
}

