using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

using Blog.Models;
using Blog.Repository.Infrastructure;
using Blog.Repository.Interface;

namespace Blog.Repository.Mock
{
    public class UserRepository : IUserRepository
    {
        private static readonly IList<Dictionary<string, object>> Users;

        private readonly ContextAccessor contextAccessor;

        static UserRepository()
        {
            Users = JsonConvert.DeserializeObject<Dictionary<string, List<Dictionary<string, object>>>>(
                Encoding.UTF8.GetString(Properties.Resources.UserSamples))["users"];
        }

        public UserRepository(ContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
        }

        public Task<AuthInfo> FindAuthInfo(string username)
        {
            Trace.Assert(this.contextAccessor.Context != null);

            return Task.FromResult((
                from u in Users
                where (string)u["username"] == username
                select new AuthInfo
                {
                    UserId = u["id"].ToString(),
                    Password = (string)u["password"],
                    IsLocked = (bool)u["is_locked"]
                }).FirstOrDefault());
        }
    }
}
