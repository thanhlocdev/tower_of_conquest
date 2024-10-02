using System.Runtime.CompilerServices;


namespace Module.Core
{
    public static class TypeIdExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOfType<T>(this TypeId self)
            => self.ToType() == typeof(T);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComplexId ToComplexId(this TypeId self)
            => new((Identifier)self, default);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComplexId ToComplexId(this TypeId self, Identifier id)
            => new((Identifier)self, id);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComplexId ToComplexId<T>(this TypeId<T> self)
            => new((Identifier)(Identifier<T>)self, default);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComplexId ToComplexId<T>(this TypeId<T> self, Identifier id)
            => new((Identifier)(Identifier<T>)self, id);
    }
}
