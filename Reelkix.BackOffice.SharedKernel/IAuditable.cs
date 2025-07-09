namespace Reelkix.BackOffice.SharedKernel
{
    public interface IAuditable
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public void Touch()
        {
            UpdatedAt = DateTime.UtcNow; // Update the UpdatedAt property to the current UTC time whenever the entity is modified.
        }
    }
}
