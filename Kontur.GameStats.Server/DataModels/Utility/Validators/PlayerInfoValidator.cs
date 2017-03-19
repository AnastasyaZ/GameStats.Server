using FluentValidation;

namespace Kontur.GameStats.Server.DataModels.Utility.Validators
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