// Program.cs - Tema 4: Pilas (Stack)
using System;
class Pila<T>
{
    private T[] elementos;
    private int tope; // indice del ultimo elemento insertado (-1 si esta vacia)
    private int capacidad;
    public Pila(int capacidad = 100)
    {
        this.capacidad = capacidad;
        elementos = new T[capacidad];
        tope = -1;
    }
    public void Push(T valor)
    {
        if (tope == capacidad - 1)
            throw new InvalidOperationException("La pila esta llena.");
        elementos[++tope] = valor;
    }
    public T Pop()
    {
        if (IsEmpty())
            throw new InvalidOperationException("La pila esta vacia.");
        return elementos[tope--];
    }
    public T Top()
    {
        if (IsEmpty())
            throw new InvalidOperationException("La pila esta vacia.");
        return elementos[tope];
    }
    public bool IsEmpty() => tope == -1;
    public void Clear() => tope = -1;
}

class Program
{
    static void Main(string[] args)
    {
        Pila<int> prueba = new Pila<int>(5);
        Console.WriteLine(prueba.IsEmpty()); // True
        prueba.Push(10);
        prueba.Push(20);
        prueba.Push(30);
        Console.WriteLine(prueba.Top()); // 30
        Console.WriteLine(prueba.Pop()); // 30
        Console.WriteLine(prueba.Top()); // 20
        prueba.Clear();
        Console.WriteLine(prueba.IsEmpty()); // True
    }
}
