using Northwind.Common; // AddNorthwindContext extension method
using Microsoft.AspNetCore.Server.Kestrel.Core; // HttpProtocols


// Creates a host for the website using defaults for a web host that is then built.
var builder = WebApplication.CreateBuilder(args);

// Adds DbContext
builder.Services.AddNorthwindContext();

// Used to decompress the http request body
builder.Services.AddRequestDecompression();

// Adds Razor page functionality
builder.Services.AddRazorPages();

builder.WebHost.ConfigureKestrel((context, options) =>
{
  options.ListenAnyIP(5001, listenOptions =>
  {
    listenOptions.Protocols = HttpProtocols.Http1AndHttp2AndHttp3;
    listenOptions.UseHttps(); // HTTP/3 requires secure connections
  });
});

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

app.Use(async (HttpContext context, Func<Task> next) =>
{
    RouteEndpoint rep = context.GetEndpoint() as RouteEndpoint;

    if(rep is not null){
        WriteLine($"Endpoint name: {rep.DisplayName}");
        WriteLine($"Endpoint route pattern: {rep.RoutePattern.RawText}");
    }

    if (context.Request.Path == "/bonjour")
    {
        // in the case of a match on URL path, this becomes a terminating
        // delegate that returns so does not call the next delegate
        await context.Response.WriteAsync("Bonjour Monde!");
        return;
    }

    // we could modify the request before calling the next delegate
    await next();
    // we could modify the response after calling the next delegate

});

app.UseHttpsRedirection();

// Decompress the http request body
app.UseRequestDecompression();

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