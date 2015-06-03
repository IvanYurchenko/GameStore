using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.BLL.Models.Localization;

namespace GameStore.BLL.Interfaces
{
    public interface ILanguageService
    {
        IEnumerable<LanguageModel> GetAll();
    }
}
