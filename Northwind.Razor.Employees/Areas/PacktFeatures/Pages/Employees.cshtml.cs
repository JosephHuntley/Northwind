using Microsoft.AspNetCore.Mvc.RazorPages; // PageModel
using Northwind.Common; // Employee, NorthwindContext
namespace PacktFeatures.Pages;
public class EmployeesPageModel : PageModel
{
  private NorthwindContext _db;
  public EmployeesPageModel(NorthwindContext db)
  {
    _db = db;
  }
  public Employee[] Employees { get; set; } = null!;
  public void OnGet()
  {
    ViewData["Title"] = "Northwind B2B - Employees";
    Employees = _db.Employees.OrderBy(e => e.LastName)
        .ThenBy(e => e.FirstName).ToArray();
  }
}
