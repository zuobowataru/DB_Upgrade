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
        
        // コンストラクタ
        public DBUpgrade()
        {
            InitializeComponent();

            // DB接続
            DB_Connect.Open_DB();
        }

        private void Table_Click(object sender, EventArgs e)
        {
            DB_Connect.get_tablelist();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DB_Connect.update_tablel1();
        }
    }

}
