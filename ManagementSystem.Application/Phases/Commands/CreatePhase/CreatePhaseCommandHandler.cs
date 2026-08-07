using MediatR;
using ManagementSystem.Domain;
using ManagementSystem.Application.Common.Interfaces;

namespace ManagementSystem.Application.Phases.Commands.CreatePhase;

public class CreatePhaseCommandHandler : IRequestHandler<CreatePhaseCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreatePhaseCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    // to do , llogjika te kalohet ne Domain dhe konstrukstori te jete privat 
    public async Task<Guid> Handle(CreatePhaseCommand request, CancellationToken cancellationToken)
    {
        return new Guid();
    }
}
