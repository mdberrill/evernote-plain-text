using System;
using System.Collections.Generic;
using System.Text;
using EvernoteSDK;
using EvernoteSDK.Advanced;

namespace EvernotePlainText
{
    public class EvernotePlainText
    {
        public EvernotePlainText(string sessionDeveloperToken, string sessionNoteStoreUrl)
        {
            AuthenticateSession(sessionDeveloperToken, sessionNoteStoreUrl);
        }

        public PlainTextEvernoteNote GetNote(string noteGuid)
        {
            var note = ENSessionAdvanced.SharedSession.PrimaryNoteStore.GetNote(noteGuid, true, true, false, false);

            var enmlContent = note.Content;
            var htmlNote = ENSessionAdvanced.SharedSession.NoteHTMLContent(enmlContent, new ENCollection());
            return new PlainTextEvernoteNote(HTMLToText(htmlNote), note.Title, note.UpdateSequenceNum);
        }

        public int GetLatestAccountUpdateNo()
        {
            var syncState = ENSessionAdvanced.SharedSession.PrimaryNoteStore.GetSyncState();
            return syncState.UpdateCount;
        }

        public IEnumerable<PlainTextEvernoteNote> GetNotesChangedSinceUpdateNo(int updateNoFrom, int maxCount)
        {
            var lastChangeResult = ENSessionAdvanced.SharedSession.PrimaryNoteStore.GetFilteredSyncChunk(updateNoFrom, maxCount, new Evernote.EDAM.NoteStore.SyncChunkFilter() { IncludeNotes = true });
            foreach (var noteChanged in lastChangeResult.Notes)
            {
                yield return GetNote(noteChanged.Guid);
            }
        }

        // Taken from the EvenoteSDK.ENMLtoHTMLConverter class, which needed to add newlines when div element ends. It gives double spacing is some circumstances but this is ok.
        private static string HTMLToText(string HTMLCode)
        {
            string result;
            try
            {
                HTMLCode = HTMLCode.Replace("\n", " ");
                HTMLCode = HTMLCode.Replace("\t", " ");
                HTMLCode = System.Text.RegularExpressions.Regex.Replace(HTMLCode, "\\s+", " ");
                HTMLCode = System.Text.RegularExpressions.Regex.Replace(HTMLCode, "<head.*?</head>", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Singleline);
                HTMLCode = System.Text.RegularExpressions.Regex.Replace(HTMLCode, "<script.*?</script>", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Singleline);
                StringBuilder stringBuilder = new StringBuilder(HTMLCode);
                string[] array = new string[]
                {
                "&nbsp;",
                "&amp;",
                "&quot;",
                "&lt;",
                "&gt;",
                "&reg;",
                "&copy;",
                "&bull;",
                "&trade;"
                };
                string[] array2 = new string[]
                {
                " ",
                "&",
                "\"",
                "<",
                ">",
                "Â®",
                "Â©",
                "â€¢",
                "â„¢"
                };
                for (int i = 0; i < array.Length; i++)
                {
                    stringBuilder.Replace(array[i], array2[i]);
                }
                stringBuilder.Replace("<br>", "\n<br>");
                stringBuilder.Replace("<br ", "\n<br ");
                stringBuilder.Replace("<p ", "\n<p ");
                stringBuilder.Replace("</div>", "\n");
                result = System.Text.RegularExpressions.Regex.Replace(stringBuilder.ToString(), "<[^>]*>", "");
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        private static void AuthenticateSession(string sessionDeveloperToken, string sessionNoteStoreUrl)
        {
            ENSessionAdvanced.SetSharedSessionDeveloperToken(sessionDeveloperToken, sessionNoteStoreUrl);

            if (ENSessionAdvanced.SharedSession.IsAuthenticated == false)
            {
                ENSessionAdvanced.SharedSession.AuthenticateToEvernote();
            }
        }
    }   
}
