using DedtechChallenge.Models;
using DedtechChallenge.Services.Interfaces;
using DedtechChallenge.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DedtechChallenge.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAll([FromQuery] int pageIndex, int pageSize)
        {
            var paginatedProducts = await PaginatedList<Product>.ToPagedListAsync(_productService.GetAll(), pageIndex, pageSize);

            return Ok(paginatedProducts);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);

            return Ok(product);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromBody] Product product)
        {
            var productCreated = await _productService.CreateAsync(product);

            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] Product product)
        {
            product.Id = id;

            var productUpdated = await _productService.UpdateAsync(product);

            return Ok(productUpdated);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteAsync(id);

            return NoContent();
        }
    }
}
