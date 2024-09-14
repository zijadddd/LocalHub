using localhub_be.Models.DTOs;

namespace localhub_be.Services.Interfaces;
public interface IUserService {
    Task<List<UserOut>> GetAll();
    Task<UserOut> Get(Guid id);
    Task<UserOut> Create(UserIn request);
    Task<MessageOut> Delete(Guid id);
    Task<MessageOut> ChangePassword(Guid id, ChangeUserPasswordIn request);
    Task<MessageOut> ChangeEmail(Guid id, ChangeUserEmailIn request);
    Task<MessageOut> ChangePhoneNumber(Guid id, ChangeUserPhoneNumberIn request);
    Task<MessageOut> ChangeAddressAndRegion(Guid id, ChangeUserAddressAndRegionIn request);
    Task<PictureOut> ChangeProfilePicture(Guid id, PictureIn request);
    Task<MessageOut> DeleteProfilePhoto(Guid id);
}
