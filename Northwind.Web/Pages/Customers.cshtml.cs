using Microsoft.AspNetCore.Mvc.RazorPages; // PageModel
using Northwind.Common; // NorthwindContext

namespace Northwind.Web.Pages;
public class CustomersModel : PageModel
{
    public IEnumerable<Customer>? Customers { get; set; }
    private NorthwindContext _db;

    public CustomersModel(NorthwindContext db){
        _db = db;
    }

     public void OnGet()
    {
        ViewData["Title"] = "Northwind B2B - Customers";
        Customers = _db.Customers.OrderBy(c => c.Country);
    }
}