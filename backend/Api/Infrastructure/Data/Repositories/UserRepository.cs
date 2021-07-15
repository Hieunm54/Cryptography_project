using APITemplate.Domain.Entities;
using APITemplate.Domain.Repositories.Interfaces;
using Project.Infrastructure.Data.Repositories;
using System.Threading.Tasks;

namespace APITemplate.Infrastructure.Data.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(IUnitOfWork context) : base(context)
        {
        }

        public override async Task<User> CreateOrUpdateAsync(User user)
        {
            bool exists = await Exists(x => x.Id == user.Id);
            if (user.Id != 0 && exists)
            {
                Update(user);
            }
            else
            {
                Add(user);
            }
            return user;
        }
    }
}
