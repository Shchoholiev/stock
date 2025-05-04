using System.ComponentModel.DataAnnotations;

namespace Stock.Core.Entities
{
    public class EntityBase
    {
        [Key]
        public int Id { get; set; }
    }
}
