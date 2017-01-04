using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using aspnet_core.Data;
using aspnet_core.Models;

namespace aspnet_core.API
{
    [Produces("application/json")]
    [Route("api/GoodsApi")]
    public class GoodsApiController : Controller
    {
        private readonly InventoryContext _context;

        public GoodsApiController(InventoryContext context)
        {
            _context = context;
        }

        // GET: api/GoodsApi
        [HttpGet]
        public IEnumerable<Good> GetGoods()
        {
            return _context.Goods;
        }

        // GET: api/GoodsApi/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGood([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Good good = await _context.Goods.SingleOrDefaultAsync(m => m.Id == id);

            if (good == null)
            {
                return NotFound();
            }

            return Ok(good);
        }

        // PUT: api/GoodsApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGood([FromRoute] int id, [FromBody] Good good)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != good.Id)
            {
                return BadRequest();
            }

            _context.Entry(good).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GoodExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/GoodsApi
        [HttpPost]
        public async Task<IActionResult> PostGood([FromBody] Good good)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Goods.Add(good);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (GoodExists(good.Id))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetGood", new { id = good.Id }, good);
        }

        // DELETE: api/GoodsApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGood([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Good good = await _context.Goods.SingleOrDefaultAsync(m => m.Id == id);
            if (good == null)
            {
                return NotFound();
            }

            _context.Goods.Remove(good);
            await _context.SaveChangesAsync();

            return Ok(good);
        }

        private bool GoodExists(int id)
        {
            return _context.Goods.Any(e => e.Id == id);
        }
    }
}