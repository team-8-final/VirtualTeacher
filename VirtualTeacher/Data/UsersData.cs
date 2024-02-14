using VirtualTeacher.Models;
using VirtualTeacher.Models.Enums;

namespace VirtualTeacher.Data;

public static class UsersData
{
    public static List<User> Seed()
    {
        return new List<User>
        {
                new() { Id = 1, Username = "admin", FirstName = "Admin", LastName = "Admin", Password = "qCs2FMBZ9j2q/7MYMah70BgC92dIHOMkTHsdoSP/G6ULPpc7yeyqwoBB5cm8f4QFy089RiV0q1ebCz8QsGa83w==", Email = "admin@example.com", UserRole = UserRole.Admin },
                new() { Id = 2, Username = "johndoe", FirstName = "John", LastName = "Doe", Password = "qCs2FMBZ9j2q/7MYMah70BgC92dIHOMkTHsdoSP/G6ULPpc7yeyqwoBB5cm8f4QFy089RiV0q1ebCz8QsGa83w==", Email = "johndoe@example.com", UserRole = UserRole.Teacher },
                new() { Id = 3, Username = "steviej", FirstName = "Stevie", LastName = "Johnson", Password = "qCs2FMBZ9j2q/7MYMah70BgC92dIHOMkTHsdoSP/G6ULPpc7yeyqwoBB5cm8f4QFy089RiV0q1ebCz8QsGa83w==", Email = "stevie@example.com", UserRole = UserRole.Teacher },
                new() { Id = 4, Username = "peter", FirstName = "Peter", LastName = "Sanders", Password = "qCs2FMBZ9j2q/7MYMah70BgC92dIHOMkTHsdoSP/G6ULPpc7yeyqwoBB5cm8f4QFy089RiV0q1ebCz8QsGa83w==", Email = "peter@example.com", UserRole = UserRole.Teacher },
                new() { Id = 5, Username = "michaelj", FirstName = "Michael", LastName = "Jordan", Password = "qCs2FMBZ9j2q/7MYMah70BgC92dIHOMkTHsdoSP/G6ULPpc7yeyqwoBB5cm8f4QFy089RiV0q1ebCz8QsGa83w==", Email = "mj23@example.com", UserRole = UserRole.Teacher },
                new() { Id = 6, Username = "stephc", FirstName = "Stephen", LastName = "Curry", Password = "qCs2FMBZ9j2q/7MYMah70BgC92dIHOMkTHsdoSP/G6ULPpc7yeyqwoBB5cm8f4QFy089RiV0q1ebCz8QsGa83w==", Email = "stephc@example.com", UserRole = UserRole.Teacher },
                new() { Id = 7, Username = "brownA", FirstName = "Alex", LastName = "Brown", Password = "qCs2FMBZ9j2q/7MYMah70BgC92dIHOMkTHsdoSP/G6ULPpc7yeyqwoBB5cm8f4QFy089RiV0q1ebCz8QsGa83w==", Email = "alexb@example.com", UserRole = UserRole.Teacher },
                new() { Id = 8, Username = "chris-white", FirstName = "Chris", LastName = "White", Password = "qCs2FMBZ9j2q/7MYMah70BgC92dIHOMkTHsdoSP/G6ULPpc7yeyqwoBB5cm8f4QFy089RiV0q1ebCz8QsGa83w==", Email = "chrisw@example.com", UserRole = UserRole.Teacher },
                new() { Id = 9, Username = "dannyy", FirstName = "Danny", LastName = "Green", Password = "qCs2FMBZ9j2q/7MYMah70BgC92dIHOMkTHsdoSP/G6ULPpc7yeyqwoBB5cm8f4QFy089RiV0q1ebCz8QsGa83w==", Email = "dannyg@example.com", UserRole = UserRole.Teacher },
                new() { Id = 10, Username = "emy223", FirstName = "Emily", LastName = "Johnson", Password = "qCs2FMBZ9j2q/7MYMah70BgC92dIHOMkTHsdoSP/G6ULPpc7yeyqwoBB5cm8f4QFy089RiV0q1ebCz8QsGa83w==", Email = "emilyj@example.com", UserRole = UserRole.Teacher },
                new() { Id = 11, Username = "fionas", FirstName = "Fiona", LastName = "Smith", Password = "qCs2FMBZ9j2q/7MYMah70BgC92dIHOMkTHsdoSP/G6ULPpc7yeyqwoBB5cm8f4QFy089RiV0q1ebCz8QsGa83w==", Email = "fionas@example.com", UserRole = UserRole.Student },
                new() { Id = 12, Username = "georgek", FirstName = "George", LastName = "Klein", Password = "qCs2FMBZ9j2q/7MYMah70BgC92dIHOMkTHsdoSP/G6ULPpc7yeyqwoBB5cm8f4QFy089RiV0q1ebCz8QsGa83w==", Email = "georgek@example.com", UserRole = UserRole.Student },
                new() { Id = 13, Username = "hannamorris", FirstName = "Hannah", LastName = "Morris", Password = "qCs2FMBZ9j2q/7MYMah70BgC92dIHOMkTHsdoSP/G6ULPpc7yeyqwoBB5cm8f4QFy089RiV0q1ebCz8QsGa83w==", Email = "hannahm@example.com", UserRole = UserRole.Student },
                new() { Id = 14, Username = "ian_c", FirstName = "Ian", LastName = "Clark", Password = "qCs2FMBZ9j2q/7MYMah70BgC92dIHOMkTHsdoSP/G6ULPpc7yeyqwoBB5cm8f4QFy089RiV0q1ebCz8QsGa83w==", Email = "ianc@example.com", UserRole = UserRole.Student },
                new() { Id = 15, Username = "julia_lopez", FirstName = "Julia", LastName = "Lopez", Password = "qCs2FMBZ9j2q/7MYMah70BgC92dIHOMkTHsdoSP/G6ULPpc7yeyqwoBB5cm8f4QFy089RiV0q1ebCz8QsGa83w==", Email = "julial@example.com", UserRole = UserRole.Student },
        };
    }
}