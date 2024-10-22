namespace DB_Upgrade0._1
{
    partial class DBUpgrade
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Table = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.DB1Path = new System.Windows.Forms.TextBox();
            this.DB2Path = new System.Windows.Forms.TextBox();
            this.DB1_label = new System.Windows.Forms.Label();
            this.DB2_label = new System.Windows.Forms.Label();
            this.DB1_button = new System.Windows.Forms.Button();
            this.DB2_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(76, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(384, 33);
            this.label1.TabIndex = 0;
            this.label1.Text = "データベースアップデートツール";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button1.Location = new System.Drawing.Point(578, 52);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(87, 33);
            this.button1.TabIndex = 2;
            this.button1.Text = "アップデート";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(0, 166);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(796, 279);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = "aaa";
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(683, 54);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(89, 29);
            this.button2.TabIndex = 4;
            this.button2.Text = "実行結果クリア";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 151);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "実行結果";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(133, 78);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(81, 19);
            this.textBox1.TabIndex = 8;
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.textBox2.Location = new System.Drawing.Point(133, 129);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(81, 19);
            this.textBox2.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(129, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "更新前バージョン";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(132, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "更新後バージョン";
            // 
            // Table
            // 
            this.Table.BackColor = System.Drawing.SystemColors.Highlight;
            this.Table.Location = new System.Drawing.Point(14, 62);
            this.Table.Name = "Table";
            this.Table.Size = new System.Drawing.Size(97, 25);
            this.Table.TabIndex = 1;
            this.Table.Text = "バージョンチェック";
            this.Table.UseVisualStyleBackColor = false;
            this.Table.Click += new System.EventHandler(this.Table_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(231, 47);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(98, 23);
            this.button3.TabIndex = 12;
            this.button3.Text = "更新SQL一覧取得";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // DB1Path
            // 
            this.DB1Path.Location = new System.Drawing.Point(255, 100);
            this.DB1Path.Name = "DB1Path";
            this.DB1Path.Size = new System.Drawing.Size(533, 19);
            this.DB1Path.TabIndex = 13;
            this.DB1Path.TextChanged += new System.EventHandler(this.DB1Path_TextChanged);
            // 
            // DB2Path
            // 
            this.DB2Path.Location = new System.Drawing.Point(255, 146);
            this.DB2Path.Name = "DB2Path";
            this.DB2Path.Size = new System.Drawing.Size(533, 19);
            this.DB2Path.TabIndex = 14;
            this.DB2Path.TextChanged += new System.EventHandler(this.DB2Path_TextChanged);
            // 
            // DB1_label
            // 
            this.DB1_label.AutoSize = true;
            this.DB1_label.Location = new System.Drawing.Point(349, 81);
            this.DB1_label.Name = "DB1_label";
            this.DB1_label.Size = new System.Drawing.Size(41, 12);
            this.DB1_label.TabIndex = 15;
            this.DB1_label.Text = "未接続";
            // 
            // DB2_label
            // 
            this.DB2_label.AutoSize = true;
            this.DB2_label.Location = new System.Drawing.Point(350, 128);
            this.DB2_label.Name = "DB2_label";
            this.DB2_label.Size = new System.Drawing.Size(41, 12);
            this.DB2_label.TabIndex = 16;
            this.DB2_label.Text = "未接続";
            // 
            // DB1_button
            // 
            this.DB1_button.Location = new System.Drawing.Point(255, 74);
            this.DB1_button.Name = "DB1_button";
            this.DB1_button.Size = new System.Drawing.Size(81, 25);
            this.DB1_button.TabIndex = 17;
            this.DB1_button.Text = "更新元DB1";
            this.DB1_button.UseVisualStyleBackColor = true;
            this.DB1_button.Click += new System.EventHandler(this.DB1_Click);
            // 
            // DB2_button
            // 
            this.DB2_button.Location = new System.Drawing.Point(255, 120);
            this.DB2_button.Name = "DB2_button";
            this.DB2_button.Size = new System.Drawing.Size(81, 26);
            this.DB2_button.TabIndex = 18;
            this.DB2_button.Text = "SQL格納DB2";
            this.DB2_button.UseVisualStyleBackColor = true;
            this.DB2_button.Click += new System.EventHandler(this.DB2_Click);
            // 
            // DBUpgrade
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.DB2_button);
            this.Controls.Add(this.DB1_button);
            this.Controls.Add(this.DB2_label);
            this.Controls.Add(this.DB1_label);
            this.Controls.Add(this.DB2Path);
            this.Controls.Add(this.DB1Path);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Table);
            this.Controls.Add(this.label1);
            this.Name = "DBUpgrade";
            this.Text = "DB_Upgrade";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button Table;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox DB1Path;
        private System.Windows.Forms.TextBox DB2Path;
        private System.Windows.Forms.Label DB1_label;
        private System.Windows.Forms.Label DB2_label;
        private System.Windows.Forms.Button DB1_button;
        private System.Windows.Forms.Button DB2_button;
    }
}

