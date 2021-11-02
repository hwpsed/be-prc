using System.Collections.Generic;
using AutoMapper;
using LaundryApp.Model;
using LaundryApp.Model.ViewEntity;
using LaundryApp.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace LaundryApp.Controllers
{
    public class OrderController : BaseApiController
    {
        private IOrderRepository _strepo;
        private IAccountRepository _accrepo;
        private readonly IMapper _mapper;

        public OrderController(IOrderRepository strepo, IMapper mapper)
        {
            _strepo = strepo;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetOrders()
        {
            var objList = _strepo.GetOrders();

            var objDtos = new List<ViewOrder>();

            foreach (var obj in objList)
            {
                objDtos.Add(_mapper.Map<ViewOrder>(obj));
            }

            return Ok(objDtos);
        }
        [HttpGet("ByAccountId/{accountId:int}")]
        public IActionResult GetOrderByAccountId (int accountId)
        {
            var objList = _strepo.GetOrdersByAccountId(accountId);
            var objDtos = new List<ViewOrder>();
            foreach (var obj in objList)
            {
                objDtos.Add(_mapper.Map<ViewOrder>(obj));
            }

            return Ok(objDtos);
        }

        [HttpGet("{orderId:int}", Name = "GetOrder")]
        public IActionResult GetOrder(int orderId)
        {
            var obj = _strepo.GetOrder(orderId);
            if (obj == null)
            {
                return NotFound();
            }
            var objDto = _mapper.Map<ViewOrder>(obj);

            return Ok(objDto);
        }

        [HttpPost]
        public IActionResult CreateOrder([FromBody] ViewOrder viewOrder)
        {
            if (viewOrder == null)
            {
                return BadRequest(ModelState);
            }

            if (_strepo.OrderExist(viewOrder.OrderId))
            {
                ModelState.AddModelError("", "Order Exsits!");
                return StatusCode(404, ModelState);
            }

            var OrderObj = _mapper.Map<Order>(viewOrder);

            if (!_strepo.CreateOrder(OrderObj))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {OrderObj.OrderId}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetOrder", new { orderId = OrderObj.OrderId }, OrderObj);
        }

        [HttpPatch("{orderId:int}", Name = "GetOrder")]
        public IActionResult UpdateOrder(int orderId, [FromBody] ViewOrder viewOrder)
        {
            if (viewOrder == null || orderId != viewOrder.OrderId)
            {
                return BadRequest(ModelState);
            }

            var OrderObj = _mapper.Map<Order>(viewOrder);

            if (!_strepo.UpdateOrder(OrderObj))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record {OrderObj.OrderId}");
                return StatusCode(500, ModelState);
            }

            return Ok();
        }

        [HttpDelete("{orderId:int}", Name = "GetOrder")]
        public IActionResult DeleteOrder(int orderId)
        {

            if (!_strepo.OrderExist(orderId))
            {
                return NotFound();
            }

            var OrderObj = _strepo.GetOrder(orderId);

            if (!_strepo.DeleteOrder(OrderObj))
            {
                ModelState.AddModelError("", $"Something went wrong when Deleting the record {OrderObj.OrderId}");
                return StatusCode(500, ModelState);
            }

            return Ok();
        }
    }
}
