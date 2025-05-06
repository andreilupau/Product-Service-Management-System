class ListaGen<T> : IEnumerable<T>
{
    private class Nod
    {
        public T Data { get; set; }
        public Nod Next { get; set; }

        public Nod(T data)
        {
            Data = data;
            Next = null;
        }
    }

    private Nod Inceput;
    public uint Count { get; set; }

    public ListaGen()
    {
        Inceput = null;
        Count = 0;
    }

    public void Add(T t)
    {
        Nod n = new Nod(t);
        n.Next = Inceput;
        Inceput = n;
        Count++;
    }

    public IEnumerator<T> GetEnumerator()
    {
        Nod current = Inceput;
        while (current != null)
        {
            yield return current.Data;
            current = current.Next;
        }
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

    // ➕ ADĂUGĂM o metodă de traversare manuală
    public void TraverseWithWhile()
    {
        Nod current = Inceput;
        while (current != null)
        {
            Console.WriteLine(current.Data);
            current = current.Next;
        }
    }
}