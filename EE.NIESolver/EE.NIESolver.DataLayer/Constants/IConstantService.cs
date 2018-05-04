using System.Collections.Generic;

namespace EE.NIESolver.DataLayer.Constants
{
    public interface IConstantService
    {
        int GetId<TEnum>(TEnum value) where TEnum : struct;
        TEnum GetEnum<TEnum>(int id) where TEnum : struct;
        string GetDescription<TEnum>(TEnum value) where TEnum: struct;
        IEnumerable<ConstantItem> GetValues<TEnum>() where TEnum : struct;
    }
}