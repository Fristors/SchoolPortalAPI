//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SchoolPortalApi
{
    using System;
    using System.Collections.Generic;
    
    public partial class TimeTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TimeTable()
        {
            this.Lesson = new HashSet<Lesson>();
        }
    
        public int id { get; set; }
        public System.DateTime Date { get; set; }
        public int idDayOfWeek { get; set; }
        public int idClass { get; set; }
    
        public virtual Class Class { get; set; }
        public virtual DayOfWeek DayOfWeek { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Lesson> Lesson { get; set; }
    }
}
