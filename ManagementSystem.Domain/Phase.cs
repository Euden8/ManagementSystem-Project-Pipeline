namespace ManagementSystem.Domain
{
    public class Phase
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public int Sequence { get; private set; }
        public string ColorHex { get; private set; } = string.Empty;
        public bool IsInitial { get; private set; }
        public bool IsTerminal { get; private set; }
        public bool IsActive { get; private set; } = true;

        private Phase()
        {
        }

        private Phase(
            Guid id,
            string name,
            int sequence,
            string colorHex,
            bool isInitial,
            bool isTerminal)
        {
            Id = id;
            Name = name;
            Sequence = sequence;
            ColorHex = colorHex;
            IsInitial = isInitial;
            IsTerminal = isTerminal;
            IsActive = true;
        }

        public static Phase Create(
            string name,
            int sequence,
            string colorHex,
            bool isInitial,
            bool isTerminal)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Phase name cannot be empty.", nameof(name));

            if (sequence < 0)
                throw new ArgumentOutOfRangeException(nameof(sequence), "Sequence cannot be negative.");

            if (string.IsNullOrWhiteSpace(colorHex))
                throw new ArgumentException("Phase color cannot be empty.", nameof(colorHex));

            if (!IsValidHexColor(colorHex))
            {
                throw new ArgumentException(
                    $"'{colorHex}' is not a valid hex color (expected format '#RRGGBB').",
                    nameof(colorHex));
            }

            if (isInitial && isTerminal)
                throw new ArgumentException("A phase cannot be both initial and terminal.");

            return new Phase(
                Guid.NewGuid(),
                name.Trim(),
                sequence,
                colorHex.Trim(),
                isInitial,
                isTerminal);
        }

        public void Deactivate() => IsActive = false;
        public void Activate() => IsActive = true;

        private static bool IsValidHexColor(string value)
        {
            if (value.Length != 7 || value[0] != '#')
                return false;

            for (var i = 1; i < value.Length; i++)
            {
                if (!Uri.IsHexDigit(value[i]))
                    return false;
            }

            return true;
        }
    }
}