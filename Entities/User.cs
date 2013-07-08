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
    
    public partial class User
    {
        public User()
        {
            this.Companies = new HashSet<Company>();
            this.Graduates = new HashSet<Graduate>();
        }
    
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] Salt { get; set; }
        public byte[] Hash { get; set; }
        public short UserType { get; set; }
        public bool IsActive { get; set; }
    
        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<Graduate> Graduates { get; set; }
    }
}