//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class JobApplication
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public int GraduateId { get; set; }
        public int StatusId { get; set; }
        public System.DateTime DateApplied { get; set; }
    
        public virtual Graduate Graduate { get; set; }
        public virtual Lookup Status { get; set; }
        public virtual Job Job { get; set; }
    }
}
