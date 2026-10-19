namespace ApiUsuarios.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public string Role { get; private set; } = "User";

    private User()
    {
    }

    public User(string name, string email)
    {
        Id = Guid.NewGuid();
        Update(name, email);
    }

    public User(string name, string email, string passwordHash, string role) : this(name, email)
    {
        PasswordHash = passwordHash;
        Role = role;
    }

    public void Update(string name, string email)
    {
        ValidateRequired(name, nameof(name));
        ValidateRequired(email, nameof(email));

        Name = name;
        Email = email;
    }

    private static void ValidateRequired(string value, string parameterName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException($"O campo '{parameterName}' é obrigatório.", parameterName);
        }
    }
}
