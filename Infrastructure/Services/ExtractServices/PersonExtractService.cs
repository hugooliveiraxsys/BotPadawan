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

        private async Task<List<string>> GetList(int totalLimit)
        {
            // Buscar somente os CPFs, retorna uma lista de cpfs, lista strings
            PersonQuery personQuery = new PersonQuery();
            personQuery.Limit = totalLimit;
            Console.WriteLine("CPFS");

            var cpfList = await _personApiRepository.GetListCpfAsync(personQuery);
            
            return cpfList;
        }

        private async Task SendRequest(User user)
        {
            // Vai buscar somente um CPF da lista que foi retornada
            // Vai retornar o usuario a partir do CPF

            //UserPost usuario = new UserPost();
            //usuario.Cpf = user.Cpf;
            //usuario.Name = user.Nome;
            //usuario.Gender = user.Sexo;
            //usuario.BirthDate = user.DataNascimento;

            //await _personApiRepository.InsertContentAsync(usuario);

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
                    await _personApiRepository.BulkInsertAsync(threeUsers);
                    Console.WriteLine($"{counter} - Inserindo valores");
                    counter++;
                    threeUsers.Clear();
                    count = 0;

                    if (aux == allUsers.Count)
                    {
                        threeUsers.Add(user);
                        await _personApiRepository.BulkInsertAsync(threeUsers);
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

        public async Task ProcessAsync(int totalLimit, int stepLimit)
        {
            var cpfs = await GetList(totalLimit);
            foreach(string cpf in cpfs)
            {
                Console.WriteLine(cpf);
            }

            //do
            //{
            //    List<> requests =
            //    List <> responses =
            //    //foreach
            //    Response response = await SendRequest(persons.FirstOrDefault());
            //    responses.Add(response);

            //    //remover person
            //} while (persons.Count != 0);
        }
    }
}
