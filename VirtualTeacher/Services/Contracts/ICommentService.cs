using VirtualTeacher.Models.QueryParameters;
using VirtualTeacher.Models;
using VirtualTeacher.Models.DTOs.Comment;

namespace VirtualTeacher.Services.Contracts
{
    public interface ICommentService
    {
        IList<Comment> FilterBy(CommentQueryParameters parameters);

        Comment GetById(int id);
        Comment Create(int loggedId, CommentCreateDto comment);
        Comment Update(int id, Comment updateData);
        string Delete(int id);
    }
}
