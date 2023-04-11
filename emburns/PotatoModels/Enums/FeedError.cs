namespace emburns.PotatoModels.Enums
{
    public enum FeedError
    {
        None,
        FeedEmpty, // No text and no attachment
        TextTooShort,
        TextTooLong,
        InvalidAttachment,
        InvalidAttachmentType,        
        ParentNotFoundOrInvalid,
    }
}
