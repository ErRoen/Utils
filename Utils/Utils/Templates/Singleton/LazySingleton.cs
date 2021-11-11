using System;

namespace Utils.Templates.Singleton
{
    public sealed class LazySingleton
    {
        private static readonly Lazy<LazySingleton> Lazy = new Lazy<LazySingleton>(() => new LazySingleton());

        public static LazySingleton Instance => Lazy.Value;

        private LazySingleton() { }
    }
}
