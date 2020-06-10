using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dominios.Classes
{
    public class Sessoes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("Fase", TypeName = "int")]
        public int Fase { get; set; }

        [Required]
        [Column("SequenciaCorreta", TypeName = "varchar(255)")]
        public string SequenciaCorreta { get; set; }

        [Required]
        [Column("SequenciaRecebida", TypeName = "varchar(255)")]
        public string SequenciaRecebida { get; set; }

        [Required]
        [Column("Errou", TypeName = "bit")]
        public bool Errou { get; set; }

        [Required]
        [Column("PassarDeFase", TypeName = "bit")]
        public bool PassarDeFase { get; set; }


    }
}
