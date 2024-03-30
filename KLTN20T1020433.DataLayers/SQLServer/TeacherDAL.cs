using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;
using DataLayers.SQLServer;
using _20T1020433KLTN.Domain.Entities;
using DataLayers.Interfaces;

namespace SQLServer
{
    public class TeacherDAL : _BaseDAL, ICommonAPI<Student>
    {
        public TeacherDAL(string connectionString) : base(connectionString)
        {
        }



        public int Count(string searchValue = "")
        {
            int count = 0;

            if (!string.IsNullOrEmpty(searchValue))
                searchValue = "%" + searchValue + "%";

            using (var connection = OpenConnection())
            {
                var sql = @"select count(*) from Customers 
                            where (@searchValue = N'') or (CustomerName like @searchValue)";
                var parameters = new { searchValue = searchValue ?? "" };
                count = connection.ExecuteScalar<int>(sql: sql, param: parameters, commandType: CommandType.Text);
                connection.Close();
            }

            return count;
        }


        public Student? Get(int id)
        {
            Student? data = null;
            using (var connection = OpenConnection())
            {
                var sql = @"select * from Customers where CustomerId = @CustomerId";
                var parameters = new
                {
                    CustomerId = id
                };
                data = connection.QueryFirstOrDefault<Student>(sql: sql, param: parameters, commandType: CommandType.Text);
                connection.Close();
            }
            return data;
        }



        public IList<Student> List(int page = 1, int pageSize = 0, string searchValue = "")
        {
            List<Student> list = new List<Student>();

            if (!string.IsNullOrEmpty(searchValue))
                searchValue = "%" + searchValue + "%";  //Phong =>  %Phong%

            using (var connection = OpenConnection())
            {
                var sql = @"with cte as
                            (
	                            select	*, row_number() over (order by CustomerName) as RowNumber
	                            from	Customers 
	                            where	(@searchValue = N'') or (CustomerName like @searchValue)
                            )
                            select * from cte
                            where  (@pageSize = 0) 
	                            or (RowNumber between (@page - 1) * @pageSize + 1 and @page * @pageSize)
                            order by RowNumber";
                var parameters = new
                {
                    page,
                    pageSize,
                    searchValue = searchValue ?? ""
                };
                list = connection.Query<Student>(sql: sql, param: parameters, commandType: CommandType.Text).ToList();
                connection.Close();
            }

            return list;
        }


    }
}
