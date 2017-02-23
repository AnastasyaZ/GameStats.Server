﻿using Nancy;

namespace Kontur.GameStats.Server.Modules
{
    public class PutDataModule : NancyModule
    {
        public PutDataModule()
        {
            Put["/servers/{endpoint}/info"] = _ =>
            {
                return HttpStatusCode.OK;
            };

            Put["/servers/{endpoint}/matches/{timestamp}"] = _ =>
            {
                return HttpStatusCode.BadGateway;
            };
        }
    }
}