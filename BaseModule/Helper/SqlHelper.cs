using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace BaseModule.Helper
{

    class SqlHelper
    {
        static string connstr = ConfigurationManager.ConnectionStrings["connstr"].ConnectionString;
        //select [cloums] from [table] where id= [id]
        public static T QueryById<T>(int id)
        {
            Type type = typeof(T);
            object inst = Activator.CreateInstance(type);
            string cloumestr = string.Join(",", type.GetProperties().Select(p => string.Format("[{0}]", p.Name)));
            string sql = string.Format($"SELECT {cloumestr} FROM {type.Name} Where id={id}");
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    foreach (var item in type.GetProperties())
                    {
                        if (reader[item.Name] is DBNull)//判断数据库读回来的值是否为可空类型
                        {
                            item.SetValue(inst, null);
                        }
                        item.SetValue(inst, reader[item.Name]);
                    }
                }
            }
            return (T)inst;
        }

        //select [cloums] from [table]
        public static IEnumerable<T> QueryByList<T>()
        {
            Type type = typeof(T);
            List<T> datalist = new List<T>();
            string cloums = string.Join(",", type.GetProperties().Select(p => string.Format($"[{p.Name}]")));
            string sql = string.Format($"Select {cloums} from {type.Name}");
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                SqlCommand command = new SqlCommand(sql, conn);
                SqlDataReader read = command.ExecuteReader();
                while (read.Read())
                {
                    object obj = Activator.CreateInstance(type);
                    foreach (var item in type.GetProperties())
                    {
                        if (read[type.Name] is DBNull)
                        {
                            item.SetValue(obj, null);
                        }
                        item.SetValue(obj, read[type.Name]);
                    }
                    datalist.Add((T)obj);
                }
            }
            return datalist;
        }

        // delete  from [table] where id=[]
        public static bool DeleteById<T>(int id)
        {
            Type type = typeof(T);
            string sql = string.Format($"delete [{type.Name}] where id={id}");
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                SqlCommand command = new SqlCommand(sql, conn);
                return command.ExecuteNonQuery() > 0;
            }
        }

        // insert into [table] (cloum1,cloum1) Value (value1,value2)
        public static bool Insert<T>(T t)
        {
            Type type = typeof(T);
            string cloums = string.Join(",", type.GetProperties().Select(p => string.Format($"[{p.Name}]")));
            string values = string.Join(",", type.GetProperties().Select(p => string.Format($"@{p.Name}")));

            string sql = string.Format($"insert into [{type.Name}] ({cloums}) Value ({values})");
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                SqlCommand command = new SqlCommand(sql, conn);
                SqlParameter[] sqlParameters = type.GetProperties().
                    Select(p => new SqlParameter($"@{type.Name}", p.GetValue(t) ?? DBNull.Value)).ToArray();
                command.Parameters.AddRange(sqlParameters);
                return command.ExecuteNonQuery() > 0;
            }

        }





        public static int ExecuteNonQuery(String connectionString, String commandText,
         CommandType commandType, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(commandText, conn))
                {
                    // There're three command types: StoredProcedure, Text, TableDirect. The TableDirect   
                    // type is only for OLE DB.    
                    cmd.CommandType = commandType;
                    cmd.Parameters.AddRange(parameters);

                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }


        public static Object ExecuteScalar(String connectionString, String commandText,
         CommandType commandType, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(commandText, conn))
                {
                    cmd.CommandType = commandType;
                    cmd.Parameters.AddRange(parameters);

                    conn.Open();
                    return cmd.ExecuteScalar();
                }
            }
        }

        public static SqlDataReader ExecuteReader(String connectionString, String commandText,
         CommandType commandType, params SqlParameter[] parameters)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            using (SqlCommand cmd = new SqlCommand(commandText, conn))
            {
                cmd.CommandType = commandType;
                cmd.Parameters.AddRange(parameters);

                conn.Open();
                // When using CommandBehavior.CloseConnection, the connection will be closed when the   
                // IDataReader is closed.  
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                return reader;
            }
        }
    }
}
