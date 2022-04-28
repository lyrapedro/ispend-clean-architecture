using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iSpend.Domain.Entities;

public abstract class Entity
{
    public int Id { get; protected set; }
    public DateTime RegisteredAt { get; protected set; }
    public DateTime ModifiedAt { get; protected set; }
}
