using System;
using System.Collections.Generic;
using System.Data.SqlClient;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment_2
{
    class MyORM<T> where T : IData
    {
        private SqlConnection _sqlConnection;
        public MyORM(SqlConnection connection) 
        {
            _sqlConnection = connection;
        }
        public MyORM(string connectionString)
            : this (new SqlConnection (connectionString))
        {

        }

        public void Insert(T item) 
        {
            var sql = new StringBuilder("insert into ");
            var type = item.GetType();
            var properties = type.GetProperties();
            sql.Append(type.Name);
           
            sql.Append('(');
            foreach (var property in properties)
            {           
                if (property.Name!="Id")
                {
                    sql.Append(' ').Append(property.Name).Append(',');
                }
            }

            sql.Remove(sql.Length - 1, 1);
            sql.Append(") values (");

            foreach (var property in properties)
            {
                if (property.Name != "Id")
                {
                    sql.Append('@').Append(property.Name).Append(',');
                }
                   
            }
            sql.Remove(sql.Length - 1, 1);
            sql.Append(");");

            var query = sql.ToString();

            if (_sqlConnection.State == System.Data.ConnectionState.Closed)

                _sqlConnection.Open();

              using  var command = new SqlCommand(query, _sqlConnection);

            foreach (var property in properties)
            {
                command.Parameters.AddWithValue(property.Name,property.GetValue(item));
            }
            command.ExecuteNonQuery();
        }
        public void Update(T item)
        {
            var sql = new StringBuilder("UPDATE ");
            var type = item.GetType();
            var properties = type.GetProperties();

            sql.Append(type.Name).Append(" SET");

            foreach (var property in properties)
            {
                if (property.Name != "Id")
                {
                    sql.Append(' ').Append(property.Name).Append(" = ").Append($"@{property.Name} ").Append(",");
                }
            }
            sql.Remove(sql.Length - 1, 1);
            sql.Append("where ");

            foreach (var property in properties)
            {
                if (property.Name=="Id")
                {
                    sql.Append(' ').Append(property.Name).Append(" = ").Append($"@{property.Name} ");
                }
            }
            var query = sql.ToString();

            if (_sqlConnection.State == System.Data.ConnectionState.Closed)

                _sqlConnection.Open();

            using var command = new SqlCommand(query, _sqlConnection);

            foreach (var property in properties)
            {
                command.Parameters.AddWithValue(property.Name, property.GetValue(item));
            }

            command.ExecuteNonQuery();
        }
        public void Delete(T item)
        {
            var sql = new StringBuilder("DELETE FROM ");
            var type = typeof(student);
            var properties = type.GetProperties();

            sql.Append(type.Name).Append(" WHERE ");
            foreach (var property in properties)
            {

                sql.Append(' ').Append(property.Name).Append(" = ").Append($" @{property.Name} ").Append(" AND " );               
            }
            sql.Remove(sql.Length - 4, 4);

            var query = sql.ToString();

            if (_sqlConnection.State == System.Data.ConnectionState.Closed)

                _sqlConnection.Open();

           using var command = new SqlCommand(query, _sqlConnection);
            foreach (var property in properties)
            {
                command.Parameters.AddWithValue(property.Name, property.GetValue(item));
            }
            command.ExecuteNonQuery();
        }
        public void Delete(int id)
        {
            //DELETE FROM table_name WHERE condition;
            var sql = new StringBuilder("DELETE FROM ");
            var type = typeof(student);
            var properties = type.GetProperties();

            sql.Append(type.Name).Append(" WHERE ");

            foreach (var property in properties)
            {
                if (property.Name == "Id")
                {
                    sql.Append(' ').Append(property.Name).Append(" = ").Append(id);
                }
            }
            var query = sql.ToString();

            if (_sqlConnection.State == System.Data.ConnectionState.Closed)

                _sqlConnection.Open();

            using  var command = new SqlCommand(query, _sqlConnection);
            command.ExecuteNonQuery();

        }
        public T GetById(int id)
        {
            var sql = new StringBuilder("select * from ");
            var type = typeof(T);
            var properties = type.GetProperties();

            sql.Append(type.Name).Append(" where ");

            foreach (var property in properties)
            {
                if (property.Name == "Id") 
                {
                    sql.Append(property.Name).Append(" = ").Append(id);
                }
            }

            var query = sql.ToString();
            if (_sqlConnection.State == System.Data.ConnectionState.Closed)

                _sqlConnection.Open();

            using SqlCommand command = new SqlCommand();
            command.CommandText = query;
            command.Connection = _sqlConnection;

            var reader = command.ExecuteReader();
           
            var student = (T)Activator.CreateInstance(type);
            
            while (reader.Read())
            { 
                foreach (var property in properties)
                {
                    property.SetValue( student,reader[property.Name] );
                }

            }
            return student;
        }

        public IList<T> GetAll()
        {
          

            var sql = new StringBuilder("select * from ");
            var type = typeof(T);
            var properties = type.GetProperties();

            sql.Append(type.Name);

               if (_sqlConnection.State == System.Data.ConnectionState.Closed)

                   _sqlConnection.Open();

             using SqlCommand command = new SqlCommand();
             command.CommandText = sql.ToString();
             command.Connection = _sqlConnection;
             var reader = command.ExecuteReader();

            
            var studentList = new List<T>();
            while (reader.Read())
            {
                var stud = (T)Activator.CreateInstance(type);
                
                foreach (var property in properties)
                {
                    property.SetValue(stud, reader[property.Name]);
                }



                studentList.Add(stud);

            }

            return (IList <T>) studentList;
        }
    }
}
