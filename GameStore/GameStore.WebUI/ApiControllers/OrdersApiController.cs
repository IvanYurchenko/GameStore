using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;
using GameStore.WebUI.Filters;
using GameStore.WebUI.Security;
using GameStore.WebUI.ViewModels;

namespace GameStore.WebUI.ApiControllers
{
    [ApiExceptionLoggingFilter]
    public class OrdersApiController : BaseApiController
    {
        private readonly IOrderService _orderService;

        public OrdersApiController(
            IOrderService orderService)
        {
            _orderService = orderService;
        }

        [ActionName(DefaultAction)]
        [CustomApiAuthorize(Roles = "Admin, Manager")]
        public HttpResponseMessage Get()
        {
            IEnumerable<OrderModel> orderModels = _orderService.GetAll();

            var orderViewModels = Mapper.Map<IEnumerable<OrderViewModel>>(orderModels);

            return Request.CreateResponse(HttpStatusCode.OK, orderViewModels);
        }

        [ActionName(DefaultAction)]
        [CustomApiAuthorize(Roles = "Admin, Manager")]
        public HttpResponseMessage Get(int id)
        {
            OrderModel orderModel = _orderService.GetModelById(id);

            if (orderModel == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            var orderViewModel = Mapper.Map<OrderViewModel>(orderModel);

            return Request.CreateResponse(HttpStatusCode.OK, orderViewModel);
        }

        [ActionName(DefaultAction)]
        [CustomApiAuthorize(Roles = "Admin, Manager")]
        public HttpResponseMessage Post(OrderViewModel orderViewModel)
        {
            if (ModelState.IsValid)
            {
                var orderModel = Mapper.Map<OrderModel>(orderViewModel);

                _orderService.Add(orderModel);

                return new HttpResponseMessage(HttpStatusCode.OK);
            }

            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [ActionName(DefaultAction)]
        [CustomApiAuthorize(Roles = "Admin, Manager")]
        public HttpResponseMessage Put(OrderViewModel orderViewModel)
        {
            if (ModelState.IsValid)
            {
                var orderModel = Mapper.Map<OrderModel>(orderViewModel);

                if (_orderService.GetModelById(orderModel.OrderId) == null)
                {
                    _orderService.Add(orderModel);
                }

                _orderService.Update(orderModel);

                return new HttpResponseMessage(HttpStatusCode.OK);
            }

            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [ActionName(DefaultAction)]
        [CustomApiAuthorize(Roles = "Admin, Manager")]
        public HttpResponseMessage Delete(int id)
        {
            OrderModel orderModel = _orderService.GetModelById(id);

            if (orderModel == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            _orderService.Remove(id);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
