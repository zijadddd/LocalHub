using localhub_be.Models.DTOs;

namespace localhub_be.Services.Interfaces;
public interface IGeneric<TDtoIn, TDtoOut> {
    Task<List<TDtoOut>> GetAll();
    Task<TDtoOut> Get(Guid id);
    Task<TDtoOut> Create(TDtoIn request);
    Task<TDtoOut> Update(Guid id, TDtoIn request);
    Task<MessageOut> Delete(Guid id);
}
