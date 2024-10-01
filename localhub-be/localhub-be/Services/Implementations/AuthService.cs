using localhub_be.Core;
using localhub_be.Core.Exceptions;
using localhub_be.Models.DAOs;
using localhub_be.Models.DTOs;
using localhub_be.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace localhub_be.Services.Implementations;
public sealed class AuthService : IAuthService {

    private readonly DatabaseContext _databaseContext;
    private readonly IConfiguration _configuration;

    public AuthService(DatabaseContext databaseContext, IConfiguration configuration) {
        _databaseContext = databaseContext;
        _configuration = configuration;
    }

    public async Task<AuthOut> AuthenticateUser(AuthIn request) {
        Auth? userAuthInfo = await _databaseContext.Auths.Include(uai => uai.Role).Include(uai => uai.User).Include(uai => uai.SuspendInfo).FirstOrDefaultAsync(user => user.Email == request.Email);

        if (userAuthInfo.SuspendInfo is not null) throw new UserIsSuspendedException();
        if (userAuthInfo is null) throw new UserAuthInfoNotFoundException(request.Email!);
        if (!BCrypt.Net.BCrypt.Verify(request.Password, userAuthInfo.Password)) throw new IncorrectPasswordException();

        return new AuthOut(CreateTokenAsync(userAuthInfo));
    }

    public async Task<MessageOut> SuspendUser(Guid id, SuspendIn request) {
        User user = await _databaseContext.Users.FirstOrDefaultAsync(user => user.Id.Equals(id));
        Auth auth = await _databaseContext.Auths.Include(auth => auth.SuspendInfo).FirstOrDefaultAsync(auth => auth.UserId.Equals(id));

        if (user is null || auth is null) throw new UserNotFoundException(id);

        SuspendInfo suspendInfo = new SuspendInfo();
        suspendInfo.Reason = request.Reason;
        auth.SuspendInfo = suspendInfo;
        _databaseContext.Auths.Update(auth);
        await _databaseContext.SaveChangesAsync();  

        return new MessageOut($"User with id {id} is suspended.");
    }

    private string CreateTokenAsync(Auth userAuthInfo) {
        List<Claim> claims = new List<Claim> {
            new Claim(ClaimTypes.NameIdentifier, userAuthInfo.UserId!.ToString()),
            new Claim(ClaimTypes.Role, userAuthInfo?.Role?.Name!)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("Secret")!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1)
            ,
            signingCredentials: creds
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }
}
