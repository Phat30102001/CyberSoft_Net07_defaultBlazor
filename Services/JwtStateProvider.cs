using System.Security.Claims;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;

namespace buoi18.Services;

/// Provider đọc token, kiểm tra token rồi tạo ClaimsPrincipal cho ứng dụng.

public class JwtStateProvider : AuthenticationStateProvider
{
    // giảm sự phụ thuộc 
    //
    private readonly ILocalStorageService _localStorageService;
    private readonly IConfiguration _configuration;
    private readonly JwtAuthService _jwt;
    // Principal không có identity xác thực đại diện cho khách chưa đăng nhập.
    private static readonly ClaimsPrincipal Anonymous =
        new(new ClaimsIdentity());

    public JwtStateProvider(ILocalStorageService localStorageService, IConfiguration configuration, JwtAuthService jwt)
    {

        _configuration = configuration;
        _localStorageService = localStorageService;
        _jwt = jwt;
    }
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        // lấy token từ local có key là authToken
        var token = await _localStorageService.GetItemAsync<string>("authToken");
        if (string.IsNullOrEmpty(token))
        {
            return new AuthenticationState(Anonymous); // ẩn danh 
        }
        try
        {
            // 
            var principal = _jwt.CheckTokenState(token);
            return new AuthenticationState(principal); // Claim.Name, Role, LopHoc
        }
        catch
        {
            // kiểm tra token fail -> hết hạn , token tầm bậy
            // xoá token ra khỏi local
            await _localStorageService.RemoveItemAsync("authToken");
            return new AuthenticationState(Anonymous);
        }
    }
    // hàm lưu token ()
    public async Task SaveToken(string token)
    {
        await _localStorageService.SetItemAsync("authToken", token);
        var principal = _jwt.CheckTokenState(token);
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
    }
    // xoá token (đăng xuất)
    public async Task RemoveToken()
    {
        await _localStorageService.RemoveItemAsync("authToken");
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(Anonymous)));
    }

}