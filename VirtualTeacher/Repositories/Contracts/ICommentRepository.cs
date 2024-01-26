using VirtualTeacher.Models.DTOs.Course;
using VirtualTeacher.Models.QueryParameters;
using VirtualTeacher.Models;
using VirtualTeacher.Models.DTOs.Comment;

namespace VirtualTeacher.Repositories.Contracts
{
    public interface ICommentRepository
    {
        IList<Comment> FilterBy(CommentQueryParameters parameters);
        Comment? GetById(int id);
        Comment? Create(Comment comment);
        Comment? Update(int id, Comment updateData);
        bool? Delete(int id);
        int GetCommentCount();
    }
}
