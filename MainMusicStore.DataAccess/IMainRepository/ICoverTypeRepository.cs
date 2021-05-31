using MainMusicStore.DataAccess.IRepository;
using MainMusicStore.Models.DbModels;


namespace MainMusicStore.DataAccess.IMainRepository
{
    public interface ICoverTypeRepository : IRepository<CoverType>
    {
        void Update(CoverType category);
    }
}
