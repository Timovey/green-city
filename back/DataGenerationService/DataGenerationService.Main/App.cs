using DataGenerationService.Core.BusinessLogic;

namespace DataGenerationService.Main
{
    public class App
    {
        private readonly GeneratingContainerLogic _generatingContainerLogic;
        private readonly GeneratingStandLogic _generatingStandLogic;
        private readonly GeneratingContractLogic _generatingContractLogic;

        private readonly GeneratingContractClientLogic _generatingContractClientLogic;
        //private readonly GeneratingContainerTypeLogic _generatingContainerTypeLogic;
        //private readonly GeneratingContract2StandLogic _generatingContract2StandLogic;

        public App(
            GeneratingContainerLogic generatingContainerLogic,
            GeneratingStandLogic generatingStandLogic,
            GeneratingContractLogic generatingContractLogic,
            GeneratingContractClientLogic generatingContractClientLogic
            //GeneratingContainerTypeLogic generatingContainerTypeLogic
            //GeneratingContract2StandLogic generatingContract2StandLogic
        )
        {
            _generatingContainerLogic = generatingContainerLogic;
            _generatingStandLogic = generatingStandLogic;
            _generatingContractLogic = generatingContractLogic;
            _generatingContractClientLogic = generatingContractClientLogic;
            //_generatingContainerTypeLogic = generatingContainerTypeLogic;
            //_generatingContract2StandLogic = generatingContract2StandLogic;
        }

        public async Task RunAsync(string[] args)
        {
            await _generatingContractLogic.GenerateContractsAsync();
            await _generatingStandLogic.GenerateStandsAsync();
            await _generatingContainerLogic.GenerateContainersAsync();
            await _generatingContractClientLogic.GenerateContractClientsAsync();
            //await _generatingContract2StandLogic.GenerateContract2StandsAsync();
            //await _generatingContainerTypeLogic.GenerateContainerTypesAsync();
        }
    }
}