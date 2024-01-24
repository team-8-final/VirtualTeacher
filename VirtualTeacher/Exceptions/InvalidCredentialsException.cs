namespace VirtualTeacher.Exceptions
{
    public class InvalidCredentialsException : ApplicationException
    {
        public InvalidCredentialsException(string message)
            : base(message)
        {}
    }
}
