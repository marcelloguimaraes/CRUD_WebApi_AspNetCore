using Dapper.Contrib.Extensions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAPI_CRUD_Ingresso.Models
{
    [Table("cinema")]
    public class Cinema
    {
        [Dapper.Contrib.Extensions.Key]
        public int IdCinema { get; set; }

        [Required]
        public string NomeCinema { get; set; }

        [Required]
        public string Endereco { get; set; }

        [Required]
        public int? IdCidade { get; set; }

        [Write(false)]
        public Cidade Cidade { get; set; }

        [Write(false)]
        public List<Sala> Salas { get; set; }

        [Write(false)]
        public int[] SalasArray { get; set; }
    }
}
