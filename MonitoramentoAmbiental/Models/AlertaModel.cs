using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MonitoramentoAmbiental.Models;

[Table("Alertas")]
[Index(nameof(Id), IsUnique = true)]
public class AlertaModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ID", TypeName = "NUMBER")]
    public int Id { get; set; }

    [Required(ErrorMessage = "{0} é obrigatório")]
    [StringLength(255, MinimumLength = 1, ErrorMessage = "O {0} deve conter entre 1 e 255 caracteres")]
    [Column("TIPO", TypeName = "VARCHAR2(255 BYTE)")]
    public string Tipo { get; set; } = string.Empty;

    [Required(ErrorMessage = "{0} é obrigatório")]
    [StringLength(1024, MinimumLength = 1, ErrorMessage = "O {0} deve conter entre 1 e 1024 caracteres")]
    [Column("DESCRICAO", TypeName = "VARCHAR2(1024 BYTE)")]
    public string? Descricao { get; set; }

    [Required(ErrorMessage = "{0} é obrigatório")]
    [StringLength(255, MinimumLength = 1, ErrorMessage = "O {0} deve conter entre 1 e 255 caracteres")]
    [Column("LOCALIDADE", TypeName = "VARCHAR2(255 BYTE)")]
    public string Localidade { get; set; } = string.Empty;

    [Required(ErrorMessage = "{0} é obrigatório")]
    [StringLength(127, MinimumLength = 1, ErrorMessage = "O {0} deve conter entre 1 e 127 caracteres")]
    [Column("CATEGORIA", TypeName = "VARCHAR2(127 BYTE)")]
    public string Categoria { get; set; } = string.Empty;

    [Required(ErrorMessage = "{0} é obrigatório")]
    [DataType(DataType.Date, ErrorMessage = "{0} invalida")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    [Display(Name = "Data de início")]
    [Column("DATA_INICIO", TypeName = "DATE")]
    public DateTime DataInicio { get; set; }

    [Required(ErrorMessage = "{0} é obrigatório")]
    [DataType(DataType.Date, ErrorMessage = "{0} invalida")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    [Display(Name = "Previsão de término")]
    [Column("PREVISAO_TERMINO", TypeName = "DATE")]
    public DateTime PrevisaoTermino { get; set; }

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

    public AlertaModel()
    {

    }

    public AlertaModel(string tipo, string descricao, string localidade, string categoria, DateTime dataInicio, DateTime previsaoTermino, DateTime criadoEm, DateTime alteradoEm, DateTime deletadoEm)
    {
        Tipo = tipo;
        Descricao = descricao;
        Localidade = localidade;
        Categoria = categoria;
        DataInicio = dataInicio;
        PrevisaoTermino = previsaoTermino;
        CriadoEm = criadoEm;
        AlteradoEm = alteradoEm;
        DeletadoEm = deletadoEm;
    }
}