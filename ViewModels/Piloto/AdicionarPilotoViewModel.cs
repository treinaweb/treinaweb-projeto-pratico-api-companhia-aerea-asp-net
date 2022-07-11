namespace CiaAerea.ViewModels.Piloto;

public class AdicionarPilotoViewModel
{
    public AdicionarPilotoViewModel(string nome, string matricula)
    {
        Nome = nome;
        Matricula = matricula;
    }

    public string Nome { get; set; }
    public string Matricula { get; set; }
}