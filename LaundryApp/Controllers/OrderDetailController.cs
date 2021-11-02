using System.Collections.Generic;
using AutoMapper;
using LaundryApp.Model;
using LaundryApp.Model.ViewEntity;
using LaundryApp.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace LaundryApp.Controllers
{
    public class OrderDetailController : BaseApiController
    {
        private IOrderDetailRepository _OrderDetailrepo;
        private readonly IMapper _mapper;

        public OrderDetailController(IOrderDetailRepository OrderDetailrepo, IMapper mapper)
        {
            _OrderDetailrepo = OrderDetailrepo;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetOrderDetails()
        {
            var objList = _OrderDetailrepo.GetOrderDetails();


            var objDtos = new List<ViewOrderDetail>();

            foreach (var obj in objList)
            {
                objDtos.Add(_mapper.Map<ViewOrderDetail>(obj));
            }

            return Ok(objDtos);
        }

        [HttpGet("{orderDetailId:int}", Name = "GetOrderDetail")]
        public IActionResult GetOrderDetail(int orderDetailId)
        {
            var obj = _OrderDetailrepo.GetOrderDetail(orderDetailId);
            if (obj == null)
            {
                return NotFound();
            }
            var objDto = _mapper.Map<ViewOrderDetail>(obj);

            return Ok(objDto);
        }

        [HttpPost]
        public IActionResult CreateOrderDetail([FromBody] ViewOrderDetail viewOrderDetail)
        {
            if (viewOrderDetail == null)
            {
                return BadRequest(ModelState);
            }

            if (_OrderDetailrepo.OrderDetailExist(viewOrderDetail.OrderDetailId))
            {
                ModelState.AddModelError("", "OrderDetail Type Exsits!");
                return StatusCode(404, ModelState);
            }

            var OrderDetailObj = _mapper.Map<OrderDetail>(viewOrderDetail);

            if (!_OrderDetailrepo.CreateOrderDetail(OrderDetailObj))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {OrderDetailObj.OrderDetailId}");
                return StatusCode(500, ModelState);
            }

            return Ok(viewOrderDetail);
        }

        [HttpPut("{orderDetailId:int}", Name = "GetOrderDetail")]
        public IActionResult UpdateOrderDetail(int orderDetailId, [FromBody] ViewOrderDetail viewOrderDetail)
        {
            if (viewOrderDetail == null || orderDetailId != viewOrderDetail.OrderDetailId)
            {
                return BadRequest(ModelState);
            }

            var OrderDetailObj = _mapper.Map<OrderDetail>(viewOrderDetail);

            if (!_OrderDetailrepo.UpdateOrderDetail(OrderDetailObj))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record {OrderDetailObj.OrderDetailId}");
                return StatusCode(500, ModelState);
            }

            return Ok();
        }

        [HttpDelete("{orderDetailId:int}", Name = "GetOrderDetail")]
        public IActionResult DeleteOrderDetail(int orderDetailId)
        {

            if (!_OrderDetailrepo.OrderDetailExist(orderDetailId))
            {
                return NotFound();
            }

            var OrderDetailObj = _OrderDetailrepo.GetOrderDetail(orderDetailId);

            if (!_OrderDetailrepo.DeleteOrderDetail(OrderDetailObj))
            {
                ModelState.AddModelError("", $"Something went wrong when Deleting the record {OrderDetailObj.OrderDetailId}");
                return StatusCode(500, ModelState);
            }

            return Ok();
        }
    }
}
