using CiaAerea.Contexts;
using CiaAerea.Entities;
using CiaAerea.Validators.Manutencao;
using CiaAerea.ViewModels.Manutencao;
using FluentValidation;

namespace CiaAerea.Services;

public class ManutencaoService
{
    private readonly CiaAereaContext _context;
    private readonly AdicionarManutencaoValidator _adicionarManutencaoValidator;
    private readonly AtualizarManutencaoValidator _atualizarManutencaoValidator;
    private readonly ExcluirManutencaoValidator _excluirManutencaoValidator;

    public ManutencaoService(CiaAereaContext context, AdicionarManutencaoValidator adicionarManutencaoValidator, AtualizarManutencaoValidator atualizarManutencaoValidator, ExcluirManutencaoValidator excluirManutencaoValidator)
    {
        _context = context;
        _adicionarManutencaoValidator = adicionarManutencaoValidator;
        _atualizarManutencaoValidator = atualizarManutencaoValidator;
        _excluirManutencaoValidator = excluirManutencaoValidator;
    }

    public ListarManutencaoViewModel AdicionarManutencao(AdicionarManutencaoViewModel dados)
    {
        _adicionarManutencaoValidator.ValidateAndThrow(dados);

        var manutencao = new Manutencao(dados.DataHora, dados.Tipo, dados.AeronaveId, dados.Observacoes);

        _context.Add(manutencao);
        _context.SaveChanges();

        return new ListarManutencaoViewModel(manutencao.Id, manutencao.DataHora, manutencao.Observacoes, manutencao.Tipo, manutencao.AeronaveId);
    }

    public IEnumerable<ListarManutencaoViewModel> ListarManutencoes(int aeronaveId)
    {
        return _context.Manutencoes.Where(m => m.AeronaveId == aeronaveId)
                                   .Select(m => new ListarManutencaoViewModel
                                   (
                                       m.Id, 
                                       m.DataHora, 
                                       m.Observacoes,
                                       m.Tipo,
                                       m.AeronaveId
                                   ));
    }

    public ListarManutencaoViewModel? AtualizarManutencao(AtualizarManutencaoViewModel dados)
    {
        _atualizarManutencaoValidator.ValidateAndThrow(dados);

        var manutencao = _context.Manutencoes.Find(dados.Id);

        if (manutencao != null)
        {
            manutencao.DataHora = dados.DataHora;
            manutencao.Tipo = dados.Tipo;
            manutencao.Observacoes = dados.Observacoes;
            manutencao.AeronaveId = dados.AeronaveId;

            return new ListarManutencaoViewModel
                       (
                           manutencao.Id, 
                           manutencao.DataHora, 
                           manutencao.Observacoes,
                           manutencao.Tipo,
                           manutencao.AeronaveId
                       );
        }

        return null;
    }

    public void ExcluirManutencao(int id)
    {
        _excluirManutencaoValidator.ValidateAndThrow(id);

        var manutencao = _context.Manutencoes.Find(id);

        if (manutencao != null)
        {
            _context.Remove(manutencao);
            _context.SaveChanges();
        }
    }
}