using AutoMapper;
using DutyRoster.Data;
using DutyRoster.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DutyRoster
{
    public class MapperConfig
    {
        public static void ConfigureMapping()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Club, ClubModel>();
                cfg.CreateMap<ClubModel, Club>();
            });
        }
    }
}