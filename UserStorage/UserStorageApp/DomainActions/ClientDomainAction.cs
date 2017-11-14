using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageApp.DomainActions
{
    public class ClientDomainAction : MarshalByRefObject
    {
        public Client Client { get; private set; }

        public void Run()
        {
            var masterDomain = AppDomain.CreateDomain("MasterDomain");

            var typeOfMasterDomainAction = typeof(MasterDomainAction);

            var masterDomainAction =
                (MasterDomainAction) masterDomain.CreateInstanceAndUnwrap(typeOfMasterDomainAction.Assembly.FullName,
                    typeOfMasterDomainAction.FullName);

            masterDomainAction.Run(2);
            Client = new Client(masterDomainAction.MasterService, masterDomainAction.RepositoryManager);

        }
    }
}
