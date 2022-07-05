using Infrastructure.Services.ExtractServices.Interfaces;
using Models.Entities;
using Models.Requests;
using Newtonsoft.Json;
using Repositories;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
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

        private async void GetList(int totalLimit)
        {
            PersonQuery personQuery = new PersonQuery();
            personQuery.Limit = totalLimit;
            Console.WriteLine("PESSOAS");
            var persons = await _personApiRepository.GetListContentAsync(personQuery);
        }

        private async void SendRequest(User user)
        {
            User usuario = new User();
            usuario.Cpf = user.Cpf;
            usuario.Nome = user.Nome;
            usuario.Sexo = user.Sexo;
            usuario.DataNascimento = user.DataNascimento;

            await _personApiRepository.InsertContentAsync(user);
        }

        private async void SaveResponse(List<User> users, int stepLimit)
        {
            List<UserPost> allUsers = new List<UserPost>();
            List<UserPost> auxUsers = new List<UserPost>();
            List<UserPost> threeUsers = new List<UserPost>();

            int counter = 1;
            int count = 0;
            int aux = 0;

            foreach (User user in users)
            {
                UserPost usuario = new UserPost();
                usuario.Cpf = user.Cpf;
                usuario.Gender = user.Sexo;
                usuario.Name = user.Nome;
                usuario.BirthDate = user.DataNascimento;
                allUsers.Add(usuario);
            }

            auxUsers.AddRange(allUsers);

            foreach (UserPost user in allUsers)
            {
                aux++;
                if (auxUsers.Count > 0 && count < stepLimit)
                {
                    threeUsers.Add(user);
                    auxUsers.RemoveAt(0);
                }
                else
                {
                    await _personApiRepository.BulkInsert(threeUsers);
                    Console.WriteLine($"{counter} - Inserindo valores");
                    counter++;
                    threeUsers.Clear();
                    count = 0;

                    if (aux == allUsers.Count)
                    {
                        threeUsers.Add(user);
                        await _personApiRepository.BulkInsert(threeUsers);
                        Console.WriteLine($"{counter} - Inserindo valores");
                        counter++;
                        threeUsers.Clear();
                    }
                    else
                    {
                        threeUsers.Add(user);
                    }
                }
                count++;
            }
        }

        public Task ProcessAsync(int totalLimit, int stepLimit)
        {
            throw new NotImplementedException();
        }
    }
}
