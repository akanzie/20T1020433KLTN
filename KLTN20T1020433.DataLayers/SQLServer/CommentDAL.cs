using Dapper;
using KLTN20T1020433.DataLayers.Interfaces;
using KLTN20T1020433.DomainModels.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.DataLayers.SQLServer
{
    public class CommentDAL : _BaseDAL, ICommentDAL
    {
        public CommentDAL(string connectionString) : base(connectionString)
        {
        }

        public int Add(Comment data)
        {
            int id = 0;
            using (var connection = OpenConnection())
            {
                var sql = @"INSERT INTO Comments (Body, TeacherId, SubmissionId, CommentedTime) 
                    VALUES (@Body, @TeacherId, @SubmissionId, @CommentedTime);
                    SELECT SCOPE_IDENTITY()";
                var parameters = new
                {
                    Body = data.Body,
                    TeacherId = data.TeacherId,
                    SubmissionId = data.SubmissionId,
                    CommentedTime = data.CommentedTime
                };
                id = connection.ExecuteScalar<int>(sql: sql, param: parameters, commandType: CommandType.Text);
                connection.Close();
            }
            return id;
        }  
        public bool Delete(int id)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                var sql = "DELETE FROM Comments WHERE CommentId = @CommentId";
                var parameters = new
                {
                    CommentId = id
                };
                result =  connection.Execute(sql, param: parameters) > 0;
                connection.Close();
            }
            return result;
        }

        public Comment? Get(int id)
        {
            Comment? data = null;
            using (var connection = OpenConnection())
            {
                var sql = "SELECT * FROM Comments WHERE CommentId = @CommentId";
                var parameters = new
                {
                    CommentId = id
                };
                data = connection.QueryFirstOrDefault<Comment>(sql: sql, param: parameters, commandType: CommandType.Text);
                connection.Close();
            }
            return data;
        }

        public Comment? GetBySubmissionId(int submissionId)
        {
            Comment? data = null;
            using (var connection = OpenConnection())
            {
                var sql = "SELECT * FROM Comments WHERE SubmissionId = @SubmissionId";
                var parameters = new
                {
                    SubmissionId = submissionId
                };
                data = connection.QueryFirstOrDefault<Comment>(sql: sql, param: parameters, commandType: CommandType.Text);
                connection.Close();
            }
            return data;
        }

        public IList<Comment> GetComments(int submissionId)
        {
            List<Comment> comments = new List<Comment>();
            using (var connection = OpenConnection())
            {
                var sql = @"SELECT * FROM Comments WHERE SubmissionId = @SubmissionId";
                var parameters = new
                {
                    SubmissionId = submissionId
                };
                comments = connection.Query<Comment>(sql: sql, param: parameters, commandType: CommandType.Text).ToList();
                connection.Close();
            }
            return comments;
        }

        public bool Update(Comment data)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                var sql = @"UPDATE Comments 
                    SET Body = @Body, TeacherId = @TeacherId, SubmissionId = @SubmissionId, CommentedTime = @CommentedTime
                    WHERE CommentId = @CommentId";
                var parameters = new
                {
                    Body = data.Body,
                    TeacherId = data.TeacherId,
                    SubmissionId = data.SubmissionId,
                    CommentedTime = data.CommentedTime,
                    CommentId = data.CommentId
                };
                result = connection.Execute(sql: sql, param: parameters, commandType: CommandType.Text) > 0;
                connection.Close();
            }

            return result;
        }

    }
}
