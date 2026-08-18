namespace ManagementSystem.Application.Phases;

public record PhaseDto(
    Guid Id,
    string Name,
    int Sequence,
    string ColorHex,
    bool IsInitial,
    bool IsTerminal,
    bool IsActive
);