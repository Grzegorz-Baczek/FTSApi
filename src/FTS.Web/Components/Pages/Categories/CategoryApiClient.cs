//using FTS.Web.Components.Pages.Categories.Models;

//namespace FTS.Web.Components.Pages.Categories;

//public class CategoryApiClient(HttpClient httpClient)
//{
//    public async Task<IEnumerable<CategoryViewModel>> GetCategories(CancellationToken cancellationToken)
//    {
//        var url = $"api/categories";
//        var result = await httpClient.GetAsync(url, cancellationToken);
//        if (!result.IsSuccessStatusCode)
//        {
//            new Exception("Categories not found");
//        }

//        return await result.Content.ReadFromJsonAsync<IEnumerable<CategoryViewModel>>(cancellationToken: cancellationToken) ?? Enumerable.Empty<CategoryViewModel>();
//    }
//}
