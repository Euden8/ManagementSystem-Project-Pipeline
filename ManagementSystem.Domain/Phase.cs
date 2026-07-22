
namespace ManagementSystem.Domain
{
    public class Phase
    { 
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Sequence { get; set; }
        public string ColorHex { get; set; } = string.Empty;
        public bool IsInitial { get; set; }
        public bool IsTerminal { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
