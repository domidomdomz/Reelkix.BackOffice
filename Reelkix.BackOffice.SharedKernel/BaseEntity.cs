namespace Reelkix.BackOffice.SharedKernel
{
    public abstract class BaseEntity
    {
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        protected BaseEntity() // protected constructor to ensure that the CreatedAt and UpdatedAt properties are set when an entity is created.
        {
            CreatedAt = DateTime.UtcNow; // Set CreatedAt to the current UTC time when the entity is created.
            UpdatedAt = DateTime.UtcNow; // Set UpdatedAt to the current UTC time when the entity is created.
        }

        public void Touch()
        {
            UpdatedAt = DateTime.UtcNow; // Update the UpdatedAt property to the current UTC time whenever the entity is modified.
        }
    }
}
