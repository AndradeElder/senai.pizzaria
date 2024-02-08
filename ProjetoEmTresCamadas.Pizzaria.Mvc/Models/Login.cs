using System.ComponentModel.DataAnnotations;

namespace ProjetoEmTresCamadas.Pizzaria.Mvc.Models
{
    public class Login
    {
        [Required]
        [EmailAddress(ErrorMessage ="Entre com um email válido")]
        public string email { get; set; }

        [Required]        
        public string password { get; set; }
    }
}
