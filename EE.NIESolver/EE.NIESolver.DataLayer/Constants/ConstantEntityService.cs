using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EE.NIESolver.DataLayer.Entities.Common;
using EE.NIESolver.DataLayer.Repositories;

namespace EE.NIESolver.DataLayer.Constants
{
    public class ConstantService : IConstantService
    {
        private readonly IDictionary<int, ConstantItem> _cache;
        private readonly IDictionary<string, ConstantItem> _index;

        public ConstantService(IDataRepository repository)
        {
            Initialize(repository);

            ConstantItem Converter(DbConstant x) => new ConstantItem {Id = x.Id, Value = x.Name, Description = x.Description ?? x.Name};
            var enums = repository.Query<DbConstant>().ToArray();

            _cache = enums.ToDictionary(x => x.Id, Converter);
            _index = enums.ToDictionary(x => x.Name, Converter);
        }

        public int GetId<TEnum>(TEnum value) where TEnum : struct
        {
            return _index[typeof(TEnum).Name + "." + value].Id;
        }

        public string GetDescription<TEnum>(TEnum value) where TEnum : struct
        {
            return _index[typeof(TEnum).Name + "." + value].Description;
        }
        
        public TEnum GetEnum<TEnum>(int id) where TEnum : struct
        {
            return Enum.Parse<TEnum>(_cache[id].Value.Split('.').Last());
        }

        public Dictionary<int, string> GetDescriptions<TEnum>() where TEnum : struct
        {
            return Enum.GetValues(typeof(TEnum))
                .OfType<TEnum>()
                .ToDictionary(GetId, GetDescription);
        }

        public IEnumerable<ConstantItem> GetValues<TEnum>() where TEnum : struct
        {
            return Enum.GetValues(typeof(TEnum))
                .OfType<TEnum>()
                .Select(x => _cache[GetId(x)])
                .ToArray();
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

            var databaseConstants = repository.Query<DbConstant>().ToArray();
            var newConstants = codeConstants.GroupJoin(databaseConstants,
                    x => x,
                    x => x.Name,
                    (code, db) => new { Enum = code, IsNew = !db.Any() })
                .Where(x => x.IsNew)
                .Select(x => x.Enum)
                .Select(x => new DbConstant { Name = x })
                .ToArray();

            repository.Add(newConstants);
            repository.Apply();
        }
    }
}
