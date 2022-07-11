namespace CiaAerea.ViewModels.Voo;

public class ListarVooViewModel
{
    public ListarVooViewModel(int id, string origem, string destino, DateTime dataHoraPartida, DateTime dataHoraChegada)
    {
        Id = id;
        Origem = origem;
        Destino = destino;
        DataHoraPartida = dataHoraPartida;
        DataHoraChegada = dataHoraChegada;
    }

    public int Id { get; set; }
    public string Origem { get; set; }
    public string Destino { get; set; }
    public DateTime DataHoraPartida { get; set; }
    public DateTime DataHoraChegada { get; set; }
}