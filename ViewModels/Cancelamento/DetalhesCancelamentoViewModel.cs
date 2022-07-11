namespace CiaAerea.ViewModels.Cancelamento;

public class DetalhesCancelamentoViewModel
{
    public DetalhesCancelamentoViewModel(int id, string motivo, DateTime dataHoraNotificacao, int vooId)
    {
        Id = id;
        Motivo = motivo;
        DataHoraNotificacao = dataHoraNotificacao;
        VooId = vooId;
    }

    public int Id { get; set; }
    public string Motivo { get; set; }
    public DateTime DataHoraNotificacao { get; set; }
    public int VooId { get; set; }
}