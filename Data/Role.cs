using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomProvider.Data
{
    public class Role
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
    }
}
