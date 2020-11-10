using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http.Description;
using BussinessLayer.DTO;
using BussinessLayer.UnitOfWork;
using BussinessLayer.Utils;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BagShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class OrdersController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;

        public OrdersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/<OrdersController>
        //[HttpGet]
        //public IEnumerable<Order> Get()
        //{
        //    return _unitOfWork.Orders.getAll();
        //}

        //// GET api/<OrdersController>/5
        //[HttpGet("{id}")]
        //[ResponseType(typeof(Order))]
        //public IActionResult Get(int id)
        //{
        //    var returnValue = _unitOfWork.Orders.getByID(id);
        //    if (returnValue == null) return NotFound();
        //    return Ok(returnValue);
        //}

        // POST api/<OrdersController>
        [HttpPost]
        public IActionResult Post([FromBody] Order orderInfo)
        {
            orderInfo.orderID = GeneratingHelpers.generateID();
            orderInfo.orderDate = DateTime.Now;
            try
            {
                _unitOfWork.Orders.addOrder(orderInfo);
                return StatusCode(StatusCodes.Status200OK);
            }catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
