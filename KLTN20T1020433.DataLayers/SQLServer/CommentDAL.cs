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

        public async Task<int> Add(Comment data)
        {
            int id = 0;
            using (var connection = await OpenConnectionAsync())
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
                id = await connection.ExecuteScalarAsync<int>(sql: sql, param: parameters, commandType: CommandType.Text);
            }
            return id;
        }
        public async Task<bool> Delete(int id)
        {
            bool result = false;
            using (var connection = await OpenConnectionAsync())
            {
                var sql = "DELETE FROM Comments WHERE CommentId = @CommentId";
                var parameters = new
                {
                    CommentId = id
                };
                result = await connection.ExecuteAsync(sql, param: parameters) > 0;                
            }
            return result;
        }

        public async Task<Comment> GetById(int id)
        {
            Comment? data = null;
            using (var connection = await OpenConnectionAsync())
            {
                var sql = "SELECT * FROM Comments WHERE CommentId = @CommentId";
                var parameters = new
                {
                    CommentId = id
                };
                data = await connection.QueryFirstOrDefaultAsync<Comment>(sql: sql, param: parameters, commandType: CommandType.Text);
                
            }
            return data;
        }

        public async Task<Comment> GetBySubmissionId(int submissionId)
        {
            Comment? data = null;
            using (var connection = await OpenConnectionAsync())
            {
                var sql = "SELECT * FROM Comments WHERE SubmissionId = @SubmissionId";
                var parameters = new
                {
                    SubmissionId = submissionId
                };
                data = await connection.QueryFirstOrDefaultAsync<Comment>(sql: sql, param: parameters, commandType: CommandType.Text);
               
            }
            return data;
        }

        public async Task<IEnumerable<Comment>> GetCommentBySubmissionId(int submissionId)
        {
            List<Comment> comments = new List<Comment>();
            using (var connection = await OpenConnectionAsync())
            {
                var sql = @"SELECT * FROM Comments WHERE SubmissionId = @SubmissionId";
                var parameters = new
                {
                    SubmissionId = submissionId
                };
                comments = (await connection.QueryAsync<Comment>(sql: sql, param: parameters, commandType: CommandType.Text)).ToList();
                
            }
            return comments;
        }

        public async Task<bool> Update(Comment data)
        {
            bool result = false;
            using (var connection = await OpenConnectionAsync())
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
                result = await connection.ExecuteAsync(sql: sql, param: parameters, commandType: CommandType.Text) > 0;
                
            }

            return result;
        }
        
    }
}
