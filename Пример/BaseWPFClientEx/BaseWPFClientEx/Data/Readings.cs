//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BaseWPFClientEx.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Readings
    {
        public int IdReading { get; set; }
        public Nullable<int> IdReader { get; set; }
        public Nullable<int> IdCopy { get; set; }
        public Nullable<System.DateTime> BeginDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
    
        public virtual Copies Copies { get; set; }
        public virtual Readers Readers { get; set; }
    }
}
