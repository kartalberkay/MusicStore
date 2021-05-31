using MainMusicStore.Data;
using MainMusicStore.DataAccess.IMainRepository;
using MainMusicStore.Models.DbModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MainMusicStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        #region Variables
        //private readonly IUnitOfWork _uow;
        private readonly ApplicationDbContext _db;
        #endregion

        #region CTOR
        public UserController(ApplicationDbContext db)
        {
            _db = db;
        } 
        #endregion

        #region Actions
        public IActionResult Index()
        {
            return View();
        }   
        #endregion

        #region IPA CALLS
        public IActionResult GetAll()
        {
            var userList = _db.ApplicationUsers.Include(c => c.Company).ToList();
            var userRole = _db.UserRoles.ToList();
            var roles = _db.Roles.ToList();

            foreach (var user in userList)
            {
                var roleId = userRole.FirstOrDefault(u => u.UserId == user.Id).RoleId;
                user.Role = roles.FirstOrDefault(u => u.Id == roleId).Name;

                if (user.Company == null)
                {
                    user.Company = new Models.DbModels.Company()
                    {
                        Name = string.Empty
                    };
                }
            }

            return Json(new { data = userList });
        }


        #endregion

       
    }
}
