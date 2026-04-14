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

    /// <summary>
    /// Deletes the specified node from the list by matching value and next reference.
    /// </summary>
    /// <param name="node">The node to delete.</param>
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

    /// <summary>
    /// Returns an enumerator that iterates through the list.
    /// </summary>
    /// <returns>An enumerator that can be used to iterate through the collection of nodes.</returns>
    public IEnumerator<Node> GetEnumerator()
    {
        Node? current = this.head;
        while (current is not null)
        {
            yield return current;
            current = current.Next;
        }
    }

    /// <summary>
    /// Returns an enumerator that iterates through the collection.
    /// </summary>
    /// <returns>An <see cref="IEnumerator"/> that can be used to iterate through the collection.</returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <summary>
    /// Returns the value of the first node that is a multiple of the specified number.
    /// </summary>
    /// <param name="num">The number to check for multiples.</param>
    /// <returns>The value of the first multiple if found; otherwise, null.</returns>
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

    /// <summary>
    /// Gets the total number of positive values in the list.
    /// </summary>
    /// <returns>The amount of positive node values.</returns>
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

    /// <summary>
    /// Appends the specified node to the end of the list.
    /// </summary>
    /// <param name="node">The node to append.</param>
    public void Append(Node node)
    {
        this.Last().Next = node;
        len++;
    }

    /// <summary>
    /// Appends a new node with the specified value to the end of the list.
    /// </summary>
    /// <param name="value">The value to append.</param>
    public void Append(int value)
    {
        this.Last().Next = new Node(value);
        len++;
    }

    /// <summary>
    /// Gets a new single linked list containing only nodes with values strictly greater than the given number.
    /// </summary>
    /// <param name="num">The threshold number.</param>
    /// <returns>A new <see cref="List"/> of elements larger than <paramref name="num"/>.</returns>
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

    /// <summary>
    /// Calculates the average value of all elements in the list.
    /// </summary>
    /// <returns>The average value as a double. Returns 0 if the list is empty.</returns>
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

    /// <summary>
    /// Removes all nodes from the list that have a value strictly greater than the current average.
    /// </summary>
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