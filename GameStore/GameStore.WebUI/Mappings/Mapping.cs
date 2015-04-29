using System.Linq;
using AutoMapper;
using GameStore.BLL.Models;
using GameStore.DAL.Entities;
using GameStore.WebUI.ViewModels;
using GameStore.WebUI.ViewModels.GamesFiltersViewModels;

namespace GameStore.WebUI.Mappings
{
    public static class Mapping
    {
        public static void MapInit()
        {
            Mapper.CreateMap<Game, GameModel>();
            Mapper.CreateMap<GameModel, Game>()
                .ForMember(g => g.GameId, d => d.Ignore())
                .ForMember(g => g.Comments, d => d.Ignore())
                .ForMember(g => g.Genres, d => d.Ignore())
                .ForMember(g => g.PlatformTypes, d => d.Ignore());

            Mapper.CreateMap<Genre, GenreModel>();
            Mapper.CreateMap<GenreModel, Genre>()
                .ForMember(g => g.ParentGenre, d => d.Ignore())
                .ForMember(g => g.Games, d => d.Ignore())
                .ForMember(g => g.GenreId, d => d.Ignore());

            Mapper.CreateMap<Comment, CommentModel>();
            Mapper.CreateMap<CommentModel, Comment>()
                .ForMember(g => g.Game, d => d.Ignore())
                .ForMember(g => g.ParentComment, d => d.Ignore())
                .ForMember(g => g.CommentId, d => d.Ignore());

            Mapper.CreateMap<PlatformType, PlatformTypeModel>();
            Mapper.CreateMap<PlatformTypeModel, PlatformType>()
                .ForMember(g => g.Games, d => d.Ignore())
                .ForMember(g => g.PlatformTypeId, d => d.Ignore());

            Mapper.CreateMap<Publisher, PublisherModel>();
            Mapper.CreateMap<PublisherModel, Publisher>()
                .ForMember(g => g.PublisherId, d => d.Ignore())
                .ForMember(s => s.Games, d => d.Ignore());

            Mapper.CreateMap<GameModel, GameViewModel>()
                .ForMember(x => x.SelectedPublisherId, y => y.Ignore())
                .ForMember(x => x.Publishers, y => y.Ignore())
                .ForMember(gameViewModel => gameViewModel.SelectedGenresIds,
                    gameModel => gameModel.ResolveUsing(model => model.Genres.Select(genre => genre.GenreId)))
                .ForMember(gameViewModel => gameViewModel.SelectedPlatformTypesIds,
                    gameModel =>
                        gameModel.ResolveUsing(
                            model => model.PlatformTypes.Select(platformType => platformType.PlatformTypeId)));

            Mapper.CreateMap<GameViewModel, GameModel>()
                .ForMember(x => x.Comments, y => y.Ignore())
                .ForMember(x => x.BasketItems, y => y.Ignore())
                .ForMember(gameModel => gameModel.Genres,
                    gameViewModel => gameViewModel.ResolveUsing(model => model.SelectedGenresIds
                        .Select(id => new GenreModel {GenreId = id})))
                .ForMember(gameModel => gameModel.PlatformTypes,
                    gameViewModel => gameViewModel.ResolveUsing(model => model.SelectedPlatformTypesIds
                        .Select(platformTypeId => new PlatformTypeModel {PlatformTypeId = platformTypeId})))
                .ForMember(gameModel => gameModel.PublisherId,
                    gameViewModel => gameViewModel.MapFrom(g => g.SelectedPublisherId));

            Mapper.CreateMap<Basket, BasketModel>();
            Mapper.CreateMap<BasketModel, Basket>();

            Mapper.CreateMap<BasketItem, BasketItemModel>()
                .ForMember(x => x.BasketModel, y => y.Ignore());
            Mapper.CreateMap<BasketItemModel, BasketItem>()
                .ForMember(x => x.Basket, y => y.Ignore());

            Mapper.CreateMap<GamesFilterViewModel, GamesFilterModel>();
            Mapper.CreateMap<GamesFilterModel, GamesFilterViewModel>()
                .ForMember(x => x.IsAvailable, y => y.Ignore())
                .ForMember(x => x.AvailableGenres, y => y.Ignore())
                .ForMember(x => x.SelectedGenres, y => y.Ignore())
                .ForMember(x => x.AvailablePublishers, y => y.Ignore())
                .ForMember(x => x.SelectedPublishers, y => y.Ignore())
                .ForMember(x => x.AvailablePlatformTypes, y => y.Ignore())
                .ForMember(x => x.SelectedPlatformTypes, y => y.Ignore());

            Mapper.CreateMap<GenreFilterViewModel, GenreModel>()
                .ForMember(x => x.ParentGenreId, y => y.Ignore());
            Mapper.CreateMap<GenreModel, GenreFilterViewModel>()
                .IgnoreAllUnmapped()
                .ForMember(x => x.GenreId, y => y.MapFrom(z => z.GenreId))
                .ForMember(x => x.Name, y => y.MapFrom(z => z.Name));

            Mapper.CreateMap<PublisherFilterViewModel, PublisherModel>()
                .ForMember(x => x.Description, y => y.Ignore())
                .ForMember(x => x.HomePage, y => y.Ignore());
            Mapper.CreateMap<PublisherModel, PublisherFilterViewModel>()
                .IgnoreAllUnmapped()
                .ForMember(x => x.PublisherId, y => y.MapFrom(z => z.PublisherId))
                .ForMember(x => x.CompanyName, y => y.MapFrom(z => z.CompanyName));
            

            Mapper.CreateMap<PlatformTypeFilterViewModel, PlatformTypeModel>();
            Mapper.CreateMap<PlatformTypeModel, PlatformTypeFilterViewModel>()
                .IgnoreAllUnmapped()
                .ForMember(x => x.PlatformTypeId, y => y.MapFrom(z => z.PlatformTypeId))
                .ForMember(x => x.Type, y => y.MapFrom(z => z.Type));

            Mapper.CreateMap<PaginationViewModel, PaginationModel>();
            Mapper.CreateMap<PaginationModel, PaginationViewModel>();

            Mapper.CreateMap<PublisherModel, PublisherViewModel>();
            Mapper.CreateMap<PublisherViewModel, PublisherModel>();
        }
    }
}