using Bunit;
using Microsoft.AspNetCore.Components;

namespace QuickSnapApp.Canvas.Tests;

/// <summary>
/// This is a method for mocking out / stubbing components that have multiple instances in the render tree.
/// https://github.com/bUnit-dev/bUnit/issues/560
/// </summary>
public static class ComponentFactoryCollectionExtensions
{
    public static void AddMock<T>(this ComponentFactoryCollection factories, T mock)
        where T : IComponent
    {
        factories.Add(new MockInstanceComponentFactory(typeof(T), mock));
    }

    public static void AddMock<T>(this ComponentFactoryCollection factories, Func<T> mockFactory)
        where T : IComponent
    {
        factories.Add(new MockComponentFactory<T>(mockFactory));
    }

    private class MockInstanceComponentFactory(Type target, IComponent mockInstance) : IComponentFactory
    {
        public bool CanCreate(Type componentType) => target == componentType;

        public IComponent Create(Type componentType) => mockInstance;
    }

    private class MockComponentFactory<T>(Func<T> mockFactory) : IComponentFactory where T : IComponent
    {
        private readonly Type target = typeof(T);

        public bool CanCreate(Type componentType) => target == componentType;

        public IComponent Create(Type componentType) => mockFactory.Invoke();
    }
}
