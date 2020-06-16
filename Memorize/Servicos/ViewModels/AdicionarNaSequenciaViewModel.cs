using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Servicos.ViewModels
{
    public class AdicionarNaSequenciaViewModel
    {
        [Required(ErrorMessage = "Informe o número referente a cor")]
        public int NumeroAdicionar { get; set; }


        [Required(ErrorMessage = "Informe a senha")]
        public string Senha { get; set; }


    }
}
