//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CRMHub.EntityFramework
{
    using System;
    using System.Collections.Generic;
    
    public partial class Event
    {
        public long EventId { get; set; }
        public string Title { get; set; }
        public Nullable<System.DateTime> EventDate { get; set; }
        public string EventTime { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string Mentors { get; set; }
        public Nullable<long> VentureId { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> MeetingId { get; set; }
    }
}
