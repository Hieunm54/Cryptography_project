using APITemplate.Domain.Entities;
using Api.Base.Pagination.Base;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;

namespace APITemplate.Domain.Services.Interfaces
{
    public interface IUserService {
        Task<IPage<User>> GetUserByCondition(User user, IPageable pageable);
        Task<User> Save(User user);
        Task<IPage<User>> FindAll(IPageable pageable);
        Task<User> FindOne(long id);
        Task Delete(long id);
    }
}
