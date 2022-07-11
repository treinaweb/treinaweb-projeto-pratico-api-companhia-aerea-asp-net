using CiaAerea.Services;
using CiaAerea.ViewModels.Cancelamento;
using CiaAerea.ViewModels.Voo;
using Microsoft.AspNetCore.Mvc;

namespace CiaAerea.Controllers;


[Route("api/voos")]
[ApiController]
public class VooController: ControllerBase
{
    private readonly VooService _vooService;

    public VooController(VooService vooService)
    {
        _vooService = vooService;
    }

    [HttpPost]
    public IActionResult AdicionarVoo(AdicionarVooViewModel dados)
    {
        var voo = _vooService.AdicionarVoo(dados);
        return CreatedAtAction(nameof(ListarVooPeloId), new { voo.Id }, voo);
    }

    [HttpGet]
    public IActionResult ListarVoos(string? origem, string? destino, DateTime? partida, DateTime? chegada)
    {
        return Ok(_vooService.ListarVoos(origem, destino, partida, chegada));
    }

    [HttpGet("{id}")]
    public IActionResult ListarVooPeloId(int id)
    {
        var voo = _vooService.ListarVooPeloId(id);

        if (voo != null)
        {
            return Ok(voo);
        }

        return NotFound();
    }

    [HttpPut("{id}")]
    public IActionResult AtualizarVoo(int id, AtualizarVooViewModel dados)
    {
        if (id != dados.Id)
            return BadRequest("O id informado na URL é diferente do id informado no corpo da requisição."); 

        var voo = _vooService.AtualizarVoo(dados);

        if (voo != null)
        {
            return Ok(voo);
        }

        return NotFound();
    }

    [HttpDelete("{id}")]
    public IActionResult ExcluirVoo(int id)
    {
        _vooService.ExcluirVoo(id);
        return NoContent();
    }

    [HttpPost("cancelar")]
    public IActionResult CancelarVoo(CancelarVooViewModel dados)
    {
        var vooCancelado = _vooService.CancelarVoo(dados);
        return Ok(vooCancelado);
    }

    [HttpGet("{id}/ficha")]
    public IActionResult GerarFichaDoVoo(int id)
    {
        var conteudo = _vooService.GerarFichaDoVoo(id);

        if (conteudo != null)
            return File(conteudo, "application/pdf");

        return NotFound();
    }
}