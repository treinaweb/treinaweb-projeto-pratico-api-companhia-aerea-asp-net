using CiaAerea.Contexts;
using CiaAerea.ViewModels.Manutencao;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CiaAerea.Validators.Manutencao;

public class AdicionarManutencaoValidator: AbstractValidator<AdicionarManutencaoViewModel>
{
    private readonly CiaAereaContext _context;

    public AdicionarManutencaoValidator(CiaAereaContext context)
    {
        _context = context;

        RuleFor(m => m.DataHora)
            .NotEmpty().WithMessage("A data/hora da manutenção deve ser informada");

        RuleFor(m => m.Tipo)
            .NotNull().WithMessage("O tipo da manutenção deve ser informado.");

        RuleFor(m => m).Custom((manutencao, validationContext) => {
            var aeronave = _context.Aeronaves.Include(a => a.Voos)
                                             .FirstOrDefault(a => a.Id == manutencao.AeronaveId);

            if (aeronave == null)
            {
                validationContext.AddFailure("Id de aeronave inválido.");
            }                    
            else
            {
                var emVoo = aeronave.Voos.Any(v => v.DataHoraPartida <= manutencao.DataHora && v.DataHoraChegada >= manutencao.DataHora);

                if (emVoo)
                {
                    validationContext.AddFailure("A aeronave selecionada estará em voo neste horário.");
                }
            }
        });
    }
}