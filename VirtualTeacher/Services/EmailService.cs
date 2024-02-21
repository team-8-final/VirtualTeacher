using MailKit.Security;
using MailKit.Net.Smtp;
using MimeKit.Text;
using MimeKit;
using VirtualTeacher.Models.DTOs;
using VirtualTeacher.Services.Contracts;
using VirtualTeacher.Models;
using VirtualTeacher.Helpers;
using System.Text;


namespace VirtualTeacher.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration config;

        public EmailService(IConfiguration config)
        {
            this.config = config;
        }

        public void EnrollConfirmation(User user, Course course)
        {
            string title = $"Confirmation: Enrollment in '{course.Title}' Course";

            StringBuilder sb = new StringBuilder();
            sb.Append($"<p>Dear {user.FirstName},</p>");
            sb.Append($"<p>Congratulations! We are thrilled to inform you that your enrollment in the '{course.Title}' course has been successfully confirmed. Welcome to a journey of language mastery and skill enhancement!</p>");
            sb.Append("<p>Here are the key details of your enrollment:</p>");
            sb.Append("<ul>");
            sb.Append($"<li>Course Name: {course.Title}</li>");
            sb.Append("<li>Enrollment Status: Confirmed</li>");
            sb.Append($"<li>Start Date: {course.StartingDate}</li>");
            sb.Append($"<li>Access Link: localhost:5000/Course/Details/{course.Id}</li>");
            sb.Append("</ul>");
            sb.Append($"<p>As a participant in '{course.Title}' you now have exclusive access to:</p>");
            sb.Append("<ul>");
            sb.Append("<li>Comprehensive modules covering essential language skills.</li>");
            sb.Append("<li>Engaging multimedia content to make learning enjoyable.</li>");
            sb.Append("<li>Interactive exercises and assignments to reinforce your understanding.</li>");
            sb.Append("<li>Dedicated support from experienced instructors.</li>");
            sb.Append("</ul>");
            sb.Append("<p>Get ready to unlock your full potential and achieve language proficiency. Your commitment to this course is a commendable step towards personal and professional growth.</p>");
            sb.Append("<p>Should you have any queries or require assistance at any point during the course, please do not hesitate to reach out to our dedicated support team at <a href='mailto:mypolyglotcourse@gmail.com'>mypolyglotcourse@gmail.com</a></p>");
            sb.Append($"<p>We wish you a fulfilling and rewarding learning experience in '{course.Title}'. Thank you for choosing Polyglot Courses for your educational journey.</p>");
            sb.Append("<p>Best regards,<br>Polyglot Courses</p>");

            EmailDto request = Map(user.Email, title, sb);
            SendEmail(request);
        }

        

        public void RegistrationConfirmation(User user)
        {
            string title = "Registration Successful - Welcome to Polyglot Courses!";

            StringBuilder sb = new StringBuilder();
            sb.Append($"<p>Dear {user.FirstName},</p>");
            sb.Append("<p>Congratulations! We are delighted to inform you that your profile registration on Polyglot Courses has been successfully completed. Welcome to our community of learners and educators dedicated to fostering knowledge and skill development.</p>");
            sb.Append("<p>Here are the key details of your registration:</p>");
            sb.Append("<ul>");
            sb.Append($"<li>Name: {user.FirstName} {user.LastName}</li>");
            sb.Append($"<li>Username: {user.Username}</li>");
            sb.Append($"<li>Email Address: {user.Email}</li>");
            sb.Append("</ul>");
            sb.Append("<p>Your registration grants you access to a world of educational opportunities and resources. Whether you are here to explore courses, enhance your skills, or contribute as an educator, we are excited to have you on board.</p>");
            sb.Append("<p>Take a moment to personalize your profile and explore the features available to you. If you have any questions or need assistance, our support team is ready to help at <a href='mailto:mypolyglotcourse@gmail.com'>mypolyglotcourse@gmail.com</a>.</p>");
            sb.Append("<p>Thank you for choosing Polyglot Courses. We look forward to being a part of your learning journey and witnessing your achievements.</p>");
            sb.Append("<p>Best regards,</p>");
            sb.Append("<p>Polyglot Courses</p>");

            EmailDto request = Map(user.Email, title, sb);
            SendEmail(request);
        }

        public void TeacherAddition(User user, Course course)
        {
            string title = $"Welcome as an Active Teacher in '{course.Title}' Course!";

            StringBuilder sb = new StringBuilder();
            sb.Append($"<p>Dear {user.FirstName},</p>");
            sb.Append($"<p>We are excited to extend a warm welcome to you as an active teacher in the '{course.Title}' course! Your expertise and passion for teaching will undoubtedly enrich the learning experience for our students.</p>");
            sb.Append($"<p>Here are the key details for your role as a teacher in '{course.Title}':</p>");
            sb.Append("<ul>");
            sb.Append($"<li>Course Name: {course.Title}</li>");
            sb.Append("<li>Your Role: Active Teacher</li>");
            sb.Append($"<li>Access Link: localhost:5000/Course/Details/{course.Id}</li>");
            sb.Append("</ul>");
            sb.Append("<p>As an active teacher, you will play a crucial role in guiding and supporting our students on their journey to mastering the English language. Your commitment to excellence in education aligns perfectly with our mission to provide high-quality learning experiences.</p>");
            sb.Append("<p>Key Responsibilities:</p>");
            sb.Append("<ul>");
            sb.Append("<li>Facilitate engaging and interactive lessons.</li>");
            sb.Append("<li>Provide timely feedback to students.</li>");
            sb.Append("<li>Foster a positive and collaborative learning environment.</li>");
            sb.Append("</ul>");
            sb.Append("<p>Feel free to explore the course materials and familiarize yourself with the content. If you have any questions or need additional resources, our support team is here to assist you at <a href='mailto:mypolyglotcourse@gmail.com'>mypolyglotcourse@gmail.com</a>.</p>");
            sb.Append("<p>Thank you for joining us in shaping the educational landscape of 'English Basics.' We look forward to a successful collaboration and the positive impact you will make on our students.</p>");
            sb.Append("<p>Polyglot Courses</p>");

            EmailDto request = Map(user.Email, title, sb);
            SendEmail(request);
        }

        public void InviteFriend(string friendEmail, string friendName, User user, Course course)
        {
            string title = $"Invitation to Join '{course.Title}' Course - Polyglot";

            StringBuilder sb = new StringBuilder();
            sb.Append($"<p>Dear {friendName.Split()[0]},</p>");
            sb.Append($"<p>I hope this email finds you well. I am reaching out to you on behalf of Polyglot Courses with exciting news! You've been personally invited by {user.FirstName} {user.LastName} to embark on a transformative journey of learning through our meticulously crafted course - '{course.Title}'.</p>");
            sb.Append($"<p>This exclusive invitation is a testament to the belief that {user.FirstName} has in the quality and value of our course. '{course.Title}' is designed to enhance language skills, boost confidence, and provide a solid foundation.</p>");
            sb.Append("<p>To accept this invitation and join Steven on this educational adventure, simply follow the link below:</p>");
            sb.Append($"<p><a href='localhost:5000/Course/Details/{course.Id}'>localhost:5000/Course/Details/{course.Id}</a></p>");
            sb.Append($"<p>Here's a glimpse of what '{course.Title}' has to offer:</p>");
            sb.Append("<ul>");
            sb.Append("<li>Comprehensive modules covering essential language skills.</li>");
            sb.Append("<li>Engaging multimedia content to make learning enjoyable.</li>");
            sb.Append("<li>Interactive exercises and assignments to reinforce your understanding.</li>");
            sb.Append("<li>Dedicated support from experienced instructors.</li>");
            sb.Append("</ul>");
            sb.Append("<p>Don't miss this opportunity to elevate your proficiency! Click the link above to enroll now.</p>");
            sb.Append("<p>If you have any questions or need assistance during the registration process, feel free to reach out to our support team at <a href='mailto:mypolyglotcourse@gmail.com'>mypolyglotcourse@gmail.com</a>.</p>");
            sb.Append($"<p>We look forward to welcoming you to '{course.Title}' and witnessing your success in mastering a new language.</p>");
            sb.Append("<p>Best regards,<br>Polyglot Courses</p>");

            EmailDto request = Map(friendEmail, title, sb);
            SendEmail(request);
        }

        public void Contact(string email, string text)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"<p>Contact message from {email}</p>");
            sb.Append($"<p>{text}</p>");

            string title = $"Contact - {email}";

            EmailDto request = Map("mypolyglotcourse@gmail.com", title, sb);
            SendEmail(request);
        }

        public void SendEmail(EmailDto request)
        {
            var email = new MimeMessage();

            email.From.Add(MailboxAddress.Parse(config.GetSection("EmailUsername").Value));
            email.To.Add(MailboxAddress.Parse(request.To));

            email.Subject = request.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

            using (var client = new SmtpClient())
            {
                client.Connect(config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
                client.Authenticate(config.GetSection("EmailUsername").Value, config.GetSection("EmailPassword").Value);
                client.Send(email);
                client.Disconnect(true);
            }
        }


        public EmailDto Map(string recepient, string title, StringBuilder body)
        {
            return new EmailDto()
            {
                To = recepient,
                Subject = title,
                Body = body.ToString()
            };
        }


    }
}
