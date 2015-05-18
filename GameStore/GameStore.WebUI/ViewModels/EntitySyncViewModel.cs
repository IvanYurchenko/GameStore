using System.Web.Mvc;

namespace GameStore.WebUI.ViewModels
{
    public abstract class EntitySyncViewModel : EntityViewModel
    {
        [HiddenInput]
        public int? NorthwindId { get; set; }
    }
}