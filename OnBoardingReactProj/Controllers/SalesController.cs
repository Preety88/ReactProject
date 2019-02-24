using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnBoardingReactProj.Controllers
{
    public class SalesController : Controller
    {
        private MVPStudioEntities1 db = new MVPStudioEntities1();

        public ActionResult Index()
        {
            return View();
        }

        // GET Products
        public JsonResult GetSalesList()
        {
            try
            {
                var salesList = db.ProductSolds.ToList();
                var asdf = Json(salesList, JsonRequestBehavior.AllowGet);
                return asdf;
            }
            catch (Exception e)
            {
                Console.Write(e.Data + "Exception Occured");
                return new JsonResult { Data = "Data Not Found", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        // CREATE Sale
        public JsonResult CreateSale(ProductSold sale)
        {
            try
            {
                db.ProductSolds.Add(sale);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.Write(e.Data + "Exception Occured");
                return new JsonResult { Data = "Sale Create Failed", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            return new JsonResult { Data = "Success", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult GetCustomers()
        {
            try
            {
                var Customerdata = db.Customers.Select(p => new { p.Id, CustomerName = p.Name }).ToList();

                return new JsonResult { Data = Customerdata, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception e)
            {
                Console.Write(e.Data + "Exception Occured");
                return new JsonResult { Data = "Data Not Found", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        public JsonResult GetProducts()
        {
            try
            {
                var ProductsData = db.Products.Select(p => new { p.Id, ProductName = p.Name }).ToList();

                return new JsonResult { Data = ProductsData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception e)
            {
                Console.Write(e.Data + "Exception Occured");
                return new JsonResult { Data = "Data Not Found", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        public JsonResult GetStores()
        {
            try
            {
                var StoresData = db.Stores.Select(p => new { p.Id, StoreName = p.Name }).ToList();

                return new JsonResult { Data = StoresData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception e)
            {
                Console.Write(e.Data + "Exception Occured");
                return new JsonResult { Data = "Data Not Found", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        // DELETE Sale
        public JsonResult DeleteSale(int id)
        {
            try
            {
                var sale = db.ProductSolds.Where(s => s.Id == id).SingleOrDefault();
                if (sale != null)
                {
                    db.ProductSolds.Remove(sale);
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

        public JsonResult GetUpdateSale(int id)
        {
            try
            {
                ProductSold sale = db.ProductSolds.Where(s => s.Id == id).SingleOrDefault();
                //string value = JsonConvert.SerializeObject(sale, Formatting.Indented, new JsonSerializerSettings
                //{
                //    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                //});
                return Json(sale, JsonRequestBehavior.AllowGet);
                //return new JsonResult { Data = sale, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception e)
            {
                Console.Write(e.Data + "Exception Occured");
                return new JsonResult { Data = "Sale Not Found", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        public JsonResult UpdateSale(ProductSold sale)
        {
            try
            {
                ProductSold sa = db.ProductSolds.Where(s => s.Id == sale.Id).SingleOrDefault();
                sa.CustomerId = sale.CustomerId;
                sa.ProductId = sale.ProductId;
                sa.StoreId = sale.StoreId;
                sa.DateSold = sale.DateSold;

                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.Write(e.Data + "Exception Occured");
                return new JsonResult { Data = "Sale Update Failed", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            return new JsonResult { Data = "Success", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}