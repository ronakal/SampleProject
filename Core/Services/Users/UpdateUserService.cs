using System;
using System.Collections.Generic;
using BusinessEntities;
using Common;

namespace Core.Services.Users
{
    [AutoRegister(AutoRegisterTypes.Singleton)]
    public class UpdateUserService : IUpdateUserService
    {
        public void Update(User user, string name, string email, UserTypes type, decimal? annualSalary, IEnumerable<string> tags)
        {
            if (!string.IsNullOrEmpty(email)) { user.SetEmail(email); }
            if (!string.IsNullOrEmpty(name)) { user.SetName(name); }
                
            user.SetType(type);
            if (annualSalary.HasValue) { user.SetMonthlySalary(annualSalary.Value / 12); }
            
            user.SetTags(tags);
        }
    }
}