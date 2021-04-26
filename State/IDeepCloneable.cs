namespace StateService
{
    public interface IDeepCloneable<T>
    {
        public T DeepCopy();
    }
}