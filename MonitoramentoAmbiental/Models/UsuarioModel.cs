using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MonitoramentoAmbiental.Models;

[Index(nameof(Email), IsUnique = true)]
public class UsuarioModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ID", TypeName = "NUMBER")]
    public int Id { get; set; }
    
    [Column("NOME", TypeName = "VARCHAR(255)")]
    public string Nome { get; set; } = string.Empty;
    
    [Column("EMAIL", TypeName = "VARCHAR(255)")]
    public string Email { get; set; } = string.Empty;

    [Column("SENHA_HASH", TypeName = "VARCHAR(255)")]
    public string SenhaHash { get; set; } = string.Empty;

    [Column("CRIADO_EM", TypeName = "TIMESTAMP")]
    public DateTime CriadoEm { get; set; }

    [Column("ALTERADO_EM", TypeName = "TIMESTAMP")]
    public DateTime? AlteradoEm { get; set; }

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