﻿using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;
using Nhom2.DomainModels;

namespace Nhom2.DataLayers.SQLServer
{
    public class SubmissionFileDAL : _BaseDAL, ICommonDAL<Province>
    {
        public SubmissionFileDAL(string connectionString) : base(connectionString)
        {
        }

        public int Add(Province data)
        {
            throw new NotImplementedException();
        }

        public int Count(string searchValue = "")
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Province? Get(int id)
        {
            throw new NotImplementedException();
        }

        public bool IsUsed(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Province> List(int page = 1, int pageSize = 0, string searchValue = "")
        {
            List<Province> list = new List<Province>();
            using (var connection = OpenConnection())
            {
                var sql = @"select * from Provinces";
                list = connection.Query<Province>(sql: sql, commandType: CommandType.Text).ToList();
                connection.Close();
            }    
            return list;
        }

        public bool Update(Province data)
        {
            throw new NotImplementedException();
        }
    }
}
