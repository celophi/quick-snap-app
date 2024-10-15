namespace QuickSnapApp.Pictures;
public interface IPicturesProvider
{
    Task SubmitAsync(string token, PicturesSubmitRequestViewModel viewModel);
}
