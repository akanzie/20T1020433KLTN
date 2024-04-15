using AutoMapper;
using KLTN20T1020433.Domain.Submission;
using KLTN20T1020433.Web.Areas.Student.Commands.Delete;
using MediatR;

namespace KLTN20T1020433.Web.Areas.Student.Commands.Create
{
    public class CreateSubmissionFileCommandHandler : IRequestHandler<CreateSubmissionFileCommand, bool>
    {
        private readonly ISubmissionFileRepository _submissionFileDB;
        private readonly IMapper _mapper;
        public CreateSubmissionFileCommandHandler(ISubmissionFileRepository submissionFileDB, IMapper mapper)
        {
            _submissionFileDB = submissionFileDB;
            _mapper = mapper;
        }
        public async Task<bool> Handle(CreateSubmissionFileCommand request, CancellationToken cancellationToken)
        {
            var file = _mapper.Map<SubmissionFile>(request);
            var result = await _submissionFileDB.Add(file);
            return result;
        }
    }
}
