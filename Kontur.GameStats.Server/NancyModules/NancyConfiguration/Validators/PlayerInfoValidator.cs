using FluentValidation;
using Kontur.GameStats.Server.DataModels;

namespace Kontur.GameStats.Server.NancyModules.NancyConfiguration.Validators
{
  public class PlayerInfoValidator:AbstractValidator<PlayerInfo>
  {
    public PlayerInfoValidator()
    {
      RuleFor(result => result.name).NotEmpty();
      RuleFor(result => result.frags).NotEmpty();
      RuleFor(result => result.kills).GreaterThanOrEqualTo(0);
      RuleFor(result => result.deaths).GreaterThanOrEqualTo(0);
    }
  }
}