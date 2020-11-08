using Arizona.Data;
using Arizona.Data.Entities;
using Arizona.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;

namespace Arizona.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ProductsController: Controller
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IArizonaRepository _arizonaRepository;
        private readonly IMapper _mapper;

        public ProductsController(ILogger<ProductsController> logger, IArizonaRepository arizonaRepository, IMapper mapper)
        {
            _logger = logger;
            _arizonaRepository = arizonaRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult<IEnumerable<Product>> Get()
        {
            var results = _arizonaRepository.GetAllProducts();
            try
            {
                return Ok(_mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(results));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get products: {ex}");
                return BadRequest("Failed to get products");
            }
        }

        [HttpGet("{id:int}")]
        public ActionResult<IEnumerable<Product>> Get(int id)
        {
            try
            {
                var product = _arizonaRepository.GetProductById(id);

                if (product != null)
                {
                    return Ok(_mapper.Map<Product, ProductViewModel>(product));
                }
                else 
                {
                    return NotFound();
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get product with id {id}: {ex}");
                return BadRequest("Failed to get product with id {id}");
            }
        }
    }
}
