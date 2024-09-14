using localhub_be.Core;
using localhub_be.Core.Exceptions;
using localhub_be.Models.DAOs;
using localhub_be.Models.DTOs;
using localhub_be.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace localhub_be.Services.Implementations;
public sealed class UserService : IUserService {
    private readonly DatabaseContext _databaseContext;
    private readonly IFileService _fileService;

    public UserService(DatabaseContext databaseContext, IFileService fileService) {
        _databaseContext = databaseContext;
        _fileService = fileService;
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

        _databaseContext.Users.Add(user);

        Auth authInfo = new Auth {
            Email = request.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
            User = user,
            Role = role
        };

        _databaseContext.Auths.Add(authInfo);
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

    public async Task<MessageOut> Delete(int id) {
        User user = await _databaseContext.Users.Include(user => user.Auth).FirstOrDefaultAsync(user => user.Id == id);
        if (user is null) throw new UserNotFoundException(id);

        _databaseContext.Remove(user);
        await _databaseContext.SaveChangesAsync();

        return new MessageOut($"User with id {id} was successfully deleted.");
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

    public async Task<MessageOut> ChangePassword(int id, ChangeUserPasswordIn request) {
        Auth auth = await _databaseContext.Auths.FirstOrDefaultAsync(auth => auth.UserId == id);

        if (auth is null) throw new UserAuthInfoNotFoundException(id);

        if (!BCrypt.Net.BCrypt.Verify(request.OldPassword, auth.Password))
            throw new PasswordsDoNotMatchException();

        if (BCrypt.Net.BCrypt.Verify(request.NewPassword, auth.Password))
            throw new PasswordReuseException();


        auth.Password = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
        auth.Updated = DateTime.Now;

        _databaseContext.Auths.Update(auth);
        await _databaseContext.SaveChangesAsync();

        return new MessageOut($"Password for the user with id {id} was successfully changed.");
    }


    public async Task<MessageOut> ChangePhoneNumber(int id, ChangeUserPhoneNumberIn request) {
        User user = await _databaseContext.Users.FirstOrDefaultAsync(user => user.Id == id);
        if (user is null) throw new UserNotFoundException(id);

        List<User> samePhoneNumbers = await _databaseContext.Users.Where(user => user.PhoneNumber.Equals(request.PhoneNumber) && user.Id != id).ToListAsync();

        if (samePhoneNumbers.Count > 0) 
            throw new OtherUserExistWithNewProvidedPhoneNumberException();

        if (user.PhoneNumber.Equals(request.PhoneNumber)) throw new SamePhoneNumberException();

        user.PhoneNumber = request.PhoneNumber;
        user.Updated = DateTime.Now;

        _databaseContext.Users.Update(user);
        await _databaseContext.SaveChangesAsync();

        return new MessageOut($"Phone number for the user with id {id} was successfully changed.");
    }

    public async Task<MessageOut> ChangeAddressAndRegion(int id, ChangeUserAddressAndRegionIn request) {
        User user = await _databaseContext.Users.FirstOrDefaultAsync(user => user.Id == id);
        if (user is null) throw new UserNotFoundException(id);

        user.Address = request.Address;
        user.Region = request.Region;
        user.Updated = DateTime.Now;

        _databaseContext.Users.Update(user);
        await _databaseContext.SaveChangesAsync();

        return new MessageOut($"Address and region for the user with id {id} was successfully changed.");
    }

    public async Task<MessageOut> ChangeEmail(int id, ChangeUserEmailIn request) {
        Auth auth = await _databaseContext.Auths.FirstOrDefaultAsync(auth => auth.UserId == id);

        if (auth is null) throw new UserAuthInfoNotFoundException(id);

        List<Auth> sameEmails = await _databaseContext.Auths.Where(auth => auth.Email.Equals(request.Email) && auth.UserId != id).ToListAsync();

        if (sameEmails.Count > 0) 
            throw new OtherUserExistWithNewProvidedEmailException(); 

        if (auth.Email.Equals(request.Email)) throw new SameEmailException();

        auth.Email = request.Email;
        auth.Updated = DateTime.Now;

        _databaseContext.Auths.Update(auth);
        await _databaseContext.SaveChangesAsync();

        return new MessageOut($"Email for the user with id {id} was successfully changed.");
    }

    public async Task<PictureOut> ChangeProfilePicture(int id, PictureIn request) {
        User user = await _databaseContext.Users.FirstOrDefaultAsync(user => user.Id == id);
        if (user is null) throw new UserNotFoundException(id);

        PictureOut response = await _fileService.SaveFile(request);

        user.ProfilePhotoUrl = response.FilePath;
        user.Updated = DateTime.Now;

        _databaseContext.Users.Update(user);
        await _databaseContext.SaveChangesAsync();

        return response;
    }

    public async Task<MessageOut> DeleteProfilePhoto(int id) {
        User user = await _databaseContext.Users.FirstOrDefaultAsync(user => user.Id == id);
        if (user is null) throw new UserNotFoundException(id);

        string uploadsPrefix = "/Uploads/";
        int index = user.ProfilePhotoUrl.IndexOf(uploadsPrefix);

        if (index == -1) throw new InvalidImageNameException();

        string result = user.ProfilePhotoUrl.Substring(index + uploadsPrefix.Length);

        MessageOut response = _fileService.DeleteFile(result);

        user.ProfilePhotoUrl = "";
        user.Updated = DateTime.Now;

        _databaseContext.Users.Update(user);
        await _databaseContext.SaveChangesAsync();

        return response;
    }
}
