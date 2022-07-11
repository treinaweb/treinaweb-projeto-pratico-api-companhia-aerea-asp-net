using System.Text;
using CiaAerea.Contexts;
using CiaAerea.Entities;
using CiaAerea.Validators.Cancelamento;
using CiaAerea.Validators.Voo;
using CiaAerea.ViewModels;
using CiaAerea.ViewModels.Cancelamento;
using CiaAerea.ViewModels.Piloto;
using CiaAerea.ViewModels.Voo;
using DinkToPdf;
using DinkToPdf.Contracts;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CiaAerea.Services;

public class VooService
{
    private readonly CiaAereaContext _context;
    private readonly AdicionarVooValidator _adicionarVooValidator;
    private readonly AtualizarVooValidator _atualizarVooValidator;
    private readonly ExcluirVooValidator _excluirVooValidator;
    private readonly CancelarVooValidator _cancelarVooValidator;
    private readonly IConverter _converter;
    public VooService(CiaAereaContext context, AdicionarVooValidator adicionarVooValidator, AtualizarVooValidator atualizarVooValidator, ExcluirVooValidator excluirVooValidator, CancelarVooValidator cancelarVooValidator, IConverter converter)
    {
        _context = context;
        _adicionarVooValidator = adicionarVooValidator;
        _atualizarVooValidator = atualizarVooValidator;
        _excluirVooValidator = excluirVooValidator;
        _cancelarVooValidator = cancelarVooValidator;
        _converter = converter;
    }

    public DetalhesVooViewModel AdicionarVoo(AdicionarVooViewModel dados)
    {
        _adicionarVooValidator.ValidateAndThrow(dados);

        var voo = new Voo
        (
            dados.Origem, 
            dados.Destino,
            dados.DataHoraPartida,
            dados.DataHoraChegada,
            dados.AeronaveId,
            dados.PilotoId
        );

        _context.Add(voo);
        _context.SaveChanges();

        return ListarVooPeloId(voo.Id)!;
    }

    public IEnumerable<ListarVooViewModel> ListarVoos(string? origem, string? destino, DateTime? partida, DateTime? chegada)
    {
        var filtroOrigem = (Voo voo) => string.IsNullOrWhiteSpace(origem) || voo.Origem == origem;
        var filtroDestino = (Voo voo) => string.IsNullOrWhiteSpace(destino) || voo.Destino == destino;
        var filtroPartida = (Voo voo) => !partida.HasValue || voo.DataHoraPartida >= partida;
        var filtroChegada = (Voo voo) => !chegada.HasValue || voo.DataHoraChegada <= chegada;

        return _context.Voos
                       .Where(filtroOrigem)
                       .Where(filtroDestino)
                       .Where(filtroPartida)
                       .Where(filtroChegada)
                       .Select(v => new ListarVooViewModel
                        (
                            v.Id,
                            v.Origem,
                            v.Destino,
                            v.DataHoraPartida,
                            v.DataHoraChegada
                        ));
    }

    public DetalhesVooViewModel? ListarVooPeloId(int id)
    {
        var voo = _context.Voos.Include(v => v.Aeronave)
                               .Include(v => v.Piloto)
                               .Include(v => v.Cancelamento)
                               .FirstOrDefault(v => v.Id == id);

        if (voo != null)
        {
            var resultado = new DetalhesVooViewModel
            (
                voo.Id, 
                voo.Origem,
                voo.Destino,
                voo.DataHoraPartida,
                voo.DataHoraChegada,
                voo.AeronaveId,
                voo.PilotoId
            );

            resultado.Aeronave = new DetalhesAeronaveViewModel
            (
                voo.Aeronave.Id,
                voo.Aeronave.Fabricante,
                voo.Aeronave.Modelo,
                voo.Aeronave.Codigo
            );

            resultado.Piloto = new DetalhesPilotoViewModel
            (
                voo.Piloto.Id,
                voo.Piloto.Nome,
                voo.Piloto.Matricula
            );

            if (voo.Cancelamento != null)
            {
                resultado.Cancelamento = new DetalhesCancelamentoViewModel
                (
                    voo.Cancelamento.Id, 
                    voo.Cancelamento.Motivo,
                    voo.Cancelamento.DataHoraNotificacao,
                    voo.Cancelamento.VooId
                );
            }
            return resultado;
        }

        return null;
    }

    public DetalhesVooViewModel? AtualizarVoo(AtualizarVooViewModel dados)
    {
        _atualizarVooValidator.ValidateAndThrow(dados);

        var voo = _context.Voos.Find(dados.Id);

        if (voo != null)
        {
            voo.Origem = dados.Origem;
            voo.Destino = dados.Destino;
            voo.DataHoraPartida = dados.DataHoraPartida;
            voo.DataHoraChegada = dados.DataHoraChegada;
            voo.AeronaveId = dados.AeronaveId;
            voo.PilotoId = dados.PilotoId;

            _context.Update(voo);
            _context.SaveChanges();

            return ListarVooPeloId(voo.Id);
        }

        return null;
    }

    public void ExcluirVoo(int id)
    {
        _excluirVooValidator.ValidateAndThrow(id);

        var voo = _context.Voos.Find(id);

        if (voo != null)
        {
            _context.Remove(voo);
            _context.SaveChanges();
        }
    }

    public DetalhesVooViewModel? CancelarVoo(CancelarVooViewModel dados)
    {
        _cancelarVooValidator.ValidateAndThrow(dados);

        var cancelamento = new Cancelamento(dados.Motivo, dados.DataHoraNotificacao, dados.VooId);

        _context.Add(cancelamento);
        _context.SaveChanges();

        return ListarVooPeloId(dados.VooId);
    }

    public byte[]? GerarFichaDoVoo(int id)
    {
        var voo = _context.Voos.Include(v => v.Aeronave)
                               .Include(v => v.Piloto)
                               .Include(v => v.Cancelamento)
                               .FirstOrDefault(v => v.Id == id);

        if (voo != null)
        {
            var builder = new StringBuilder();

            builder.Append($"<h1 style='text-align: center'>Ficha do Voo { voo.Id.ToString().PadLeft(10, '0') }</h1>")
                   .Append($"<hr>")
                   .Append($"<p><b>ORIGEM:</b> { voo.Origem } (saída em { voo.DataHoraPartida:dd/MM/yyyy} às { voo.DataHoraPartida:hh:mm})</p>")
                   .Append($"<p><b>DESTINO:</b> { voo.Destino} (chegada em { voo.DataHoraChegada:dd/MM/yyyy} às { voo.DataHoraChegada:hh:mm})</p>")
                   .Append($"<hr>")
                   .Append($"<p><b>AERONAVE:</b> { voo.Aeronave!.Codigo } ({ voo.Aeronave.Fabricante } { voo.Aeronave.Modelo })</p>")
                   .Append($"<hr>")
                   .Append($"<p><b>PILOTO:</b> { voo.Piloto!.Nome } ({ voo.Piloto.Matricula})</p>")
                   .Append($"<hr>");
            if (voo.Cancelamento != null)
            {
                builder.Append($"<p style='color: red'><b>VOO CANCELADO:</b> { voo.Cancelamento.Motivo }</p>");
            }

            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4
                },
                Objects = {
                    new ObjectSettings() {
                        PagesCount = true,
                        HtmlContent = builder.ToString(),
                        WebSettings = { DefaultEncoding = "utf-8" }
                    }
                }
            };

            return _converter.Convert(doc);
        }

        return null;
    }
}