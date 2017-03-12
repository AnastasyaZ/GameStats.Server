using FluentValidation;

namespace Kontur.GameStats.Server.DataModels.Utilitys.Validators
{
  public class PlayerResultValidator:AbstractValidator<PlayerResult>
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