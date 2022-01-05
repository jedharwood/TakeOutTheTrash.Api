using TakeOutTheTrash.Api.Models;
using FluentValidation;

namespace TakeOutTheTrash.Api.Validators
{
    public class FeedbackSubmissionValidator : AbstractValidator<FeedbackSubmission>
    {
        public FeedbackSubmissionValidator()
        {
            RuleFor(x => x.Description).NotEmpty().MaximumLength(500);
            RuleFor(x => x.CityId).NotEmpty().GreaterThanOrEqualTo(0);
        }
    }
}
