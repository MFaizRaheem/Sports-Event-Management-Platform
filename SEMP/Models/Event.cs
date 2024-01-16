﻿using Microsoft.AspNetCore.Components.Routing;


namespace SEMP.Models
{
    public class Event
    {
        public int Id { get; set; }
 
        public string Name { get; set; }

        public DateTime Date { get; set; }

        public string Location { get; set; }
    }
}
