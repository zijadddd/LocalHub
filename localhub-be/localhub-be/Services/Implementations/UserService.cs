using localhub_be.Core;
using localhub_be.Core.Exceptions;
using localhub_be.Models.DAOs;
using localhub_be.Models.DTOs;
using localhub_be.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace localhub_be.Services.Implementations;
public sealed class UserService : IUserService {
    private readonly DatabaseContext _databaseContext;

    public UserService(DatabaseContext databaseContext) {
        _databaseContext = databaseContext;
    }

    public async Task<string> ChangePassword(int id, ChangeUserPasswordIn request) {
        User user = await _databaseContext.Users.Include(user => user.Auth).FirstOrDefaultAsync(user => user.Id == id);
        if (user is null) throw new UserNotFoundException(id);
        if (!user.Auth.Password.Equals(request.OldPassword)) throw new PasswordsDoNotMatchException();
        if (user.Auth.Password.Equals(request.NewPassword)) throw new PasswordReuseException();

        user.Auth.Password = request.NewPassword;

        _databaseContext.Users.Update(user);
        await _databaseContext.SaveChangesAsync();

        return $"Password for the user with ID {id} was successfully changed.";
    }

    public Task<UserOut> Create(UserIn request) {
        throw new NotImplementedException();
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
        if (user == null) throw new UserNotFoundException(id);

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

    public Task<UserOut> Update(UserIn request) {
        throw new NotImplementedException();
    }
}
