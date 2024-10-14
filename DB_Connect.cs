
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
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
        string connStr1 = ConfigurationManager.AppSettings["DB1ConnString"];
        string dbPathStr1 = ConfigurationManager.AppSettings["DB1Path"];

        string connStr2 = ConfigurationManager.AppSettings["DB2ConnString"];
        string dbPathStr2 = ConfigurationManager.AppSettings["DB2Path"];


        private OleDbConnection connection1 = new OleDbConnection();    // データベース１（更新元）接続用オブジェクト
        private OleDbConnection connection2 = new OleDbConnection();    // データベース２（更新先）接続用オブジェクト

        private OleDbDataAdapter dataAdapter1 = new OleDbDataAdapter(); // テーブル操作実行用オブジェクト
        private OleDbCommand command1 = new OleDbCommand();             // クエリ格納用オブジェクト
        private DataTable dataTable1 = new DataTable();                 // 

        // 外部変数
        private String DBConnectionString1;
        private String DBConnectionString2;

        DataTable schemaTable1;
        DataTable schemaTable2;

        // コンストラクタ
        public DB_Connect()
	    {
            // データベース1の接続設定
            string connectionString1 = string.Format(connStr1, dbPathStr1);
            this.DBConnectionString1 = connectionString1;

            // データベース2の接続設定
            string connectionString2 = string.Format(connStr2, dbPathStr2);
            this.DBConnectionString2 = connectionString2;

        }
        // Open_DB
        public void Open_DB()
        {
            connection1.ConnectionString = DBConnectionString1;
            connection1.Open();

            connection2.ConnectionString = DBConnectionString2;
            connection2.Open();


        }
        // table 一覧取得
        public List<string> get_tablelist()
        {

            // 文字列を格納するリストを作成
            List<string> tableNames = new List<string>();


            // テーブル一覧を取得するためにSchema情報を取得します。
            this.schemaTable2 = connection2.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            foreach (DataRow row in schemaTable2.Rows)
            {
                // String型
                String tableName = row["TABLE_NAME"].ToString();

                // システムテーブルは除外する

                string firstFourChars = tableName.Substring(0, 4);
                if (firstFourChars == "MSys")
                {
                    // 除外対象
                }
                else
                {
                    //　対象
                    tableNames.Add(tableName);
                }
                    //Console.WriteLine(tableName);
            }
            return tableNames;
        }

        // table　更新
        public void update_tablel1()
        {
            // 文字列を格納するリストを作成
            List<string> message = new List<string>();


            //
            //      本処理
            //      バージョンアップ
            //      SQLを発行して



        }

        //更新対象バージョンかチェックし、NGの場合は処理を抜ける
        public Boolean Check_Version(ref String write_version ){


            // 直前の取得結果をクリア
            dataTable1.Clear();

            // ①SQLを発行して、特定テーブルから値取得する
            // Selectで接続する宣言
            dataAdapter1.SelectCommand = command1;
            //

            string sql = ConfigurationManager.AppSettings["SQL_Select_Version"];
            command1.CommandText = sql;
            command1.Connection = connection1;

            dataAdapter1.SelectCommand = command1;
            // SQL実行
            dataAdapter1.Fill(dataTable1);

            // SQLの実行結果を文字列で取得
            String SQLResult = DataTableToString(dataTable1);
            write_version = SQLResult;

            // ②バージョン☑
            // 特定バージョン以上の値かどうか☑する。

            int OKVersion;
            String fixVersion = ConfigurationManager.AppSettings["ChkVersion"];

            // チェック用バージョン
            OKVersion = int.Parse(SQLResult);


            if (int.Parse(fixVersion) >= OKVersion)
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

    }



}