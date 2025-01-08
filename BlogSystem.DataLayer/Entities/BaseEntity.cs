using System;
using System.ComponentModel.DataAnnotations;

namespace Blog_System.DataLayer.Entities
{
    // Represents the base entity that other entities will inherit from
    public class BaseEntity
    {
        // Unique identifier for each entity (primary key)
        [Key]
        public int Id { get; set; }

        // Date and time when the entity was created (default is the current date and time)
        public DateTime CreationDate { get; set; } = DateTime.Now;

        // Flag to indicate if the entity has been deleted (soft delete)
        public bool IsDelete { get; set; }
    }
}
