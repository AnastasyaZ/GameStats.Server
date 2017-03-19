using FluentValidation;

namespace Kontur.GameStats.Server.DataModels.Utility.Validators
{
  public class GameServerInfoValidator:AbstractValidator<GameServer>
  {
    public GameServerInfoValidator()
    {
      RuleFor(info => info.endpoint).NotEmpty().WithMessage("You must specify server endpoint.");
      RuleFor(info => info.info).SetValidator(new GameServerValidator());
    }
  }
}