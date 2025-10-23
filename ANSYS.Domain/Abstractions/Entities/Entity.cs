using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANSYS.Domain.Abstractions.Entities
{
    public class Entity<T> : IEntity
    {
        public T Id { get; set; }
    }
}
