using Microsoft.EntityFrameworkCore;
using XYZUniversity.API.Data;
using XYZUniversity.API.Models.Domain;

namespace XYZUniversity.API.Repositories
{
    public class SQLPaymentRepository : IPaymentRepository
    {
        private readonly XYZUniversityDbContext dbContext;

        public SQLPaymentRepository(XYZUniversityDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Payment> CreateAsync(Payment payment)
        {
            await dbContext.Payments.AddAsync(payment);
            await dbContext.SaveChangesAsync();
            return payment;
        }

        public async Task<Payment> DeleteAsync(Guid id)
        {
            var existingPayment = await dbContext.Payments.FirstOrDefaultAsync(x => x.Id == id);
            if (existingPayment == null)
            {
                return null;
            }
            dbContext.Payments.Remove(existingPayment);
            await dbContext.SaveChangesAsync();
            return existingPayment;

        }
        public async Task<List<Payment>> GetAllAsync()
        {
            return await dbContext.Payments.ToListAsync();
        }

        public async Task<Payment?> GetByIdAsync(Guid id)
        {
            return await dbContext.Payments.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Payment?> UpdateAsync(Guid id, Payment payment)
        {
            var existingPayment = await dbContext.Payments.FirstAsync(x => x.Id == id);
            if (existingPayment == null)
            {
                return null;

            }
            existingPayment.Amount = payment.Amount;
            existingPayment.PaymentDate = payment.PaymentDate;

            await dbContext.SaveChangesAsync();
            return existingPayment;
        }
    }
}
