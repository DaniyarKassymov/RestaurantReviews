namespace exam9kassymovdaniyar.ViewModels.Restaurant;

public class RestaurantPaginationVm
{
    public int PageNumber { get; }
    public int TotalPages { get; }
    
    public RestaurantPaginationVm(int count, int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
    }
    
    public bool HasPreviousPage => (PageNumber > 1);
    
    public bool HasNextPage => (PageNumber < TotalPages);
}