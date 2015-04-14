using AutoMapper;
using GameStore.BLL.Models;
using GameStore.DAL.Entities;

namespace GameStore.BLL.Mappings
{
    public static class Mapping
    {
        public static void MapInit()
        {
            // Mapping eneities to models
            Mapper.CreateMap<Game, GameModel>();
            Mapper.CreateMap<GameModel, Game>();

            Mapper.CreateMap<Genre, GenreModel>();
            Mapper.CreateMap<GenreModel, Genre>()
                .ForMember(g => g.ParentGenre, d => d.Ignore())
                .ForMember(g => g.Games, d => d.Ignore());

            Mapper.CreateMap<Comment, CommentModel>();
            Mapper.CreateMap<CommentModel, Comment>()
                .ForMember(g => g.Game, d => d.Ignore())
                .ForMember(g => g.ParentComment, d => d.Ignore());

            Mapper.CreateMap<PlatformType, PlatformTypeModel>();
            Mapper.CreateMap<PlatformTypeModel, PlatformType>().ForMember(g => g.Games, d => d.Ignore());

        }
    }
}