namespace algorithms_lab2.runner
{
    public interface IStack<T>
    {
        void Push(T elem);
        T Pop();
        T Top();
        bool IsEmpty();
        void Print();
    }
}