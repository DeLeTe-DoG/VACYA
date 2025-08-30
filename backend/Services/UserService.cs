using System;
using backend.Interfaces;
using backend.Models;

namespace backend.Services;

public class UserService : IUser
{
  private readonly List<UserDTO> _users = new();

  public void Register(RegisterDTO dto)
  {
    var user = new UserDTO
    {
      Id = _users.Count > 0 ? _users.Max(u => u.Id) + 1 : 1,
      Name = dto.Name,
      Email = dto.Email,
      PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
    };
    _users.Add(user);
  }

  public UserDTO LogIn(string name, string emal, string password)
  {
    var user = _users.FirstOrDefault(u => u.Name == name);
    if (user == null)
    {
      return null;
    }
    bool IsPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
    if (!IsPasswordValid) return null;
    return user;
  }

  public UserDTO GetByName(string name)
  {
    var user = _users.FirstOrDefault(u => u.Name == name);
    return user;
  }
}