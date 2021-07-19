using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestApplication.Models;

namespace TestApplication.Controllers
{
    public class HomeController : Controller
    {
       
      //  productTable product = new productTable();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        //Register
        public ActionResult Index(userTable u)
        {
            inventoryDBEntities db = new inventoryDBEntities();
            userTable user = new userTable();
            user.firstname = u.firstname;
            user.lastname = u.lastname;
            user.email = u.email;
            user.password = u.password;
            db.userTables.Add(user);
            db.SaveChanges();
            return View();
        }

        // product list
        public ActionResult About()
        {
            inventoryDBEntities db = new inventoryDBEntities();
            List<productTable> product = db.productTables.ToList();


            return View(product);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //Login
        [HttpPost]
        public ActionResult Contact(userTable u)
        {
            inventoryDBEntities db = new inventoryDBEntities();
            
            var detailes = db.userTables.Where(m => m.email == u.email && m.password == u.password).FirstOrDefault();
            if (detailes != null)
            {
                return RedirectToAction("About", "Home");
            }

            return View();
        }

        public ActionResult addProduct()
        {
            return View();
        }

        //add Product
        [HttpPost]
        public ActionResult addProduct(productTable p)
        {
            inventoryDBEntities db = new inventoryDBEntities();
            productTable product = new productTable();
            product.productname = p.productname;
            product.customername = p.customername;
            product.sellingunit = p.sellingunit;
            product.unitcost = p.unitcost;
            product.totalcost = p.totalcost;
            db.productTables.Add(product);
            db.SaveChanges();

            return RedirectToAction("About","Home");
        }


        //edit product list
        public ActionResult editProduct(int id)
        {
            inventoryDBEntities db = new inventoryDBEntities();
            productTable product = db.productTables.SingleOrDefault(m => m.productid == id);

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //edit data
        public ActionResult editData(productTable p)
        {
            inventoryDBEntities db = new inventoryDBEntities();
            productTable product = db.productTables.SingleOrDefault(m => m.productid == p.productid);

            product.productname = p.productname;
            product.customername = p.customername;
            product.sellingunit = p.sellingunit;
            product.unitcost = p.unitcost;
            product.totalcost = p.totalcost;
            db.SaveChanges();

            return RedirectToAction("About","Home");
        }


        //Delete product list
        public ActionResult deleteProduct(int id)
        {
            inventoryDBEntities db = new inventoryDBEntities();
            productTable product = db.productTables.SingleOrDefault(m => m.productid == id);

            return View(product);
        }

        //delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult deleteData(int id)
        {
            inventoryDBEntities db = new inventoryDBEntities();
            productTable product = db.productTables.SingleOrDefault(m => m.productid ==id);

            if (product == null)
            {
                return RedirectToAction("deleteProduct", "Home");
            }
            else
            {
                db.productTables.Remove(product);
                db.SaveChanges();
            }
            return RedirectToAction("About", "Home");
        }

    }
}