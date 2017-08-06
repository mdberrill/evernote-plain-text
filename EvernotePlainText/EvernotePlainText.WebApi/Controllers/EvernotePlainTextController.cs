using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EvernotePlainText.WebApi.Controllers
{
    public class EvernotePlainTextController : ApiController
    {
        [HttpGet]
        [ActionName("note")]
        public PlainTextEvernoteNote Get(string sessionDeveloperToken, string sessionNoteStoreUrl, string noteGuid)
        {
            return new EvernotePlainText(sessionDeveloperToken, sessionNoteStoreUrl).GetNote(noteGuid);
        }
        [HttpGet]
        [ActionName("LatestAccountUpdateNo")]
        public int GetLatestAccountUpdateNo(string sessionDeveloperToken, string sessionNoteStoreUrl)
        {
            return new EvernotePlainText(sessionDeveloperToken, sessionNoteStoreUrl).GetLatestAccountUpdateNo();
        }

        [HttpGet]
        [ActionName("NotesSince")]
        public IEnumerable<PlainTextEvernoteNote> GetNotesSince(string sessionDeveloperToken, string sessionNoteStoreUrl, int updateNoFrom, int maxCount)
        {
            return new EvernotePlainText(sessionDeveloperToken, sessionNoteStoreUrl).GetNotesChangedSinceUpdateNo(updateNoFrom, maxCount);
        }


    }
}
