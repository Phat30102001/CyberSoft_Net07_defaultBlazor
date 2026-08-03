using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using buoi18.Models;

namespace buoi18.Services;

public interface IStudentApiService
{
    Task<List<StudentDto>> GetAllAsync();
    Task<HttpResponseMessage> AddAsync(StudentDto student);
    Task<HttpResponseMessage> UpdateAsync(StudentDto student);
    Task<HttpResponseMessage> DeleteAsync(int id);
}

public class StudentApiService : IStudentApiService
{
    private readonly string _baseUrl = "https://svcy.myclass.vn/api/SinhVien";
    private readonly HttpClient _http;
    public StudentApiService(HttpClient http)
    {
        _http = http;
    }

    public async Task<HttpResponseMessage> AddAsync(StudentDto student)
    {
        try
        {
            var resspon  = await _http.PostAsJsonAsync($"{_baseUrl}/ThemSinhVien", student);
            return resspon;
        }catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public Task<HttpResponseMessage> DeleteAsync(int id)
    {
        throw new System.NotImplementedException();
    }

    public Task<List<StudentDto>> GetAllAsync()
    {
        throw new System.NotImplementedException();
    }

    public Task<HttpResponseMessage> UpdateAsync(StudentDto student)
    {
        throw new System.NotImplementedException();
    }
}