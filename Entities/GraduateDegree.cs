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
    
    public partial class GraduateDegree
    {
    	public int Id { get; set; }
    	public int GraduateId { get; set; }
    	public string University { get; set; }
    	public string Degree { get; set; }
    	public string Specialisation { get; set; }
    
    	public virtual Graduate Graduate { get; set; }
    }
}
