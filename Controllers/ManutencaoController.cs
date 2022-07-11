using CiaAerea.Services;
using CiaAerea.ViewModels.Manutencao;
using Microsoft.AspNetCore.Mvc;

namespace CiaAerea.Controllers;

[Route("api/manutencoes")]
[ApiController]
public class ManutencaoController: ControllerBase
{
    private readonly ManutencaoService _manutencaoService;

    public ManutencaoController(ManutencaoService manutencaoService)
    {
        _manutencaoService = manutencaoService;
    }

    [HttpPost]
    public IActionResult AdicionarManutencao(AdicionarManutencaoViewModel dados)
    {
        var manutencao = _manutencaoService.AdicionarManutencao(dados);
        return Ok(manutencao);
    }

    [HttpGet]
    public IActionResult ListarManutencoes(int aeronaveId)
    {
        return Ok(_manutencaoService.ListarManutencoes(aeronaveId));
    }

    [HttpPut("{id}")]
    public IActionResult AtualizarManutencao(int id, AtualizarManutencaoViewModel dados)
    {
        if (id != dados.Id)
            return BadRequest("O id informado na URL é diferente do id informado no corpo da requisição.");

        var manutencao = _manutencaoService.AtualizarManutencao(dados);
        return Ok(manutencao);
    }

    [HttpDelete("{id}")]
    public IActionResult ExcluirManutencao(int id)
    {
        _manutencaoService.ExcluirManutencao(id);
        return NoContent();
    }
}