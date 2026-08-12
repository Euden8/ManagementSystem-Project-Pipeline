namespace ManagementSystem.Domain.Entities
{
    public abstract class BaseEntity
    {
        public DateTime CreatedAt { get; private set; }
        public string CreatedBy { get; private set; }

        public DateTime? ModifiedAt { get; private set; }
        public string? ModifiedBy { get; private set; }

        public bool IsDeleted { get; private set; }
        public DateTime? DeletedAt { get; private set; }
        public string? DeletedBy { get; private set; }

        protected BaseEntity(string createdBy)
        {
            if (string.IsNullOrWhiteSpace(createdBy))
                throw new ArgumentException("CreatedBy cannot be empty.", nameof(createdBy));

            CreatedBy = createdBy;
            CreatedAt = DateTime.UtcNow;
        }

        protected void MarkModified(string modifiedBy)
        {
            if (string.IsNullOrWhiteSpace(modifiedBy))
                throw new ArgumentException(nameof(modifiedBy));

            ModifiedBy = modifiedBy;
            ModifiedAt = DateTime.UtcNow;
        }

        protected void MarkDeleted(string deletedBy)
        {
            if (IsDeleted)
                throw new InvalidOperationException("Entity is already deleted.");

            if (string.IsNullOrWhiteSpace(deletedBy))
                throw new ArgumentException(nameof(deletedBy));

            IsDeleted = true;
            DeletedBy = deletedBy;
            DeletedAt = DateTime.UtcNow;
        }
    }
}
