namespace FrameworkDesign2021.ServiceLocator.Pattern
{
    public abstract class AbstractInitialContext
    {
        public abstract IService LookUp(string name);
    }
}