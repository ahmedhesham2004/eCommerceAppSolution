using eCommerceApp.Domain.Entities.Cart;
using eCommerceApp.Domain.Interfaces.Cart;
using eCommerceApp.Infrastructure.Data;

namespace eCommerceApp.Infrastructure.Repositorys.Cart;
public class CartRepositorys(ApplicationDbContext context) : ICart
{
    private readonly ApplicationDbContext _context = context;

    public async Task<int> SaveCheckoutAsync(IEnumerable<Achieve> checkouts)
    {
        await _context.CheckoutAchieves.AddRangeAsync(checkouts);
        return await _context.SaveChangesAsync();
    }
}
