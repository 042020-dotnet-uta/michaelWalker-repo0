using System.Collections.Generic;

using VendorApp.Model;

namespace VendorApp.BusinessLogic
{
  /// <summary>
  /// All the status that the Packet can have.
  /// </summary>
  public enum PacketStatus
  {
    /// <summary>
    /// Clarifies to the ConsoleInterface that nothing went wrong
    /// and the console can continue operations like normal.
    /// </summary>
    Pass,
    /// <summary>
    /// Signals that something within the Business Logic has gone awry,
    /// and alternative steps need to be taken to address this.
    /// </summary>
    Invalid,
    /// <summary>
    /// Signals that an system error had occured and has been caught.
    /// </summary>
    Error,
    /// <summary>
    /// The default status when one hasn't been set yet
    /// </summary>
    NULL
  }

  /// <summary>
  /// A Packet is what serves as a bridge between the Business Logic and the Console Interface.  
  /// Most of methods in the Business Logic will return a Packet which contains information that the
  /// Console Interface will then use to display a message and perform various operations based
  /// on the status.
  /// </summary>
  public class Packet
  {

    /// <summary>
    /// The status of the packet.  This will signal whether the operation performed
    /// from the business logic had either passed or failed.
    /// </summary>
    public PacketStatus Status { get; set; }

    /// <summary>
    /// The message
    /// </summary>
    public string Text { get; set; }

    public Packet()
    {
      // Default Status is NULL
      Status = PacketStatus.NULL;
    }

  }

  /// <summary>
  /// A Packet is what serves as a bridge between the Business Logic and the Console Interface.  
  /// Most of methods in the Business Logic will return a Packet which contains information that the
  /// Console Interface will then use to display a message and perform various operations based
  /// on the status.
  /// 
  /// The Packet may also contain data in the form of a generic Enumerable if the 
  /// Console Interface requires it.
  /// </summary>
  public class Packet<T> : Packet
  {
    // TODO: add docs
    public T Data { get; set; }

    /// <summary>
    /// The data that the Console Interface can use to display to the user
    /// and perform operations based on the user's output and the retrieved data.
    /// </summary>
    public ICollection<T> DataList { get; set; }
  }

}