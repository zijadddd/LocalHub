using localhub_be.Core;
using localhub_be.Core.Exceptions;
using localhub_be.Models.DAOs;
using localhub_be.Models.DTOs;
using localhub_be.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace localhub_be.Services.Implementations;
public sealed class UserService : IUserService {
    private readonly DatabaseContext _databaseContext;

    public UserService(DatabaseContext databaseContext) {
        _databaseContext = databaseContext;
    }

    public async Task<UserOut> Create(UserIn request) {
        DateTime minDate = new DateTime(1900, 01, 01);
        DateTime maxDate = DateTime.Today;

        if (!(DateTime.Parse(request.BirthDate) >= minDate && DateTime.Parse(request.BirthDate) <= maxDate))
            throw new InvalidDateException("birth date");

        if (!(DateTime.Parse(request.MembershipDate) >= minDate && DateTime.Parse(request.MembershipDate) <= maxDate))
            throw new InvalidDateException("membership date");


        Auth auth = await _databaseContext.Auths.FirstOrDefaultAsync(auth => auth.Email.Equals(request.Email));

        if (auth is not null)
            throw new UserWithEmailAlreadyExistException(request.Email);

        Role role = await _databaseContext.Roles.FirstOrDefaultAsync(role => role.Name.Equals(request.Role));

        if (role is null) throw new RoleDoesNotExistException(request.Role);

        User user = new User {
            FirstName = request.FirstName,
            LastName = request.LastName,
            BirthDate = DateTime.Parse(request.BirthDate),
            Address = request.Address,
            Region = request.Region,
            PhoneNumber = request.PhoneNumber,
            MembershipDate = DateTime.Parse(request.MembershipDate),    
        };

        await _databaseContext.Users.AddAsync(user);

        Auth authInfo = new Auth {
            Email = request.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
            User = user,
            Role = role
        };

        await _databaseContext.Auths.AddAsync(authInfo);
        await _databaseContext.SaveChangesAsync();

        UserOut response = new UserOut(
            user.Id,
            user.FirstName,
            user.LastName,
            user.BirthDate,
            user.Address,
            user.Region,
            user.PhoneNumber,
            user.MembershipDate,
            user.Created,
            user.Updated,
            user.Auth.Email
        );

        return response;
    }

    public async Task<string> Delete(int id) {
        User user = await _databaseContext.Users.Include(user => user.Auth).FirstOrDefaultAsync(user => user.Id == id);
        if (user is null) throw new UserNotFoundException(id);

        _databaseContext.Remove(user);
        await _databaseContext.SaveChangesAsync();

        return $"User with id {id} was successfully deleted.";
    }

    public async Task<UserOut> Get(int id) {
        User user = await _databaseContext.Users.Include(user => user.Auth).FirstOrDefaultAsync(user => user.Id == id);
        if (user is null) throw new UserNotFoundException(id);

        UserOut response = new UserOut(
            user.Id, 
            user.FirstName, 
            user.LastName, 
            user.BirthDate,
            user.Address, 
            user.Region, 
            user.PhoneNumber, 
            user.MembershipDate, 
            user.Created, 
            user.Updated, 
            user.Auth.Email
        );
        
        return response;
    }

    public async Task<List<UserOut>> GetAll() {
        List<User> users = await _databaseContext.Users.Include(user => user.Auth).ToListAsync();
        if (users is null) throw new NoUsersFoundException();

        List<UserOut> response = users.Select(user => new UserOut(
            user.Id, 
            user.FirstName, 
            user.LastName, 
            user.BirthDate, 
            user.Address, 
            user.Region, 
            user.PhoneNumber, 
            user.MembershipDate, 
            user.Created, 
            user.Updated, 
            user.Auth.Email
        )).ToList();

        return response;
    }

    public async Task<string> ChangePassword(int id, ChangeUserPasswordIn request) {
        Auth auth = await _databaseContext.Auths.FirstOrDefaultAsync(auth => auth.UserId == id);

        if (auth is null) throw new UserAuthInfoNotFoundException(id);
        if (!auth.Password.Equals(request.OldPassword)) throw new PasswordsDoNotMatchException();
        if (auth.Password.Equals(request.NewPassword)) throw new PasswordReuseException();

        auth.Password = request.NewPassword;
        auth.Updated = DateTime.Now;

        _databaseContext.Auths.Update(auth);
        await _databaseContext.SaveChangesAsync();

        return $"Password for the user with id {id} was successfully changed.";
    }

    public async Task<string> ChangePhoneNumber(int id, ChangeUserPhoneNumberIn request) {
        User user = await _databaseContext.Users.FirstOrDefaultAsync(user => user.Id == id);
        if (user is null) throw new UserNotFoundException(id);

        user.PhoneNumber = request.PhoneNumber;
        user.Updated = DateTime.Now;

        _databaseContext.Users.Update(user);
        await _databaseContext.SaveChangesAsync();

        return $"Phone number for the user with id {id} was successfully changed.";
    }

    public async Task<string> ChangeAddressAndRegion(int id, ChangeUserAddressAndRegionIn request) {
        User user = await _databaseContext.Users.FirstOrDefaultAsync(user => user.Id == id);
        if (user is null) throw new UserNotFoundException(id);

        user.Address = request.Address;
        user.Region = request.Region;
        user.Updated = DateTime.Now;

        _databaseContext.Users.Update(user);
        await _databaseContext.SaveChangesAsync();

        if (!request.Address.IsNullOrEmpty() && request.Region.IsNullOrEmpty()) 
            return $"Address for the user with id {id} was successfully changed.";

        if (request.Address.IsNullOrEmpty() && !request.Region.IsNullOrEmpty()) 
            return $"Region for the user with id {id} was successfully changed.";

        return $"Address and region for the user with id {id} was successfully changed.";
    }

    public async Task<string> ChangeEmail(int id, ChangeUserEmailIn request) {
        Auth auth = await _databaseContext.Auths.FirstOrDefaultAsync(auth => auth.UserId == id);
        if (auth is null) throw new UserAuthInfoNotFoundException(id);

        auth.Email = request.Email;
        auth.Updated = DateTime.Now;

        _databaseContext.Auths.Update(auth);
        await _databaseContext.SaveChangesAsync();

        return $"Email for the user with id {id} was successfully changed.";
    }
}
