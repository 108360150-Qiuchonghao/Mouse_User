using MyToDo.Shared.Contact;
using MyToDo.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Service
{
    public class PullService : IPullDataService
    {
        private readonly HttpRestClient client;
       
        private readonly string serviceName = "user";

        public PullService(HttpRestClient client)
        {
            this.client = client;
        }

        public async Task<ApiResponse<PullDatesDto>> GetAllDates(PullDatesDto pullDatesDto)
        {
                                
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.POST;
            request.Route = $"api/{serviceName}/pullAllDates";
            request.Parameter = pullDatesDto;
            return await client.ExecuteAsync<PullDatesDto>(request);
        }

        public async Task<ApiResponse<PullAllDataDto>> PullHistoryDatas(PullDatesDto pullDatesDto)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.POST;
            request.Route = $"api/{serviceName}/pullalldata";
            request.Parameter = pullDatesDto;
            return await client.ExecuteAsync<PullAllDataDto>(request);
        }
    }
}
