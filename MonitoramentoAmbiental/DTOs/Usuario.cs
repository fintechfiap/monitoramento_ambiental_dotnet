using System.ComponentModel.DataAnnotations;

namespace MonitoramentoAmbiental.DTOs;

public class CadastroRequest
{
    [Required(ErrorMessage = "{0} é obrigatório")]
    [StringLength(255, MinimumLength = 1, ErrorMessage = "O {0} deve conter entre 1 e 255 caracteres")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "{0} é obrigatório")]
    [DataType(DataType.EmailAddress, ErrorMessage = "{0} inválido")]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "{0} é obrigatório")]
    [StringLength(255, MinimumLength = 8, ErrorMessage = "A {0} deve conter entre 8 e 255 caracteres")]
    [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "A senha deve conter apenas caracteres alfanuméricos.")]
    public string Senha { get; set; } = string.Empty;
}

public class LoginRequest
{
    [Required(ErrorMessage = "{0} é obrigatório")]
    [DataType(DataType.EmailAddress, ErrorMessage = "{0} inválido")]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "{0} é obrigatório")]
    [StringLength(255, MinimumLength = 8, ErrorMessage = "A {0} deve conter entre 8 e 255 caracteres")]
    public string Senha { get; set; } = string.Empty;
}