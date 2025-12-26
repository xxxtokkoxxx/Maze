using VContainer;

namespace _Maze.CodeBase.Extensions
{
    public static class VContainerExtension
    {
        public static RegistrationBuilder Register<TInterface1, TInterface2, TInterface3, TInterface4, TImplement>(
            this IContainerBuilder builder,
            Lifetime lifetime)
            where TImplement : TInterface1, TInterface2, TInterface3, TInterface4
            => builder.Register<TImplement>(lifetime).As(typeof(TInterface1), typeof(TInterface2), typeof(TInterface3),
                typeof(TInterface4));
    }
}