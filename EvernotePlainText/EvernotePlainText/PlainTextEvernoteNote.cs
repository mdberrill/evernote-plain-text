namespace EvernotePlainText
{
    public class PlainTextEvernoteNote
    {
        public string Id { get; private set; }
        public string PlainTextContent { get; private set; }
        public string Title { get; private set; }
        public int UpdateNo { get; private set; }

        public PlainTextEvernoteNote(string plainText, string title, int updateNo, string id)
        {
            Id = id;
            PlainTextContent = plainText;
            Title = title;
            UpdateNo = updateNo;
        }
    }
}
