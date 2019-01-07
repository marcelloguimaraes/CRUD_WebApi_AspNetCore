using Dapper.Contrib.Extensions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAPI_CRUD_Ingresso.Models
{
    [Table("sala")]
    public class Sala
    {
        [Dapper.Contrib.Extensions.Key]
        public int IdSala { get; set; }

        [Required]
        public string NomeSala { get; set; }

        [Write(false)]
        public List<Sessao> Sessoes { get; set; }
    }
}