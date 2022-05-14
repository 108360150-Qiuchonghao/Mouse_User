using MyToDo.Shared.Contact;
using MyToDo.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Service.GetData
{
    public interface IConnectService
    {

        Task<ApiResponse<ConnectionDto>> Connect(ConnectionDto connectionDto);


    }
}
