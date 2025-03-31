using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Domain.Entities
{
    public class Client
    {
        public int Id { get; set; }
        public required string ClientId { get; set; }
        public required string Name { get; set; }
        public required string ClientURL { get; set; }
    }
}
