using Dapper.Contrib.Extensions;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPI_CRUD_Ingresso.Models
{
    [Table("sessao")]
    public class Sessao
    {
        [Dapper.Contrib.Extensions.Key]
        public int IdSessao { get; set; }

        [Required]
        public double Preco { get; set; }
        
        [Required]
        public string DataSessao { get; set; }

        [Required]
        public string Hora { get; set; }

        [Required]
        public string TipoIdioma { get; set; }

        [Required]
        public int? IdCinema { get; set; }

        [Required]
        public int? IdSala { get; set; }

        [Required]
        public int? IdFilme { get; set; }
    }
}