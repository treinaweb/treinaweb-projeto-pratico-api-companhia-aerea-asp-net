namespace CiaAerea.ViewModels.Cancelamento;

public class CancelarVooViewModel
{
    public CancelarVooViewModel(string motivo, DateTime dataHoraNotificacao, int vooId)
    {
        Motivo = motivo;
        DataHoraNotificacao = dataHoraNotificacao;
        VooId = vooId;
    }

    public string Motivo { get; set; }
    public DateTime DataHoraNotificacao { get; set; }
    public int VooId { get; set; }
}