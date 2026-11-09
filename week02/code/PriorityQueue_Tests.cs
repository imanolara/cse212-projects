using Microsoft.VisualStudio.TestTools.UnitTesting;

// Problem 2 - Test cases for all PriorityQueue requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue Low, Medium, and High in that order, with the highest-priority item last.
    // Expected Result: Items are added at the back. Dequeue returns High, Medium, and Low in
    // descending priority order.
    // Defect(s) Found: The final item was not examined when searching for the highest priority.
    public void TestPriorityQueue_HighestPriority()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Low", 1);
        priorityQueue.Enqueue("Medium", 3);
        priorityQueue.Enqueue("High", 5);

        Assert.AreEqual("[Low (Pri:1), Medium (Pri:3), High (Pri:5)]", priorityQueue.ToString());
        Assert.AreEqual("High", priorityQueue.Dequeue());
        Assert.AreEqual("Medium", priorityQueue.Dequeue());
        Assert.AreEqual("Low", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue two items with the same highest priority, with lower-priority items before
    // and after them.
    // Expected Result: The first high-priority item is returned before the second high-priority item.
    // Defect(s) Found: Equal priorities replaced the earlier match, so the later item was returned first.
    public void TestPriorityQueue_EqualPriorityUsesFifo()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Low Front", 1);
        priorityQueue.Enqueue("First High", 5);
        priorityQueue.Enqueue("Second High", 5);
        priorityQueue.Enqueue("Low Back", 2);

        Assert.AreEqual("First High", priorityQueue.Dequeue());
        Assert.AreEqual("Second High", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Dequeue the highest-priority item and then dequeue again.
    // Expected Result: The first item is removed, so the second call returns the remaining item.
    // Defect(s) Found: Dequeue returned a value without removing its item from the queue.
    public void TestPriorityQueue_DequeueRemovesItem()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("First", 10);
        priorityQueue.Enqueue("Second", 1);

        Assert.AreEqual("First", priorityQueue.Dequeue());
        Assert.AreEqual("Second", priorityQueue.Dequeue());
        Assert.AreEqual("[]", priorityQueue.ToString());
    }

    [TestMethod]
    // Scenario: Dequeue from an empty priority queue.
    // Expected Result: An InvalidOperationException is thrown with the message "The queue is empty."
    // Defect(s) Found: None. The correct exception type and message were already produced.
    public void TestPriorityQueue_Empty()
    {
        var priorityQueue = new PriorityQueue();

        var exception = Assert.ThrowsException<InvalidOperationException>(
            () => priorityQueue.Dequeue()
        );
        Assert.AreEqual("The queue is empty.", exception.Message);
    }
}
