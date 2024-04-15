﻿using KLTN20T1020433.Domain.Submission;
using MediatR;

namespace KLTN20T1020433.Web.Areas.Student.Commands.Delete
{
    public class RemoveSubmissionFileCommandHandler : IRequestHandler<RemoveSubmissionFileCommand, bool>
    {
        private readonly ISubmissionFileRepository _submissionFileDB;

        public RemoveSubmissionFileCommandHandler(ISubmissionFileRepository submissionFileDB)
        {
            _submissionFileDB = submissionFileDB;
        }
        public async Task<bool> Handle(RemoveSubmissionFileCommand request, CancellationToken cancellationToken)
        {
            bool result = false;
            if (!File.Exists(request.FilePath))
            {
                return result;
            }
            File.Delete(request.FilePath);
            result = await _submissionFileDB.Delete(request.Id);
            return result;
        }
    }
}
