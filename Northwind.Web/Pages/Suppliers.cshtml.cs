using Microsoft.AspNetCore.Mvc.RazorPages; // PageModel
using Northwind.Common; // NorthwindContext
using Microsoft.AspNetCore.Mvc; // [BindProperty], IActionResult

namespace Northwind.Web.Pages;
public class SuppliersModel : PageModel
{
    public IEnumerable<Supplier>? Suppliers { get; set; }
    private NorthwindContext _db;
    [BindProperty]
    public Supplier? Supplier { get; set; }

    public SuppliersModel(NorthwindContext db)
    {
        _db = db;
    }

    public IActionResult OnPost(){
        if((Supplier is not null) && ModelState.IsValid)
        {
            _db.Suppliers.Add(Supplier);
            _db.SaveChanges();
            return RedirectToPage("/Suppliers");
        }
        else
        {
            return Page();
        }
    }

    public void OnGet()
    {
        ViewData["Title"] = "Northwind B2B - Suppliers";
        Suppliers = _db.Suppliers.OrderBy(c => c.Country).ThenBy(c => c.CompanyName);
    }
}