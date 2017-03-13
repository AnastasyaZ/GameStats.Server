using FluentValidation;

namespace Kontur.GameStats.Server.DataModels.Utility.Validators
{
  public class MatchResultValidator:AbstractValidator<MatchResult>
  {
    public MatchResultValidator()
    {
      RuleFor(result => result.map).NotEmpty();
      RuleFor(result => result.gameModel).NotEmpty();
      RuleFor(result => result.fragLimit).NotEmpty();
      RuleFor(result => result.timeLimit).NotEmpty();
      RuleFor(result => result.timeElapsed).NotEmpty();
      RuleForEach(result => result.scoreboard).SetValidator(new PlayerResultValidator());
    }
  }
}