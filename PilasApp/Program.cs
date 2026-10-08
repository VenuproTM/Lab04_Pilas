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

    static void RevertirTexto()
    {
        Console.Write("Ingrese una cadena de texto: ");
        string texto = Console.ReadLine();

        Pila<char> pila = new Pila<char>(texto.Length);
        foreach (char c in texto)
            pila.Push(c);

        string invertido = "";
        while (!pila.IsEmpty())
            invertido += pila.Pop();

        Console.WriteLine($"Cadena invertida: {invertido}");
    }

    static void RevertirNumeros()
    {
        Console.Write("Ingrese números separados por espacio: ");
        string[] partes = Console.ReadLine()
            .Split(' ', StringSplitOptions.RemoveEmptyEntries);

        Pila<int> pila = new Pila<int>(partes.Length);
        foreach (string dato in partes)
            pila.Push(int.Parse(dato));

        Console.Write("Serie invertida: ");
        while (!pila.IsEmpty())
            Console.Write(pila.Pop() + " ");
        Console.WriteLine();
    }
}

class Program
{
    static string InfijoAPrefijo(string infijo)
    {
        char[] caracteres = infijo.ToCharArray();
        Array.Reverse(caracteres);

        for (int i = 0; i < caracteres.Length; i++)
        {
            if (caracteres[i] == '(')
                caracteres[i] = ')';
            else if (caracteres[i] == ')')
                caracteres[i] = '(';
        }

        string invertida = new string(caracteres);
        string postfija = InfijoAPostfijo(invertida);

        char[] resultado = postfija.ToCharArray();
        Array.Reverse(resultado);

        return new string(resultado);
    }
    static int Precedencia(char operador)
    {
        if (operador == '+' || operador == '-')
            return 1;

        if (operador == '*' || operador == '/')
            return 2;

        return 0;
    }

    static string InfijoAPostfijo(string infijo)
    {
        Pila<char> pila = new Pila<char>(infijo.Length);
        string resultado = "";

        foreach (char c in infijo)
        {
            if (char.IsDigit(c))
            {
                resultado += c;
            }
            else if (c == '(')
            {
                pila.Push(c);
            }
            else if (c == ')')
            {
                while (!pila.IsEmpty() && pila.Top() != '(')
                    resultado += pila.Pop();

                if (!pila.IsEmpty())
                    pila.Pop();
            }
            else
            {
                while (!pila.IsEmpty() &&
                    pila.Top() != '(' &&
                    Precedencia(pila.Top()) > Precedencia(c))
                {
                    resultado += pila.Pop();
                }

                pila.Push(c);
            }
        }

        while (!pila.IsEmpty())
            resultado += pila.Pop();

        return resultado;
    }
    static double EvaluarPrefijo(string prefijo)
    {
        Pila<double> pila = new Pila<double>(prefijo.Length);

        for (int i = prefijo.Length - 1; i >= 0; i--)
        {
            char c = prefijo[i];

            if (char.IsDigit(c))
            {
                pila.Push(c - '0');
            }
            else
            {
                double operandoIzq = pila.Pop();
                double operandoDer = pila.Pop();
                double resultado = 0;

                switch (c)
                {
                    case '+':
                        resultado = operandoIzq + operandoDer;
                        break;

                    case '-':
                        resultado = operandoIzq - operandoDer;
                        break;

                    case '*':
                        resultado = operandoIzq * operandoDer;
                        break;

                    case '/':
                        resultado = operandoIzq / operandoDer;
                        break;
                }

                pila.Push(resultado);
            }
        }

        return pila.Pop();
    }

    static void Main(string[] args)
    {
        int opcion;

        do
        {
            Console.WriteLine("\n===== APLICACIONES DE LA PILA =====");
            Console.WriteLine("[1] Revertir una cadena de texto");
            Console.WriteLine("[2] Revertir una serie de números");
            Console.WriteLine("[3] Evaluar una expresión aritmética");
            Console.WriteLine("[4] Salir");
            Console.Write("Seleccione una opción: ");

            opcion = int.Parse(Console.ReadLine());

            switch (opcion)
            {
                case 1:
                    Pila<char> pilaTexto = new Pila<char>();

                    Console.Write("Ingrese una cadena de texto: ");
                    string texto = Console.ReadLine();

                    foreach (char c in texto)
                        pilaTexto.Push(c);

                    string invertido = "";

                    while (!pilaTexto.IsEmpty())
                        invertido += pilaTexto.Pop();

                    Console.WriteLine($"Cadena invertida: {invertido}");
                    break;

                case 2:
                    Console.Write("Ingrese números separados por espacio: ");

                    string[] partes = Console.ReadLine()
                        .Split(' ', StringSplitOptions.RemoveEmptyEntries);

                    Pila<int> pilaNumeros = new Pila<int>(partes.Length);

                    foreach (string dato in partes)
                        pilaNumeros.Push(int.Parse(dato));

                    Console.Write("Serie invertida: ");

                    while (!pilaNumeros.IsEmpty())
                        Console.Write(pilaNumeros.Pop() + " ");

                    Console.WriteLine();
                    break;

                case 3:
                    Console.Write("Ingrese una expresión (ej. (2+3)*4): ");

                    string infijo = Console.ReadLine().Replace(" ", "");

                    string prefijo = InfijoAPrefijo(infijo);

                    Console.WriteLine($"Expresión en prefijo: {prefijo}");
                    Console.WriteLine($"Resultado: {EvaluarPrefijo(prefijo)}");
                    break;

                case 4:
                    Console.WriteLine("Saliendo del sistema...");
                    break;

                default:
                    Console.WriteLine("Opción inválida.");
                    break;
            }

        } while (opcion != 4);
    }
}
