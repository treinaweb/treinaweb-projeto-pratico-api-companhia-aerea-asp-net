namespace CiaAerea.ViewModels;

public class DetalhesAeronaveViewModel
{
    public DetalhesAeronaveViewModel(int id, string fabricante, string modelo, string codigo)
    {
        Id = id;
        Fabricante = fabricante;
        Modelo = modelo;
        Codigo = codigo;
    }

    public int Id { get; set; }
    public string Fabricante { get; set; }
    public string Modelo { get; set; }
    public string Codigo { get; set; }
}