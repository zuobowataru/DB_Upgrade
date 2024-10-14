using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.OleDb;
using System.IO;

namespace DB_Upgrade0._1
{
    public partial class DBUpgrade : Form
    {
        //DB接続関連Class
        private DB_Connect DB_Connect = new DB_Connect();
       

        // メッセージ表示領域
        public void MessageForm()
        {
            // フォームの設定
           richTextBox1.Text = "リッチテキストメッセージ表示" + Environment.NewLine;
           richTextBox1.ScrollBars = RichTextBoxScrollBars.Vertical;


        }
        // メッセージをリッチテキストに表示するメソッド
        private void ShowMessage(string message)
        {
            // this.richTextBox1.Clear();  // 以前のメッセージをクリア
            richTextBox1.Text += message + Environment.NewLine;
        }

        // コンストラクタ
        public DBUpgrade()
        {
            InitializeComponent();

            // DB接続
            DB_Connect.Open_DB();

            // メッセージ表示
            MessageForm();
           
        }

        private void Table_Click(object sender, EventArgs e)
        {
            DB_Connect.get_tablelist();
            ShowMessage("Hello world");
            ShowMessage("Hello world2");
            ShowMessage("Hello world3");


        }

        private void button1_Click(object sender, EventArgs e)
        {
            DB_Connect.update_tablel1();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
           

        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();  // 以前のメッセージをクリア
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }

}
