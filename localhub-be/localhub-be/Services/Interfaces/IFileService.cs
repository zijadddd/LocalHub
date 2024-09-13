using localhub_be.Models.DTOs;

namespace localhub_be.Services.Interfaces;
public interface IFileService {
    Task<PictureOut> SaveFile(PictureIn request);
    MessageOut DeleteFile(string fileNameWithExtension);
}
