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

        public Task<User> GetByCpfAsync(User user);

        public Task<List<string>> GetListCpfAsync (PersonQuery personQuery);

        public Task InsertContentAsync(UserPost user);

        public Task BulkInsertAsync(List<UserPost> users);

    }
}