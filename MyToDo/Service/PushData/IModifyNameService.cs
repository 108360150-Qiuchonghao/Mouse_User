using MyToDo.Shared.Contact;
using MyToDo.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Service.PushData
{
    public interface IModifyNameService
    {
        Task<ApiResponse<ModifyNameDto>> Modify(ModifyNameDto modifyNameDto);
    }
}
