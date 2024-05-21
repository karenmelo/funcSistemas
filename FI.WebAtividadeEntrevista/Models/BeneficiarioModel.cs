using System.ComponentModel.DataAnnotations;

namespace WebAtividadeEntrevista.Models
{
    public class BeneficiarioModel
    {     
        /// <summary>
        /// Nome
        /// </summary>
        [Required]
        public string Nome { get; set; }
        /// <summary>
        /// CPF
        /// </summary>
        [Required]
        public string Cpf { get; set; }
    }
}