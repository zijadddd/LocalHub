using localhub_be.Core;
using localhub_be.Models.DTOs;
using localhub_be.Services.Interfaces;

namespace localhub_be.Services.Implementations;
public sealed class UserService : IUserService {
    private readonly DatabaseContext _databaseContext;

    public UserService(DatabaseContext databaseContext) {
        _databaseContext = databaseContext;
    }

    public Task<string> ChangePassword(int id, ChangeUserPasswordIn request) {
        throw new NotImplementedException();
    }

    public Task<UserOut> Create(UserIn request) {
        throw new NotImplementedException();
    }

    public Task<string> Delete(int id) {
        throw new NotImplementedException();
    }

    public Task<UserOut> Get(int id) {
        throw new NotImplementedException();
    }

    public Task<List<UserOut>> GetAll() {
        throw new NotImplementedException();
    }

    public Task<UserOut> Update(UserIn request) {
        throw new NotImplementedException();
    }
}
