
using Blazored.LocalStorage;
using buoi18.Hubs;
using buoi18.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// builder ra cacs tham so dong lenh, doc tham so tu appseting,..
var builder = WebApplication.CreateBuilder(args);

// đăng ký razor để dùng trang chủ của _host
//  và blazorserver kích hoạt signalR , 
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSignalR();

// HTTPCLIENT
builder.Services.AddHttpClient();

//  DI SERVICES
builder.Services.AddScoped<IStudentApiService, StudentApiService>();

// phát hành token
builder.Services.AddScoped<JwtAuthService>();

//DI  bật phần phân quyền trong component Blazor
builder.Services.AddAuthenticationCore();
// DI localstore blazored - thư viện đọc localstorage
builder.Services.AddBlazoredLocalStorage();
// Đăng lý JwtStateProvider
builder.Services.AddScoped<JwtStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(ser => ser.GetRequiredService<JwtStateProvider>());


// Đăng kí các dịch vụ 
var app = builder.Build();

// kiểm tra xem co phai đang chạy local hay khong
if (app.Environment.IsDevelopment()) 
{
    app.UseDeveloperExceptionPage();
}
else // mooi truong deploy : 
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// http:localhost:5000 -> https:5001
// app.UseHttpsRedirection();
app.UseStaticFiles();
//bật định tuyến trong App
app.UseRouting();

//signalR
app.MapBlazorHub();
// kết nối với Hub 
// localhost:PORT/votehub
app.MapHub<VoteHub>("/votehub");
app.MapHub<ChatHub>("/chathub");

// nếu không khớp với page nào thì chuyển về _host để xử lý
app.MapFallbackToPage("/_Host");

// fetch . /check 

///
/// khởi chạy
app.Run();

