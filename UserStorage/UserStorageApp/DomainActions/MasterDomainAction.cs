using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public void Run(int slavesCount)
        {
            CreateSlaves(slavesCount);

            var path = ReadSetting("SavePath");
            var rep = new UserRepositoryWithState(path);
            RepositoryManager = rep;

            var masterService = new UserStorageServiceMaster();
            foreach (var slaveDomainAction in SlaveDomainActions)
            {
                masterService.Sender.AddReceiver(slaveDomainAction.Receiver);
            }

            MasterService = new UserStorageServiceLog(masterService);
        }

        private void CreateSlaves(int slavesCount)
        {
            var typeOfSlaveDomainAction = typeof(SlaveDomainAction);
            for (int i = 0; i < slavesCount; i++)
            {
                var slaveDomain = AppDomain.CreateDomain($"SlaveDomain{i}");

                var slaveDomainAction =
                    (SlaveDomainAction) slaveDomain.CreateInstanceAndUnwrap(typeOfSlaveDomainAction.Assembly.FullName,
                        typeOfSlaveDomainAction.FullName);
                slaveDomainAction.Run();
                SlaveDomainActions.Add(slaveDomainAction);
            }
        }

        private static string ReadSetting(string key)
        {
            var appSettings = ConfigurationManager.AppSettings;
            string path = appSettings[key];
            return path;
        }
    }
}
