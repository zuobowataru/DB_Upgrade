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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DB_Upgrade0._1
{
    public partial class DBUpgrade : Form
    {
        //DB接続関連Class
        private DB_Connect DB_Connect = new DB_Connect();

        //DB接続関連のヘッダ
        private String LogHead = "(ログレベル)(時間) （関数名）　（処理結果）";


        // メッセージ表示域の初期化
        //(ログレベル) (時間)　（処理対象）　（処理結果）　（メッセージ）
        //
        private void MessageForm()
        {
            // フォームの設定
           richTextBox1.Text = "実行結果のメッセージ表示" + Environment.NewLine;
           richTextBox1.ScrollBars = RichTextBoxScrollBars.Vertical;


        }
        // メッセージをリッチテキストに表示するメソッド
        private void ShowMessage(string fnc_name,string message, string bikou)
        {
            // 現在の日時を取得
            DateTime now = DateTime.Now;
            String LogMsg = null;

            LogMsg += "(Info)";    //ログレベル
            LogMsg += "\t";    //タブ
            LogMsg += now;       //処理時間
            LogMsg += "\t";    //タブ
            LogMsg += fnc_name;  //関数名
            LogMsg += "\t";    //タブ
            LogMsg += message;　   //処理結果
            LogMsg += "\t";    //タブ
            LogMsg += bikou;    //その他メッセージ
            LogMsg += Environment.NewLine;  // 改行
            richTextBox1.Text += LogMsg;
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
        //
        // DBアップデートが必要なのかチェックする。
        //
        private void Table_Click(object sender, EventArgs e)
        {
            Boolean jikko;
            String before_version = null;
            String after_version = null;

            jikko = DB_Connect.Check_Version(ref before_version);

            this.textBox1.Text = before_version;

            //　更新プログラムバージョンを取得
            jikko = DB_Connect.Check_Version(ref before_version);
            after_version = DB_Connect.get_aft_version();
            this.textBox2.Text = after_version;

            // 現バージョンが更新バージョンDB以前の場合
            if (jikko == false)
            {
                ShowMessage("Check_Version", before_version, "DB更新対象外です");

            }
            ShowMessage("Check_Version", "", "更新前後のバージョンが想定内なら、【アップデート】ボタンを押してください");

        }      

        // DB更新プログラム
        // テーブル更新
        private void button1_Click(object sender, EventArgs e)
        {
            
            
            String after_version = null;
            List<string> messages = new List<string>();

          
            after_version = DB_Connect.version_up(ref messages);
            // 実行ログ出力
            foreach (string mess in messages)
            {
                ShowMessage("Update", mess, "発行SQL内容");
            }


        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
           

        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();  // 以前のメッセージをクリア
            richTextBox1.Text = LogHead +Environment.NewLine; //ログヘッダ
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }

}
