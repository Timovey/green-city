using GreenCity.Http;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransportContracts.DataContracts.Driver;
using TransportContracts.DataContracts.DriverVacation;
using TransportContracts.Filters;
using TransportContracts.ViewModels;

namespace TransportContracts.Interfaces
{
    public interface IDriverApi
    {
        [Post("/Driver/GetFilteredDriver")]
        Task<CommonHttpResponse<IList<DriverViewModel>>> GetFilteredDriverAsync(
           [Body] DriverFilterModel filter
        );

        [Post("/Driver/GetDriver")]
        Task<CommonHttpResponse<IList<DriverViewModel>>> GetDriverAsync(
            [Body] GetDriverContract getContract
        );

        [Post("/Driver/CreateDriver")]
        Task<CommonHttpResponse<DriverViewModel>> CreateDriverAsync(
            [Body] CreateDriverContract createContract
        );

        [Post("/Driver/UpdateDriver")]
        Task<CommonHttpResponse<DriverViewModel>> UpdateDriverAsync(
            [Body] UpdateDriverContract updateContract
        );

        [Post("/Driver/DeleteDriver")]
        Task<CommonHttpResponse> DeleteDriverAsync(
            [Body] DeleteDriverContract deleteContract
        );

        [Post("/Driver/GetFilteredDriverVacation")]
        Task<CommonHttpResponse<IList<DriverVacationViewModel>>> GetFilteredDriverVacationAsync(
          [Body] DriverVacationFilterModel filter
       );

        [Post("/Driver/GetDriverVacation")]
        Task<CommonHttpResponse<IList<DriverVacationViewModel>>> GetDriverVacationAsync(
            [Body] GetDriverVacationContract getContract
        );

        [Post("/Driver/CreateDriverVacation")]
        Task<CommonHttpResponse<DriverVacationViewModel>> CreateDriverVacationAsync(
            [Body] CreateDriverVacationContract createContract
        );

        [Post("/Driver/UpdateDriverVacation")]
        Task<CommonHttpResponse<DriverVacationViewModel>> UpdateDriverVacationAsync(
            [Body] UpdateDriverVacationContract updateContract
        );

        [Post("/Driver/DeleteDriverVacation")]
        Task<CommonHttpResponse> DeleteDriverVacationAsync(
            [Body] DeleteDriverVacationContract deleteContract
        );
    }
}
