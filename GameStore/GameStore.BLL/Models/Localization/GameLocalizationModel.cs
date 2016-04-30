namespace GameStore.BLL.Models.Localization
{
    public class GameLocalizationModel : LocalizationEntityModel
    {
        public int GameLocalizationId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
