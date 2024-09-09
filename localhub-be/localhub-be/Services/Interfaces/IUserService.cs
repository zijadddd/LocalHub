using localhub_be.Models.DTOs;

namespace localhub_be.Services.Interfaces;
public interface IUserService {
    Task<List<UserOut>> GetAll();
    Task<UserOut> Get(int id);
    Task<UserOut> Create(UserIn request);
    Task<string> Delete(int id);
    Task<string> ChangePassword(int id, ChangeUserPasswordIn request);
    Task<string> ChangeEmail(int id, ChangeUserEmailIn request);
    Task<string> ChangePhoneNumber(int id, ChangeUserPhoneNumberIn request);
    Task<string> ChangeAddressAndRegion(int id, ChangeUserAddressAndRegionIn request);
}
