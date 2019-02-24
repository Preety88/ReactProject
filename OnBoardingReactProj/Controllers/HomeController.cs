using System.Web.Mvc;

namespace OnBoardingReactProj.Controllers
{
    public class HomeController : Controller
    {


        public ActionResult Index()
        {
            //AppDbContext g = new AppDbContext();
            //g.Database.CreateIfNotExists();
            return View();
        }

       

        //public JsonResult ShowAllProducts()
        //{
        //    var listProduct = db.Products.ToList();
        //    return Json(new { Success = true, data = listProduct }, JsonRequestBehavior.AllowGet);
        //}

    }

    
}