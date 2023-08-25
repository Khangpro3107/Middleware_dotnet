using Middleware.Middlewares;
using Middleware.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSingleton<ConsoleService>();
builder.Services.AddSingleton<MyFirstMiddleware>();
builder.Services.AddSingleton<MySecondMiddleware>();
// a middleware can be registered with ctor params, it is basically a dep
builder.Services.AddTransient<ParameterizedMiddleware>(sp => new ParameterizedMiddleware("hello"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

// correct app.Use
app.Use(async (context, next) =>
{
    // do something
    await Console.Out.WriteLineAsync("app.Use");
    await next(context);
    await Console.Out.WriteLineAsync("app.Use after");
});

// if this app.Run is uncommented, this middleware will terminate the pipeline
//app.Run(async context =>
//{
//    await context.Response.WriteAsync("app.Run");
//});

// this is the same as the middleware in line 38, is used to prove the order of execution of middlewares (first to second middleware and back)
app.Use(async (context, next) =>
{
    // do something
    await Console.Out.WriteLineAsync("app.Use 2");
    await next(context);
    await Console.Out.WriteLineAsync("app.Use 2 after");
});

// incorrect app.Use as app.Use MUST call next, use app.Run instead
//app.Use(async (context, next) =>
//{
//    await context.Response.WriteAsync("haha");
//});


// simple app.Map
//app.Map("/middleware", app =>
//{
//    app.Run(async context => await context.Response.WriteAsync("app.Map"));
//});

// using a controller
//app.MapControllerRoute("first", "controller={MyFirst}/action={Get}");

app.Use(async (context, next) =>
{
    // "trying" to read the body of a req
    //using (StreamReader sr = new StreamReader(context.Request.Body))
    //{
    //    string body = await sr.ReadToEndAsync();
    //    object myObj =                                    // incomplete as this is not the preferred way of reading the body of a req
    //    await Console.Out.WriteLineAsync(body);
    //}
    await next();
});

// app.Map can be nested
app.Map("/middleware", app =>
{
    // the below line shows how to only run a middleware when navigating to the correct route
    app.UseMiddleware<MyFirstMiddleware>();
    app.Map("/nested-1", app =>
    {
        app.Run(async context => await context.Response.WriteAsync("app.Map nested 1"));
    });
    app.Map("/nested-2", app =>
    {
        app.Run(async context => await context.Response.WriteAsync("app.Map nested 2"));
    });
});

// this middleware will not run if it is positioned after app.Map
app.Use(async (context, next) =>
{
    await Console.Out.WriteLineAsync("After app.Map");
    await next();
});

// this middleware is related to reading the body of a req, not used in presentation
//app.UseMiddleware<MyFirstMiddleware>();

// using many middlewares
app.UseMiddleware<MySecondMiddleware>();

app.UseMiddleware<ConventionalMiddleware>();

app.UseMiddleware<ParameterizedMiddleware>();

app.Run();
