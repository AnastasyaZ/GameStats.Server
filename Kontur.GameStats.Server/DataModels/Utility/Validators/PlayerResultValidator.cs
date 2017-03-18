using FluentValidation;

namespace Kontur.GameStats.Server.DataModels.Utility.Validators
{
  public class PlayerResultValidator:AbstractValidator<PlayerInfo>
  {
    public PlayerResultValidator()
    {
      RuleFor(result => result.name).NotEmpty();
      RuleFor(result => result.frags).NotEmpty();
      RuleFor(result => result.kills).NotEmpty();
      RuleFor(result => result.deaths).NotEmpty();
    }
  }
}