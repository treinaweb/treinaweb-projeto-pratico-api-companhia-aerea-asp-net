namespace CiaAerea.ViewModels.Piloto;

public class ListarPilotoViewModel
{
    public ListarPilotoViewModel(int id, string nome)
    {
        Id = id;
        Nome = nome;
    }

    public int Id { get; set; }
    public string Nome { get; set; }
}