using System.Threading.Tasks;
using APITemplate.Domain.Entities;
using APITemplate.Domain.Repositories.Interfaces;
using APITemplate.Domain.Services.Interfaces;
using Api.Base.Pagination.Base;
using System.Collections.Generic;

namespace APITemplate.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Delete(long id)
        {
            await _userRepository.DeleteByIdAsync(id);
            await _userRepository.SaveChangesAsync();
        }

        public async Task<IPage<User>> FindAll(IPageable pageable)
        {
            return await _userRepository.QueryHelper()
                .GetPageAsync(pageable);
        }

        public async Task<User> FindOne(long id)
        {
            return await _userRepository.QueryHelper()
                .GetOneAsync(user => user.Id == id);
        }

        public Task<IPage<User>> GetUserByCondition(User user, IPageable pageable)
        {
            return _userRepository.QueryHelper()
                .WhereIf(!string.IsNullOrWhiteSpace(user.Name), x => x.Name.ToLower().Contains(user.Name.ToLower()))
                .WhereIf(!string.IsNullOrWhiteSpace(user.Email), x => x.Email.ToLower().Equals(user.Email.ToLower()))
                .GetPageAsync(pageable);
        }

        public async Task<User> Save(User user)
        {
            await _userRepository.CreateOrUpdateAsync(user);
            await _userRepository.SaveChangesAsync();
            return user;
        }
    }
}
