using Dapper;
using KLTN20T1020433.Domain.Comment;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.Infrastructure.Repositories
{
    public class CommentRepository : _BaseRepository, ICommentRepository
    {
        public CommentRepository(string connectionString) : base(connectionString)
        {
        }      

        public async Task<int> Add(Comment data)
        {
            int id = 0;
            using (var connection = await OpenConnectionAsync())
            {
                var parameters = new
                {
                    Body = data.Body,
                    TeacherId = data.TeacherId,
                    SubmissionId = data.SubmissionId,
                    CommentedTime = data.CommentedTime
                };
                id = await connection.ExecuteScalarAsync<int>(
                    "AddComment", parameters, commandType: CommandType.StoredProcedure);
            }
            return id;
        }
        public async Task<bool> Delete(int id)
        {
            bool result = false;
            using (var connection = await OpenConnectionAsync())
            {
                var parameters = new
                {
                    CommentId = id
                };
                result = await connection.ExecuteAsync(
                    "DeleteComment", parameters, commandType: CommandType.StoredProcedure) > 0;
            }
            return result;
        }

        public async Task<Comment?> GetById(int id)
        {
            Comment? data = null;
            using (var connection = await OpenConnectionAsync())
            {
                var parameters = new
                {
                    CommentId = id
                };
                data = await connection.QueryFirstOrDefaultAsync<Comment>(
                    "GetCommentById", parameters, commandType: CommandType.StoredProcedure);
            }
            return data;
        }
       

        public async Task<IEnumerable<Comment>> GetCommentsBySubmissionId(int submissionId)
        {
            List<Comment> comments = new List<Comment>();
            using (var connection = await OpenConnectionAsync())
            {
                var parameters = new
                {
                    SubmissionId = submissionId
                };
                comments = (await connection.QueryAsync<Comment>(
                    "GetCommentsBySubmissionId", parameters, commandType: CommandType.StoredProcedure)).ToList();
            }
            return comments;
        }

        public async Task<bool> Update(Comment data)
        {
            bool result = false;
            using (var connection = await OpenConnectionAsync())
            {
                var parameters = new
                {
                    CommentId = data.CommentId,
                    Body = data.Body,
                    TeacherId = data.TeacherId,
                    SubmissionId = data.SubmissionId,
                    CommentedTime = data.CommentedTime
                };
                result = await connection.ExecuteAsync(
                    "UpdateComment", parameters, commandType: CommandType.StoredProcedure) > 0;
            }

            return result;
        }
        
    }
}
