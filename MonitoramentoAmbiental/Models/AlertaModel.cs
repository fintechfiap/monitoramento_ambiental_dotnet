using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MonitoramentoAmbiental.Models;

public class AlertaModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ID", TypeName = "NUMBER")]
    public int Id { get; set; }

    [Column("TIPO", TypeName = "VARCHAR2(255 BYTE)")]
    public string Tipo { get; set; } = string.Empty;

    [Column("DESCRICAO", TypeName = "VARCHAR2(1024 BYTE)")]
    public string? Descricao { get; set; }

    [Column("LOCALIDADE", TypeName = "VARCHAR2(255 BYTE)")]
    public string Localidade { get; set; } = string.Empty;

    [Column("CATEGORIA", TypeName = "VARCHAR2(127 BYTE)")]
    public string Categoria { get; set; } = string.Empty;

    [Column("DATA_INICIO", TypeName = "DATE")]
    public DateTime DataInicio { get; set; }
    
    [Column("PREVISAO_TERMINO", TypeName = "DATE")]
    public DateTime PrevisaoTermino { get; set; }
    
    [Column("CRIADO_EM", TypeName = "TIMESTAMP")]
    public DateTime CriadoEm { get; set; }
    
    [Column("ALTERADO_EM", TypeName = "TIMESTAMP")]
    public DateTime? AlteradoEm { get; set; }
    
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