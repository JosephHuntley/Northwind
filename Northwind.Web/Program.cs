using Northwind.Common; // AddNorthwindContext extension method


// Creates a host for the website using defaults for a web host that is then built.
var builder = WebApplication.CreateBuilder(args);

// Adds DbContext
builder.Services.AddNorthwindContext();

// Adds Razor page functionality
builder.Services.AddRazorPages();

var app = builder.Build();

// In development environment any errors are displayed in the web browser. 
// Past ASP .Net 6 this code is executed automatically
    // if (app.Environment.IsDevelopment())
    // {
    //   app.UseDeveloperExceptionPage();
    // }

// If the environment is not development enables HSTS
if (!app.Environment.IsDevelopment())
{
  app.UseHsts();
}

app.UseHttpsRedirection();


app.UseDefaultFiles(); // index.html, default.html, and so on
app.UseStaticFiles();

app.MapRazorPages();

// Respond to /hello GET requests with plain text: Hello World!.
    // app.MapGet("/hello", () => "Hello World!");
// Respond to all HTTP GET requests with plain text: Hello World!.
    // app.MapGet("/", () => "Hello World!");


// The call to the Run method is a blocking call, so the hidden <Main>$ method does not return until the web server stops running.
app.Run();

// Lines below Run() are executed after the web server is shut down.

WriteLine("This executes after the web server has stopped!");