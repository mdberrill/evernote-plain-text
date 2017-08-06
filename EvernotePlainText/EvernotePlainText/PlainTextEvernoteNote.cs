namespace EvernotePlainText
{
    public class PlainTextEvernoteNote
    {
        public string PlainTextContent { get; private set; }
        public string Title { get; private set; }
        public int UpdateNo { get; private set; }

        public PlainTextEvernoteNote(string plainText, string title, int updateNo)
        {
            PlainTextContent = plainText;
            Title = title;
            UpdateNo = updateNo;
        }
    }
}
