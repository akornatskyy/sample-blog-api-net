using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;

using Blog.Models;
using Blog.Repository.Infrastructure;
using Blog.Repository.Interface;
using Blog.Repository.Properties;

namespace Blog.Repository.Mock
{
    public class UserRepository : IUserRepository
    {
        private static readonly JToken Users;

        private readonly ContextAccessor contextAccessor;

        static UserRepository()
        {
            var samples = JObject.Parse(Encoding.UTF8.GetString(Resources.UserSamples));
            Users = samples["users"];
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