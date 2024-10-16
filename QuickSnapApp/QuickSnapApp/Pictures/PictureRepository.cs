using QuickSnapApp.Providers;

namespace QuickSnapApp.Pictures;
public class PictureRepository(ISecureStorageProvider _secureStorageProvider, IPicturesProvider _picturesProvider) : IPictureRepository
{
    public async Task SaveAsync(byte[] data, string contentType)
    {
        var token = await _secureStorageProvider.GetAsync("authToken");
        await _picturesProvider.SubmitAsync(token!, new PicturesSubmitRequestViewModel { Data = data, ContentType = contentType });
    }
}
