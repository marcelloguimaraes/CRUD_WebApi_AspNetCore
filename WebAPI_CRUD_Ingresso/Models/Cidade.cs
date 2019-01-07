using Dapper.Contrib.Extensions;
using System.ComponentModel.DataAnnotations;

namespace WebAPI_CRUD_Ingresso.Models
{
    [Table("cidade")]
    public class Cidade
    {
        [Dapper.Contrib.Extensions.Key]
        public int IdCidade { get; set; }

        [Required]
        public string NomeCidade { get; set; }
    }
}
