using System;
using System.Net;
using System.Numerics;
using KLTN20T1020433.DomainModels.Entities;
using KLTN20T1020433.DomainModels.Interfaces;
using Dapper;

namespace KLTN20T1020433.DataLayers.SQLServer
{
    public class SubmissionDAL : _BaseDAL, ISubmissionDAL
    {
        public SubmissionDAL(string connectionString) : base(connectionString)
        {
        }

        public int Add(Submission data)
        {
            throw new NotImplementedException();
        }

        public int AddSubmissionFile(SubmissionFile file)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int submissionId)
        {
            throw new NotImplementedException();
        }

        public bool DeleteSubmissionFile(SubmissionFile file)
        {
            throw new NotImplementedException();
        }

        public Submission Get(int SubmissionId)
        {
            throw new NotImplementedException();
        }

        public IList<SubmissionFile> GetFilesOfSubmission(int submissionId = 0)
        {
            throw new NotImplementedException();
        }

        public SubmissionFile? GetSubmissionFile(int submission, int fileId)
        {
            throw new NotImplementedException();
        }

        public IList<Submission> GetSubmissionsOfTest(int testId)
        {
            throw new NotImplementedException();
        }

        public bool Update(Submission data)
        {
            throw new NotImplementedException();
        }
    }
}
