using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GamesCatalog_API.InputModel
{
    public class GameInputModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The game name must be contain between 3 and 100 characters")]
        public string Name { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "The game name must be contain between 1 and 100 characters")]
        public string Producer { get; set; }
        [Required]
        [Range(1,1000, ErrorMessage = "The price must cost at least R$ 1,00 and a maximum of R$ 1000,00")]
        public double Price { get; set; }
    }
}
