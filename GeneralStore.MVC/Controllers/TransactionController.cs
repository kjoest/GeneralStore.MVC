using GeneralStore.MVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GeneralStore.MVC.Controllers
{
    public class TransactionController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        [Authorize]
        // GET: Transaction
        public ActionResult Index()
        {
            return View(_db.Transactions.ToList());
        }

        // GET: Transaction/{id}
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "No Transaction ID Present.");
            }
            var transaction = _db.Transactions.Find(id);
            if(transaction == null)
            {
                return HttpNotFound();
            }

            return View(transaction);
        }

        // GET: Transaction/Create
        // ViewData / ViewBags
        public ActionResult Create()
        {
            //ViewBag.CustomerItems = _db.Customers.Select(customer => new SelectListItem
            //{
            //    Text = customer.FirstName + " " + customer.LastName,
            //    Value = customer.CustomerId.ToString()
            //}).ToArray();
            //ViewBag.ProductItems = new SelectList(_db.Products, "Products", "Name");

            var viewModel = new CreateTransactionViewModel();
            viewModel.Customers = _db.Customers.Select(customer => new SelectListItem
            {
                Text = customer.FirstName + " " + customer.LastName,
                Value = customer.CustomerId.ToString()
            });
            viewModel.Products = _db.Products.Select(product => new SelectListItem
            {
                Text = product.Name,
                Value = product.ProductId.ToString()
            });

            return View(viewModel/*new Transaction()*/);
        }

        // POST: Transaction/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Transaction model)
        {
            //ViewBag.CustomerItems = _db.Customers.Select(customer => new SelectListItem
            //{
            //    Text = customer.FirstName + " " + customer.LastName,
            //    Value = customer.CustomerId.ToString()
            //});
            //ViewBag.ProductItems = new SelectList(_db.Products, "CustomerId", "FullName");

            if (ModelState.IsValid)
            {
                _db.Transactions.Add(model);
                if(_db.SaveChanges() == 1)
                {
                    return Redirect("/transaction");
                }
                ViewData["ErrorMessage"] = "Couldn't save your transaction. Please try again later";
            }
            ViewData["ErrorMessage"] = "Model state was invalid";

            var viewModel = new CreateTransactionViewModel();
            viewModel.Customers = _db.Customers.Select(customer => new SelectListItem
            {
                Text = customer.FirstName + " " + customer.LastName,
                Value = customer.CustomerId.ToString()
            });
            viewModel.Products = _db.Products.Select(product => new SelectListItem
            {
                Text = product.Name,
                Value = product.ProductId.ToString()
            });

            return View(model);
        }

        // GET: Transaction/Delete/{id}
        public ActionResult Delete(int id)
        {
            Transaction transaction = _db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }

            return View(transaction);
        }

        // POST: Transaction/Delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? id)
        {
            Transaction transaction = _db.Transactions.Find(id);
            _db.Transactions.Remove(transaction);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Transaction/Edit/{id}
        public ActionResult Edit(int? id)
        {
            Transaction transation = _db.Transactions.Find(id);
            if (transation == null)
            {
                return HttpNotFound();
            }

            ViewData["Customers"] = _db.Customers.Select(customer => new SelectListItem
            {
                Text = customer.FirstName + " " + customer.LastName,
                Value = customer.CustomerId.ToString()
            });
            ViewData["Products"] = _db.Products.Select(product => new SelectListItem
            {
                Text = product.Name,
                Value = product.ProductId.ToString()
            });

            return View(transation);
        }

        // POST: Transaction/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(transaction).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(transaction);
        }
    }
}