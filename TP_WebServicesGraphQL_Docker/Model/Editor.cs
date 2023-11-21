using System.ComponentModel.DataAnnotations;

namespace TP_WebServicesGraphQL_Docker.Model
{
    public class Editor
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [StringLength(128)]
        [Required]
        public string Name { get; set; }

        [Required]
        public List<Game> Games { get; set; }
    }
}
