using localhub_be.Models.DTOs;

namespace localhub_be.Services.Interfaces;
public interface IUserService {
    Task<List<UserOut>> GetAll();
    Task<UserOut> Get(int id);
    Task<UserOut> Create(UserIn request);
    Task<MessageOut> Delete(int id);
    Task<MessageOut> ChangePassword(int id, ChangeUserPasswordIn request);
    Task<MessageOut> ChangeEmail(int id, ChangeUserEmailIn request);
    Task<MessageOut> ChangePhoneNumber(int id, ChangeUserPhoneNumberIn request);
    Task<MessageOut> ChangeAddressAndRegion(int id, ChangeUserAddressAndRegionIn request);
    Task<PictureOut> ChangeProfilePicture(int id, PictureIn request);
    Task<MessageOut> DeleteProfilePhoto(int id);
}
