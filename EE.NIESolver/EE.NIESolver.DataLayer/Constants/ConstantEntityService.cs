using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EE.NIESolver.DataLayer.Entities.Common;
using EE.NIESolver.DataLayer.Repositories;

namespace EE.NIESolver.DataLayer.Constants
{
    public interface IConstantService
    {
        int GetId<TEnum>(TEnum value) where TEnum : struct;
        TEnum GetEnum<TEnum>(int id) where TEnum : struct;
    }

    public class ConstantService : IConstantService
    {
        private class EnumItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
        }

        private static IDictionary<int, EnumItem> _cache;
        private static IDictionary<string, EnumItem> _index;

        public ConstantService(IDataRepository repository)
        {
            if (_cache == null)
            {
                Initialize(repository);

                EnumItem Converter(ConstantEntity x) => new EnumItem {Id = x.Id, Name = x.Name, Description = x.Description};
                var enums = repository.Select<ConstantEntity>().ToArray();

                _cache = enums.ToDictionary(x => x.Id, Converter);
                _index = enums.ToDictionary(x => x.Name, Converter);
            }
        }

        public int GetId<TEnum>(TEnum value) where TEnum : struct
        {
            return _index[typeof(TEnum).Name + "." + value].Id;
        }

        public TEnum GetEnum<TEnum>(int id) where TEnum : struct
        {
            return Enum.Parse<TEnum>(_cache[id].Name.Split('.').Last());
        }

        private void Initialize(IDataRepository repository)
        {
            var codeConstants = new List<string>();

            var assambly = Assembly.GetAssembly(GetType());
            foreach (var enumType in assambly.DefinedTypes.Where(x => x.IsEnum))
            {
                foreach (var enumValueName in enumType.GetEnumNames())
                {
                    codeConstants.Add(enumType.Name + "." + enumValueName);
                }
            }

            var databaseConstants = repository.Select<ConstantEntity>().ToArray();
            var newConstants = codeConstants.GroupJoin(databaseConstants,
                    x => x,
                    x => x.Name,
                    (code, db) => new { Enum = code, IsNew = !db.Any() })
                .Where(x => x.IsNew)
                .Select(x => x.Enum)
                .Select(x => new ConstantEntity { Name = x })
                .ToArray();

            repository.Add(newConstants);
            repository.Apply();
        }
    }
}
