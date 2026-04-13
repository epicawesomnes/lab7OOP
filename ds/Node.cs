using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualBasic.FileIO;

namespace ds;

/// <summary>
/// Represents a node in a singly linked list.
/// </summary>
public class Node
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Node"/> class with default values (Value = 0, Next = null).
    /// </summary>
    public Node()
    {
        this.Value = 0;
        this.Next = null;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Node"/> class with the specified value and next node reference.
    /// </summary>
    /// <param name="value">The integer value to store in the node.</param>
    /// <param name="next">The reference to the next node in the list.</param>
    public Node(int value, Node? next)
    {
        this.Value = value;
        this.Next = next;
    }

    public Node(int value)
    {
        this.Value = value;
        this.Next = null;
    }

    /// <summary>
    /// Gets or sets the value of the node.
    /// </summary>
    public int Value
    {
        get;
        set;
    }

    /// <summary>
    /// Gets or sets the reference to the next node in the list.
    /// </summary>
    public Node? Next
    {
        get;
        set;
    }

}

