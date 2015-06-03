using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using AutoMapper;
using GameStore.BLL.Models;
using GameStore.BLL.Models.Localization;
using GameStore.BLL.Models.Payment;
using GameStore.BLL.Models.Payment.External;
using GameStore.BLL.Models.Security;
using GameStore.Core;
using GameStore.Core.Enums;
using GameStore.DAL.Entities;
using GameStore.DAL.Entities.Localization;
using GameStore.DAL.Entities.Security;
using GameStore.DAL.Northwind;
using GameStore.WebUI.Security;
using GameStore.WebUI.ViewModels;
using GameStore.WebUI.ViewModels.GamesFilters;
using GameStore.WebUI.ViewModels.Localization;
using GameStore.WebUI.ViewModels.Payment;
using GameStore.WebUI.ViewModels.Payment.Info;
using GameStore.WebUI.ViewModels.Security;
using Order = GameStore.DAL.Entities.Order;

namespace GameStore.WebUI.Mappings
{
    public static class Mapping
    {
        public static void MapInit()
        {
            InitEntitiesWithNorthwindEntitiesMapping();
            InitEntitiesWithModelsMapping();
            InitModelsWithModelsMapping();
            InitModelsWithViewModelsMapping();
        }

        private static void InitEntitiesWithNorthwindEntitiesMapping()
        {
            Mapper.CreateMap<Order_Detail, OrderItem>()
                .ForMember(x => x.Quantity, y => y.ResolveUsing(z => Convert.ToInt32(z.Quantity)))
                .ForMember(x => x.Price, y => y.MapFrom(z => z.UnitPrice))
                .ForMember(x => x.NorthwindOrderId, y => y.MapFrom(z => z.OrderID))
                .ForMember(x => x.NorthwindProductId, y => y.MapFrom(z => z.ProductID))
                .ForMember(x => x.GameId, y => y.Ignore())
                .ForMember(x => x.OrderId, y => y.Ignore())
                .ForMember(x => x.Game, y => y.Ignore())
                .ForMember(x => x.Order, y => y.Ignore())
                .ForMember(x => x.OrderItemId, y => y.UseValue(0))
                .ForMember(x => x.IsReadonly, y => y.UseValue(true));

            Mapper.CreateMap<DAL.Northwind.Order, Order>()
                .ForMember(x => x.IsReadonly, y => y.UseValue(true))
                .ForMember(x => x.OrderId, y => y.UseValue(0))
                .ForMember(x => x.SessionKey, y => y.Ignore())
                .ForMember(x => x.OrderDate, y => y.MapFrom(z => z.OrderDate))
                .ForMember(x => x.OrderItems,
                    y => y.ResolveUsing(z => Mapper.Map<IEnumerable<OrderItem>>(z.Order_Details)))
                .ForMember(x => x.OrderStatus, y => y.UseValue(OrderStatus.Shipped))
                .ForMember(x => x.NorthwindId, y => y.MapFrom(z => z.OrderID));

            Mapper.CreateMap<Category, Genre>()
                .ForMember(x => x.IsReadonly, y => y.UseValue(true))
                .ForMember(x => x.ParentGenre, y => y.Ignore())
                .ForMember(x => x.ParentGenreId, y => y.Ignore())
                .ForMember(x => x.Games, y => y.Ignore())
                .ForMember(x => x.GenreId, y => y.UseValue(0))
                .ForMember(x => x.GenreLocalizations, y => y.ResolveUsing(
                    z => new List<GenreLocalization>
                    {
                        new GenreLocalization
                        {
                            LanguageId = Constants.EnglishLanguageId,
                            Name = z.CategoryName,
                        }
                    }
                    ))
                .ForMember(x => x.NorthwindId, y => y.MapFrom(z => z.CategoryID));

            Mapper.CreateMap<Supplier, Publisher>()
                .ForMember(x => x.NorthwindId, y => y.MapFrom(z => z.SupplierID))
                .ForMember(x => x.IsReadonly, y => y.UseValue(true))
                .ForMember(x => x.PublisherId, y => y.UseValue(0))
                .ForMember(x => x.Games, y => y.Ignore())
                .ForMember(x => x.PublisherLocalizations, y => y.ResolveUsing(
                    z => new List<PublisherLocalization>
                    {
                        new PublisherLocalization
                        {
                            LanguageId = Constants.EnglishLanguageId,
                            CompanyName = z.CompanyName,
                            Description = z.CompanyName + " description. "
                        }
                    }
                    ));

            Mapper.CreateMap<Product, Game>()
                .ForMember(x => x.NorthwindId, y => y.MapFrom(z => z.ProductID))
                .ForMember(x => x.Key, y => y.MapFrom(z => z.ProductName))
                .ForMember(x => x.Price, y => y.ResolveUsing(z => z.UnitPrice ?? 0))
                .ForMember(x => x.PublicationDate, y => y.Ignore())
                .ForMember(x => x.Comments, y => y.Ignore())
                .ForMember(x => x.PlatformTypes, y => y.Ignore())
                .ForMember(x => x.IsReadonly, y => y.UseValue(true))
                .ForMember(x => x.GameId, y => y.UseValue(0))
                .ForMember(x => x.BasketItems, y => y.Ignore())
                .ForMember(x => x.UnitsInStock, y => y.ResolveUsing(z => z.UnitsInStock ?? 0))
                .ForMember(x => x.AddedDate, y => y.UseValue(DateTime.UtcNow))
                .ForMember(x => x.Discontinued, y => y.MapFrom(z => z.Discontinued))
                .ForMember(x => x.PublisherId, y => y.Ignore())
                .ForMember(x => x.Genres, y => y.Ignore())
                .ForMember(x => x.Publisher, y => y.Ignore())
                .ForMember(x => x.OrderItems, y => y.Ignore())
                .ForMember(x => x.GameLocalizations, y => y.ResolveUsing(
                    z => new List<GameLocalization>
                    {
                        new GameLocalization
                        {
                            LanguageId = Constants.EnglishLanguageId,
                            Name = z.ProductName,
                            Description = z.ProductName + "description here. "
                        }
                    }
                    ));
        }

        private static void InitEntitiesWithModelsMapping()
        {
            Mapper.CreateMap<Game, GameModel>();
            Mapper.CreateMap<GameModel, Game>()
                .ForMember(g => g.GameId, d => d.Ignore())
                .ForMember(g => g.Comments, d => d.Ignore())
                .ForMember(g => g.Genres, d => d.Ignore())
                .ForMember(g => g.PlatformTypes, d => d.Ignore())
                .ForMember(g => g.OrderItems, d => d.Ignore())
                .ForMember(g => g.NorthwindId, d => d.Ignore());

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
                .ForMember(x => x.PublisherId, d => d.Ignore())
                .ForMember(s => s.Games, d => d.Ignore())
                .ForMember(x => x.NorthwindId, d => d.Ignore());

            Mapper.CreateMap<Basket, BasketModel>();
            Mapper.CreateMap<BasketModel, Basket>();

            Mapper.CreateMap<BasketItem, BasketItemModel>()
                .ForMember(x => x.Basket, y => y.Ignore());
            Mapper.CreateMap<BasketItemModel, BasketItem>()
                .ForMember(x => x.Basket, y => y.Ignore());

            Mapper.CreateMap<OrderItem, OrderItemModel>()
                .ForMember(x => x.Order, y => y.Ignore());
            Mapper.CreateMap<OrderItemModel, OrderItem>()
                .ForMember(x => x.Order, y => y.Ignore());

            Mapper.CreateMap<Order, OrderModel>();
            Mapper.CreateMap<OrderModel, Order>();

            Mapper.CreateMap<User, UserModel>();
            Mapper.CreateMap<UserModel, User>();

            Mapper.CreateMap<Role, RoleModel>();
            Mapper.CreateMap<RoleModel, Role>()
                .ForMember(x => x.Users, y => y.Ignore());

            Mapper.CreateMap<Language, LanguageModel>().ReverseMap();
            Mapper.CreateMap<GameLocalization, GameLocalizationModel>().ReverseMap();
            Mapper.CreateMap<GenreLocalization, GenreLocalizationModel>().ReverseMap();
            Mapper.CreateMap<PlatformTypeLocalization, PlatformTypeLocalizationModel>().ReverseMap();
            Mapper.CreateMap<PublisherLocalization, PublisherLocalizationModel>().ReverseMap();
        }

        private static void InitModelsWithModelsMapping()
        {
            Mapper.CreateMap<BasketItemModel, OrderItemModel>()
                .ForMember(x => x.OrderItemId, y => y.Ignore())
                .ForMember(x => x.OrderId, y => y.Ignore())
                .ForMember(x => x.Order, y => y.Ignore())
                .ForMember(x => x.NorthwindOrderId, y => y.Ignore())
                .ForMember(x => x.NorthwindProductId, y => y.Ignore());
            Mapper.CreateMap<OrderItemModel, BasketItemModel>()
                .ForMember(x => x.BasketItemId, y => y.Ignore())
                .ForMember(x => x.BasketId, y => y.Ignore())
                .ForMember(x => x.Basket, y => y.Ignore());

            Mapper.CreateMap<BasketModel, OrderModel>()
                .ForMember(x => x.OrderId, y => y.Ignore())
                .ForMember(x => x.OrderDate, y => y.Ignore())
                .ForMember(x => x.OrderItems,
                    y => y.ResolveUsing(m => Mapper.Map<IEnumerable<OrderItemModel>>(m.BasketItems)))
                .ForMember(x => x.OrderStatus, y => y.UseValue(OrderStatus.New))
                .ForMember(x => x.NorthwindId, y => y.Ignore());
            Mapper.CreateMap<OrderModel, BasketModel>()
                .ForMember(x => x.BasketId, y => y.Ignore())
                .ForMember(x => x.BasketItems,
                    y => y.ResolveUsing(m => Mapper.Map<IEnumerable<BasketItemModel>>(m.OrderItems)));

            Mapper.CreateMap<UserModel, CustomPrincipalSerializeModel>()
                .ForMember(x => x.Roles, y => y.ResolveUsing(z => z.Roles.Select(r => r.RoleName).ToArray()));

            Mapper.CreateMap<UserModel, LoginModel>();
        }

        private static void InitModelsWithViewModelsMapping()
        {
            Mapper.CreateMap<GameModel, GameViewModel>()
                .ForMember(x => x.SelectedPublisherId, y => y.Ignore())
                .ForMember(x => x.Publishers, y => y.Ignore())
                .ForMember(gameViewModel => gameViewModel.SelectedGenresIds,
                    gameModel => gameModel.ResolveUsing(model => model.Genres.Select(genre => genre.GenreId)))
                .ForMember(gameViewModel => gameViewModel.SelectedPlatformTypesIds,
                    gameModel =>
                        gameModel.ResolveUsing(
                            model => model.PlatformTypes.Select(platformType => platformType.PlatformTypeId)))
                .ForMember(x => x.Name, y => y.ResolveUsing(z =>
                {
                    GameLocalizationModel gameLocalizationModel = z.GameLocalizations
                        .FirstOrDefault(loc => String.Equals(loc.Language.Code,
                            CultureInfo.CurrentCulture.TwoLetterISOLanguageName,
                            StringComparison.CurrentCultureIgnoreCase));

                    GameLocalizationModel englishLocalizationModel = z.GameLocalizations
                        .FirstOrDefault(loc => String.Equals(loc.Language.Code,
                            Constants.EnglishLanguageCode,
                            StringComparison.CurrentCultureIgnoreCase));

                    return gameLocalizationModel != null
                        ? gameLocalizationModel.Name
                        : englishLocalizationModel.Name;
                }))
                .ForMember(x => x.Description, y => y.ResolveUsing(z =>
                {
                    GameLocalizationModel gameLocalizationModel = z.GameLocalizations
                        .FirstOrDefault(loc => String.Equals(loc.Language.Code,
                            CultureInfo.CurrentCulture.TwoLetterISOLanguageName,
                            StringComparison.CurrentCultureIgnoreCase));

                    GameLocalizationModel englishLocalizationModel = z.GameLocalizations
                        .FirstOrDefault(loc => String.Equals(loc.Language.Code,
                            Constants.EnglishLanguageCode,
                            StringComparison.CurrentCultureIgnoreCase));

                    return gameLocalizationModel != null
                        ? gameLocalizationModel.Description
                        : englishLocalizationModel.Description;
                }));

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
                .ForMember(x => x.IsReadonly, y => y.Ignore())
                .ForMember(x => x.NorthwindId, y => y.Ignore())
                .ForMember(x => x.ParentGenreId, y => y.Ignore())
                .ForMember(x => x.GenreLocalizations, y => y.Ignore());
            Mapper.CreateMap<GenreModel, GenreFilterViewModel>()
                .ForMember(x => x.IsSelected, y => y.Ignore())
                .ForMember(x => x.GenreId, y => y.MapFrom(z => z.GenreId))
                .ForMember(x => x.Name, y => y.MapFrom(z =>
                    z.GenreLocalizations.First(x =>
                        String.Equals(x.Language.Code,
                                      CultureInfo.CurrentCulture.TwoLetterISOLanguageName,
                                      StringComparison.CurrentCultureIgnoreCase))
                    .Name));

            Mapper.CreateMap<PublisherModel, PublisherFilterViewModel>()
                .ForMember(x => x.IsSelected, y => y.Ignore())
                .ForMember(x => x.PublisherId, y => y.MapFrom(z => z.PublisherId))
                .ForMember(x => x.CompanyName, y => y.MapFrom(z =>
                    z.PublisherLocalizations.First(x =>
                        String.Equals(x.Language.Code,
                                      CultureInfo.CurrentCulture.TwoLetterISOLanguageName,
                                      StringComparison.CurrentCultureIgnoreCase))
                    .CompanyName));

            Mapper.CreateMap<PlatformTypeFilterViewModel, PlatformTypeModel>()
                .ForMember(x => x.IsReadonly, y => y.Ignore())
                .ForMember(x => x.PlatformTypeLocalizations, y => y.Ignore());
            Mapper.CreateMap<PlatformTypeModel, PlatformTypeFilterViewModel>()
                .ForMember(x => x.IsSelected, y => y.Ignore())
                .ForMember(x => x.PlatformTypeId, y => y.MapFrom(z => z.PlatformTypeId))
                .ForMember(x => x.Type, y => y.ResolveUsing(z =>
                    z.PlatformTypeLocalizations.First(x =>
                        String.Equals(x.Language.Code,
                                      CultureInfo.CurrentCulture.TwoLetterISOLanguageName,
                                      StringComparison.CurrentCultureIgnoreCase))
                    .Type));

            Mapper.CreateMap<PaginationViewModel, PaginationModel>();
            Mapper.CreateMap<PaginationModel, PaginationViewModel>();

            Mapper.CreateMap<PublisherModel, PublisherViewModel>()
                .ForMember(x => x.CompanyName, y => y.ResolveUsing(z =>
                {
                    PublisherLocalizationModel platformTypeLocalizationModel = z.PublisherLocalizations
                        .FirstOrDefault(loc => String.Equals(loc.Language.Code,
                            CultureInfo.CurrentCulture.TwoLetterISOLanguageName,
                            StringComparison.CurrentCultureIgnoreCase));

                    PublisherLocalizationModel englishLocalizationModel = z.PublisherLocalizations
                        .FirstOrDefault(loc => String.Equals(loc.Language.Code,
                            Constants.EnglishLanguageCode,
                            StringComparison.CurrentCultureIgnoreCase));

                    return platformTypeLocalizationModel != null
                        ? platformTypeLocalizationModel.CompanyName 
                        : englishLocalizationModel.CompanyName;
                }))
                .ForMember(x => x.Description, y => y.ResolveUsing(z =>
                {
                    PublisherLocalizationModel platformTypeLocalizationModel = z.PublisherLocalizations
                        .FirstOrDefault(loc => String.Equals(loc.Language.Code,
                            CultureInfo.CurrentCulture.TwoLetterISOLanguageName,
                            StringComparison.CurrentCultureIgnoreCase));

                    PublisherLocalizationModel englishLocalizationModel = z.PublisherLocalizations
                        .FirstOrDefault(loc => String.Equals(loc.Language.Code,
                            Constants.EnglishLanguageCode,
                            StringComparison.CurrentCultureIgnoreCase));

                    return platformTypeLocalizationModel != null
                        ? platformTypeLocalizationModel.Description
                        : englishLocalizationModel.Description;
                }));

            Mapper.CreateMap<VisaInfoModel, VisaInfoViewModel>();
            Mapper.CreateMap<VisaInfoViewModel, VisaInfoModel>();

            Mapper.CreateMap<BankInfoModel, BankInfoViewModel>();
            Mapper.CreateMap<BankInfoViewModel, BankInfoModel>();

            Mapper.CreateMap<TerminalInfoModel, TerminalInfoViewModel>();
            Mapper.CreateMap<TerminalInfoViewModel, TerminalInfoModel>();

            Mapper.CreateMap<PaymentInfoModel, PaymentInfoViewModel>();
            Mapper.CreateMap<PaymentInfoViewModel, PaymentInfoModel>();

            Mapper.CreateMap<OrderModel, OrderViewModel>();
            Mapper.CreateMap<OrderViewModel, OrderModel>()
                .ForMember(x => x.OrderItems, y => y.Ignore());

            Mapper.CreateMap<LoginModel, LoginViewModel>();
            Mapper.CreateMap<LoginViewModel, LoginModel>();

            Mapper.CreateMap<RegistrationViewModel, UserModel>()
                .ForMember(x => x.IsDisabled, y => y.Ignore())
                .ForMember(x => x.UserId, y => y.Ignore())
                .ForMember(x => x.CreateDate, y => y.Ignore())
                .ForMember(x => x.Roles, y => y.Ignore())
                .ForMember(x => x.IsReadonly, y => y.Ignore());

            Mapper.CreateMap<UserModel, UserViewModel>()
                .ForMember(x => x.PasswordConfirm, y => y.MapFrom(z => z.Password))
                .ForMember(x => x.SelectedRoles, y => y.ResolveUsing(z => z.Roles.Select(r => r.RoleId)))
                .ForMember(x => x.AllRoles, y => y.Ignore());

            Mapper.CreateMap<UserViewModel, UserModel>()
                .ForMember(x => x.Roles,
                    y => y.ResolveUsing(model => model.SelectedRoles
                        .Select(id => new RoleModel { RoleId = id })));

            Mapper.CreateMap<RoleModel, RoleViewModel>();
            Mapper.CreateMap<RoleViewModel, RoleModel>();

            Mapper.CreateMap<GenreModel, GenreViewModel>()
                .ForMember(x => x.AllGenres, y => y.Ignore())
                .ForMember(x => x.Name, y => y.ResolveUsing(z =>
                {
                    GenreLocalizationModel genreLocalizationModel = z.GenreLocalizations
                        .FirstOrDefault(loc => String.Equals(loc.Language.Code,
                            CultureInfo.CurrentCulture.TwoLetterISOLanguageName,
                            StringComparison.CurrentCultureIgnoreCase));

                    GenreLocalizationModel englishLocalizationModel = z.GenreLocalizations
                        .FirstOrDefault(loc => String.Equals(loc.Language.Code,
                            Constants.EnglishLanguageCode,
                            StringComparison.CurrentCultureIgnoreCase));

                    return genreLocalizationModel != null ? genreLocalizationModel.Name : englishLocalizationModel.Name;
                }));

            Mapper.CreateMap<CommentModel, CommentViewModel>();
            Mapper.CreateMap<CommentViewModel, CommentModel>();

            Mapper.CreateMap<LanguageModel, LanguageViewModel>().ReverseMap();
            Mapper.CreateMap<GameLocalizationModel, GameLocalizationViewModel>().ReverseMap();
            Mapper.CreateMap<GenreLocalizationModel, GenreLocalizationViewModel>().ReverseMap();
            Mapper.CreateMap<PlatformTypeLocalizationModel, PlatformTypeLocalizationViewModel>().ReverseMap();
            Mapper.CreateMap<PublisherLocalizationModel, PublisherLocalizationViewModel>().ReverseMap();

            Mapper.CreateMap<GameModel, GameAddUpdateViewModel>()
                .ForMember(x => x.SelectedGenresIds, y => y.ResolveUsing(z => z.Genres.Select(g => g.GenreId)))
                .ForMember(x => x.SelectedPlatformTypesIds, y => y.ResolveUsing(z => z.PlatformTypes.Select(p => p.PlatformTypeId)))
                .ForMember(x => x.SelectedPublisherId, y => y.ResolveUsing(z => z.PublisherId))
                .ForMember(x => x.Publishers, y => y.Ignore())
                .ForMember(x => x.Genres, y => y.Ignore())
                .ForMember(x => x.PlatformTypes, y => y.Ignore());

            Mapper.CreateMap<GameAddUpdateViewModel, GameModel>()
                .ForMember(x => x.OrderItems, y => y.Ignore())
                .ForMember(x => x.Comments, y => y.Ignore())
                .ForMember(x => x.BasketItems, y => y.Ignore())
                .ForMember(x => x.Publisher, y => y.Ignore())
                .ForMember(gameModel => gameModel.Genres,
                    gameViewModel => gameViewModel.ResolveUsing(model => model.SelectedGenresIds
                        .Select(id => new GenreModel { GenreId = id })))
                .ForMember(gameModel => gameModel.PlatformTypes,
                    gameViewModel => gameViewModel.ResolveUsing(model => model.SelectedPlatformTypesIds
                        .Select(platformTypeId => new PlatformTypeModel { PlatformTypeId = platformTypeId })))
                .ForMember(gameModel => gameModel.PublisherId,
                    gameViewModel => gameViewModel.MapFrom(g => g.SelectedPublisherId));

            Mapper.CreateMap<GenreModel, GenreAddUpdateViewModel>()
                .ForMember(x => x.AllGenres, y => y.Ignore());
            Mapper.CreateMap<GenreAddUpdateViewModel, GenreModel>();

            Mapper.CreateMap<PublisherModel, PublisherAddUpdateViewModel>();
            Mapper.CreateMap<PublisherAddUpdateViewModel, PublisherModel>();

            Mapper.CreateMap<BasketModel, BasketViewModel>();

            Mapper.CreateMap<BasketItemModel, BasketItemViewModel>();

            Mapper.CreateMap<OrderModel, OrderViewModel>();

            Mapper.CreateMap<OrderItemModel, OrderItemViewModel>();

            Mapper.CreateMap<PlatformTypeModel, PlatformTypeViewModel>()
                .ForMember(x => x.Type, y => y.ResolveUsing(z =>
            {
                PlatformTypeLocalizationModel platformTypeLocalizationModel = z.PlatformTypeLocalizations
                    .FirstOrDefault(loc => String.Equals(loc.Language.Code,
                        CultureInfo.CurrentCulture.TwoLetterISOLanguageName,
                        StringComparison.CurrentCultureIgnoreCase));

                PlatformTypeLocalizationModel englishLocalizationModel = z.PlatformTypeLocalizations
                    .FirstOrDefault(loc => String.Equals(loc.Language.Code,
                        Constants.EnglishLanguageCode,
                        StringComparison.CurrentCultureIgnoreCase));

                return platformTypeLocalizationModel != null ? platformTypeLocalizationModel.Type : englishLocalizationModel.Type;
            }));
        }
    }
}