namespace ManagementSystem.Domain.Entities
{
    public class Phase
    {
        public Guid Id { get; }
        public string Name { get; }
        public int Order { get; }

        public Phase(Guid id, string name, int order)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Phase ID cannot be empty.", nameof(id));

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Phase name cannot be empty.", nameof(name));
            
            if(order <= 0)
                throw new ArgumentException("Phase order must be at least 1.", nameof(order));

            Id = id;
            Name = name;
            Order = order;
        }
    }
}

