using CiaAerea.Entities.Enums;

namespace CiaAerea.Entities;

public class Manutencao
{
    public Manutencao(DateTime dataHora, TipoManutencao tipo, int aeronaveId, string? observacoes = null)
    {
        DataHora = dataHora;
        Observacoes = observacoes;
        Tipo = tipo;
        AeronaveId = aeronaveId;
    }

    public int Id { get; set; }
    public DateTime DataHora { get; set; }
    public string? Observacoes { get; set; }
    public TipoManutencao Tipo { get; set; }
    public int AeronaveId { get; set; }
    public Aeronave Aeronave { get; set; } = null!;
}