namespace ManagementSystem.Domain
{
    public static class PhaseTransitionRules
    {
        public static void EnsureValidTransition(Phase currentPhase, Phase targetPhase)
        {
            if (!TryValidate(currentPhase, targetPhase, out var errorMessage))
            {
                throw new InvalidOperationException(errorMessage);
            }
        }

        public static bool TryValidate(Phase currentPhase, Phase targetPhase, out string? errorMessage)
        {
            if (currentPhase is null)
                throw new ArgumentNullException(nameof(currentPhase));

            if (targetPhase is null)
                throw new ArgumentNullException(nameof(targetPhase));

            if (currentPhase.Id == targetPhase.Id)
            {
                errorMessage = $"The project is already in phase '{currentPhase.Name}'.";
                return false;
            }

            if (currentPhase.IsTerminal)
            {
                errorMessage =
                    $"Phase '{currentPhase.Name}' is terminal; a project cannot be moved out of it.";
                return false;
            }

            if (!targetPhase.IsActive)
            {
                errorMessage =
                    $"Phase '{targetPhase.Name}' is inactive and cannot receive new projects.";
                return false;
            }

            var sequenceGap = targetPhase.Sequence - currentPhase.Sequence;

            if (sequenceGap != 1 && sequenceGap != -1)
            {
                errorMessage =
                    $"Illegal phase jump: cannot move from '{currentPhase.Name}' (sequence {currentPhase.Sequence}) " +
                    $"directly to '{targetPhase.Name}' (sequence {targetPhase.Sequence}). " +
                    "Projects can only move to the immediately adjacent phase.";
                return false;
            }

            errorMessage = null;
            return true;
        }
    }
}