using System;
using System.Collections.Generic;

namespace HealthAssistApp.Services.Data
{
    public interface IRecipesService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        T GetByName<T>(string name);
    }
}
