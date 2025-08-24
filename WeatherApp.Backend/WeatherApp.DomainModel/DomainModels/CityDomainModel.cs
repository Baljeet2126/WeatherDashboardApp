﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.DomainModel.DomainModels
{
    public class CityDomainModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;
    }
}
