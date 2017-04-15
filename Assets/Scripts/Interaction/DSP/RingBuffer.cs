using System;

public class RingBuffer<T>
{
    private T[] buffer;

    private int index = -1;

    public RingBuffer(int size)
    {
        buffer = new T[size];
    }

    public void Add(T value)
    {
        index = (index + 1) % buffer.Length;
        buffer[index] = value;
    }

    public T[] Buffer
    {
        get
        {
            return buffer;
        }
    }

    public T this[int index]
    {
        get
        {
            return buffer[index];
        }
        set
        {
            buffer[index] = value;
        }
    }
}
