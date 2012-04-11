using System.ComponentModel.DataAnnotations;

namespace Ex2AppWebFbiMostWanted.Models
{
    // HACK: criada classe de Model para tabela simples
    public class FbiMostWanted
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Full name / Nome completo")]
        public string FullName { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Photo URL / URL de fotografia")]
        public string Photo { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Alias name / Alcunha")]
        public string AliasName { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Crimes comitted / Crimes cometidos")]
        public string CrimesComitted { get; set; }
    }
}