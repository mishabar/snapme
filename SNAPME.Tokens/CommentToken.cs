using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SNAPME.Data;

namespace SNAPME.Tokens
{
    public class CommentToken
    {
        public string user_id { get; set; }
        public string username { get; set; }
        public int rating { get; set; }
        public string comment { get; set; }
    }

    public static class CommentTokenExtensions
    {
        public static CommentToken AsToken(this Comment comment)
        {
            return new CommentToken
            {
                user_id = comment.user_id,
                username = comment.username,
                rating = comment.rating,
                comment = comment.comment
            };
        }
    }
}
