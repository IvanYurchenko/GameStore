using System.Collections.Generic;
using GameStore.BLL.Models.Localization;

namespace GameStore.BLL.Interfaces
{
    public interface ILanguageService
    {
        IEnumerable<LanguageModel> GetAll();
    }
}
