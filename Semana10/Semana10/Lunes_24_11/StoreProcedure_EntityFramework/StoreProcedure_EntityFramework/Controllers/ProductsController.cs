using StoreProcedure_EntityFramework.DAL;
using StoreProcedure_EntityFramework.Models;
using System;
using System.Web.Mvc;

namespace StoreProcedure_EntityFramework.Controllers
{
    public class ProductsController : Controller
    {
        Product_DAL product_DAL = new Product_DAL();

        // GET: Products
        public ActionResult Index()
        {
            var listProducts = product_DAL.GetAllProducts();
            if (listProducts.Count > 0)
            {
                return View(listProducts);
            }

            return View();
        }

        // GET: Products/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var product = product_DAL.GetProductById(id);
                if (product != null)
                {
                    return View(product);
                }
            }
            catch(Exception ex)
            {
                return View();
            }

            return RedirectToAction("Index");
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        public ActionResult Create(ProductModel product)
        {
            try
            {
                bool isValid = false;
                if (ModelState.IsValid)
                {
                    isValid = product_DAL.InsertProduct(product);
                    if (isValid)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View();
                    }

                }

                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int id)
        {
            var product = product_DAL.GetProductById(id);
            if(product != null)
            {
                return View(product);
            }
            return View();
        }

        // POST: Products/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ProductModel product)
        {
            try
            {
                bool isValid = false;
                if (ModelState.IsValid)
                {
                    isValid = product_DAL.UpdateProduct(product);
                    if (isValid)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View();
                    }

                }

                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int id)
        {
            var product = product_DAL.GetProductById(id);
            if (product != null)
            {
                return View(product);
            }
            return View();
        }

        // POST: Products/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, ProductModel product)
        {
            try
            {
                string result = "";
                result = product_DAL.DeleteProduct(id);
                if (result.Contains("eliminado"))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }
    }
}
