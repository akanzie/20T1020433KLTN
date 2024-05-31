﻿using KLTN20T1020433.Application.Commands.StudentCommands.Create;
using KLTN20T1020433.Application.Commands.StudentCommands.Delete;
using KLTN20T1020433.Application.Commands.StudentCommands.Update;
using KLTN20T1020433.Application.Queries.StudentQueries;
using KLTN20T1020433.Application.Services;
using KLTN20T1020433.Web.AppCodes;
using KLTN20T1020433.Web.Areas.Student.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KLTN20T1020433.Web.Areas.Student.Controllers
{
    [Area("Student")]
    [Authorize]
    public class SubmissionController : Controller
    {
        private readonly IMediator _mediator;

        public SubmissionController(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<IActionResult> Detail(int testId = 0)
        {

            try
            {
                var user = User.GetUserData();
                if (testId <= 0)
                {
                    return BadRequest(ErrorMessages.GeneralError);
                }
                var submission = await _mediator.Send(new GetSubmissionByStudentIdAndTestIdQuery { TestId = testId, StudentId = user.UserId! });
                if (submission == null)
                {
                    return BadRequest(ErrorMessages.GeneralError);
                }
                var test = await _mediator.Send(new GetTestByIdQuery { Id = testId });
                if (test == null)
                {
                    return BadRequest(ErrorMessages.GeneralError);
                }
                if (submission != null)
                {
                    var comments = await _mediator.Send(new GetCommentsBySubmissionIdQuery { SubmissionId = submission.SubmissionId });
                    var model = new SubmissionModel
                    {
                        CanSubmitLate = test.CanSubmitLate,
                        TestStartTime = test.StartTime,
                        TestEndTime = test.EndTime,
                        Submission = submission,
                        Comments = comments
                    };

                    return View(model);
                }
                return BadRequest(ErrorMessages.GeneralError);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred in Detail: {ex.Message}");
                return BadRequest(ErrorMessages.RequestNotCompleted);
            }
        }
        [HttpPost]
        public async Task<IActionResult> UploadSubmissionFile(List<IFormFile> files, int testId = 0)
        {
            try
            {
                var user = User.GetUserData();
                if (testId <= 0)
                {
                    return Json(ErrorMessages.GeneralError);
                }

                var submission = await _mediator.Send(new GetSubmissionByStudentIdAndTestIdQuery { TestId = testId, StudentId = user.UserId! });
                if (submission == null)
                {
                    return Json(ErrorMessages.GeneralError);
                }
                var test = await _mediator.Send(new GetTestByIdQuery { Id = testId });
                if (test == null)
                {
                    return Json(ErrorMessages.GeneralError);
                }
                if (files == null || files.Count == 0)
                    return Json(ErrorMessages.NoFilesUploaded);
                foreach (var item in files)
                {
                    if (item == null || item.Length == 0 || item.Length >= FileUtils.MAX_FILE_SIZE)
                    {
                        return Json(ErrorMessages.InvalidOrLargeFile);
                    }
                    if (!(await _mediator.Send(new CreateSubmissionFileCommand { File = item, SubmissionId = submission.SubmissionId, CanSubmitLate = test.CanSubmitLate, TestEndTime = test.EndTime, TestTitle = test.Title, TestStartTime = test.StartTime, SubmissionStatus = submission.Status })))
                        return Json(ErrorMessages.FileUploadError);
                }
                return Json(SuccessMessages.FileUploadSuccess);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred in UploadSubmissionFile: {ex.Message}");
                return Json(ErrorMessages.RequestNotCompleted);
            }
        }
        [HttpPost]
        public async Task<IActionResult> RemoveSubmissionFile(Guid id, int testId = 0)
        {
            try
            {
                var user = User.GetUserData();
                if (testId <= 0)
                {
                    return Json(ErrorMessages.GeneralError);
                }
                var submission = await _mediator.Send(new GetSubmissionByStudentIdAndTestIdQuery { TestId = testId, StudentId = user.UserId! });
                if (submission == null)
                {
                    return Json(ErrorMessages.GeneralError);
                }
                var test = await _mediator.Send(new GetTestByIdQuery { Id = testId });
                if (test == null)
                {
                    return Json(ErrorMessages.GeneralError);
                }
                var file = await _mediator.Send(new GetSubmissionFileByIdQuery { Id = id });
                if (file == null)
                    return Json(ErrorMessages.FileNotFound);
                else
                {
                    var message = await _mediator.Send(new RemoveSubmissionFileCommand { Id = id, CanSubmitLate = test.CanSubmitLate, TestEndTime = test.EndTime, SubmissionStatus = submission.Status, TestStartTime = test.StartTime });
                    return Json(message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred in RemoveSubmissionFile: {ex.Message}");
                return Json(ErrorMessages.RequestNotCompleted);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Submit(int testId = 0)
        {
            try
            {
                var user = User.GetUserData();
                if (testId <= 0)
                {
                    return Json(ErrorMessages.GeneralError);
                }
                var submission = await _mediator.Send(new GetSubmissionByStudentIdAndTestIdQuery { TestId = testId, StudentId = user.UserId! });
                if (submission == null)
                {
                    return Json(ErrorMessages.GeneralError);
                }
                var test = await _mediator.Send(new GetTestByIdQuery { Id = testId });
                if (test == null)
                {
                    return Json(ErrorMessages.GeneralError);
                }
                if (test!.IsConductedAtSchool && !Utils.CheckIPAddress(user.ClientIP!))
                {
                    return Json(ErrorMessages.InvalidIPAddress);
                }
                if (!test.CanSubmitLate && DateTime.Now > test.EndTime)
                {
                    return Json(ErrorMessages.SubmissionTimeExceeded);
                }
                var message = await _mediator.Send(new SubmitTestCommand
                {
                    SubmissionId = submission.SubmissionId,
                    IPAddress = user.ClientIP!,
                    IsCheckIP = test.IsCheckIP,
                    SubmittedTime = DateTime.Now,
                    TestEndTime = test.EndTime,
                    CanSubmitLate = test.CanSubmitLate
                });
                return Json(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred in Submit: {ex.Message}");
                return Json(ErrorMessages.RequestNotCompleted);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Cancel(int testId = 0)
        {
            try
            {
                var user = User.GetUserData();
                if (testId <= 0)
                {
                    return Json(ErrorMessages.GeneralError);
                }
                var submission = await _mediator.Send(new GetSubmissionByStudentIdAndTestIdQuery { TestId = testId, StudentId = user.UserId });
                if (submission == null)
                {
                    return Json(ErrorMessages.GeneralError);
                }
                var test = await _mediator.Send(new GetTestByIdQuery { Id = testId });
                if (test == null)
                {
                    return Json(ErrorMessages.GeneralError);
                }
                var message = await _mediator.Send(new CancelSubmissionCommand { SubmissionId = submission.SubmissionId, CanSubmitLate = test.CanSubmitLate, TestEndTime = test.EndTime });
                return Json(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred in Cancel: {ex.Message}");
                return Json(ErrorMessages.RequestNotCompleted);
            }
        }
        public async Task<IActionResult> DownloadFile(Guid id, int testId = 0)
        {
            try
            {
                var user = User.GetUserData();
                if (testId <= 0)
                {
                    return View("NotFound");
                }

                var submission = await _mediator.Send(new GetSubmissionByStudentIdAndTestIdQuery { TestId = testId, StudentId = user.UserId! });
                if (submission == null)
                {
                    return View("NotFound");
                }
                var test = await _mediator.Send(new GetTestByIdQuery { Id = testId });
                if (test == null)
                {
                    return View("NotFound");
                }
                var file = await _mediator.Send(new GetSubmissionFileByIdQuery { Id = id });

                if (file == null || !System.IO.File.Exists(file.FilePath))
                {
                    return View("NotFound");
                }
                byte[] fileBytes = await FileUtils.ReadFileAsync(file.FilePath);
                return File(fileBytes, file.MimeType, file.OriginalName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred in DownloadTestFile: {ex.Message}");
                return Json(ErrorMessages.RequestNotCompleted);
            }
        }
        public async Task<IActionResult> SubmissionFiles(int testId = 0)
        {
            try
            {
                var user = User.GetUserData();
                if (testId <= 0)
                {
                    return BadRequest(ErrorMessages.GeneralError);
                }
                var submission = await _mediator.Send(new GetSubmissionByStudentIdAndTestIdQuery { TestId = testId, StudentId = user.UserId });
                if (submission != null)
                {
                    var files = await _mediator.Send(new GetFilesBySubmissionIdQuery { SubmissionId = submission.SubmissionId });
                    var model = new SubmissionFileModel
                    {
                        Files = files,
                        TestId = testId,
                        Status = submission.Status
                    };
                    return View(model);
                }
                return BadRequest(ErrorMessages.GeneralError);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred in ListSubmissionFiles: {ex.Message}");
                return BadRequest(ErrorMessages.RequestNotCompleted);
            }
        }
    }
}