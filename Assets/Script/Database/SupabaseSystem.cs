using System.Threading.Tasks;
using Script.Singleton;
using Supabase;
using UnityEngine;

namespace Script.Database
{
    public class SupabaseManager : Singleton<SupabaseManager>
    {
        [SerializeField] string url = "url_supabase_project";
        [SerializeField] string key = "key_supabase";
        
        private Client _client;

        public Client GetClient() => _client;

        private async void Start()
        {
            _client = new Client(url, key, new SupabaseOptions
            {
                AutoRefreshToken = true,
            });

            await _client.InitializeAsync();

            Debug.Log("Connexion à Supabase réussie !");
        }

        public async Task<Database.Table.Account> GetAccount(string mail)
        {
            var response = await SupabaseManager.instance.GetClient()
                .From<Database.Table.Account>()
                .Select("*")
                .Where(account => account.email.Equals(mail))
                .Single();

            return response;
        }
    }
}