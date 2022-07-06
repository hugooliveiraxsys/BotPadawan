using Models;
using Models.Entities;
using Models.Requests;
using Newtonsoft.Json;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        // Pegar a lista da API
        // Enviar quantidade de requisições
        // Salvar quantidade requisições

        public async Task BulkInsertAsync(List<UserPost> users)
        {
            var userSerialized = JsonConvert.SerializeObject(users);
            var contentString = new StringContent(userSerialized, Encoding.UTF8, "application/json");
            await _cliente.PostAsync(_cliente.BaseAddress + "person/bulkmanual", contentString);
        }

        public async Task InsertContentAsync(UserPost user)
        {
            var jsonContent = JsonConvert.SerializeObject(user);
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
