using System.ComponentModel.DataAnnotations;

namespace MasterclassDapper.Models {
    public class City {

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Uf { get; set; }
    }
}