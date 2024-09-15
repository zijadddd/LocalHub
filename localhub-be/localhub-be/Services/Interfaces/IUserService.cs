using localhub_be.Models.DTOs;

namespace localhub_be.Services.Interfaces;
public interface IUserService : IGeneric<UserIn, UserOut> {
    Task<MessageOut> ChangePassword(Guid id, ChangeUserPasswordIn request);
    Task<MessageOut> ChangeEmail(Guid id, ChangeUserEmailIn request);
    Task<MessageOut> ChangePhoneNumber(Guid id, ChangeUserPhoneNumberIn request);
    Task<MessageOut> ChangeAddressAndRegion(Guid id, ChangeUserAddressAndRegionIn request);
    Task<PictureOut> ChangeProfilePicture(Guid id, PictureIn request);
    Task<MessageOut> DeleteProfilePhoto(Guid id);
}
