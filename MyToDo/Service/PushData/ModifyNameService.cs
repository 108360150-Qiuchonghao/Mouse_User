using MyToDo.Shared.Contact;
using MyToDo.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Service.PushData
{
    public class ModifyNameService : IModifyNameService
    {
        private readonly HttpRestClient client;
        //有待更改
        private readonly string serviceName = "user";

        public ModifyNameService(HttpRestClient client)
        {
            this.client = client;
        }
        public async Task<ApiResponse<ModifyNameDto>> Modify(ModifyNameDto modifynamedto)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.POST;
            request.Route = $"api/{serviceName}/modifyName";
            request.Parameter = modifynamedto;
            return await client.ExecuteAsync<ModifyNameDto>(request);
        }
    }
}
