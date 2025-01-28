using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebMicroondas.Models
{
    public class AquecimentoPreDefinido
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O Nome é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O Alimento é obrigatório.")]
        public string Alimento { get; set; }

        [Required(ErrorMessage = "O Tempo é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "O Tempo não pode ser menor que 1.")]
        public int? Tempo { get; set; }

        [Range(1, 10, ErrorMessage = "A Potência deve estar entre 1 e 10.")]
        public int? Potencia { get; set; }

        [Required(ErrorMessage = "A Mensagem de Aquecimento é obrigatória.")]
        [RegularExpression(@"^[^.*\-\[\]\+]+$", ErrorMessage = "A Mensagem de Aquecimento não pode conter os seguintes caracteres: . * - [ ] +")]
        public string MensagemDeAquecimento { get; set; }
        public string InstrucoesComplementares { get; set; }
    }
}