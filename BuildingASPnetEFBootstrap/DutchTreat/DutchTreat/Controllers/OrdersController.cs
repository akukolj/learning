using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DutchTreat.Data.Entities;
using DutchTreat.Data.Interfaces;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DutchTreat.Controllers
{
    [Route("api/[Controller]")]
    public class OrdersController :Controller
    {
        private readonly IDutchRepository _repository;
        private readonly ILogger<OrdersController> _logger;
        private readonly IMapper _mapper;

        public OrdersController(IDutchRepository repository, ILogger<OrdersController> logger,IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                return Ok(_repository.GetAllOrders());
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to get orders {e}");
                return BadRequest("Failed to get orders");
            }
        }

        [HttpGet("{id:int}")]
        public ActionResult Get(int id)
        {
            try
            {
                var order = _repository.GetOrderById(id);
                return Ok(_mapper.Map<Order,OrderViewModel>(order));
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to get orders {e}");
                return BadRequest("Failed to get orders");
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]OrderViewModel order)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newOrder = Mapper.Map<OrderViewModel, Order>(order);
                    _repository.AddEntity(newOrder);
                    if (_repository.SaveAll())
                    {
                        return Created($"/api/orders/{newOrder.Id}", order);
                    }
                        
                }
                else
                {
                    return BadRequest("failed to save");
                }
             
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to add new order{e}");
            }
            return BadRequest("failed to save");
        }
    }
}
