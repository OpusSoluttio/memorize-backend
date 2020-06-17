using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Servicos.ViewModels
{
    public class DeletarSessaoViewModel
    {
        [Required(ErrorMessage = "Informe a senha")]
        public string Senha { get; set; }


    }
}
