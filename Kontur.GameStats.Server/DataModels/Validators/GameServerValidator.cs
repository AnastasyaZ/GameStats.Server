﻿using FluentValidation;

namespace Kontur.GameStats.Server.DataModels.Validators
{
  public class GameServerValidator:AbstractValidator<GameServer>
  {
    public GameServerValidator()
    {
      RuleFor(server => server.name).NotEmpty().WithMessage("You must specify a server name");
      RuleFor(server => server.gameModes).NotEmpty().WithMessage("You must specify game models");
    }
  }
}