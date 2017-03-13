using FluentValidation;

namespace Kontur.GameStats.Server.DataModels.Utility.Validators
{
  public class GameServerInfoValidator:AbstractValidator<GameServerInfo>
  {
    public GameServerInfoValidator()
    {
      RuleFor(info => info.endpoint).NotEmpty().WithMessage("You must specify server endpoint.");
      RuleFor(info => info.gameServer).SetValidator(new GameServerValidator());
    }
  }
}