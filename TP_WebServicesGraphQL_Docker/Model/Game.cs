using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace TP_WebServicesGraphQL_Docker.Model
{
    public class Game
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [StringLength(128)]
        [Required]
        public string Name { get; set; }

        [Required]
        public List<string> Genres { get; set; }

        public DateTime PublicationDate { get; set; }

        [Required]
        public List<Editor> Editors { get; set; } = new List<Editor>();

        [Required]
        public List<Studio> Studios { get; set; } = new List<Studio>();

        [Required]
        public List<string> Platforms { get; set; } = new List<string>();
    }
}
