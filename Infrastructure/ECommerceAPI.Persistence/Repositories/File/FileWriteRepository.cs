using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Persistence.Contexts;

namespace ECommerceAPI.Persistence.Repositories
{
    public class FileWriteRepository : WriteRepository<Domain.Entities.File>, IFileWriteRepository
    {
        public FileWriteRepository(ECommerceAPIDbContext context) : base(context)
        {
        }
    }
}
