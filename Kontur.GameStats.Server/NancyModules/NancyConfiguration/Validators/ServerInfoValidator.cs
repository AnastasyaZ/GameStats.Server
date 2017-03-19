using FluentValidation;
using Kontur.GameStats.Server.DataModels;

namespace Kontur.GameStats.Server.NancyModules.NancyConfiguration.Validators
{
  public class ServerInfoValidator:AbstractValidator<ServerInfo>
  {
    public ServerInfoValidator()
    {
      RuleFor(server => server.name).NotEmpty().WithMessage("You must specify a server name");
      RuleFor(server => server.gameModes).NotEmpty().WithMessage("You must specify game models");
      RuleForEach(server => server.gameModes).NotEmpty().WithMessage("Game model cant be null or empty");
    }
  }
}