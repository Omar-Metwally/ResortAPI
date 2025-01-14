﻿using System.Text.Json.Serialization;

namespace WebApplication5.Models.DominModels.Auth
{
    public class AuthModel
    {
        public Guid id { get; set; }
        public string? Message { get; set; }
        public bool IsAuthenticated { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public List<string>? IsOwner { get; set; }
        public string? Token { get; set; }
        public DateTime? ExpiresOn { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        [JsonIgnore]
        public string? RefreshToken { get; set; }
        public ICollection<Apartment>? Apartments { get; set; } = new List<Apartment>();

        public DateTime RefreshTokenExpiration { get; set; }
    }
}