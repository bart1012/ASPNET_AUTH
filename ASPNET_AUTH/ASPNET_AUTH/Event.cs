﻿namespace ASPNET_AUTH
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Venue { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public int MaximumCapacity { get; set; }
        public int CurrentAttendance { get; set; }

    }
}
