using System.Web.Mvc;

namespace GameStore.WebUI.ViewModels
{
    public abstract class EntityViewModel
    {
        [HiddenInput]
        public bool IsReadonly { get; set; }
    }
}