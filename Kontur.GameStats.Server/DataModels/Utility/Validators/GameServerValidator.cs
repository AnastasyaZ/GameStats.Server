using FluentValidation;

namespace Kontur.GameStats.Server.DataModels.Utility.Validators
{
  public class GameServerValidator:AbstractValidator<ServerInfo>
  {
    public GameServerValidator()
    {
      RuleFor(server => server.name).NotEmpty().WithMessage("You must specify a server name");
      RuleFor(server => server.gameModes).NotEmpty().WithMessage("You must specify game models");
      RuleForEach(server => server.gameModes).NotEmpty().WithMessage("Game model cant be null or empty");
    }
  }
}