﻿namespace MovieApp.Entities
{
    public class User
    {
        public Guid Id { get; set; }    
        public string Username { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
    }

    public enum UserRole
    {
        Admin = 1,
        User = 2
    }
}
