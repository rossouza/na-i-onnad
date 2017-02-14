using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurrasDaTrinca.DB.ViewModel
{
    public class ParticipanteVM
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Favor escrever um nome")]
        [DisplayName("Nome")]
        public string Nome { get; set; }
    }
}
