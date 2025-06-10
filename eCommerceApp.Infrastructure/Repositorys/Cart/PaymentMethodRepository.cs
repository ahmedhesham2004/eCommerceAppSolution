using eCommerceApp.Domain.Entities.Cart;
using eCommerceApp.Domain.Interfaces.Cart;
using eCommerceApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace eCommerceApp.Infrastructure.Repositorys.Cart;
public class PaymentMethodRepository(ApplicationDbContext context) : IPaymentMethod
{
    private readonly ApplicationDbContext _context = context;

    public async Task<IEnumerable<PaymentMethod>> GetPaymentMethodAsync() 
        => await _context.PaymentMethods.AsNoTracking().ToListAsync();
}
