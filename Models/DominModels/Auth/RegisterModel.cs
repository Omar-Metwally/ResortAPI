﻿using System.ComponentModel.DataAnnotations;

namespace WebApplication5.Models.DominModels.Auth
{
    public class RegisterModel
    {
        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        [StringLength(50)]
        public string Username { get; set; }

        [StringLength(128)]
        public string Email { get; set; }

        [StringLength(256)]
        public string Password { get; set; }

        public bool IsOwner { get; set; }

    }
}