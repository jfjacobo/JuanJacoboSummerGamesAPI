using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JuanJacoboSummerGames.Models
{
    [ModelMetadataType(typeof(SportMetaData))]
    public class SportDTO 
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        
        public ICollection<AthleteDTO> Athletes { get; set; }

    }
}
