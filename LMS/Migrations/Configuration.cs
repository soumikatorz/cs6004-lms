namespace LMS.Migrations
{
    using FizzWare.NBuilder;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.SqlTypes;
    using System.Diagnostics;
    using System.Linq;
    using System.Web;

    internal sealed class Configuration : DbMigrationsConfiguration<LMS.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            if (!context.Roles.Any(r => r.Name == "Manager"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var mgr = new IdentityRole { Name = "Manager" };
                manager.Create(mgr);
                var assistant = new IdentityRole { Name = "Assistant" };
                manager.Create(assistant);
            }

            if (!context.Users.Any(u => u.UserName == "manager@example.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { FullName="Manager", UserName = "manager@example.com", Email= "manager@example.com" };

                manager.Create(user, "Password@123");
                manager.AddToRole(user.Id, "Manager");
            }

            var randomNumber = new RandomGenerator();

            var publishers = Builder<Publisher>.CreateListOfSize(10)
                .All()
                .With(p => p.ID = Guid.NewGuid())
                .With(p => p.Name = Faker.Company.Name())
                .With(p => p.Location = Faker.Address.UkPostCode())
                .Build();

            context.Publishers.AddOrUpdate(publishers.ToArray());

            var authors = Builder<Author>.CreateListOfSize(20)
                .All()
                .With(a => a.ID = Guid.NewGuid())
                .With(a => a.FirstName = Faker.Name.First())
                .With(a => a.LastName = Faker.Name.Last())
                .Build();

            context.Authors.AddOrUpdate(authors.ToArray());

            var press = Builder<Press>.CreateListOfSize(5)
                .All()
                .With(p => p.ID = Guid.NewGuid())
                .With(p => p.Name = Faker.Company.Name())
                .Build();

            context.Press.AddOrUpdate(press.ToArray());

            var categories = Builder<Category>.CreateListOfSize(20)
                .All()
                .With(c => c.ID = Guid.NewGuid())
                .With(c => c.Name = Faker.Lorem.Sentence())
                .Build();

            context.Categories.AddOrUpdate(categories.ToArray());

            var books = Builder<Book>.CreateListOfSize(50)
                .All()
                .With(b => b.ID = Guid.NewGuid())
                .With(b => b.ISBN = Faker.RandomNumber.Next().ToString())
                .With(b => b.Name = Faker.Lorem.Sentence())
                .With(b => b.Publisher = Pick<Publisher>.RandomItemFrom(publishers))
                .With(b => b.Press = Pick<Press>.RandomItemFrom(press))
                .With(b => b.PublishedDate = DateTime.Now.AddYears(-randomNumber.Next(1, 50)))
                .With(b => b.Charge = randomNumber.Next(50, 1000))
                .With(b => b.PenaltyCharge = randomNumber.Next(100, 1000))
                .With(b => b.Authors = Pick<Author>.UniqueRandomList(With.Between(1).And(5).Elements).From(authors))
                .With(b => b.Categories = Pick<Category>.UniqueRandomList(With.Between(1).And(3).Elements).From(categories))
                .Build();

            context.Books.AddOrUpdate(books.ToArray());

            foreach (var book in books)
            {
                var bookCopies = Builder<BookCopy>.CreateListOfSize(randomNumber.Next(3, 10))
                    .All()
                    .With(b => b.ID = Guid.NewGuid())
                    .With(b => b.CopyNumber = randomNumber.Next(1, 10))
                    .With(b => b.Book = book)
                    .With(b => b.Location = Faker.Address.UkPostCode())
                    .With(b => b.Available = true)
                    .Build();

                context.BookCopies.AddOrUpdate(bookCopies.ToArray());
            }

            var memberships = Builder<Membership>.CreateListOfSize(5)
                .All()
                .With(b => b.ID = Guid.NewGuid())
                .With(b => b.Name = Faker.Lorem.Sentence())
                .With(b => b.MaxLoans = randomNumber.Next(1, 10))
                .Build();

            context.Memberships.AddOrUpdate(memberships.ToArray());

            var members = Builder<Member>.CreateListOfSize(100)
                .All()
                .With(a => a.ID = Guid.NewGuid())
                .With(a => a.FirstName = Faker.Name.First())
                .With(a => a.LastName = Faker.Name.Last())
                .With(a => a.DateOfBirth = DateTime.Now.AddYears(-randomNumber.Next(18, 50)))
                .With(a => a.Membership = Pick<Membership>.RandomItemFrom(memberships))
                .With(a => a.Address = Faker.Address.StreetAddress())
                .With(a => a.PhoneNumber = Faker.Phone.Number())
                .Build();

            context.Members.AddOrUpdate(members.ToArray());

            var loanTypes = Builder<LoanType>.CreateListOfSize(5)
                .All()
                .With(a => a.ID = Guid.NewGuid())
                .With(a => a.Name = Faker.Lorem.Sentence())
                .With(a => a.Duration = randomNumber.Next(5, 20))
                .Build();

            context.LoanTypes.AddOrUpdate(loanTypes.ToArray());

            List<Loan> loans = new List<Loan>();
            
            for(int i = 1; i <= 30; i++)
            {
                var book = Pick<Book>.RandomItemFrom(books);
                var bookCopy = Pick<BookCopy>.RandomItemFrom(book.Copies.ToList());
                if (bookCopy.Available)
                {
                    var loanType = Pick<LoanType>.RandomItemFrom(loanTypes);
                    var dateIssued = DateTime.Now.AddDays(-randomNumber.Next(1, 90));
                    var dueDate = dateIssued.AddDays(loanType.Duration);
                    var dateReturned = randomNumber.Next(1, 100) > 50 ? dateIssued.AddDays(loanType.Duration + randomNumber.Next(-5, 10)) : DateTime.MinValue;
                    var penalty = dateReturned.Equals(DateTime.MinValue) ? 0 : (dateReturned < dueDate ? 0 : (dateReturned - dueDate).TotalDays * book.PenaltyCharge);

                    var loan = new Loan
                    {
                        ID = Guid.NewGuid(),
                        BookCopy = bookCopy,
                        Member = Pick<Member>.RandomItemFrom(members),
                        IssuedOn = dateIssued,
                        DueDate = dueDate,
                        LoanCharge = loanType.Duration * book.Charge,
                        PenaltyCharge = penalty,
                        LoanType = loanType,
                    };

                    if (!dateReturned.Equals(DateTime.MinValue))
                    {
                        loan.ReturnedOn = dateReturned;
                    }

                    bookCopy.Available = false;
                    loans.Add(loan);
                }
            }

            context.Loans.AddOrUpdate(loans.ToArray());
            context.SaveChanges();
        }
    }
}
