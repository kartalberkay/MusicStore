
using System;

namespace MainMusicStore.DataAccess.IMainRepository
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository category { get; }
        ICompanyRepository Company { get; }
        IProductRepository Product { get; }
        ICoverTypeRepository CoverType { get; }
        IApplicationUserRepository ApplicationUser { get; }
        ISCallRepository sp_call { get; }
        void Save();
    }
}
