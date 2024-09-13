using TransportContracts.DataContracts.TransportWorkShift;

namespace TransportService.Core.BusinessLogic
{
    public class HangfireLogic
    {
        private readonly TransportSheduleLogic _transportSheduleLogic;

        public HangfireLogic(TransportSheduleLogic transportSheduleLogic)
        {
            _transportSheduleLogic = transportSheduleLogic;
        }

        public async Task FillTransportNextWorkDateAsync(DateTime startDate)
        {
            await _transportSheduleLogic.FillTransportNextWorkDateAsync(new ActualizeWorkShitsContract()
            {
                DateTime = startDate
            });
        }
    }
}
