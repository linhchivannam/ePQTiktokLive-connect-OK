using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePQTiktokLive.MODEL
{
    public class PQComment
    {
        public string CommentId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserAvatar { get; set; }
        public string Text { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsHighlighted { get; set; } // Indicates if the comment is highlighted
        public string phoneNumber { get; set; } // Extracted phone number if any
    }
}
