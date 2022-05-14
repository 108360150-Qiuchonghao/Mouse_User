using MyToDo.Shared.Contact;
using MyToDo.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Service.PushData
{
    public class PushDataService: IPushDataService
    {
        private readonly HttpRestClient client;
        //有待更改
        private readonly string serviceName = "user";

        public PushDataService(HttpRestClient client)
        {
            this.client = client;
        }
        public async Task<ApiResponse<PushDataDto>> AddHistory(PushDataDto pushDataDto)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.POST;
            request.Route = $"api/{serviceName}/addhistory";
            request.Parameter = pushDataDto;
            return await client.ExecuteAsync<PushDataDto>(request);
        }
    }
}
