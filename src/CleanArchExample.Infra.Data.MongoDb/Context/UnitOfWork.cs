using CleanArchExample.Application.Repositories;

namespace CleanArchExample.Infra.Data.MongoDb.Context
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IMongoContext _context;
        public UnitOfWork(IMongoContext context)
        {
            _context = context;
        }

        public async Task<bool> Commit()
        {
            var affectedLines = await _context.SaveChanges();

            return affectedLines > 0;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
