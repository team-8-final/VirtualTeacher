using Microsoft.EntityFrameworkCore;
using VirtualTeacher.Data;
using VirtualTeacher.Models;
using VirtualTeacher.Models.QueryParameters;
using VirtualTeacher.Repositories.Contracts;

namespace VirtualTeacher.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext context;

        public CommentRepository(AppDbContext context)
        {
            this.context = context;
        }

        private IQueryable<Comment> GetComments()
        {
            return context.Comments
                .Include(c => c.Lecture)
                .Include(c => c.Author);
        }

        public Comment Create(Comment comment)
        {
            context.Comments.Add(comment);
            context.SaveChanges();

            return comment;
        }

        public Comment? GetById(int id)
        {
            Comment? comment = GetComments().FirstOrDefault(c => c.Id == id);

            return comment;
        }

        public Comment? Update(int id, Comment updateData)
        {
            var updatedComment = GetById(id);

            if (updatedComment == null)
                return null;

            updatedComment.Content = updateData.Content ?? updatedComment.Content;
            updatedComment.IsEdited = true;
            updatedComment.EditDate = DateTime.Now;

            context.Update(updatedComment);
            context.SaveChanges();

            return updatedComment;
        }

        public bool? Delete(int id)
        {
            Comment? comment = GetById(id);

            if (comment == null)
                return null;

            comment.IsDeleted = true;
            context.SaveChanges();

            return true;
        }

        public IList<Comment> FilterBy(CommentQueryParameters parameters)
        {
            IQueryable<Comment> result = GetComments();

            result = FilterByAuthor(result, parameters.Student);
            result = FilterByFirstName(result, parameters.FirstName);
            result = FilterByLastName(result, parameters.LastName);
            result = SortBy(result, parameters.SortBy);
            result = OrderBy(result, parameters.SortOrder);

            return new List<Comment>(result.ToList());
        }

        public int GetCommentCount()
        {
            return context.Comments.Count();
        }

        //Query methods
        private static IQueryable<Comment> FilterByAuthor(IQueryable<Comment> comments, string username)
        {
            if (!string.IsNullOrEmpty(username))
            {
                username = username.ToLower();
                return comments.Where(c => c.Author.Username.ToLower().Contains(username));
            }
            else return comments;
        }

        private static IQueryable<Comment> FilterByFirstName(IQueryable<Comment> comments, string firstName)
        {
            if (!string.IsNullOrEmpty(firstName))
            {
                firstName = firstName.ToLower();
                return comments.Where(c => c.Author.FirstName.ToLower().Contains(firstName));
            }
            else return comments;
        }

        private static IQueryable<Comment> FilterByLastName(IQueryable<Comment> comments, string lastName)
        {
            if (!string.IsNullOrEmpty(lastName))
            {
                lastName = lastName.ToLower();
                return comments.Where(c => c.Author.LastName.ToLower().Contains(lastName));
            }
            else return comments;
        }

        public IQueryable<Comment> SortBy(IQueryable<Comment> comments, string sortByCriteria)
        {
            switch (sortByCriteria)
            {
                case "date":
                    return comments.OrderBy(c => c.CreatedOn);
                default:
                    return comments;
            }
        }

        private static IQueryable<Comment> OrderBy(IQueryable<Comment> comments, string sortOrder)
        {
            return (sortOrder == "desc") ? comments.Reverse() : comments;
        }
    }
}
