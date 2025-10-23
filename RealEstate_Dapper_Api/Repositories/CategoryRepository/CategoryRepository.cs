using RealEstate_Dapper_Api.Models.DapperContext;
using Dapper;
using RealEstate_Dapper_Api.Dtos.CategoryDtos;

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

    public async void DeleteCategory(int id)
    {
        string query = "Delete From Category Where CategoryID = @categoryID";
        var parameters = new DynamicParameters();
        parameters.Add("@categoryID", id);
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

    public async Task<GetByIDCategoryDto> GetCategory(int id)
    {
        string query = "Select * From Category Where CategoryID = @categoryID";
        var parameters = new DynamicParameters();
        parameters.Add("@categoryID", id);
        using (var connection = _context.CreateConnection())
        {
           var values = await connection.QueryFirstOrDefaultAsync<GetByIDCategoryDto>(query, parameters);
            return values;
        }
    }

    public async void UpdateCategory(UpdateCategoryDto updateCategoryDto)
    {
        string query = "Update Category Set CategoryName = @categoryName, CategoryStatus = @categoryStatus Where CategoryID = @categoryID";
        var parameters = new DynamicParameters();
        parameters.Add("@categoryName", updateCategoryDto.CategoryName);
        parameters.Add("@categoryStatus", updateCategoryDto.CategoryStatus);
        parameters.Add("@categoryID", updateCategoryDto.CategoryID);
        using (var connection = _context.CreateConnection())
        {
            await connection.ExecuteAsync(query, parameters);
        }
    }
}
