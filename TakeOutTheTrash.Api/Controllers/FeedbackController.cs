using System;
using Microsoft.AspNetCore.Mvc;
using TakeOutTheTrash.Api.Models;
using TakeOutTheTrash.Api.Repositories;
using FluentValidation;
using FluentValidation.Results;
using System.Collections.Generic;

namespace TakeOutTheTrash.Api.Controllers
{
    [ApiController]
    [Route("feedback")]
    public class FeedbackController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly IValidator<FeedbackSubmission> _requestValidator;

        public FeedbackController(
            IRepository repository,
            IValidator<FeedbackSubmission> requestValidator
            )
        {
            _repository = repository; // import logger
            _requestValidator = requestValidator;
        }

        [HttpPost]
        public IActionResult Post([FromBody] FeedbackSubmission request) // make async once repository is fleshed-out // set up cors
        {
            var validationResult = _requestValidator.Validate(request); // make async once reposity is fleshed-out
            if (!validationResult.IsValid)
            {
                var validationMessage = GetErrorStrings(validationResult);
                //log
                return BadRequest(validationMessage);
            }

            request.CreateDate = DateTime.UtcNow;

            var feedbackSubmission = _repository.AddFeedbackSubmission(request);

            if (!feedbackSubmission)
            {
                return BadRequest();
            }

            return Ok();
        }

        public string[] GetErrorStrings(ValidationResult validationResult)
        {
            var errorMessageList = new List<string>();

            foreach (var error in validationResult.Errors)
            {
                errorMessageList.Add(error.ErrorMessage);
            }

            return errorMessageList.ToArray();
        }
    }
}

