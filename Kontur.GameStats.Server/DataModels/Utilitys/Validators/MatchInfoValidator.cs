using FluentValidation;

namespace Kontur.GameStats.Server.DataModels.Utilitys.Validators
{
  public class MatchInfoValidator:AbstractValidator<MatchInfo>
  {
    public MatchInfoValidator()
    {
      RuleFor(info => info.endpoint).NotEmpty().WithMessage("You must specify server endpoint.");
      RuleFor(info => info.timestamp).NotEmpty().WithMessage("Timestamp asre missing or its incorrect.");
      RuleFor(info => info.result).SetValidator(new MatchResultValidator());
    }
  }
}