using MediatR;
using ManagementSystem.Domain;
using ManagementSystem.Infrastructure;

namespace ManagementSystem.Application.Phases.Commands.CreatePhase;

public class CreatePhaseCommandHandler : IRequestHandler<CreatePhaseCommand, Guid>
{
    private readonly ApplicationDbContext _context;

    public CreatePhaseCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    // to do , llogjika te kalohet ne Domain dhe konstrukstori te jete privat 
    public async Task<Guid> Handle(CreatePhaseCommand request, CancellationToken cancellationToken)
    {
        return new Guid();
    }
}
