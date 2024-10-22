using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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




    }


}
