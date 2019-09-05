namespace CoordinateConverter.FileInteractions
{
    interface IDragDropTarget
    {
        void OnFileDrop(string[] filepaths);
        void OnTextDrop(string str);
    }
}
