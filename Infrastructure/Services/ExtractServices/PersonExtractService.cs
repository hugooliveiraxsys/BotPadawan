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
            var persons = await _personApiRepository.GetListContentAsync(personQuery);

            Console.WriteLine("PESSOAS");
        }

        private void SendRequest(User user)
        {
            User usuario = new User();
            usuario.Cpf = user.Cpf;
            usuario.Nome = user.Nome;
            usuario.Sexo = user.Sexo;
            usuario.DataNascimento = user.DataNascimento;

            var jsonContent = JsonConvert.SerializeObject(usuario);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            await _cliente.PostAsync("person/create/", contentString);
        }
        private void SaveResponse()
        {
            List<UserPost> allUsers = new List<UserPost>();
            List<UserPost> auxUsers = new List<UserPost>();
            List<UserPost> threeUsers = new List<UserPost>();

            int counter = 1;

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
                    await BulkInsert(threeUsers);
                    Console.WriteLine($"{counter} - Inserindo valores");
                    counter++;
                    threeUsers.Clear();
                    count = 0;

                    if (aux == allUsers.Count)
                    {
                        threeUsers.Add(user);
                        await BulkInsert(threeUsers);
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
    }

        public Task ProcessAsync(int totalLimit, int stepLimit)
        {
            Console.WriteLine("Extraindo a pessoa");

            foreach (User user in persons)
            {
                Console.WriteLine($"Name:{user.Nome},\tCPF:{user.Cpf}");
            }

            Console.WriteLine("-------------");
            int personQuantity = int.Parse(commands.GetNextCommand());
            await getApiRepository.InsertListAsync(persons, personQuantity);

            //getApiRepository.InsertList();

            // Pegar a lista da API
            // Enviar quantidade de requisições
            // Salvar quantidade requisições



            List<UserPost> allUsers = new List<UserPost>();
            List<UserPost> auxUsers = new List<UserPost>();
            List<UserPost> threeUsers = new List<UserPost>();
            
            int counter = 1;

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
                    await BulkInsert(threeUsers);
                    Console.WriteLine($"{counter} - Inserindo valores");
                    counter++;
                    threeUsers.Clear();
                    count = 0;

                    if (aux == allUsers.Count)
                    {
                        threeUsers.Add(user);
                        await BulkInsert(threeUsers);
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
    }
}
