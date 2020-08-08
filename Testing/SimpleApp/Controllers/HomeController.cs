using Microsoft.AspNetCore.Mvc;
using SimpleApp.Models;

namespace SimpleApp.Controllers
{
    public class HomeController : Controller
    {
        // Modify the controller to use the ProductDataSource class as the source for its data
        public IDataSource dataSource = new ProductDataSource();
        public ViewResult Index()
        {
            //return View(Product.GetProducts());
            return View(dataSource.Products);
        }
    }
}