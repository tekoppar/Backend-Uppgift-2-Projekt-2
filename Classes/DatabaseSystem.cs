using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt2.Classes
{
    public class DatabaseSystem
    {
        public static DatabaseSystem DBS;

        static DatabaseSystem()
        {
            DatabaseSystem.DBS = new DatabaseSystem();
        }
        public DatabaseSystem()
        {
        }

        static private Dictionary<string, object> ListToDict(List<string> fields, List<string> values)
        {
            return Enumerable.Range(0, fields.Count).ToDictionary(i => fields[i], i => (object)values[i]);
        }

        private string AppendWhere(List<string> haystacks, List<string> needles)
        {
            string whereQuery = "";
            for (int i = 0; i < haystacks.Count; i++)
            {
                if (haystacks[i] != "Id")
                whereQuery += haystacks[i] + " = '" + needles[i] + "'";
                else
                    whereQuery += haystacks[i] + " = " + needles[i] + "";

                if (i < haystacks.Count - 1)
                    whereQuery += " AND ";
            }

            return whereQuery;
        }

        public void InsertSingle(string table, List<string> fields, List<string> values)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\Users\Tekoppar\source\repos\Backend Uppgift 2\Database1.mdf';Integrated Security=True"))
            {
                List<string> newFields = new List<string>();
                fields.ForEach(delegate (string value) { newFields.Add("@" + value); });
                Dictionary<string, object> valuePairs = DatabaseSystem.ListToDict(newFields, values);
                string sqlFields = string.Join(", ", fields.ToArray());
                if (fields.Count == 1)
                    sqlFields = sqlFields.Replace(", ", "");

                string sqlValues = string.Join(", ", newFields.ToArray());
                if (newFields.Count == 1)
                    sqlValues = sqlValues.Replace(", ", "");

                connection.Execute("INSERT INTO " + table + " (" + sqlFields + ") VALUES (" + sqlValues + ");", new DynamicParameters(valuePairs));
            }
        }

        public void InsertSingle<T>(string table, List<string> fields, T insertObject)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\Users\Tekoppar\source\repos\Backend Uppgift 2\Database1.mdf';Integrated Security=True"))
            {
                List<string> newFields = new List<string>();
                fields.ForEach(delegate (string value) { newFields.Add("@" + value); });

                string sqlValues = string.Join(", ", newFields.ToArray());
                if (newFields.Count == 1)
                    sqlValues = sqlValues.Replace(", ", "");

                connection.Execute("INSERT INTO " + table + " VALUES (" + sqlValues + ");", insertObject);
            }
        }

        public void UpdateSingle<T>(string table, List<string> fields, string haystack, string needle, T insertObject)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\Users\Tekoppar\source\repos\Backend Uppgift 2\Database1.mdf';Integrated Security=True"))
            {
                List<string> newFields = new List<string>();
                fields.ForEach(delegate (string value) { newFields.Add("@" + value); });
                string sqlQuery = "UPDATE " + table + " SET ";

                for (int i = 0; i < fields.Count; i++)
                {
                    sqlQuery += fields[i] + " = @" + fields[i];

                    if (i < fields.Count - 1)
                        sqlQuery += ", ";
                }
                sqlQuery += " WHERE " + haystack + " = " + needle;

                connection.Execute(sqlQuery, insertObject);
            }
        }

        public void UpdateSingle(string table, List<string> fields, List<string> values, string haystack, string needle)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\Users\Tekoppar\source\repos\Backend Uppgift 2\Database1.mdf';Integrated Security=True"))
            {
                List<string> newFields = new List<string>();
                fields.ForEach(delegate (string value) { newFields.Add("@" + value); });
                Dictionary<string, object> valuePairs = DatabaseSystem.ListToDict(newFields, values);
                valuePairs.Add("needle", needle);
                string sqlQuery = "UPDATE " + table + " SET ";

                for (int i = 0; i < fields.Count; i++)
                {
                    sqlQuery += fields[i] + " = @" + fields[i];

                    if (i < fields.Count - 1)
                        sqlQuery += ", ";
                }
                sqlQuery += " WHERE " + haystack + " = @needle";

                connection.Execute(sqlQuery, new DynamicParameters(valuePairs));
            }
        }

        public T SelectSingle<T>(string table, List<string> fields)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\Users\Tekoppar\source\repos\Backend Uppgift 2\Database1.mdf';Integrated Security=True"))
            {
                string sqlFields = string.Join(", ", fields.ToArray());
                if (fields.Count == 1)
                    sqlFields = sqlFields.Replace(", ", "");

                T values = connection.ExecuteScalar<T>("SELECT " + sqlFields + " FROM " + table);
                return values;
            }
        }

        public T SelectSingleWhere<T>(string table, List<string> fields, List<string> haystacks, List<string> needles)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\Users\Tekoppar\source\repos\Backend Uppgift 2\Database1.mdf';Integrated Security=True"))
            {
                string sqlParams = this.AppendWhere(haystacks, needles);
                string stringFields = string.Join(", ", fields.ToArray());

                if (fields.Count == 1)
                    stringFields = stringFields.Replace(", ", "");

                sqlParams = "SELECT " + stringFields + " FROM " + table + " WHERE " + sqlParams;
                T values = connection.ExecuteScalar<T>(sqlParams);
                return values;
            }
        }

        public List<T> Select<T>(string table, List<string> fields)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\Users\Tekoppar\source\repos\Backend Uppgift 2\Database1.mdf';Integrated Security=True"))
            {
                string sqlFields = string.Join(", ", fields.ToArray());
                if (fields.Count == 1)
                    sqlFields = sqlFields.Replace(", ", "");

                List<T> values = connection.Query<T>("SELECT " + sqlFields + " FROM " + table).ToList();
                return values;
            }
        }

        public T SelectWhereObject<T>(string table, List<string> fields, List<string> haystacks, List<string> needles)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\Users\Tekoppar\source\repos\Backend Uppgift 2\Database1.mdf';Integrated Security=True"))
            {
                string sqlParams = this.AppendWhere(haystacks, needles);
                string sqlFields = string.Join(", ", fields.ToArray());
                if (fields.Count == 1)
                    sqlFields = sqlFields.Replace(", ", "");

                T values = connection.QueryFirst<T>("SELECT " + sqlFields + " FROM " + table + " WHERE " + sqlParams);
                return values;
            }
        }

        public List<T> SelectWhere<T>(string table, List<string> fields, List<string> haystacks, List<string> needles)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\Users\Tekoppar\source\repos\Backend Uppgift 2\Database1.mdf';Integrated Security=True"))
            {
                string sqlParams = this.AppendWhere(haystacks, needles);
                string sqlFields = string.Join(", ", fields.ToArray());
                if (fields.Count == 1)
                    sqlFields = sqlFields.Replace(", ", "");

                List<T> values = connection.Query<T>("SELECT " + sqlFields + " FROM " + table + " WHERE " + sqlParams).ToList();
                return values;
            }
        }
    }
}
