using Models;
using Models.Entities;
using Models.Requests;
using Newtonsoft.Json;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class PersonApiRepository : IPersonApiRepository
    {
        public HttpClient _cliente = new HttpClient();
        static int count = 0;
        static int aux = 0;

        public PersonApiRepository()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44337/");
            _cliente = client;
        }

        public async Task BulkInsert(List<UserPost> users)
        {
            var userSerialized = JsonConvert.SerializeObject(users);
            var contentString = new StringContent(userSerialized, Encoding.UTF8, "application/json");
            await _cliente.PostAsync(_cliente.BaseAddress + "person/bulkmanual", contentString);
        }

        public async Task InsertListAsync(List<User> users, int personQuantity)
        {
            // Pegar a lista da API
            // Enviar quantidade de requisições
            // Salvar quantidade requisições



            List<UserPost> allUsers = new List<UserPost>();
            List<UserPost> auxUsers = new List<UserPost>();
            List<UserPost> threeUsers = new List<UserPost>();
            int times = personQuantity;
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
                if (auxUsers.Count > 0 && count < times)
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

        public async Task InsertContentAsync(User user)
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

        public async Task<User> GetContentAsync(string id)
        {
            var response = await _cliente.GetAsync("person/" + id);
            var content = await response.Content.ReadAsStringAsync();

            var user = JsonConvert.DeserializeObject<User>(content);

            return user;
        }

        public async Task<List<User>> GetListContentAsync(PersonQuery personQuery)
        {
            string json = JsonConvert.SerializeObject(personQuery);

            StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _cliente.PostAsync("person/list", httpContent);
            var content = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<User[]>(content);

            Console.WriteLine(users.Length);
            return users.ToList();
        }
    }
}
