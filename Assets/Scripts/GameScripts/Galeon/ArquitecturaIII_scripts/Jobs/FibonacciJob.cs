using Unity.Collections;
using Unity.Jobs;
public struct FibonacciJob : IJob
{
    private int n;
    public NativeArray<int> fibonacciSerie;
    public FibonacciJob(int a_n, ref NativeArray<int> arr)
    {
        n = a_n;
        fibonacciSerie = arr;
    }
    private void Fibonacci(int n)
    {
        int aux, a, b;
        b = 1;
        a = 0;
        for (int i = 0; i < n; ++i)
        {
            aux = a;
            a = b;
            b = aux + a;
            fibonacciSerie[i] = a;
        }
    }
    public void Execute()
    {
        Fibonacci(n);
    }
}