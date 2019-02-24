using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnBoardingReactProj.Controllers
{
    public class StoreController : Controller
    {
        private MVPStudioEntities1 db = new MVPStudioEntities1();

        public ActionResult Index()
        {
            return View();
        }

        // GET Products
        public JsonResult GetStoreList()
        {
            try
            {
                var listStores = db.Stores.ToList();
                return Json(new { Success = true, data = listStores }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.Write(e.Data + "Exception Occured");
                return new JsonResult { Data = "Data Not Found", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        // CREATE Store
        public JsonResult CreateStore(Store store)
        {
            try
            {
                db.Stores.Add(store);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.Write(e.Data + "Exception Occured");
                return new JsonResult { Data = "Store Create Failed", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            return new JsonResult { Data = "Success", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        // DELETE Product
        public JsonResult DeleteStore(int id)
        {
            try
            {
                var store = db.Stores.Where(p => p.Id == id).SingleOrDefault();
                if (store != null)
                {
                    db.Stores.Remove(store);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Data + "Exception Occured");
                return new JsonResult { Data = "Deletion Failed", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            return new JsonResult { Data = "Success", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        // UPDATE Product
        public JsonResult GetUpdateStore(int id)
        {
            try
            {
                Store store = db.Stores.Where(x => x.Id == id).SingleOrDefault();
                return Json(store, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.Write(e.Data + "Exception Occured");
                return new JsonResult { Data = "Store Not Found", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        public JsonResult UpdateStore(Store store)
        {
            try
            {
                Store sto = db.Stores.Where(p => p.Id == store.Id).SingleOrDefault();
                sto.Name = store.Name;
                sto.Address = store.Address;
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.Write(e.Data + "Exception Occured");
                return new JsonResult { Data = "Store Update Failed", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            return new JsonResult { Data = "Success", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}