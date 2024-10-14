
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;

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
        private DataTable dataTable1 = new DataTable();

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
        public void get_tablelist()
        {
            // テーブル一覧を取得するためにSchema情報を取得します。
            this.schemaTable1 = connection1.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            foreach (DataRow row in schemaTable1.Rows)
            {
                string tableName = row["TABLE_NAME"].ToString();
                Console.WriteLine(tableName);
            }
            // テーブル一覧を取得するためにSchema情報を取得します。
            this.schemaTable2 = connection2.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            foreach (DataRow row in schemaTable2.Rows)
            {
                string tableName = row["TABLE_NAME"].ToString();
                Console.WriteLine(tableName);
            }
        }

        // table 一覧取得
        public void update_tablel1() {

            dataAdapter1.SelectCommand = command1;

            string sql = "SELECT * FROM ta支店";
            command1.CommandText = sql;
            command1.Connection = connection1;

            dataAdapter1.SelectCommand = command1;
            // SQL実行
            dataAdapter1.Fill(dataTable1);

            // 取得結果表示
            foreach (DataRow dr in dataTable1.Rows)
            {
                Console.WriteLine(dr.ItemArray[0]);
            }


        }


        // Close_DB
        public void Close_DB()
        {
            connection1.Close();
        }


    }



}