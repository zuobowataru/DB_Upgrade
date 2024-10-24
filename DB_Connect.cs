
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.IO;
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

        }
        //　関数名　　Open_DB1
        //　概要      DB1への接続を確立する 
        //  引数      接続先のDBパス
        //　返却値    boolean型  true  false
        //
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
        //　関数名　　Open_DB2
        //　概要      DB2への接続を確立する 
        //  引数      接続先のDBパス
        //　返却値    boolean型  true  false
        //
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

            // DBUpdate後のバージョン取得
            sql = ConfigurationManager.AppSettings["SQL_Select_VSN"];
            vs_count = select_table_Rlt(connection2, sql, ref SqlResults);

            // 更新完了したときのバージョンを取得
            after_version = SqlResults[vs_count - 1];

            return after_version;
        }
        //　関数名　　update_ddl
        //　概要      DB更新処理（メイン） 
        //  引数      boolean型　エラー有無　string型　メッセージ
        //　返却値    string型  バージョンアップ後
        //
        public Boolean update_ddl(ref List<Boolean> error_flg, ref List<string> messages)
        {
            // 文字列を格納するリストを作成
            List<string> SqlResults1 = new List<string>();
            List<string> SqlResults2 = new List<string>();
            List<string> SqlResults3 = new List<string>();

            string sql, after_version;
            int vs_count;
            Boolean kekka_flg,func_kekka = true;
            //
            //      ①更新用SQLをDB2から取得する
            //

            // ①SQLを発行して、特定テーブルから値取得する
            sql = ConfigurationManager.AppSettings["SQL_Select_UpCmd"];
            select_table_Rlt(connection2, sql, ref SqlResults1);

            // ②リピートフラグL取得
            sql = ConfigurationManager.AppSettings["SQL_Select_RepFlg"];
            select_table_Rlt(connection2, sql, ref SqlResults2);
          
            // ③DBUpdate後のバージョン取得
            sql = ConfigurationManager.AppSettings["SQL_Select_VSN"];
            vs_count = select_table_Rlt(connection2, sql, ref SqlResults3);
            // 更新完了したときのバージョンを取得
            after_version = SqlResults3[vs_count - 1];

            //
            //  SQLを発行してDB1を更新をする。
            //

            try
            {
                int i = 0;
                // アップデートSQLを実行
                for (i = 0; i < SqlResults1.Count; i++)
                {
                    // SQLの実行
                    messages.Add( SqlResults1[i]);
                    kekka_flg = update_table_Rlt(connection1, SqlResults1[i]);
                       
                    // SQLの実行結果をメッセージに反映
                    if (kekka_flg == true)
                        error_flg.Add(false);
                    else 
                        error_flg.Add(true);

                }
                // 後処理
                //　最新バージョン書き込み
                //　バージョン更新する。
                string[] vup_sqls = new string[3];
                // 現在の日付と時刻を取得
                DateTime currentDate = DateTime.Now;

                sql = ConfigurationManager.AppSettings["SQL_Update_Version"];
                vup_sqls[0] = string.Format(sql, after_version);

                sql = ConfigurationManager.AppSettings["SQL_Update_Name"];
                vup_sqls[1] = string.Format(sql, "DB_System");

                sql = ConfigurationManager.AppSettings["SQL_Update_Date"];
                vup_sqls[2] = string.Format(sql, currentDate.ToString("yyyy/MM/dd"));


                // Version UP SQLを実行していく
                foreach (string vup_sql in vup_sqls)
                {
                    // SQLの実行
                    messages.Add(vup_sql);
                    kekka_flg = update_table_Rlt(connection1, vup_sql);

                    // SQLの実行結果をメッセージに反映
                    if (kekka_flg == true)
                        error_flg.Add(false);
                    else
                        error_flg.Add(true);

                }

            }
            catch
            {

                MessageBox.Show("例外発生。想定外エラーが発生しました。");
                func_kekka = false;
            }
            finally
            {
            // 引数を返す
            }
            return func_kekka;
           
        }

        //　関数名　　Check_Version
        //　概要      現在の更新バージョンを取得する 
        //  引数      string型　バージョン
        //　返却値    boolean型  
        //
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

             return true;

        }

        // Close_DB
        public void Close_DB()
        {
            connection1.Close();
            connection2.Close();
        }
        // デザインのボタンと紐づく
        // ボタン押下後、更新用SQLを全部出力する
        public void all_hyoji(ref List<string> messages) {
            List<string> SqlResults1 = new List<string>();
                        
            string sql = ConfigurationManager.AppSettings["SQL_Select_UpCmd"];
            select_table_Rlt(connection2, sql, ref SqlResults1);
            messages = SqlResults1;
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
        // update用のSQL
        // 返却値：Boolen
        // 接続先、SQLを指定して、その結果をString型で返す。
        // 実行した行数を返す
        //
        public Boolean update_table_Rlt(OleDbConnection connection, string sql)
        {

            DataTable dataTable = new DataTable();
            OleDbCommand command = new OleDbCommand();             // クエリ格納用オブジェクト
            Boolean kekka_flg;
            // トランザクション開始
            command.Transaction = connection.BeginTransaction();

            command.Connection = connection;

            command.CommandText = sql;

            try
            {
                command.ExecuteNonQuery();
                // コミット
                command.Transaction.Commit();

                kekka_flg = true;
            }
            catch { 
                command.Transaction.Rollback();

                kekka_flg = false;
            }
            // 取得カラム数を返却
            return kekka_flg;

        }
    }
}