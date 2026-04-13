using System.Collections;
using System.Net.NetworkInformation;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Xml.XPath;
using Microsoft.VisualBasic;

namespace ds;

/// <summary>
/// Represents a singly linked list data structure.
/// </summary>
public class List : IEnumerable<Node>
{
    private Node head;
    private int len;

    /// <summary>
    /// Initializes a new instance of the <see cref="List"/> class with a single default node.
    /// </summary>
    public List()
    {
        this.head = new();
        this.len = 1;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="List"/> class with a specified number of default nodes.
    /// </summary>
    /// <param name="len">The length of the list to create.</param>
    public List(int len)
    {
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(len, 0, nameof(len));

        this.head = new();
        Node current = this.head;
        for (int i = 0; i < len - 1; i++)
        {
            current.Next = new();
            current = current.Next;
        }
        this.len = len;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="List"/> class populated from an integer array.
    /// </summary>
    /// <param name="array">The array of integers used to populate the list.</param>
    public List(int[] array)
    {
        this.head = new();
        Node current = this.head;
        for (int i = 0; i < array.Length - 1; i++)
        {
            current.Next = new();
            current.Value = array[i];
            current = current.Next;
        }
        current.Value = array[^1];

        this.len = array.Length;
    }

    /// <summary>
    /// Gets or sets the node at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index of the node to get or set.</param>
    /// <returns>The node at the specified index.</returns>
    /// <exception cref="IndexOutOfRangeException">Thrown when the index is greater than the length of the list.</exception>
    /// <exception cref="NullReferenceException">Thrown when attempting to access a null node reference.</exception>
    public Node this[int index]
    {
        get
        {
            if (index > len - 1) {throw new IndexOutOfRangeException("Index is bigger than lenght of list");}
            
            Node current = this.head;
            for (int i = 0; i < index; i++)
            {
                if (current.Next is null) { throw new NullReferenceException(); }
                current = current.Next;
            }
            return current;
        }
        set
        {
            if (index > len - 1) {throw new IndexOutOfRangeException("Index is bigger than lenght of list");}
            
            Node current = this.head;
            for (int i = 0; i < index; i++)
            {
                if (current.Next is null) { throw new NullReferenceException(); }
                current = current.Next;
            }
            current.Value = value.Value;
        }
    }

    /// <summary>
    /// Gets the current number of nodes in the list.
    /// </summary>
    public int Len
    {
        get
        {
            return this.len;
        }
    }

    /// <summary>
    /// Deletes the node at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index of the node to delete.</param>
    public void DeleteNode(int index)
    {
        if (index == 0)
        {
            if (this.head.Next is null)
            {
                throw new ArgumentException("You cant delete the only node.");
            }
            else
            {
                this.head = this.head.Next;
                this.len -= 1;
            }
            return;
        }

        if (index == this.len - 1) 
        {
            this[index - 1].Next = null;
            this.len -= 1;
            return;
        }
        this[index - 1].Next = this[index + 1];
        this.len -= 1;
    }

    public void DeleteNode(Node node)
    {
        for (int i = 0; i < this.len; i++)
        {
            if (this[i].Value == node.Value && this[i].Next == node.Next)
            {
                this.DeleteNode(i);
                return;
            }
        }
    }

    public IEnumerator<Node> GetEnumerator()
    {
        Node? current = this.head;
        while (current is not null)
        {
            yield return current;
            current = current.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public int? GetFirstmultiple(int num)
    {
        foreach (var node in this)
        {
            if (node.Value % num == 0)
            {
                return node.Value;
            }
        }

        return null;
    }

    public int GetNumPositives()
    {
        int result = 0;
        foreach (var node in this)
        {
            if (node.Value > 0) 
            {
                result++;
            }
        }
        return result;
    }

    public void Append(Node node)
    {
        this.Last().Next = node;
        len++;
    }

    public void Append(int value)
    {
        this.Last().Next = new Node(value);
        len++;
    }

    public List GetListOfElementsBiggerThan(int num)
    {
        List result = new();

        foreach (var node in this)
        {
            if (node.Value > num)
            {
                result.Append(node.Value);
            }
        }
        result.DeleteNode(0);

        return result;
    }

    public double GetAvarage()
    {
        if (this.len == 0) return 0;
        
        double sum = 0;
        foreach (var node in this)
        {
            sum += node.Value;
        }
        return sum / this.len;
    }

    public void DeleteElementsBiggerThanAvarage()
    {
        double avg = this.GetAvarage();

        for (int i = 0; i < this.len; )
        {
            if (this[i].Value > avg)
            {
                this.DeleteNode(i);
            }
            else
            {
                i++;
            }
        }
    }
}