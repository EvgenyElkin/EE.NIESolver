using System;

namespace EE.NIESolver.DataLayer.Constants
{
    public static class ConstantConext
    {
        private static Lazy<IConstantService> _current;
        public static IConstantService Current => _current.Value;

        public static void SetFactory(Func<IConstantService> factory)
        {
            _current = new Lazy<IConstantService>(factory);
        }

        public static int GetId<TEnum>(this TEnum value) where TEnum : struct
        {
            return _current.Value.GetId(value);
        }

        public static string GetDescription<TEnum>(this int id) where TEnum : struct
        {
            var value = _current.Value.GetEnum<TEnum>(id);
            return _current.Value.GetDescription(value);
        }

        public static TEnum GetEnum<TEnum>(this int id) where TEnum : struct
        {
            return _current.Value.GetEnum<TEnum>(id);
        }
    }
}