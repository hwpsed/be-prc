using System.Collections.Generic;
using AutoMapper;
using LaundryApp.Model;
using LaundryApp.Model.ViewEntity;
using LaundryApp.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace LaundryApp.Controllers
{
    public class ProductController : BaseApiController
    {
        private IProductRepository _strepo;
        private readonly IMapper _mapper;

        public ProductController(IProductRepository strepo, IMapper mapper)
        {
            _strepo = strepo;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            var objList = _strepo.GetProducts();

            var objDtos = new List<ViewProduct>();

            foreach (var obj in objList)
            {
                objDtos.Add(_mapper.Map<ViewProduct>(obj));
            }

            return Ok(objDtos);
        }

        [HttpGet("{ProductId:int}", Name = "GetProduct")]
        public IActionResult GetProductById(int ProductId)
        {
            var obj = _strepo.GetProduct(ProductId);
            if (obj == null)
            {
                return NotFound();
            }
            var objDto = _mapper.Map<ViewProduct>(obj);

            return Ok(objDto);
        }



        [HttpPost]
        public IActionResult CreateProduct([FromBody] ViewProduct viewProduct)
        {
            if (viewProduct == null)
            {
                return BadRequest(ModelState);
            }

            if (_strepo.ProductExist(viewProduct.ProductId))
            {
                ModelState.AddModelError("", "Product Exsits!");
                return StatusCode(404, ModelState);
            }

            var ProductObj = _mapper.Map<Product>(viewProduct);

            if (!_strepo.CreateProduct(ProductObj))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {ProductObj.ProductId}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetProduct", new { ProductId = ProductObj.ProductId }, ProductObj);
        }

        [HttpPatch("{ProductId:int}", Name = "GetProduct")]
        public IActionResult UpdateProduct(int ProductId, [FromBody] ViewProduct viewProduct)
        {
            if (viewProduct == null || ProductId != viewProduct.ProductId)
            {
                return BadRequest(ModelState);
            }

            var ProductObj = _mapper.Map<Product>(viewProduct);

            if (!_strepo.UpdateProduct(ProductObj))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record {ProductObj.ProductId}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{ProductId:int}", Name = "GetProduct")]
        public IActionResult DeleteProduct(int ProductId)
        {

            if (!_strepo.ProductExist(ProductId))
            {
                return NotFound();
            }

            var ProductObj = _strepo.GetProduct(ProductId);

            if (!_strepo.DeleteProduct(ProductObj))
            {
                ModelState.AddModelError("", $"Something went wrong when Deleting the record {ProductObj.ProductId}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
