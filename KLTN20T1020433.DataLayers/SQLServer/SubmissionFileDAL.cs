using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;
using DataLayers.Interfaces;
using KLTN20T1020433.DomainModels.Entities;

namespace KLTN20T1020433.DataLayers.SQLServer
{
    public class SubmissionFileDAL : _BaseDAL, ICommonDAL<SubmissionFile>
    {
        public SubmissionFileDAL(string connectionString) : base(connectionString)
        {
        }

        public int Add(SubmissionFile data)
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

        public SubmissionFile? Get(int id)
        {
            throw new NotImplementedException();
        }

        public bool IsUsed(int id)
        {
            throw new NotImplementedException();
        }

        public IList<SubmissionFile> List(int page = 1, int pageSize = 0, string searchValue = "")
        {
            List<SubmissionFile> list = new List<SubmissionFile>();
            using (var connection = OpenConnection())
            {
                var sql = @"select * from Provinces";
                list = connection.Query<SubmissionFile>(sql: sql, commandType: CommandType.Text).ToList();
                connection.Close();
            }    
            return list;
        }

        public bool Update(SubmissionFile data)
        {
            throw new NotImplementedException();
        }
    }
}
