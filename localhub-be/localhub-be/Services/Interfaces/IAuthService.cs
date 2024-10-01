using localhub_be.Models.DTOs;

namespace localhub_be.Services.Interfaces;
public interface IAuthService {
    Task<AuthOut> AuthenticateUser(AuthIn request);
    Task<MessageOut> SuspendUser(Guid id, SuspendIn request);
}
