using emburns.Models;

namespace emburns.PotatoModels
{
    public class CommentBaseQuery : PotatoModelBase
    {
        public int Userid { get; set; }
        public int Postid { get; set; }
        public string? Text { get; set; }
        public DateTime Created { get; set; }
        public bool Status { get; set; }
        public UserBaseQuery User { get; set; }

        public CommentBaseQuery(Comment comment)
        {
            Id = comment.Id;
            Postid = comment.Postid;
            Text = comment.Text;
            Created = comment.Created;
            Status = comment.Status;
            User = new UserBaseQuery(comment.User);
        }

        /// <summary>
        /// Gets A list of <b>CommentBaseQuery</b> from a list of <b>Models.Comment</b>
        /// </summary>
        /// <param name="comments">List of <b>Models.Comment</b></param>
        /// <returns>List of <b>CommentBaseQuery</b></returns>
        public static List<CommentBaseQuery> FromCommentModelList(List<Comment> comments)
        {
            var commentList = new List<CommentBaseQuery>();

            foreach (Comment comment in comments) {
                commentList.Add(new CommentBaseQuery(comment));
            }

            return commentList;
        }
    }
}
