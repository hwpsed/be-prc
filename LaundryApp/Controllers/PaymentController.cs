using System.Collections.Generic;
using AutoMapper;
using LaundryApp.Model;
using LaundryApp.Model.ViewEntity;
using LaundryApp.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace LaundryApp.Controllers
{
    public class PaymentController : BaseApiController
    {
        private IPaymentRepository _strepo;
        private readonly IMapper _mapper;

        public PaymentController(IPaymentRepository strepo, IMapper mapper)
        {
            _strepo = strepo;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetPayments()
        {
            var objList = _strepo.GetPayments();

            var objDtos = new List<ViewPayment>();

            foreach (var obj in objList)
            {
                objDtos.Add(_mapper.Map<ViewPayment>(obj));
            }

            return Ok(objDtos);
        }

        [HttpGet("{paymentId:int}", Name = "GetPayment")]
        public IActionResult GetPayment(int paymentId)
        {
            var obj = _strepo.GetPayment(paymentId);
            if (obj == null)
            {
                return NotFound();
            }
            var objDto = _mapper.Map<ViewPayment>(obj);

            return Ok(objDto);
        }

        [HttpPost]
        public IActionResult CreatePayment([FromBody] ViewPayment viewPayment)
        {
            if (viewPayment == null)
            {
                return BadRequest(ModelState);
            }

            if (_strepo.PaymentExist(viewPayment.PaymentId))
            {
                ModelState.AddModelError("", "Payment Exsits!");
                return StatusCode(404, ModelState);
            }

            var PaymentObj = _mapper.Map<Payment>(viewPayment);

            if (!_strepo.CreatePayment(PaymentObj))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {PaymentObj.PaymentId}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetPayment", new { paymentId = PaymentObj.PaymentId }, PaymentObj);
        }

        [HttpPatch("{paymentId:int}", Name = "GetPayment")]
        public IActionResult UpdatePayment(int paymentId, [FromBody] ViewPayment viewPayment)
        {
            if (viewPayment == null || paymentId != viewPayment.PaymentId)
            {
                return BadRequest(ModelState);
            }

            var PaymentObj = _mapper.Map<Payment>(viewPayment);

            if (!_strepo.UpdatePayment(PaymentObj))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record {PaymentObj.PaymentId}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{paymentId:int}", Name = "GetPayment")]
        public IActionResult DeletePayment(int paymentId)
        {

            if (!_strepo.PaymentExist(paymentId))
            {
                return NotFound();
            }

            var PaymentObj = _strepo.GetPayment(paymentId);

            if (!_strepo.DeletePayment(PaymentObj))
            {
                ModelState.AddModelError("", $"Something went wrong when Deleting the record {PaymentObj.PaymentId}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
