namespace emburns.Media
{
    public interface IStorageService
    {
        public string UploadFile(IFormFile file, PotatoModels.Enums.AttachmentType type);
    }
}
