//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InventoryAccounting.dbo
{
    using System;
    using System.Collections.Generic;
    
    public partial class Storage
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Storage()
        {
            this.Accounting_Card = new HashSet<Accounting_Card>();
            this.Expenditure_Invoice = new HashSet<Expenditure_Invoice>();
            this.Receipt_Invoice = new HashSet<Receipt_Invoice>();
        }
    
        public int ID_Storage { get; set; }
        public string Name { get; set; }
        public Nullable<int> Phone { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Accounting_Card> Accounting_Card { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Expenditure_Invoice> Expenditure_Invoice { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Receipt_Invoice> Receipt_Invoice { get; set; }
    }
}