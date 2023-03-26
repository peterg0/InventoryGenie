﻿using InventoryGenie.Data;
using InventoryGenie.Models;
using Microsoft.AspNetCore.Mvc;

namespace InventoryGenie.Controllers
{
    public class ReportingController : Controller
    {

        public ReportingController(ApplicationDbContext ctx)
        {
            Employee.Context = ctx;
        }


        [HttpGet]
        public IActionResult Index()
        {
            if (!IsAuthenticatedAndAuthorized())
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        public IActionResult SalesReport(string from,string to)
        {
            List<SaleRecord> salesReport = Employee.LoggedInEmployee.GenerateSalesReport(from, to);
            return View(salesReport);
        }

        [HttpPost]
        public IActionResult OrdersReport(string from, string to)
        {
            List<OrderRecord> OrdersReport = Employee.LoggedInEmployee.GenerateOrdersReport(from, to);
            return View(OrdersReport);
        }

        [HttpGet]
        public IActionResult QuantityReport()
        {
            if (!IsAuthenticatedAndAuthorized())
                return RedirectToAction("Index", "Home");


            List<Product> products =
                Employee.LoggedInEmployee.OrderManagementSearchProducts(null, null, true);
            List<int> QuantityNotReceivedCount = new List<int>();
            foreach (Product product in products)
            {
                QuantityNotReceivedCount.Add(Employee.LoggedInEmployee.GetQuantityNotReceivedForProduct(product.Name));
            }
            ViewBag.QuantityNotReceivedCount = QuantityNotReceivedCount;
            return View(products);
        }

        // if the LoggedInEmployee is not null and role is GM (1) or WL(2) then return true.
        // if it's not true then user will be redirected to Home.
        // Home will redirect to login if there is no logged in Employee. 
        private static bool IsAuthenticatedAndAuthorized()
        {
            return Employee.LoggedInEmployee != null && (Employee.LoggedInEmployee.RoleId == 1 || Employee.LoggedInEmployee.RoleId == 2);
        }
    }
}
