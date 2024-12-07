using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonitoramentoAmbiental.ViewModels;

public class AlertaViewModel
{
    [Required(ErrorMessage = "{0} é obrigatório")]
    [StringLength(255, MinimumLength = 1, ErrorMessage = "O {0} deve conter entre 1 e 255 caracteres")]
    public string Tipo { get; set; } = string.Empty;

    [Required(ErrorMessage = "{0} é obrigatório")]
    [StringLength(1024, MinimumLength = 1, ErrorMessage = "O {0} deve conter entre 1 e 1024 caracteres")]
    public string? Descricao { get; set; }

    [Required(ErrorMessage = "{0} é obrigatório")]
    [StringLength(255, MinimumLength = 1, ErrorMessage = "O {0} deve conter entre 1 e 255 caracteres")]
    public string Localidade { get; set; } = string.Empty;

    [Required(ErrorMessage = "{0} é obrigatório")]
    [StringLength(127, MinimumLength = 1, ErrorMessage = "O {0} deve conter entre 1 e 127 caracteres")]
    public string Categoria { get; set; } = string.Empty;

    [Required(ErrorMessage = "{0} é obrigatório")]
    [DataType(DataType.Date, ErrorMessage = "{0} invalida")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    [Display(Name = "Data de início")]
    public DateTime DataInicio { get; set; }

    [Required(ErrorMessage = "{0} é obrigatório")]
    [DataType(DataType.Date, ErrorMessage = "{0} invalida")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    [Display(Name = "Previsão de término")]
    public DateTime PrevisaoTermino { get; set; }
}

