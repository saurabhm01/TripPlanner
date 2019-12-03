using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tripzz.Data;
using Tripzz.Entity;
using Tripzz.Service.Interface;

namespace Tripzz.Service
{
    public class UserService : IUserService
    {
        private readonly IRepository<UserModel> repoUser;

        public UserService(IRepository<UserModel> _repoUser)
        {
            repoUser = _repoUser;
        }

        public void AddOrUpdateUserDetail(UserModel userData)
        {
            if (userData.Id > 0)
            {
                repoUser.Update(userData);
            }
            else
            {
                repoUser.Insert(userData);
            }
        }

        public UserModel VerifyUserDetail(string name, string password)
        {
            var userDetail = repoUser.GetAll().Where(t => t.UserName.Trim().Equals(name.Trim(), StringComparison.InvariantCultureIgnoreCase) && t.Password == password).FirstOrDefault();
            if (userDetail != null && userDetail.Id > 0)
            {
                return userDetail;
            }
            return null;
        }

        public UserModel GetUserDetailById(int id)
        {
            return repoUser.Get(id);
        }

    }
}
