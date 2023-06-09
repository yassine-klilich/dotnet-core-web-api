﻿namespace PracticeWebAPI.Models
{
    public class Owner
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<Pet> Pets { get; set; } = new List<Pet>();
    }
}
