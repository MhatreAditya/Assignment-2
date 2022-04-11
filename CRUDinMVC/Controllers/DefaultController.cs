using CRUDinMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace CRUDinMVC.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default

        public ActionResult ProductManagent()
        {

            return View();
        }
        public ActionResult Index()
        {
             ProductCatelogManagementModel model = new ProductCatelogManagementModel();

             DBHandler db = new DBHandler();
             List<ProductCatelogModel> products = new List<ProductCatelogModel>();
            List<Category> categories = new List<Category>();

            categories = db.GetCategories();
            products = db.GetProducts();

            model.Products = products;
            model.Categories = categories;


            return View(model);
        }

        // GET: Default/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Default/Create
        public ActionResult Create()
        {
            ProductModel model = new ProductModel();

            DBHandler db = new DBHandler();
            model.Categories = db.GetCategories();
            ViewBag.ShowCategoryList = true;

            return View(model);
        }

        // POST: Default/Create
        [HttpPost]
        public ActionResult Create(ProductModel pmodel)
        {
            try
            {

                ViewBag.ShowCategoryList = false;
                DBHandler db = new DBHandler();

                db.CreateProduct(pmodel);

                return RedirectToAction("Index");

                /* Category cat = new Category();
                 cat.Id = 1;
                 cat.Name = "Biscuits";

                 // TODO: Add insert logic here
                 DBHandler db = new DBHandler();
                 if (db.AddCategory(cmodel))
                 {
                     ViewBag.Message = "Category Details Added Successfully";
                 }*/
                //return View("Index");
            }
            catch
            {
                return View("Index");
            }
        }

        // GET: Default/Edit/5
        public ActionResult Update(int id)
        {
            ProductModel pmodel = new ProductModel();

            List<Category> categories = new List<Category>();

            DBHandler db = new DBHandler();
            categories = db.GetCategories(); 
            pmodel.Categories = categories;
            ViewBag.ShowCategoryList = true;

            pmodel.Product = db.getProductById(id);

            return View(pmodel);
        }

        // POST: Default/Edit/5
        [HttpPost]
        public ActionResult Update(int id, ProductModel pmodel)
        {
            try
            {
                // TODO: Add update logic here
                ViewBag.ShowCategoryList = false;
                DBHandler db = new DBHandler();

                db.UpdateProduct(pmodel);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        //GET
        [HttpGet]
        [Route("CreateCat")]
        public ActionResult CreateCat()
        {
            return View("CreateUpdateCategory");
        }

        //POST
        [HttpPost]
        public ActionResult CreateUpdateCategory(Category cmodel)
        {

            DBHandler db = new DBHandler();
            if (string.IsNullOrEmpty(cmodel.Id.ToString()))
                db.AddCategory(cmodel);
            else
                db.UpdateCategory(cmodel);

            return RedirectToAction("Index");
        }

        //Get
        [HttpGet]
        [Route("UpdateCat")]
        public ActionResult UpdateCat(int id)
        {
            Category cmodel = new Category();

            DBHandler db = new DBHandler();
            cmodel = db.getCategoryById(id);

            return View("CreateUpdateCategory", cmodel);
        }
        //POST

        public ActionResult Updatecategory(int id, Category Cmodel)
        {

            return View();
        }

        // GET: Default/Delete/5
        public ActionResult Delete(int id)
        {
            DBHandler db = new DBHandler();
            db.DeleteProduct(id);

            return RedirectToAction("Index");
        }

        // POST: Default/Delete/5
        [HttpPost]
        public JsonResult DeleteProduct(int productId)
        {
            try
            {
                // TODO: Add delete logic here
                DBHandler db = new DBHandler();
                db.DeleteProduct(productId);

            }
            catch
            {
            }
            return Json(new { SaveResult = "success" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteCategory(int catId)
        {
            try
            {
                // TODO: Add delete logic here
                DBHandler db = new DBHandler();
                db.DeleteCategory(catId);

            }
            catch(Exception ex)
            {
                string message = ex.Message;
            }
            return Json(new { SaveResult = "success" }, JsonRequestBehavior.AllowGet);
        }
    }
}
