using Infrastructure.Services.ExtractServices.Interfaces;
using Models.Entities;
using Models.Requests;
using Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Services.ExtractServices
{
    public class PersonExtractService : IPersonExtractService
    {
        private IPersonApiRepository _personApiRepository;

        public PersonExtractService(IPersonApiRepository personApiRepository)
        {
            _personApiRepository = personApiRepository;
        }

        private async Task<List<string>> GetList(int totalLimit)
        {
            // Buscar somente os CPFs, retorna uma lista de cpfs, lista strings
            PersonQuery personQuery = new PersonQuery();
            personQuery.Limit = totalLimit;

            var cpfList = await _personApiRepository.GetListCpfAsync(personQuery);
            return cpfList;
        }

        private async Task<User> SendRequest(string cpf)
        {
            //Vai buscar somente um CPF da lista que foi retornada
            //Vai retornar o usuario a partir do CPF
            User user = new User();
            user.Cpf = cpf;
;           return await _personApiRepository.GetByCpfAsync(user);
        }

        private async Task SaveResponse(List<User> users)
        {
            List<UserPost> usersPost = new List<UserPost>();
            foreach (User user in users)
            {
                UserPost userPost = new UserPost();
                userPost.Cpf = user.Cpf;
                userPost.Name = user.Nome;
                userPost.Gender = user.Sexo;
                userPost.BirthDate = user.DataNascimento;
                usersPost.Add(userPost);
            }
            await _personApiRepository.BulkInsertAsync(usersPost);
        }

        public async Task ProcessAsync(int totalLimit, int stepLimit)
        {
            List<string> CpfsRequests = await GetList(totalLimit);

            List<User> usersResponses = new List<User>();
            List<User> auxUsers = new List<User>();
            int runCounter = 0, counter = 0;
            
            foreach (string Cpf in CpfsRequests)
            {
                usersResponses.Add(await SendRequest(Cpf));
            }

            foreach(var user in usersResponses)
            {
                auxUsers.Add(user);
                counter++;
                runCounter++;
                if (counter == stepLimit)
                {
                    counter = 0;
                    await SaveResponse(auxUsers);
                    auxUsers.Clear();
                }
                else if(runCounter > (totalLimit -1))
                {
                    await SaveResponse(auxUsers);
                    auxUsers.Clear();
                }
            }
        }
    }
}
