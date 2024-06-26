﻿using Dapper;
using KLTN20T1020433.Domain.Submission;
using KLTN20T1020433.Domain.Test;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.Infrastructure.Repositories
{
    public class SubmissionFileRepository : _BaseRepository, ISubmissionFileRepository
    {
        public SubmissionFileRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<bool> Add(SubmissionFile file)
        {
            try
            {
                bool result = false;
                using (var connection = await OpenConnectionAsync())
                {
                    var parameters = new
                    {
                        FileId = file.FileId,
                        FileName = file.FileName ?? "",
                        FilePath = file.FilePath ?? "",
                        MimeType = file.MimeType ?? "",
                        Size = file.Size,
                        SubmissionId = file.SubmissionId,
                        OriginalName = file.OriginalName ?? ""
                    };

                    // Call the stored procedure
                    result = await connection.ExecuteAsync("AddSubmissionFile", parameters, commandType: CommandType.StoredProcedure) > 0;
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đã xảy ra lỗi khi thêm tệp bài nộp: " + ex.Message);
                throw;
            }
        }
        public async Task<bool> CheckFileAuthorize(string studentId, Guid id)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đã xảy ra lỗi khi kiểm tra quyền sở hữu tệp: " + ex.Message);
                throw;
            }
        }

        public async Task<bool> Delete(Guid fileId)
        {
            try
            {
                bool result = false;
                using (var connection = await OpenConnectionAsync())
                {
                    var parameters = new
                    {
                        FileId = fileId
                    };

                    // Call the stored procedure
                    result = await connection.ExecuteAsync("DeleteSubmissionFile", parameters, commandType: CommandType.StoredProcedure) > 0;
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đã xảy ra lỗi khi xóa tệp bài nộp: " + ex.Message);
                throw;
            }
        }
        public async Task<IEnumerable<SubmissionFile>> GetFilesBySubmissionId(int submissionId)
        {
            try
            {
                List<SubmissionFile> files;
                using (var connection = await OpenConnectionAsync())
                {
                    files = (await connection.QueryAsync<SubmissionFile>("GetFilesBySubmissionId",
                                                                         new { SubmissionId = submissionId },
                                                                         commandType: CommandType.StoredProcedure))
                                                                         .ToList();
                }
                return files;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đã xảy ra lỗi khi truy vấn các tệp của bài nộp: " + ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<SubmissionFile>> GetFilesByTestId(int testId)
        {
            try
            {
                List<SubmissionFile> files;
                using (var connection = await OpenConnectionAsync())
                {
                    files = (await connection.QueryAsync<SubmissionFile>("GetSubmissionFilesByTestId",
                                                                         new { TestId = testId },
                                                                         commandType: CommandType.StoredProcedure))
                                                                         .ToList();
                }
                return files;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đã xảy ra lỗi khi truy vấn các tệp của bài nộp: " + ex.Message);
                throw;
            }
        }

        public async Task<SubmissionFile?> GetById(Guid fileId)
        {
            try
            {
                SubmissionFile? file = null;
                using (var connection = await OpenConnectionAsync())
                {                    
                    var parameters = new
                    {
                        FileId = fileId
                    };
                    file = await connection.QueryFirstOrDefaultAsync<SubmissionFile>("GetSubmissionFileById", param: parameters, commandType: CommandType.StoredProcedure);
                }
                return file;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đã xảy ra lỗi khi truy vấn tệp bài nộp theo ID: " + ex.Message);
                throw;
            }
        }

        public async Task<int> CountFilesBySubmissionId(int submissionId)
        {
            try
            {
                int count = 0;
                using (var connection = await OpenConnectionAsync())
                {
                    var parameters = new
                    {
                        SubmissionId = submissionId

                    };
                    count = await connection.ExecuteScalarAsync<int>(
                        "CountFilesBySubmissionId", parameters, commandType: CommandType.StoredProcedure);
                }
                return count;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đã xảy ra lỗi khi đếm số lượng file bài nộp của sinh viên: " + ex.Message);
                throw;
            }
        }
    }
}
