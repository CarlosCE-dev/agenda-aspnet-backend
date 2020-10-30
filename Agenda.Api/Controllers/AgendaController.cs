using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using Agenda.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Api.Controllers
{
    [Route("api/[controller]")]
    public class AgendaController : ControllerBase
    {
        private readonly Context _context;
         
        public AgendaController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> Get()
        {
            var agendas = await _context.Agendas.OrderBy( a => a.status ).ThenBy( a => a.id ).ToListAsync();
            return Ok(agendas);
        }

        [HttpGet]
        [Route("get/{id:int}")]
        public async Task<IActionResult> GetAgenda(int id)
        {
            var agenda = await _context.Agendas.FirstOrDefaultAsync(a => a.id == id);
            return Ok(agenda);
        }

        [HttpPost]
        [Route("delete/{id:int}")]
        public async Task<IActionResult> DeleteAgenda(int id)
        {
            var agenda = await _context.Agendas.FirstOrDefaultAsync(a => a.id == id);
            if(agenda == null)
                return BadRequest();

            _context.Agendas.Remove(agenda);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody]Models.Agenda agenda)
        {
            try
            {
                await _context.Agendas.AddAsync(agenda);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return BadRequest();
            }
            return Ok(agenda);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="agenda"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("edit")]
        public async Task<IActionResult> Edit([FromBody]Models.Agenda agenda)
        {
            var agendaToUpdate = await _context.Agendas.FirstOrDefaultAsync(a => a.id == agenda.id);
            _context.Entry(agendaToUpdate).CurrentValues.SetValues(agenda);

            await _context.SaveChangesAsync();

            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Agenda"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("status")]
        public async Task<IActionResult> CambiarEstatus([FromBody]Models.Agenda agenda)
        {
            var agendaToUpdate = await _context.Agendas.FirstOrDefaultAsync(a => a.id == agenda.id);
            agendaToUpdate.status = agenda.status;
            _context.Agendas.Update(agenda);
            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}
