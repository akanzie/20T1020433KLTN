﻿using KLTN20T1020433.Application.Commands.StudentCommands.Create;
using KLTN20T1020433.Application.Commands.StudentCommands.Delete;
using KLTN20T1020433.Application.Commands.StudentCommands.Update;
using KLTN20T1020433.Application.DTOs;
using KLTN20T1020433.Application.DTOs.StudentDTOs;
using KLTN20T1020433.Application.Queries.StudentQueries;
using KLTN20T1020433.Application.Services;
using KLTN20T1020433.Web.AppCodes;
using KLTN20T1020433.Web.Areas.Student.Models;
using KLTN20T1020433.Web.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KLTN20T1020433.Web.Controllers.Student
{
    public class TestController : Controller
    {
        const int PAGE_SIZE = 10;
        const string TEST_SEARCH = "test_search";
        private readonly IMediator _mediator;

        public TestController(IMediator mediator)
        {
            _mediator = mediator;
        }
        public IActionResult ListTest()
        {
            GetTestsBySearchQuery? input = ApplicationContext.GetSessionData<GetTestsBySearchQuery>(TEST_SEARCH);
            if (input == null)
            {
                input = new GetTestsBySearchQuery()
                {
                    Page = 1,
                    PageSize = PAGE_SIZE,
                    FromTime = null,
                    ToTime = null,
                    SearchValue = "",
                    Status = null,
                    Type = null,
                    StudentId = "20T1020433"
                };

            }
            return View(input);
        }
        public async Task<IActionResult> Search(GetTestsBySearchQuery input)
        {
            var data = await _mediator.Send(input);
            int rowCount = await _mediator.Send(new GetRowCountQuery { StudentId = input.StudentId ?? "20T1020433", SearchValue = input.SearchValue, Status = input.Status, FromTime = input.FromTime, ToTime = input.ToTime, Type = input.Type });
            var model = new TestSearchResult()
            {
                Page = input.Page,
                PageSize = input.PageSize,
                RowCount = rowCount,
                FromTime = input.FromTime,
                ToTime = input.ToTime,
                SearchValue = input.SearchValue ?? "",
                Status = input.Status,
                Type = input.Type,
                Data = data
            };

            // Lưu lại vào session điều kiện tìm kiếm
            ApplicationContext.SetSessionData(TEST_SEARCH, input);

            return View(model);

        }
        public async Task<IActionResult> Detail(int id = 0)
        {
            var test = await _mediator.Send(new GetTestByIdQuery { Id = id });
            if (test == null)
            {
                return RedirectToAction("Index", "StudentHome");
            }
            IEnumerable<GetTestFileResponse> files = await _mediator.Send(new GetFilesByTestIdQuery { TestId = id });
            var model = new TestModel
            {
                Test = test,
                Files = files
            };
            return View(model);

        }
        public async Task<IActionResult> Submission(int testId = 0)
        {
            string studentId = "20T1020433";
            var submission = await _mediator.Send(new GetSubmissionByStudentIdAndTestIdQuery { TestId = testId, StudentId = studentId });

            if (submission != null)
            {
                var comments = await _mediator.Send(new GetCommentsBySubmissionIdQuery { SubmissionId = submission.SubmissionId });

                var model = new SubmissionModel
                {
                    Submission = submission,
                    Comments = comments
                };

                return PartialView(model);
            }
            return RedirectToAction("Index", "StudentHome");
        }
        [HttpPost]
        public async Task<IActionResult> UploadSubmissionFile(List<IFormFile> files, int submissionId = 0)
        {
            if (files == null || files.Count == 0)
                return Json("Không có tệp nào được gửi.");
            else
            {
                foreach (var item in files)
                {
                    if (await _mediator.Send(new CreateSubmissionFileCommand { File = item, SubmissionId = submissionId })) ;
                }
            }
            return Json("Tải file lên thành công.");
        }
        [HttpPost]
        public async Task<IActionResult> RemoveSubmissionFile(Guid id)
        {
            GetSubmissionFileResponse file = await _mediator.Send(new GetSubmissionFileByIdQuery { Id = id });
            if (file == null)
                return Json("Không tìm thấy file");
            else
            {
                await _mediator.Send(new RemoveSubmissionFileCommand { Id = id, FilePath = file.FilePath });
            }
            return Json("Xóa file thành công.");
        }
        [HttpPost]
        public async Task<IActionResult> Submit(int id)
        {
            var submission = await _mediator.Send(new GetSubmissionByIdQuery { Id = id });
            if (submission != null)
            {
                var ipAddress = HttpContext.Connection.RemoteIpAddress;
                GetTestByIdResponse test = await _mediator.Send(new GetTestByIdQuery { Id = submission.TestId });
                if (test!.IsConductedAtSchool && !Utils.CheckIPAddress(ipAddress))
                {
                    return BadRequest("Địa chỉ IP không hợp lệ. Bạn không được phép nộp bài.");
                }
                SubmitTestCommand command = new SubmitTestCommand
                {
                    SubmissionId = id,
                    IPAddress = ipAddress,
                    IsCheckIP = test.IsCheckIP,
                    SubmittedTime = DateTime.Now,
                    TestEndTime = test.EndTime
                };
                if (await _mediator.Send(command))
                {
                    submission = await _mediator.Send(new GetSubmissionByIdQuery { Id = id });
                    var comments = await _mediator.Send(new GetCommentsBySubmissionIdQuery { SubmissionId = id });
                    var model = new SubmissionModel
                    {
                        Submission = submission,
                        Comments = comments
                    };

                    return PartialView("Submission", model);
                }
                else
                {
                    return BadRequest("Bạn chưa gửi file.");
                }
            }
            return BadRequest("Có lỗi xảy ra.");
        }
        [HttpPost]
        public async Task<IActionResult> Cancel(int id)
        {
            var submission = await _mediator.Send(new GetSubmissionByIdQuery { Id = id });

            if (submission != null)
            {
                var ipAddress = HttpContext.Connection.RemoteIpAddress;

                if (await _mediator.Send(new CancelSubmissionCommand { SubmissionId = id }))
                {
                    submission = await _mediator.Send(new GetSubmissionByIdQuery { Id = id });
                    var comments = await _mediator.Send(new GetCommentsBySubmissionIdQuery { SubmissionId = id });
                    var model = new SubmissionModel
                    {
                        Submission = submission,
                        Comments = comments
                    };
                    return PartialView("Submission", model);
                }
            }
            return BadRequest("Có lỗi xảy ra.");
        }
        public async Task<IActionResult> Download(Guid id)
        {
            string studentId = "20T1020433";
            bool isAuthorized = false;// await FileDataService.CheckFileAuthorize(studentId, id);
            if (isAuthorized)
            {
                GetSubmissionFileResponse fileInfo = await _mediator.Send(new GetSubmissionFileByIdQuery { Id = id });
                if (fileInfo == null)
                {
                    return BadRequest();
                }
                string filePath = fileInfo.FilePath;
                string mimeType = fileInfo.MimeType;
                if (!System.IO.File.Exists(filePath))
                {
                    return Json("Không tìm thấy file");
                }
                byte[] fileBytes = await FileUtils.ReadFileAsync(filePath);
                return File(fileBytes, mimeType, fileInfo.OriginalName);
            }
            else
            {
                return Json("Bạn không có quyền truy cập file.");
            }
        }
        public async Task<IActionResult> ListSubmissionFiles(int submissionId = 0)
        {
            var submission = await _mediator.Send(new GetSubmissionByIdQuery { Id = submissionId });
            if (submission != null)
            {
                var files = await _mediator.Send(new GetFilesBySubmissionIdQuery { SubmissionId = submissionId });
                var model = new SubmissionFileModel
                {
                    Files = files,
                    Status = submission.Status
                };
                return PartialView(model);
            }
            return BadRequest("Có lỗi xảy ra.");
        }
    }
}
