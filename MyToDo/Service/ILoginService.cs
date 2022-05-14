using MyToDo.Shared.Contact;
using MyToDo.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Service
{
    public interface ILoginService
    {
        Task<ApiResponse<UserDto>> Login(UserDto user);

        Task<ApiResponse> Resgiter(UserDto user);

    }
}
