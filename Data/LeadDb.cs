using api.Entities;

namespace api.Data
{
    public class LeadDb : DbContext
    {
        public LeadDb(DbContextOptions<LeadDb> options) : base(options) { }
        public DbSet<Lead> Leads { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<BlogPost> BlogPost { get; set; }
        public DbSet<ContactInfo> ContactInfos { get; set; }
        public DbSet<MainContent> MainContents { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Service> Services { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Lead>().HasData(
                new Lead
                {
                    Id = 1,
                    ClientName = "Test Client",
                    ClientQuestion = "Test Question",
                    Contact = "test@mail.ru",
                    Date = DateTime.Now,
                    Status = 0
                }
            );

            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser { Id = 1, Username = "admin", Password = "admin", Role = "admin" },
                new ApplicationUser { Id = 2, Username = "user", Password = "user", Role = "user" }
            );

            modelBuilder.Entity<ContactInfo>().HasData(
                new ContactInfo
                {
                    Id = 1,
                    Phone = "123-456-7890",
                    Email = "contact@test.ru",
                    Address = "123 Test st, Test City, TC 12345"
                }
            );

            modelBuilder.Entity<BlogPost>().HasData(
                new BlogPost
                {
                    Id = 1,
                    Title = "Test Blog Post",
                    Content = "This is a test blog post.",
                    ImageUrl = "https://ru.freepik.com/free-vector/hand-drawn-essay-illustration_40350252.htm#query=%D1%82%D0%B5%D1%81%D1%82&position=0&from_view=keyword&track=ais_hybrid&uuid=a0c7331a-7a90-477a-ae49-0e3691a60774",
                    PublishDate = DateTime.Now
                }
            );

            modelBuilder.Entity<MainContent>().HasData(
                new MainContent
                {
                    Id = 1,
                    HeaderContent = "Welcom to Our Website",
                    MenuButtonText = "Learn More"
                }
            );

            modelBuilder.Entity<Project>().HasData(
                new Project
                {
                    Id = 1,
                    Title = "Test project",
                    Description = "This is test project.",
                    ImageUrl = "https://images.app.goo.gl/bE8dX7sMGcDCvv3V6"
                }
            );

            modelBuilder.Entity<Service>().HasData(
                new Service
                {
                    Id = 1,
                    Title = "Test Service",
                    Description = "This is a test service."
                }
            );
        }

        public void InitializeDatabase()
        {
            if (!Users.Any(u => u.Username == "admin"))
            {
                Users.AddRange(
                    new ApplicationUser { Username = "admin", Password = "admin", Role = "admin" },
                    new ApplicationUser { Username = "user", Password = "user", Role = "user" }
                );
                SaveChanges();
            }
        }
    }
}