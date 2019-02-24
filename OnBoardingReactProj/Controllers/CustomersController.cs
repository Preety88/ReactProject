using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnBoardingReactProj.Controllers
{
    public class CustomersController : Controller
    {
        // GET: Customers
        private MVPStudioEntities1 db = new MVPStudioEntities1();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ShowAllCustomers()
        {
            var listCustomer = db.Customers.ToList();
            return Json(new { Success = true, data = listCustomer }, JsonRequestBehavior.AllowGet);
        }




        [HttpPost]
        public JsonResult CreateCustomers(Customer customer)
        {
            try
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                Console.Write("Success");
            }
            catch (Exception e)
            {
                Console.Write(e.Data + "Exception Occured");
                return new JsonResult { Data = "Customer Create Failed", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            return new JsonResult { Data = "Success", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }



        [HttpGet]
        public JsonResult GetUpdateCustomer(int Id)
        {
            try
            {
                Customer customer = db.Customers.Where(c => c.Id == Id).SingleOrDefault();
                return Json(new { data = customer, success = true, message = "Submitted Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.Write(e.Data + "Exception Occured");
                return Json(new { data = "Customer Not Found" }, JsonRequestBehavior.AllowGet);
            }
        }


        //public JsonResult GetUpdateCustomer(int Id)
        //{
        //    try
        //    {
        //        Customer customer = db.Customers.Where(x => x.Id ==Id).SingleOrDefault();
        //        string value = JsonConvert.SerializeObject(customer, Formatting.Indented, new JsonSerializerSettings
        //        {
        //            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        //        });
        //        return Json(value, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception e)
        //    {
        //        Console.Write(e.Data + "Exception Occured");
        //        return new JsonResult { Data = "Customer Not Found", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        //    }
        //}




        [HttpPost]
        public JsonResult UpdateCustomer(Customer customer)
        {
            try
            {
                //db.Entry(customer).State = EntityState.Modified;
                //or we can write -
                var updatedCust = db.Customers.Where(x => x.Id == customer.Id).Select(x => x).FirstOrDefault();
                updatedCust.Name = customer.Name;
                updatedCust.Address = customer.Address;
                db.SaveChanges();

                return Json(new { data = updatedCust, success = true, message = "Submitted Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }


        }

        [HttpPost]
        public JsonResult Delete(int Id)
        {
            try
            {
                Customer cust = db.Customers.Where(x => x.Id == Id).FirstOrDefault<Customer>();
                db.Customers.Remove(cust);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}