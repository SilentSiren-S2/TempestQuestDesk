using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempestDB
{
    public static class CommandBuilder
    {
        public static string CreateSelectString(string tableName)
        {
            string selectCommand = $"select * from {tableName}";
            return selectCommand;
        }

        public static string CreateSelectString(string tableName, List<string> columns)
        {
            string selectCommand = "select ";

            var column = columns[0];
            for (int i = 1; i < columns.Count; i++)
            {
                column = columns[i];
                selectCommand += ", " + column;
            }

            selectCommand += $" from {tableName}";
            return selectCommand;
        }

        public static string CreateInsertString(string tableName, params string[] args)
        {
            string insertCommand = $"insert into {tableName} (";
            foreach (string arg in args)
            {
                insertCommand += $"{arg}, ";
            }
            insertCommand = $"{insertCommand.Remove(insertCommand.Length - 2)}) \n values (";
            foreach (string arg in args)
            {
                insertCommand += $"@{arg}, ";
            }
            insertCommand = $"{insertCommand.Remove(insertCommand.Length - 2)})";
            return insertCommand;
        }

        public static string CreateInsertString(string tableName, DataRow row)
        {
            string insertCommand = $"insert into {tableName} values \n";
            List<string> values = new List<string>();

            foreach (var item in row.ItemArray)
            {
                if (item is string)
                {
                    values.Add("'" + item.ToString() + "'");
                }
                else
                {
                    values.Add(item.ToString());
                }
            }

            string result = "(" + string.Join(", ", values) + ")";
            insertCommand += result;
            return insertCommand;
        }

        public static string CreateInsertString(string tableName, DataTable dataTable)
        {
            string insertCommand = $"insert into {tableName} values \n";
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow row = dataTable.Rows[i];

                List<string> values = new List<string>();

                foreach (var item in row.ItemArray)
                {
                    if (item is string)
                    {
                        values.Add("'" + item.ToString() + "'");
                    }
                    else
                    {
                        values.Add(item.ToString());
                    }
                }

                string result = "(" + string.Join(", ", values) + ")";
                insertCommand += result;
                insertCommand += ", \n";
            }
            insertCommand = insertCommand.Remove(insertCommand.Length - 3);
            return insertCommand;
        }

        public static string CreateDeleteString(string tableName, int id)
        { 
            string deleteCommand = $"delete {tableName} where ID = {id}";
            return deleteCommand;
        }

        public static string CreateDeleteString(string tableName)
        {
            string deleteCommand = $"delete {tableName} where true";
            return deleteCommand;
        }

        //public static string CreateUpdateString(string tableName, DataTable dataTable) 
        //{
        //    if (dataTable.Columns.Contains("ID"))
        //    {
        //        string updateString = $"update {tableName}\n set ";

        //        foreach (DataRow row)
        //        {

        //        }

        //        return updateString;
        //    }
        //    else
        //    {
        //        throw new ArgumentException("The DataTable must contain an 'ID' column.");
        //    }
        //}
        public static string CreateUpdateString(string tableName, DataRow row) 
        {
            if (row.Table.Columns.Contains("ID"))
            {
                string updateString = $"UPDATE {tableName} SET ";

                string id = row["ID"].ToString();

                foreach (DataColumn column in row.Table.Columns)
                {
                    if (column.ColumnName != "ID")
                    {
                        string columnName = column.ColumnName;
                        string value = row[column].ToString();

                        updateString += $"{columnName} = '{value}', ";
                    }
                }

                updateString = updateString.TrimEnd(',', ' ');

                updateString += $" WHERE ID = {id};";

                return updateString;
            }
            else
            {
                throw new ArgumentException("The DataRow must belong to a DataTable that contains an 'ID' column.");
            }
        }
    }
}
