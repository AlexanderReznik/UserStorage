using System;
using System.Collections.Generic;
using System.Configuration;
using System.ServiceModel;
using ServiceConfigurationSection;
using UserStorageServices.Logging;
using UserStorageServices.Repositories;
using UserStorageServices.Services;

namespace UserStorageApp.DomainActions
{
    public class MasterDomainAction : MarshalByRefObject
    {
        public IUserRepositoryManager RepositoryManager { get; private set; }

        public IUserStorageService MasterService { get; private set; }

        private List<SlaveDomainAction> SlaveDomainActions { get; } = new List<SlaveDomainAction>();

        public void Run()
        {
            this.CreateSlaves();

            var path = ReadSetting("SavePath");
            var rep = new UserRepositoryWithState(path);
            this.RepositoryManager = rep;

            var masterService = new UserStorageServiceMaster();
            foreach (var slaveDomainAction in this.SlaveDomainActions)
            {
                masterService.Sender.AddReceiver(slaveDomainAction.Receiver);
            }

            this.MasterService = new UserStorageServiceLog(masterService);
        }

        private static string ReadSetting(string key)
        {
            var appSettings = ConfigurationManager.AppSettings;
            string path = appSettings[key];
            return path;
        }

        private void CreateSlaves()
        {
            var typeOfSlaveDomainAction = typeof(SlaveDomainAction);
            var serviceConfiguration = (ServiceConfigurationSection.ServiceConfigurationSection)System.Configuration.ConfigurationManager.GetSection("serviceConfiguration");

            foreach (var serviceInstance in serviceConfiguration.ServiceInstances)
            {
                if (serviceInstance.Mode == ServiceInstanceMode.Slave)
                {
                    var slaveDomain = AppDomain.CreateDomain($"SlaveDomain{SlaveDomainActions.Count}");

                    var slaveDomainAction =
                        (SlaveDomainAction)slaveDomain.CreateInstanceAndUnwrap(
                            typeOfSlaveDomainAction.Assembly.FullName,
                            typeOfSlaveDomainAction.FullName);
                    slaveDomainAction.Run();
                    this.SlaveDomainActions.Add(slaveDomainAction);
                }
            }
        }
    }
}
