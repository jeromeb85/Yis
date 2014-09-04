namespace Yis.Framework.Core.Factory.Contract
{
    public interface IFactory
    {
        object CreateInstance();
    }

    public interface IFactory<T> where T : class
    {
        T CreateInstance();
    }
}