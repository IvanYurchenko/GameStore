using System.Collections.Generic;
using GameStore.BLL.Models.Localization;

namespace GameStore.BLL.Interfaces
{
    public interface ILanguageService
    {
        /// <summary>
        /// Gets all languages.
        /// </summary>
        /// <returns></returns>
        IEnumerable<LanguageModel> GetAll();
    }
}
