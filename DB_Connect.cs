
using System;
using System.Configuration;
using System.Data;
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
        string connStrTemplate = ConfigurationManager.AppSettings["DBConnString"];

        // App.configのappSettingsタグ内に記述した「DBPath」のキーの値を取得して変数「dbPathStr」に格納する
        string dbPathStr = ConfigurationManager.AppSettings["DBPath"];

        private OleDbConnection connection = new OleDbConnection();    // データベース接続用オブジェクト
        private OleDbDataAdapter dataAdapter = new OleDbDataAdapter(); // テーブル操作実行用オブジェクト
        private OleDbCommand command = new OleDbCommand();             // クエリ格納用オブジェクト

        // 外部変数
        private String DBConnectionString;
        DataTable schemaTable;

        // コンストラクタ
        public DB_Connect()
	    {
            // データベースの接続変数を設定
            string connectionString = string.Format(connStrTemplate, dbPathStr);
            this.DBConnectionString = connectionString;
        }
        // Open_DB
        public void Open_DB()
        {
            connection.ConnectionString = DBConnectionString;
            connection.Open();

            // テーブル一覧を取得するためにSchema情報を取得します。
            this.schemaTable = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            foreach (DataRow row in schemaTable.Rows)
            {
                string tableName = row["TABLE_NAME"].ToString();
                Console.WriteLine(tableName);
            }


        }

        // Close_DB
        public void Close_DB()
        {
            connection.Close();
        }


    }



}