using VirtualTeacher.Exceptions;
using VirtualTeacher.Helpers;
using VirtualTeacher.Models;
using VirtualTeacher.Models.DTOs.Comment;
using VirtualTeacher.Models.Enums;
using VirtualTeacher.Models.QueryParameters;
using VirtualTeacher.Repositories;
using VirtualTeacher.Repositories.Contracts;
using VirtualTeacher.Services.Contracts;

namespace VirtualTeacher.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository commentRepository;
        private readonly IUserRepository userRepository;
        private readonly ModelMapper mapper;

        public CommentService(ICommentRepository commentRepository, ModelMapper mapper, IUserRepository userRepository)
        {
            this.commentRepository = commentRepository;
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public Comment Create(int loggedId, CommentCreateDto comment)
        {
            //Get logged id from controller => authService
            var author = userRepository.GetById(loggedId);
            var newComment = mapper.MapCreate(comment, author);

            return commentRepository.Create(newComment);
        }

        public IList<Comment> FilterBy(CommentQueryParameters parameters)
        {
            return commentRepository.FilterBy(parameters);
        }

        public Comment GetById(int id)
        {
            var comment = commentRepository.GetById(id);

            return comment ?? throw new EntityNotFoundException($"Comment with id '{id}' was not found.");
        }

        //may be busted
        public Comment Update(int id, Comment updateData)
        {
            //check user authorization from controller => authService
            var updatedComment = commentRepository.Update(id, updateData);

            return updatedComment ?? throw new EntityNotFoundException($"The comment could not be updated.");
        }

        public string Delete(int id)
        {
            //check user/admin authorization from controller => authService
            bool? commentDeleted = commentRepository.Delete(id);

            if (commentDeleted == true)
                return $"Comment with id '{id}' was deleted.";

            if (commentDeleted == null)
                throw new EntityNotFoundException($"Comment with id '{id}' was not found.");

            throw new EntityNotFoundException($"Comment with id '{id}' could not be deleted.");
        }

    }
}
