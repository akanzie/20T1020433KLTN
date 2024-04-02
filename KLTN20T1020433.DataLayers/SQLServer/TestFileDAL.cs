﻿using System;
using System.Net;
using System.Numerics;
using Dapper;
using KLTN20T1020433.DomainModels.Entities;

namespace KLTN20T1020433.DataLayers.SQLServer
{
    public class TestFileDAL : _BaseDAL//, ICommonDAL<TestFile>
    {
        public TestFileDAL(string connectionString) : base(connectionString)
        {
        }

        //public int Add(TestFile data)
        //{
        //    int id = 0;
        //    using (var connection = OpenConnection())
        //    {
        //        var sql = @"if exists(select * from Employees where Email = @Email)
        //                        select -1
        //                    else
        //                        begin
        //                            insert into Employees(FullName, BirthDate, Address, Phone, Email, Photo, IsWorking)
        //                            values(@FullName, @BirthDate, @Address, @Phone, @Email, @Photo, @IsWorking);

        //                            select @@identity;
        //                        end";
        //        var parameters = new
        //        {
        //            FullName = data.FullName ?? "",
        //            BirthDate = data.BirthDate,
        //            Address = data.Address ?? "",
        //            Phone = data.Phone ?? "",
        //            Email = data.Email ?? "",
        //            Photo = data.Photo ?? "",
        //            IsWorking = data.IsWorking
        //        };
        //        id = connection.ExecuteScalar<int>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text);
        //        connection.Close();
        //    }
        //    return id;
        //}

        //public int Count(string searchValue = "")
        //{
        //    int count = 0;
        //    if (!string.IsNullOrEmpty(searchValue))
        //        searchValue = "%" + searchValue + "%";
        //    using (var connection = OpenConnection())
        //    {
        //        var sql = @"select count(*) from Employees 
        //                    where (@searchValue = N'') or (FullName like @searchValue)";
        //        var parameters = new
        //        {
        //            searchValue = searchValue ?? "",
        //        };
        //        count = connection.ExecuteScalar<int>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text);
        //        connection.Close();
        //    }
        //    return count;
        //}

        //public bool Delete(int id)
        //{
        //    bool result = false;
        //    using (var connection = OpenConnection())
        //    {
        //        var sql = @"delete from Employees where EmployeeID = @EmployeeID";
        //        var parameters = new
        //        {
        //            EmployeeID = id
        //        };
        //        result = connection.Execute(sql: sql, param: parameters, commandType: System.Data.CommandType.Text) > 0;
        //        connection.Close();
        //    }
        //    return result;
        //}

        //public TestFile? Get(int id)
        //{
        //    TestFile? data = null;
        //    using (var connection = OpenConnection())
        //    {
        //        var sql = @"select * from Employees where EmployeeID = @EmployeeID";
        //        var parameters = new { EmployeeID = id };
        //        data = connection.QueryFirstOrDefault<TestFile>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text);
        //        connection.Close();
        //    }
        //    return data;
        //}

        //public bool IsUsed(int id)
        //{
        //    bool result = false;
        //    using (var connection = OpenConnection())
        //    {
        //        var sql = @"if exists(select * from Orders where EmployeeID = @EmployeeID)
        //                        select 1
        //                    else 
        //                        select 0";
        //        var parameters = new { EmployeeID = id };
        //        result = connection.ExecuteScalar<bool>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text);
        //        connection.Close();
        //    }
        //    return result;
        //}

        //public IList<TestFile> List(int page = 1, int pageSize = 0, string searchValue = "")
        //{
        //    List<TestFile> data = new List<TestFile>();
        //    if (!string.IsNullOrEmpty(searchValue))
        //        searchValue = "%" + searchValue + "%";
        //    using (var connection = OpenConnection())
        //    {
        //        var sql = @"with cte as
        //                    (
        //                     select	*, row_number() over (order by FullName) as RowNumber
        //                     from	Employees 
        //                     where	(@searchValue = N'') or (FullName like @searchValue)
        //                    )
        //                    select * from cte
        //                    where  (@pageSize = 0) 
        //                     or (RowNumber between (@page - 1) * @pageSize + 1 and @page * @pageSize)
        //                    order by RowNumber";
        //        var parameters = new
        //        {
        //            page = page,
        //            pageSize = pageSize,
        //            searchValue = searchValue ?? ""
        //        };
        //        data = connection.Query<TestFile>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text).ToList();
        //        connection.Close();
        //    }
        //    return data;
        //}

        //public bool Update(TestFile data)
        //{
        //    bool result = false;
        //    using (var connection = OpenConnection())
        //    {
        //        var sql = @"if not exists(select * from Employees where EmployeeID <> @EmployeeID and Email = @email)
        //                        begin
        //                            update Employees 
        //                            set FullName = @FullName,
        //                                BirthDate = @BirthDate,
        //                                Address = @Address,
        //                                Phone = @Phone,
        //                                Email = @Email,
        //                                Photo = @Photo,
        //                                IsWorking = @IsWorking
        //                            where EmployeeID = @EmployeeID
        //                        end";
        //        var parameters = new
        //        {
        //            EmployeeID = data.EmployeeID,
        //            FullName = data.FullName ?? "",
        //            BirthDate = data.BirthDate,
        //            Address = data.Address ?? "",
        //            Phone = data.Phone ?? "",
        //            Email = data.Email ?? "",
        //            Photo = data.Photo ?? "",
        //            IsWorking = data.IsWorking
        //        };
        //        result = connection.Execute(sql: sql, param: parameters, commandType: System.Data.CommandType.Text) > 0;
        //        connection.Close();
        //    }
        //    return result;
        //}
    }
}