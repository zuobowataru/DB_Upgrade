using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace DB_Upgrade0._1
{
    class DesigneProcess
    {
        public string OpenDBFileDialog() {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "ファイルを開く";
            openFileDialog.Filter = "dbファイル (*.mdb)|*.mdb|すべてのファイル (*.*)|*.*";
            string filePath = "";
            openFileDialog.Multiselect = false;
            // 初期ディレクトリを設定
            openFileDialog.InitialDirectory = Environment.CurrentDirectory;

            //        openFileDialog.ShowDialog();
            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                //  filePath = openFileDialog.FileName;
                //  MessageBox.Show("選択されたファイル: " + filePath);
                return null;
            }
      
            filePath = openFileDialog.FileName;
            return filePath;
        }

        // ファイル名が特定値かチェックする
        // 引数　ファイルの絶対パス
        public Boolean ChkFName(String FPath)
        {
            String kotei = "UpdateSQL.mdb";

            // 特定の部分文字列を含んでいるかを確認
            bool containsSubstring = FPath.Contains(kotei);

            
            return containsSubstring;
        }
    }


}
