using IririApi.Libs.Infrastructure.Concrete;
using IririApi.Libs.Infrastructure.Contract;
using IririApi.Libs.Model;
using IririApi.Libs.Model.IRepository;
using IririApi.Libs.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IririApi.Libs.Repository
{
   

    public class AdminRepository : Repository<MemberRegistrationUser>
    {
        private readonly AuthenticationContext _dbContext;

        public AdminRepository(AuthenticationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

    }
}
