using CiaAerea.Contexts;
using CiaAerea.ViewModels.Voo;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CiaAerea.Validators.Voo;

public class AtualizarVooValidator: AbstractValidator<AtualizarVooViewModel>
{
    private readonly CiaAereaContext _context;

    public AtualizarVooValidator(CiaAereaContext context)
    {
        _context = context;

        RuleFor(v => v.Origem)
            .NotEmpty().WithMessage("É necessário informar a origem do voo.")
            .Length(3).WithMessage("Aeroporto de origem inválido.");
        
        RuleFor(v => v.Destino)
            .NotEmpty().WithMessage("É necessário informar o destino do voo.")
            .Length(3).WithMessage("Aeroporto de destino inválido.");

        RuleFor(v => v)
            .Must(voo => voo.DataHoraPartida > DateTime.Now).WithMessage("A data/hora de partida deve ser superior à data e hora atuais.")
            .Must(voo => voo.DataHoraChegada > voo.DataHoraPartida).WithMessage("A data/hora de chegada deve ser superior à data/hora de partida.");

        RuleFor(v => v).Custom((voo, validationContext) =>{
            var piloto = _context.Pilotos
                                 .Include(p => p.Voos)
                                 .FirstOrDefault(p => p.Id == voo.PilotoId);

            if (piloto == null)
            {
                validationContext.AddFailure("Piloto inválido.");
            }
            else
            {
                var pilotoEmVoo = piloto.Voos.Any(v => v.Id != voo.Id &&
                                                       (v.DataHoraPartida <= voo.DataHoraPartida && v.DataHoraChegada >= voo.DataHoraChegada) ||
                                                       (v.DataHoraPartida >= voo.DataHoraPartida && v.DataHoraChegada <= voo.DataHoraChegada) ||
                                                       (v.DataHoraChegada >= voo.DataHoraPartida && v.DataHoraChegada <= voo.DataHoraChegada));

                if (pilotoEmVoo)  
                {
                    validationContext.AddFailure("Este piloto estará em voo no horário selecionado.");
                }                   
            }

            var aeronave = _context.Aeronaves      
                                   .Include(a => a.Voos)
                                   .Include(a => a.Manutencoes)
                                   .FirstOrDefault(a => a.Id == voo.AeronaveId);

            if (aeronave == null)
            {
                validationContext.AddFailure("Aeronave inválida.");
            } 
            else
            {
                var aeronaveEmVoo = aeronave.Voos.Any(v => v.Id != voo.Id &&
                                                           (v.DataHoraPartida <= voo.DataHoraPartida && v.DataHoraChegada >= voo.DataHoraChegada) ||
                                                           (v.DataHoraPartida >= voo.DataHoraPartida && v.DataHoraChegada <= voo.DataHoraChegada) ||
                                                           (v.DataHoraChegada >= voo.DataHoraPartida && v.DataHoraChegada <= voo.DataHoraChegada));

                if (aeronaveEmVoo)  
                {
                    validationContext.AddFailure("Esta aeronave estará em voo no horário selecionado.");
                }                            

                var aeronaveEmManutencao = aeronave.Manutencoes.Any(m => m.DataHora >= voo.DataHoraPartida && m.DataHora <= voo.DataHoraChegada);

                if (aeronaveEmManutencao)
                {
                    validationContext.AddFailure("Esta aeronave estará em manutenção no horário selecionado.");
                }
            }

        });  
    }
}