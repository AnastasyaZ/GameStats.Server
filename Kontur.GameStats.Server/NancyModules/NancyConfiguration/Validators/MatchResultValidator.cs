using FluentValidation;
using Kontur.GameStats.Server.DataModels;

namespace Kontur.GameStats.Server.NancyModules.NancyConfiguration.Validators
{
  public class MatchResultValidator:AbstractValidator<MatchResult>
  {
    public MatchResultValidator()
    {
      RuleFor(result => result.map).NotEmpty();
      RuleFor(result => result.gameMode).NotEmpty();
      RuleFor(result => result.fragLimit).GreaterThanOrEqualTo(0);
      RuleFor(result => result.timeLimit).GreaterThanOrEqualTo(0);
      RuleFor(result => result.timeElapsed).GreaterThanOrEqualTo(0);
      RuleForEach(result => result.scoreboard).SetValidator(new PlayerInfoValidator());
    }
  }
}