﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JuanJacoboSummerGames.Models
{
    [ModelMetadataType(typeof(SportMetaData))]
    public class Sport : Auditable
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        
        public ICollection<Athlete> Athletes { get; set; }

    }
}
