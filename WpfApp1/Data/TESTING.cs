//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfApp1.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class TESTING
    {
        public int ID_TESTING { get; set; }
        public Nullable<int> ID_REGISTRATION { get; set; }
        public Nullable<int> ID_TEST { get; set; }
        public Nullable<int> ID_ANSWER { get; set; }
    
        public virtual ANSWERS ANSWERS { get; set; }
        public virtual REGISTRATION REGISTRATION { get; set; }
        public virtual TEST_T TEST_T { get; set; }
    }
}
