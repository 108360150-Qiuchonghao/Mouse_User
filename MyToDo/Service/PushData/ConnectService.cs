using MyToDo.Service.GetData;
using MyToDo.Shared.Contact;
using MyToDo.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Service.PushData
{
    public class ConnectService: IConnectService
    {
        private readonly HttpRestClient client;
        //有待更改
        private readonly string serviceName = "user";

        public ConnectService(HttpRestClient client)
        {
            this.client = client;
        }
        public async Task<ApiResponse<ConnectionDto>> Connect(ConnectionDto connectionDto)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.POST;
            request.Route = $"api/{serviceName}/Login";
            request.Parameter = connectionDto;
            return await client.ExecuteAsync<ConnectionDto>(request);
        }
    }
}
