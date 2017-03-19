using FluentValidation;
using Kontur.GameStats.Server.DataModels;

namespace Kontur.GameStats.Server.NancyModules.NancyConfiguration.Validators
{
  public class GameServerValidator:AbstractValidator<GameServer>
  {
    public GameServerValidator()
    {
      RuleFor(info => info.endpoint).NotEmpty().WithMessage("You must specify server endpoint.");
      RuleFor(info => info.info).SetValidator(new ServerInfoValidator());
    }
  }
}