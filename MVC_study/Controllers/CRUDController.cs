using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_study.Models;

namespace MVC_study.Controllers
{
    public class CRUDController : Controller
    {
        // GET: CRUD
        public ActionResult Index()
        {
            var db = new FabricsEntities();
            //var data = db.Product.Where(p => p.ProductName.StartsWith("C")&p.Price>=5&p.Price<=10);
            //var data = db.Product;
            //最原始的做出sql
            var data = db.Database.SqlQuery<Product>("select * from Product").AsQueryable();
            
            return View(data);
        }

        // GET: CRUD/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CRUD/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: CRUD/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                //Product c = new Product();
                //產生一個Product物件
                Product P = new Product();
                //將上面的collection帶入就是傳進來的 collection["這是網頁上的name"]
                //型別的部分滑鼠移過去就可以看到要轉什麼型別
                P.ProductName = Convert.ToString(collection["ProductName"]);
                P.Price = Convert.ToDecimal(collection["Price"]);
                P.Active = true;
                P.Stock = Convert.ToDecimal(collection["Stock"]);

                var db = new FabricsEntities();
                db.Product.Add(P);
                db.SaveChanges();
                
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult BatchUpdate()
        {
            var db = new FabricsEntities();
            var data = db.Product.Where(p => p.ProductName.StartsWith("C"));

            foreach (var item in data)
            {
                item.Price = item.Price * 2;
            }
            db.SaveChanges();
            //return View();
            return RedirectToAction("Index");
        }

        // GET: CRUD/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CRUD/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CRUD/Delete/5
        public ActionResult Delete(int id)
        {
            
            var db = new FabricsEntities();
            //找到要刪除的id
            var client = db.Client.Find(id);
            //foreach先找到這資料表最後一個關聯性先找最後一筆做刪除
            foreach (var order in client.Order.ToList())
            {

                //找到order的關聯orderLine做刪除
                db.OrderLine.RemoveRange(order.OrderLine);

            }
            //再將order中與這個client有關連到的做移除
            db.Order.RemoveRange(client.Order.ToList());
            //將選擇的id做移除
            db.Client.Remove(client);
            //做交易處理正式嘗試移除
            db.SaveChanges();



            //return View();
            return RedirectToAction("Index");
        }

        // POST: CRUD/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
