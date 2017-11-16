using System;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using ServiceConfigurationSection;

namespace UserStorageApp.DomainActions
{
    public class ClientDomainAction : MarshalByRefObject
    {
        public Client Client { get; private set; }

        public void Run()
        {
            var serviceConfiguration = (ServiceConfigurationSection.ServiceConfigurationSection)System.Configuration.ConfigurationManager.GetSection("serviceConfiguration");
            if (serviceConfiguration.ServiceInstances.Count(i => i.Mode == ServiceInstanceMode.Master) != 1)
            {
                throw new ConfigurationErrorsException("Number of master services is not 1.");
            }

            var masterDomain = AppDomain.CreateDomain("MasterDomain");

            var typeOfMasterDomainAction = typeof(MasterDomainAction);

            var masterDomainAction =
                (MasterDomainAction)masterDomain.CreateInstanceAndUnwrap(
                    typeOfMasterDomainAction.Assembly.FullName,
                    typeOfMasterDomainAction.FullName);

            masterDomainAction.Run();
            Client = new Client(masterDomainAction.MasterService, masterDomainAction.RepositoryManager);
        }
    }
}
