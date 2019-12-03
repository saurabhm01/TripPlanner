using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tripzz.Data;
using Tripzz.Entity;

namespace Tripzz.Service.Interface
{
    public interface IUserService
    {
        UserModel VerifyUserDetail(string name, string password);
        void AddOrUpdateUserDetail(UserModel userData);
        UserModel GetUserDetailById(int id);
    }
}
