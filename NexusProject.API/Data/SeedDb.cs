using NexusProject.API.Helpers;
using NexusProject.Shared.Entities;
using NexusProject.Shared.Enums;
using System.Linq;
using System.Threading.Tasks;

namespace NexusProject.API.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;

        }
        //Method for seed the database
        public async Task SeedAsync()
        {

            await _context.Database.EnsureCreatedAsync();
            await CheckRoleAsync();
            await CheckUserAsync("98764597", "Edwin", "Ramirez", "EDWINRAMIREZGON@GMAIL.COM", UserType.Admin, "https://nexusprojectitm.s3.us-east-2.amazonaws.com/edwin.png");
            await CheckUserAsync("101715698", "Juan David", "Velasquez", "JUANDAV12@GMAIL.COM", UserType.Admin, "https://nexusprojectitm.s3.us-east-2.amazonaws.com/juan.png");
            await CheckUserAsync("123456", "Tutor", "Number1", "Tutor1@Nexus.com", UserType.Tutor,"");
            await CheckUserAsync("78910", "Young", "Number1", "Young1@Nexus.com", UserType.Young,"");
            await CheckYoungAsync();
            await CheckTutorAsync();
            await CheckAdminAsync();

        }




        private async Task CheckRoleAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.Tutor.ToString());
            await _userHelper.CheckRoleAsync(UserType.Young.ToString());

        }




        private async Task<User> CheckUserAsync(string document, string firstname, string lastname, string email, UserType userType, string photo)
        {
            var user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                user = new User
                {

                    Document = document,
                    FirstName = firstname,
                    LastName = lastname,
                    Email = email,
                    UserName = email,
                    UserType = userType,
                    Photo = photo,
                };

                await _userHelper.AddUserAsync(user, "Ca121203");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());
                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);
            }
            return user;
        }

        private async Task CheckYoungAsync()
        {
            if (!_context.Youngs.Any())
            {
                _context.Youngs.Add(new Young { Interests = "much", UserDocument = "78910" });
            }
            await _context.SaveChangesAsync();
        }

        private async Task CheckTutorAsync()
        {
            if (!_context.Tutors.Any())
            {
                _context.Tutors.Add(new Tutor { Speciality = "MAth", Profession = "Docent", Availability = "Much", UserDocument = "123456" });
            }
            await _context.SaveChangesAsync();
        }

        private async Task CheckAdminAsync()
        {
            if (!_context.Admins.Any())
            {
                _context.Admins.Add(new Admin { Rol = "Backend", Area = "Inf", UserDocument = "98764597" });
                _context.Admins.Add(new Admin { Rol = "Frontend", Area = "Inf", UserDocument = "101715698" });

            }
            await _context.SaveChangesAsync();
        }

    }
}