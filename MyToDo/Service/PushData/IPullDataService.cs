using MyToDo.Shared.Contact;
using MyToDo.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Service
{
    public interface IPullDataService
    {
        Task<ApiResponse<PullDatesDto>> GetAllDates(PullDatesDto pulldataDto);
        Task<ApiResponse<PullAllDataDto>> PullHistoryDatas(PullDatesDto pulldataDto);
    }
}
