using RealEstate_Dapper_Api.Dtos;
using RealEstate_Dapper_Api.Models.DapperContext;
using Dapper;

namespace RealEstate_Dapper_Api.Repositories.CategoryRepository;

public class CategoryRepository : ICategoryRepository
{
    private readonly Context _context;

    public CategoryRepository(Context context)
    {
        _context = context;
    }

    public async void CreateCategory(CreateCategoryDto createCategoryDto)
    {
        string query = "Insert Into Category (CategoryName,CategoryStatus) Values (@categoryName, @categoryStatus)";
        var parameters = new DynamicParameters();
        parameters.Add("@categoryName", createCategoryDto.CategoryName);
        parameters.Add("@categoryStatus", true);
        using (var connection = _context.CreateConnection())
        {
            await connection.ExecuteAsync(query, parameters);
        }
    }

    public async Task<List<ResultCategoryDto>> GetAllCategoryAsync()
    {
        string query = "Select * From Category";
        using (var connection = _context.CreateConnection())
        {
            var values = await connection.QueryAsync<ResultCategoryDto>(query);
            return values.ToList();
        }
    }
}
