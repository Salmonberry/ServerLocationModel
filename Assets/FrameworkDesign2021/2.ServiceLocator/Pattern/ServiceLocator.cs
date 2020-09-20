using System;

namespace FrameworkDesign2021.ServiceLocator.Pattern
{
    public class ServiceLocator
    {
        private readonly Cache mCache = new Cache();

        private readonly AbstractInitialContext mContext;

        public ServiceLocator(AbstractInitialContext context)
        {
            mContext = context;
        }

        public IService GetService(string name)
        {
            var service = mCache.GetService(name);

            if (service == null)
            {
                service = mContext.LookUp(name);

                mCache.AddService(service);
            }

            if (service == null)
            {
                throw new Exception("Service:" + name + " 不存在");
            }

            return service;
        }
    }
}