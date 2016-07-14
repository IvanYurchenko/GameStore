using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GameStore.BLL.Models.Localization;

namespace GameStore.BLL.Models
{
    public class PublisherModel : EntitySyncModel
    {
        [Key]
        public int PublisherId { get; set; }

        public string HomePage { get; set; }

        public ICollection<PublisherLocalizationModel> PublisherLocalizations { get; set; } 
    }
}