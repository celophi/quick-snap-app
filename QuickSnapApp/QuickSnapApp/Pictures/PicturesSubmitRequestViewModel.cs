namespace QuickSnapApp.Pictures;
public sealed record PicturesSubmitRequestViewModel
{
    public required byte[] Data { get; init; }
    public required string ContentType { get; init; }
}
