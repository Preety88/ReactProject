using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnBoardingReactProj.Controllers
{
    public class ProductsController : Controller
    {
        private MVPStudioEntities1 db = new MVPStudioEntities1();

        public ActionResult Index()
        {
            return View();
        }

        // GET Products
        public JsonResult GetProductList()
        {
            try
            {
                var productList = db.Products.ToList();

                return Json(productList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.Write(e.Data + "Exception Occured");
                return new JsonResult { Data = "Data Not Found", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        // DELETE Product
        public JsonResult DeleteProduct(int id)
        {
            try
            {
                var product = db.Products.Where(p => p.Id == id).SingleOrDefault();
                if (product != null)
                {
                    db.Products.Remove(product);
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

        // CREATE Product
        public JsonResult CreateProduct(Product product)
        {
            try
            {
                db.Products.Add(product);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.Write(e.Data + "Exception Occured");
                return new JsonResult { Data = "Product Create Failed", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            return new JsonResult { Data = "Success", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        // UPDATE Product
        public JsonResult GetUpdateProduct(int id)
        {
            try
            {
                //Product product = db.Products.Where(p => p.Id == id).SingleOrDefault();
                //return new JsonResult { Data = product, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

                Product product = db.Products.Where(x => x.Id == id).SingleOrDefault();
                return Json(product, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.Write(e.Data + "Exception Occured");
                return new JsonResult { Data = "Product Not Found", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        public JsonResult UpdateProduct(Product product)
        {
            try
            {
                Product prod = db.Products.Where(p => p.Id == product.Id).SingleOrDefault();
                prod.Name = product.Name;
                prod.Price = product.Price;
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.Write(e.Data + "Exception Occured");
                return new JsonResult { Data = "Product Update Failed", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            return new JsonResult { Data = "Success", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}
