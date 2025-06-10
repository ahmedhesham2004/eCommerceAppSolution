using eCommerceApp.Domain.Entities.Cart;

namespace eCommerceApp.Domain.Interfaces.Cart;
public interface ICart
{
    Task<int> SaveCheckoutAsync(IEnumerable<Achieve> checkouts);
}
