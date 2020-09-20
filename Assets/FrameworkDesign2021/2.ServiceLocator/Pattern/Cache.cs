using System.Collections.Generic;
using System.Linq;

namespace FrameworkDesign2021.ServiceLocator.Pattern
{
    public class Cache
    {
        List<IService> mServices = new List<IService>();

        public IService GetService(string serviceName)
        {
            return mServices.SingleOrDefault(s => s.Name == serviceName);
        }

        public void AddService(IService service)
        {
            mServices.Add(service);
        }
    }
}