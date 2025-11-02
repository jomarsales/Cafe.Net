using CafeDotNet.Core.Users.ValueObjects;

namespace CafeDotNet.Core.Users.DTOs;

public class AuthenticationResponse
{
    public long Id { get; set; }
    public string? Username { get; set; }
    public RoleType Role { get; set; }
}
