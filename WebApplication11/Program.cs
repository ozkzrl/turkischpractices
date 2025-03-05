var builder = WebApplication.CreateBuilder(args);

// Session ve diðer servisleri ekle
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session süresi
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession(); // Session'ý kullanýma aç
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ThisWortTwo}/{action=Index}/{id?}");

app.Run();

