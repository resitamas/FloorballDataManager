﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FloorballServer.Models.Floorball
{
    public class TeamModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Year { get; set; }

        public string Coach { get; set; }

        public short Points { get; set; }

        public short Standing { get; set; }

        public int TeamId { get; set; }

        public short Match { get; set; }

        public short Scored { get; set; }

        public short Get { get; set; }

        public int StadiumId { get; set; }

        public int LeagueId { get; set; }

    }
}