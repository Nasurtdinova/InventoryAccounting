using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryAccounting
{
    class Connection
    {
        public static dbo.InventoryAccountingEntities connection = new dbo.InventoryAccountingEntities();
    }
}
