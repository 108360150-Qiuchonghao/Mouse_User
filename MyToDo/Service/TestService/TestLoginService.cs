using MyToDo.Shared.Contact;
using MyToDo.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Service.TestService
{
    public class TestLoginService : ITestLoginService
    {
        private readonly HttpRestClient client;
        //有待更改
        private readonly string serviceName = "admin";

        public TestLoginService(HttpRestClient client)
        {
            this.client = client;
        }

        public async Task<ApiResponse<AdminDto>> Login(AdminDto admin)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.POST;
            request.Route = $"api/{serviceName}/login";
            request.Parameter = admin;
            return await client.ExecuteAsync<AdminDto>(request);
        }

        public async Task<ApiResponse> Resgiter(UserDto user)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.POST;
            request.Route = $"api/{serviceName}/resgiter";
            request.Parameter = user;
            return await client.ExecuteAsync(request);
        }


    }
}
