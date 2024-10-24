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
using System.Runtime.Remoting.Messaging;
using static System.Net.Mime.MediaTypeNames;

namespace DB_Upgrade0._1
{
    public partial class DBUpgrade : Form
    {
        //DB接続関連Class
        private DB_Connect DB_Connect = new DB_Connect();

        //DB接続関連のヘッダ
     //   private String LogHead = "(ログレベル)(時間) （関数名）　（処理結果）";


        // メッセージ表示域の初期化
        //(ログレベル) (時間)　（処理対象）　（処理結果）　（メッセージ）
        //
        private void MessageForm()
        {
            // フォームの設定
            richTextBox1.Text = "実行結果のメッセージ表示" + Environment.NewLine;
            richTextBox1.ScrollBars = RichTextBoxScrollBars.Vertical;


        }
        //  関数　メッセージをリッチテキストに表示するメソッド
        //  引数1（1,INFO,2,Error）引数2 引数3 引数4
        //
        private void ShowMessage(Boolean error,string fnc_name, string message, string bikou)
        {
            // 現在の日時を取得
            DateTime now = DateTime.Now;
            String LogMsg = null;

            richTextBox1.SelectionStart = richTextBox1.TextLength;
            richTextBox1.SelectionLength = 0;

            if (error == true)
            { richTextBox1.SelectionColor = Color.Red; }
            else
                richTextBox1.SelectionColor = Color.Black;


            if (error == true)
            LogMsg += "(Error)";    //ログレベル
            else
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

            richTextBox1.AppendText(LogMsg);
            richTextBox1.SelectionColor = richTextBox1.ForeColor; 

        }

        // コンストラクタ
        public DBUpgrade()
        {
            // DB接続
            //DB_Connect.Open_DB();

            InitializeComponent();
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

            Boolean error_flg = Check_DB_connect();
            if (error_flg == false)
            {
                ShowMessage(true, "Update", "DB接続エラー", "DB接続を確認してください");
                return;
            }

            jikko = DB_Connect.Check_Version(ref before_version);

            this.textBox1.Text = before_version;

            //　更新プログラムバージョンを取得
            jikko = DB_Connect.Check_Version(ref before_version);
            after_version = DB_Connect.get_aft_version();
            this.textBox2.Text = after_version;

        }

        // DB更新プログラム【メイン処理】
        // テーブル更新
        private void button1_Click(object sender, EventArgs e)
        {

            List<string> messages = new List<string>();
            List<Boolean> error = new List<Boolean>();

            Boolean error_flg = Check_DB_connect();
            if (error_flg == false)
            {
                ShowMessage(true,"Update", "DB接続エラー", "DB接続を確認してください");
                return;
            }

            ShowMessage(false, "MSG", "更新開始", "アップデートが開始しました。");

            DB_Connect.update_ddl(ref error,ref messages);
            // 実行ログ出力
            for (int i=0;i<messages.Count;i++)
            {
                ShowMessage(error[i], "Update", messages[i], "発行SQL内容");
            }
            ShowMessage(false, "MSG", "更新完了", "アップデートが完了しました。");

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {


        }
        // クリア処理　メッセージ領域をクリアする
        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();  // 以前のメッセージをクリア
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        //  DB更新SQL
        private void button3_Click(object sender, EventArgs e)
        {
            int i = 0;
            List<string> messages = new List<string>();
            Boolean error_flg = Check_DB_connect();
            if (error_flg == false)
            {
                ShowMessage(true, "Update", "DB接続エラー", "DB接続を確認してください");
                return;
            }


            DB_Connect.all_hyoji(ref messages);

            ShowMessage(false,"No", "Update SQL", "コメント");
            // 実行ログ出力
            foreach (string mess in messages)
            {
                i++;
                ShowMessage(false,Convert.ToString(i), mess, "UpdateSQL");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void DB1Path_TextChanged(object sender, EventArgs e)
        {

        }

        private void DB2Path_TextChanged(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void DB1_Click(object sender, EventArgs e)
        {
            DesigneProcess DesigneProcess = new DesigneProcess();
            String FPath;
            Boolean DB_Connect_flg;

            DB1_label.AutoSize = true;
            FPath = DesigneProcess.OpenDBFileDialog();
            DB1Path.Text = FPath;

            DB_Connect_flg = DB_Connect.Open_DB1(FPath);

            // デザイナ変更
            if (FPath == "" | DB_Connect_flg == false)
            {
                DB1_label.Text = "未接続";
                DB1_label.BackColor = Color.Red;
            }
            else
            {
                DB1_label.Text = "接続";
                DB1_label.BackColor = Color.Green;
            }

        }

        private void DB2_Click(object sender, EventArgs e)
        {
            DesigneProcess DesigneProcess = new DesigneProcess();
            String FPath;
            Boolean DB_Connect_flg;
            Boolean Fname_flg;

            DB2_label.AutoSize = true;
            FPath = DesigneProcess.OpenDBFileDialog();
            DB2Path.Text = FPath;

            DB_Connect_flg = DB_Connect.Open_DB2(FPath);

            Fname_flg = DesigneProcess.ChkFName(FPath);

            // デザイナ変更
            if (FPath == "" | DB_Connect_flg == false | Fname_flg == false)
            {
                DB2_label.Text = "未接続";
                DB2_label.BackColor = Color.Red;
            }
            else
            {
                DB2_label.Text = "接続";
                DB2_label.BackColor = Color.Green;
            }
        }
        // DB接続有無を確認、１つでも未接続の場合エラーを返す
         private Boolean Check_DB_connect()
        {
            Boolean return_flg;
            if (DB1_label.Text.Equals("未接続"))
                return_flg = false;
            else if (DB2_label.Text.Equals("未接続")) 
            {
                return_flg = false;

            }
            else
            {
                return_flg = true;
            }

            return return_flg;
        }
        // DB接続クリア
        private void DBClearButton_Click(object sender, EventArgs e)
        {

            DB_Connect.Close_DB();

            DB1_label.Text = "未接続";
            DB1_label.BackColor = Color.Red;
            DB1Path.Text = "";
            DB2_label.Text = "未接続";
            DB2_label.BackColor = Color.Red;
            DB2Path.Text = "";

            // version表記をクリア
            this.textBox1.Text = "";
            this.textBox2.Text = "";

        }
    }
}
