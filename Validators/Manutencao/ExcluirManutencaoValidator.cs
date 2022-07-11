using CiaAerea.Contexts;
using CiaAerea.ViewModels.Manutencao;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CiaAerea.Validators.Manutencao;

public class ExcluirManutencaoValidator: AbstractValidator<int>
{
    private readonly CiaAereaContext _context;

    public ExcluirManutencaoValidator(CiaAereaContext context)
    {
        _context = context;

        RuleFor(id => _context.Manutencoes.Find(id))
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("Id da manutenção inválido.")
            .Must(manutencao => manutencao!.DataHora > DateTime.Now).WithMessage("Não é possível excluir uma manutenção já realizada.");
    }
}