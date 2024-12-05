using System.ComponentModel.DataAnnotations;

namespace MonitoramentoAmbiental.Models;

public class Alerta
{
    [Key]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "{0} é obrigatório")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "O {0} deve conter no minimo 3 caracteres")]
    public string Tipo { get; set; }

    [Required(ErrorMessage = "{0} é obrigatório")]
    [StringLength(1000, MinimumLength = 3, ErrorMessage = "O {0} deve conter no minimo 3 caracteres")]
    [Display(Name = "Descrição")]
    public string Descricao { get; set; }

    [Required(ErrorMessage = "{0} é obrigatório")]
    public string Localidade { get; set; }
    
    [Required(ErrorMessage = "{0} is required")]
    [DataType(DataType.Date, ErrorMessage = "{0} invalida")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    [Display(Name = "Data de início")]
    public DateTime DataInicio { get; set; }
    
    [DataType(DataType.Date, ErrorMessage = "{0} invalida")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    [Display(Name = "Previsão de término")]
    public DateTime PrevisaoTermino { get; set; }
    
    [Required(ErrorMessage = "{0} is required")]
    [DataType(DataType.Date, ErrorMessage = "{0} invalida")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    [Display(Name = "Data de criação")]
    public DateTime CriadoEm { get; set; }
    
    [DataType(DataType.Date, ErrorMessage = "{0} invalida")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    [Display(Name = "Data de alteração")]
    public DateTime AlteradoEm { get; set; }

    [DataType(DataType.Date, ErrorMessage = "{0} invalida")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy")] 
    [Display(Name = "Data de exclusão")]
    public DateTime DeletadoEm { get; set; }
    
    public Alerta()
    {
        
    }

    public Alerta(string tipo, string descricao, string localidade, DateTime dataInicio, DateTime previsaoTermino, DateTime criadoEm, DateTime alteradoEm, DateTime deletadoEm)
    {
        Tipo = tipo;
        Descricao = descricao;
        Localidade = localidade;
        DataInicio = dataInicio;
        PrevisaoTermino = previsaoTermino;
        CriadoEm = criadoEm;
        AlteradoEm = alteradoEm;
        DeletadoEm = deletadoEm;
    }
}