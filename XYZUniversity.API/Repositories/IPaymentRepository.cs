using XYZUniversity.API.Models.Domain;

namespace XYZUniversity.API.Repositories
{
    public interface IPaymentRepository
    {
        Task<Payment> CreateAsync(Payment payment);
        Task<List<Payment>> GetAllAsync();
        Task<Payment?> GetByIdAsync(Guid id);
        Task<Payment?>UpdateAsync(Guid id , Payment payment);
        Task<Payment>DeleteAsync(Guid id);
        
    }
}
