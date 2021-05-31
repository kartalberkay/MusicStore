using MainMusicStore.DataAccess.IMainRepository;
using MainMusicStore.Models.DbModels;
using Microsoft.AspNetCore.Mvc;

namespace MainMusicStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        #region Variables
        private readonly IUnitOfWork _uow; 
        #endregion

        #region CTOR
        public CategoryController(IUnitOfWork uow)
        {
            _uow = uow;
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
            var allObj = _uow.category.GetAll();
            return Json(new { data = allObj });
        }

        public IActionResult Delete(int id)
        {
            var deleteData = _uow.category.Get(id);
            if (deleteData == null)
            {
                return Json(new { success = false, message = "Data Not Found" });
            }
            _uow.category.Remove(deleteData);
            _uow.Save();
            return Json(new {success = true,message = "Delete Operation Successfully" });
        }
        #endregion

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            if (id != null)
            {
                return View(_uow.category.Get((int)id));
            }
            return View(new Category());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Category category)
        {
            if (ModelState.IsValid)
            {
                if (category.Id == 0)
                {
                    //Create
                    _uow.category.Add(category);
                }
                else
                {
                    //Update
                    _uow.category.Update(category);
                }
                _uow.Save();
                return RedirectToAction("Index");
            }
            return View(category);
        }
    }
}
