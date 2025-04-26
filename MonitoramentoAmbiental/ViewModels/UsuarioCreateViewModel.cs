using System.ComponentModel.DataAnnotations;

namespace MonitoramentoAmbiental.ViewModels;

public class UsuarioCreateViewModel
{
    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength(255, ErrorMessage = "O nome deve ter no máximo 255 caracteres")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O e-mail é obrigatório")]
    [EmailAddress(ErrorMessage = "E-mail inválido")]
    [StringLength(255, ErrorMessage = "O e-mail deve ter no máximo 255 caracteres")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "A senha é obrigatória")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "A senha deve ter entre 6 e 100 caracteres")]
    public string Senha { get; set; } = string.Empty;

    [Required(ErrorMessage = "A role é obrigatória")]
    [StringLength(50, ErrorMessage = "A role deve ter no máximo 50 caracteres")]
    public string Role { get; set; } = "Operador";
} 