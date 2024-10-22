
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq.Expressions;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms;

namespace DB_Upgrade0._1
{
    //
    //  DB接続関連の基本クラス
    //

    public class DB_Connect
    {
        // DB接続関連
        //  App.configのappSettingsタグ内に記述した「DBConnString」のキーの値を取得して変数「connStrTemplate」に格納する
        string connStr = ConfigurationManager.AppSettings["DBConnString"];
        string dbPathStr1 = ConfigurationManager.AppSettings["DB1Path"];
        string dbPathStr2 = ConfigurationManager.AppSettings["DB2Path"];


        public OleDbConnection connection1 = new OleDbConnection();    // データベース１（更新元）接続用オブジェクト
        public OleDbConnection connection2 = new OleDbConnection();    // データベース２（更新先）接続用オブジェクト

        // 外部変数
        public String DBConnectionString1 { get; set; }
        public String DBConnectionString2 { get; set; }


        // コンストラクタ
        public DB_Connect()
        {
            // データベース1の接続設定
           // this.DBConnectionString1 = string.Format(connStr, dbPathStr1);
            
            // データベース2の接続設定
           // this.DBConnectionString2 = string.Format(connStr, dbPathStr2);

        }
        // Open_DB
        public Boolean Open_DB1(String dbPath)
        {
            try{
                // データベース1の接続設定
                this.DBConnectionString1 = string.Format(connStr, dbPath);
                connection1.ConnectionString = DBConnectionString1;
                connection1.Open();
            }
            catch { 
            
                return false;
            }

            return true;
        }
        public Boolean Open_DB2(String dbPath)
        {
            try
            {
                // データベース1の接続設定
                this.DBConnectionString1 = string.Format(connStr, dbPath);
                connection2.ConnectionString = DBConnectionString1;
                connection2.Open();
            }
            catch
            {

                return false;
            }

            return true;
        }

        public string get_aft_version() {
            // 文字列を格納するリストを作成
            List<string> SqlResults = new List<string>();
            string sql, after_version;
            int vs_count;

            // ③バージョン取得L取得
            sql = ConfigurationManager.AppSettings["SQL_Select_VSN"];
            vs_count = select_table_Rlt(connection2, sql, ref SqlResults);

            // 更新完了したときのバージョンを取得
            after_version = SqlResults[vs_count - 1];

            return after_version;
        }

        // table　更新
        public string version_up(ref List<string> messages)
        {
            // 文字列を格納するリストを作成
            List<string> SqlResults1 = new List<string>();
            List<string> SqlResults2 = new List<string>();
            List<string> SqlResults3 = new List<string>();
    
            string after_version = null;
            string sql;
            //
            //      ①更新用SQLをDB2から取得する
            //
            // 直前の取得結果をクリア


            // ①SQLを発行して、特定テーブルから値取得する
            //①アップデートSQL取得
            sql = ConfigurationManager.AppSettings["SQL_Select_UpCmd"];
            select_table_Rlt(connection2, sql, ref SqlResults1);
            messages = SqlResults1;

            // ②リピートフラグL取得
            sql = ConfigurationManager.AppSettings["SQL_Select_RepFlg"];
            select_table_Rlt(connection2, sql, ref SqlResults2);

            
            //
            //      ②SQLを発行してDB1を更新をする。
            //

            try
            {
                int i = 0;
                int j = 0;
                // SKipするSQLカラム
                List<int> skipnum = new List<int>();
                // リピート不可SSQLの位置を取得
                foreach (var flgs in SqlResults2)
                {
                    
                    i++;
                    if (string.Equals(flgs,"False"))
                    {
                        // リピート不可SQLに対して処置を考える
                        skipnum.Add(i);
                    }

                }
                i = 0;
                // アップデートSQLを実行
                foreach (var sqls1 in SqlResults1)
                {
                    i++;

                    //2度実行失敗SQLは、発行を回避する。
                    if (i == skipnum[j]) {
                        j++;
                    }
                    else
                    update_table_Rlt(connection1, sqls1);
       
                }
            }
            catch
            {

                MessageBox.Show("例外発生。SQL実行でエラーが発生しました。");

            }

            return after_version;

        }

        //更新対象バージョンかチェックする。
        //更新対象外の場合は、Falseを返して処理を抜ける
        public Boolean Check_Version(ref String write_version) {

            List<string> SqlResults1 = new List<string>();

            // ①SQLを発行して、特定テーブルから値取得する

            string sql = ConfigurationManager.AppSettings["SQL_Select_Version"];
            select_table_Rlt(connection1, sql, ref SqlResults1);

            write_version = SqlResults1[0];

            string[] fixVersions = write_version.Split('.');
            foreach (string fixVersion in fixVersions)
            {
                Console.WriteLine(fixVersion);  // 出力: apple banana cherry
            }

            // ②バージョン☑
            // 特定バージョン以上の値かどうか☑する。


            int[] numbers = new int[] { 2, 0 };

            // チェック用バージョン

            // チェック
            if (int.Parse(fixVersions[0]) >= numbers[0])
            {
                // OK

            }
            else
            {
                // NG
                return false;
            }

            return true;

        }

        // Close_DB
        public void Close_DB()
        {
            connection1.Close();
        }
        // デザインのボタンと紐づく
        // ボタン押下後、更新用SQLを全部出力する
        public void all_hyoji(ref List<string> messages) {
            List<string> SqlResults1 = new List<string>();
                        
            string sql = ConfigurationManager.AppSettings["SQL_Select_UpCmd"];
            select_table_Rlt(connection2, sql, ref SqlResults1);
            messages = SqlResults1;
        }
        // デザインのボタンと紐づく
        public void all_hyosji(ref List<string> messages)
        {
            List<string> SqlResults1 = new List<string>();

            string sql = ConfigurationManager.AppSettings["SQL_Select_UpCmd"];
            select_table_Rlt(connection2, sql, ref SqlResults1);
            messages = SqlResults1;
        }


        // DataTable型をString型に変換する関数
        // DataTableの各行と各列をカンマ区切りの形式で連結し、最終的に1つの文字列にまとめる。
        // SQLの取得結果が1行の場合のみ使う
        static string DataTableToString(DataTable table)
        {
            string result = "";

            // 各行を取得
            foreach (DataRow row in table.Rows)
            {
                foreach (var item in row.ItemArray)
                {
                    result += item.ToString();
                }
            }

            return result;
        }
        // DataTable型をString型に変換する関数
        // DataTableの各行と各列をカンマ区切りの形式で連結し、最終的に1つの文字列にまとめる。
        // 以下は出力イメージ
        // version_row
        // 2.0.0
        static string DataTableToString2(DataTable table)
        {
            string result = "";

            // 列名を取得
            foreach (DataColumn column in table.Columns)
            {
                result += column.ColumnName + ",";
            }
            result = result.TrimEnd(',') + Environment.NewLine; // 最後のカンマを削除

            // 各行を取得
            foreach (DataRow row in table.Rows)
            {
                foreach (var item in row.ItemArray)
                {
                    result += item.ToString() + ",";
                }
                result = result.TrimEnd(',') + Environment.NewLine; // 最後のカンマを削除
            }

            return result;
        }
        // DataTable型をString型に変換する関数
        // DataTableの各行と各列をカンマ区切りの形式で連結し、最終的に1つの文字列にまとめる。
        // SQLの取得結果が1行の場合のみ使う
        public List<string> DataTablesToString(DataTable table)
        {
            // 文字列を格納するリストを作成
            List<string> SqlResults = new List<string>();

            // 各行を取得
            foreach (DataRow row in table.Rows)
            {
                foreach (var item in row.ItemArray)
                {
                    SqlResults.Add(item.ToString());
                }
            }

            return SqlResults;
        }
        // select 取得
        // 接続先、SQLを指定して、その結果をString型で返す。
        // 取得したデータのカラム数を返す
        //
        public int select_table_Rlt(OleDbConnection connection, string sql, ref List<string> SQLlist) {

            DataTable dataTable = new DataTable();
            OleDbCommand command = new OleDbCommand();             // クエリ格納用オブジェクト
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(); // テーブル操作実行用オブジェクト

            command.Connection = connection;
            command.CommandText = sql;
            dataAdapter.SelectCommand = command;
            // SQL実行
            dataAdapter.Fill(dataTable);

            SQLlist = DataTablesToString(dataTable);

            // 取得カラム数を返却
            return dataTable.Rows.Count;
        }
        // update
        // 接続先、SQLを指定して、その結果をString型で返す。
        // 実行した行数を返す
        //
        public int update_table_Rlt(OleDbConnection connection, string sql)
        {

            DataTable dataTable = new DataTable();
            OleDbCommand command = new OleDbCommand();             // クエリ格納用オブジェクト
            int affectedRows = 0;
            // トランザクション開始
            command.Transaction = connection.BeginTransaction();

            command.Connection = connection;

            command.CommandText = sql;
            try
            {
                affectedRows = command.ExecuteNonQuery();
                // コミット
                command.Transaction.Commit();
                               
            }
           
            catch(Exception ex){ 
            //
            }
        
            // 取得カラム数を返却
            return affectedRows;
        }            
          
    }
}