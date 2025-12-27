using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchMakingService.Domain.Entities
{
    public record MatchResponseDTO
    {
        [Required] public bool IsWhite {get; set;}
        [Required] public string link { get; set; }
    }
}
