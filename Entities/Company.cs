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
    
    public partial class Company
    {
        public Company()
        {
            this.CompanyJobs = new HashSet<CompanyJob>();
        }
    
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Sector { get; set; }
        public string City { get; set; }
        public string Profile { get; set; }
        public string Website { get; set; }
        public string LinkedIn { get; set; }
        public string GooglePlus { get; set; }
        public bool IsActive { get; set; }
    
        public virtual User User { get; set; }
        public virtual ICollection<CompanyJob> CompanyJobs { get; set; }
    }
}
