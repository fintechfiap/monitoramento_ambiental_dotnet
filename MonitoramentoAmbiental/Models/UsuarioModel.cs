using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MonitoramentoAmbiental.Models;

[Table("Usuarios")]
[Index(nameof(Email), IsUnique = true)]
public class UsuarioModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ID", TypeName = "NUMBER")]
    public int Id { get; set; }

    [Required(ErrorMessage = "{0} é obrigatório")]
    [StringLength(255, MinimumLength = 1, ErrorMessage = "O {0} deve conter entre 1 e 255 caracteres")]
    [Column("NOME", TypeName = "VARCHAR(255)")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "{0} é obrigatório")]
    [DataType(DataType.EmailAddress, ErrorMessage = "{0} invalido")]
    [EmailAddress]
    [Column("EMAIL", TypeName = "VARCHAR(255)")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "{0} é obrigatório")]
    [StringLength(255, MinimumLength = 8, ErrorMessage = "O {0} deve conter entre 8 e 255 caracteres")]
    [Column("SENHA_HASH", TypeName = "VARCHAR(255)")]
    public string SenhaHash { get; set; } = string.Empty;

    [Required(ErrorMessage = "{0} é obrigatório")]
    [DataType(DataType.DateTime, ErrorMessage = "{0} inválida")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
    [Display(Name = "Data de criação")]
    [Column("CRIADO_EM", TypeName = "TIMESTAMP")]
    public DateTime CriadoEm { get; set; }

    [DataType(DataType.DateTime, ErrorMessage = "{0} inválida")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
    [Display(Name = "Data de alteração")]
    [Column("ALTERADO_EM", TypeName = "TIMESTAMP")]
    public DateTime? AlteradoEm { get; set; }

    [DataType(DataType.DateTime, ErrorMessage = "{0} inválida")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
    [Display(Name = "Data de exclusão")]
    [Column("DELETADO_EM", TypeName = "TIMESTAMP")]
    public DateTime? DeletadoEm { get; set; }

    public UsuarioModel()
    {
    }

    public UsuarioModel(string nome, string email, string senhaHash, DateTime criadoEm, DateTime alteradoEm, DateTime deletadoEm)
    {
        Nome = nome;
        Email = email;
        SenhaHash = senhaHash;
        CriadoEm = criadoEm;
        AlteradoEm = alteradoEm;
        DeletadoEm = deletadoEm;
    }
}