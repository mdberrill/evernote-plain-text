namespace EvernotePlainText
{
    public class PlainTextEvernoteNote
    {
        public string PlainTextContext { get; private set; }
        public string Title { get; private set; }
        public int UpdateNo { get; private set; }

        public PlainTextEvernoteNote(string plainText, string title, int updateNo)
        {
            PlainTextContext = plainText;
            Title = title;
            UpdateNo = updateNo;
        }
    }
}
