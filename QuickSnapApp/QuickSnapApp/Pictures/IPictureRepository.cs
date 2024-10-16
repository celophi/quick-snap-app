namespace QuickSnapApp.Pictures;
public interface IPictureRepository
{
    Task SaveAsync(byte[] data, string contentType);
}
