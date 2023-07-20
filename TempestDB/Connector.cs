using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using static TempestDB.CommandBuilder;
//using Microsoft.Data.SqlClient;

namespace TempestDB
{
    public class Connector
    {
        public string ConnectionString { get; set;}
        public SqlConnection Connection { get { return new SqlConnection(ConnectionString); } set { this.Connection = value; } }
        public SqlCommand SelectCommand { get; set; }
        public SqlCommand InsertCommand { get; set; }
        public SqlCommand UpdateCommand { get; set; }
        public SqlCommand DeleteCommand { get; set; }

        public static void BasicExecute(string queryString, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(
                       connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
                command.Connection.Close();
            }
        }

        public void CreateSelectCommand(string tableName, List<string> columns) 
        {
            this.SelectCommand = new SqlCommand(CreateSelectString(tableName, columns), Connection);
        }

        public void CreateSelectCommand(string tableName)
        {
            this.SelectCommand = new SqlCommand(CreateSelectString(tableName), Connection);
        }

        public void CreateInsertCommand(string tableName, params string[] args)
        {
            this.InsertCommand = new SqlCommand(CreateInsertString(tableName, args), Connection);
        }

        public void CreateInsertCommand(string tableName, DataRow row)
        {
            this.InsertCommand = new SqlCommand(CreateInsertString(tableName, row), Connection);
        }

        public void CleanTableCommand(string tableName)
        {
            this.DeleteCommand = new SqlCommand(CreateDeleteString(tableName), Connection);
        }

        public void CreateDeleteCommand(string tableName, int id)
        {
            this.DeleteCommand = new SqlCommand(CreateDeleteString(tableName, id), Connection);
        }

        public void CreateUpdateCommand(string tableName, DataTable table)
        {
            foreach(DataRow row in table.Rows)
            {
                this.UpdateCommand = new SqlCommand(CreateUpdateString(tableName, row), Connection);
            }
        }
        public void CreateUpdateCommand(string tableName, DataRow row)
        {
            this.UpdateCommand = new SqlCommand(CreateUpdateString(tableName, row), Connection);
        }

        public DataTable SelectExecute()
        {
            DataTable dataTable = new DataTable();
            using (Connection)
            {
                SelectCommand.Connection.Open();
                SqlDataReader reader = SelectCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        dataTable.Columns.Add(reader.GetName(i), reader.GetFieldType(i));
                    }

                    while (reader.Read())
                    {
                        DataRow row = dataTable.NewRow();

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            row[i] = reader[i];
                        }

                        dataTable.Rows.Add(row);
                    }
                }
                SelectCommand.Connection.Close();
            }
            return dataTable;
        }

        public void InsertExecute()
        {
            using (Connection)
            {
                InsertCommand.Connection.Open();
                InsertCommand.ExecuteNonQuery();
                InsertCommand.Connection.Close();
            }
        }

        public void DeleteExecute()
        {
            using (Connection)
            {
                DeleteCommand.Connection.Open();
                DeleteCommand.ExecuteNonQuery();
                DeleteCommand.Connection.Close();
            }
        }

        public void UpdateExecute(string tableName, DataTable table)
        {
            foreach (DataRow row in table.Rows)
            {
                UpdateExecute(tableName, row);
            }
        }

        public void UpdateExecute(string tableName, DataRow row)
        {
            CreateUpdateCommand(tableName, row);
            using (Connection)
            {
                UpdateCommand.Connection.Open();
                UpdateCommand.ExecuteNonQuery();
                UpdateCommand.Connection.Close();
            }
        }

        public void UpdateExecute()
        {
            using (Connection)
            {
                UpdateCommand.Connection.Open();
                UpdateCommand.ExecuteNonQuery();
                UpdateCommand.Connection.Close();
            }
        }
    }
}
