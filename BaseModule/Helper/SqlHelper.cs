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
        static string connstr = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
        /// <summary>
        /// 查询单行实体数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public static T GetEntity<T>(int id) 
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
                        if (reader[item.Name]==null)
                        {
                            item.SetValue(inst, null);
                        }
                        item.SetValue(inst, reader[item.Name]);
                    }
                }
            }
            return (T)inst;
        }
        public static IEnumerable<T> GetALLEntity<T>()
        {
            Type type = typeof(T);
            List<T> EntityallList = new List<T>();
            string cloumestr = string.Join(",", type.GetProperties().Select(p => string.Format("[{0}]", p.Name)));
            string sql = string.Format($"SELECT {cloumestr} FROM {type.Name}");
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())//表示有数据  开始读
                {
                    object inst = Activator.CreateInstance(type);//创建对象
                    foreach (var item in type.GetProperties())
                    {
                        if (reader[item.Name] == null)
                        {
                            item.SetValue(inst, null);
                        }
                        item.SetValue(inst, reader[item.Name]);
                    }
                    EntityallList.Add((T)inst);
                }
            }
            return EntityallList;
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
