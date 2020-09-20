namespace FrameworkDesign2021.ServiceLocator.Pattern
{
    public interface IService 
    {
        string Name { get; }

        void Execute();
    }
}