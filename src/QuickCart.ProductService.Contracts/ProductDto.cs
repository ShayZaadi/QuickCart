namespace QuickCart.ProductService.Contracts
{
    public record ProductDto(
    int Id,
    string Name,
    string Description,
    decimal Price,
    bool Available
    );
}
