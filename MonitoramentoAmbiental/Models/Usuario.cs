using System.ComponentModel.DataAnnotations;

namespace MonitoramentoAmbiental.Models;

public class Usuario
{
    [Key]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "{0} é obrigatório")]
    [StringLength(255, MinimumLength = 3, ErrorMessage = "O {0} deve conter no minimo 3 caracteres")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "{0} é obrigatório")]
    [DataType(DataType.EmailAddress, ErrorMessage = "{0} invalido")]
    public string Email { get; set; }

    [Required(ErrorMessage = "{0} é obrigatório")]
    [StringLength(255, MinimumLength = 8, ErrorMessage = "O {0} deve conter no minimo 8 caracteres")]
    public string SenhaHash { get; set; }
    
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

    public Usuario()
    {
    }

    public Usuario(string nome, string email, string senhaHash, DateTime criadoEm, DateTime alteradoEm, DateTime deletadoEm)
    {
        Nome = nome;
        Email = email;
        SenhaHash = senhaHash;
        CriadoEm = criadoEm;
        AlteradoEm = alteradoEm;
        DeletadoEm = deletadoEm;
    }
}