using Dapper.Contrib.Extensions;
using System.ComponentModel.DataAnnotations;

namespace WebAPI_CRUD_Ingresso.Models
{
    [Table("filme")]
    public class Filme
    {
        [Dapper.Contrib.Extensions.Key]
        public int IdFilme { get; set; }

        [Required]
        public string NomeFilme { get; set; }

        [Required]
        public string Genero { get; set; }

        [Required]
        public int? ClassificacaoIdade { get; set; }

        [Required]
        public string Sinopse { get; set; }

        [Required]
        public int? Duracao { get; set; }
    }
}
