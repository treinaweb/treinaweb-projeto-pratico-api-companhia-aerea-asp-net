namespace CiaAerea.ViewModels;

public class ListarAeronaveViewModel
{
    public ListarAeronaveViewModel(int id, string modelo, string codigo)
    {
        Id = id;
        Modelo = modelo;
        Codigo = codigo;
    }

    public int Id { get; set; }
    public string Modelo { get; set; }
    public string Codigo { get; set; }
}