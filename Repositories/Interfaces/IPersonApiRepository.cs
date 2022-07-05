using Models;
using Models.Entities;
using Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IPersonApiRepository
    {

        public Task<User> GetContentAsync(string id);

        public Task<List<User>> GetListContentAsync(PersonQuery personQuery);

        public Task InsertContentAsync(User user);

        public Task InsertListAsync(List<User> users, int numeroPessoas);

        public Task BulkInsert(List<UserPost> users);

        public Task ProcessAsync(int totalLimit, int stepLimit);

    }
}
