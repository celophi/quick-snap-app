using FluentAssertions;
using Moq;
using System.Linq.Expressions;
using System.Reflection;

namespace QuickSnapApp.Canvas.Tests;
public static class MockExtensions
{
    /// <summary>
    /// Asserts that the order of invocations and the associated arguments are correct.
    /// Note that this solution is not perfect, and it does not catch missed instructions that follow after the assertion!
    /// It only catches out of order instructions.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="mock"></param>
    /// <param name="expressions"></param>
    public static void ShouldBeInOrder<T>(this Mock<T> mock, params Expression<Action<T>>[] expressions) where T : class
    {
        // All closures have the same instance of sharedCallCount
        var sharedCallCount = 0;

        // the list are the indeces where the expression appears.
        var groups = new Dictionary<string, List<int>>();

        for (var i = 0; i < expressions.Length; i++)
        {
            // Moq has a unique way to identify if an expression is identical to another when attaching callbacks.
            // This method serializes the expression name, types, arguments, so that calls can be grouped to make sure an invocation is in order.
            var debugViewProperty = typeof(Expression)
                .GetProperty("DebugView", BindingFlags.Instance | BindingFlags.NonPublic);

            var serializedExpression = debugViewProperty!.GetValue(expressions[i]) as string;

            if (!groups.ContainsKey(serializedExpression!))
            {
                groups.Add(serializedExpression!, new List<int>());
            }

            groups[serializedExpression!].Add(i);

            mock.Setup(expressions[i]).Callback(() =>
            {
                mock.Verify(expressions[sharedCallCount]);
                groups.ContainsKey(serializedExpression!).Should().BeTrue();
                groups[serializedExpression!].Contains(sharedCallCount).Should().BeTrue();

                sharedCallCount++;
            });
        }
    }
}
