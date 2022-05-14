using MyToDo.Shared.Contact;
using MyToDo.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Service.TestService
{
    public interface ITestLoginService
    {
        Task<ApiResponse<AdminDto>> Login(AdminDto admin);

        Task<ApiResponse> Resgiter(UserDto admin);

    }
}
