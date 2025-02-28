using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TicketApp.BL.Services;
using TicketApp.Model.Entities;
using TicketBlazorApp.Data;

namespace TicketBlazorApp.Controllers
{
    public class BusesController : Controller
    {
        readonly IBusesService _service;
        public BusesController(IBusesService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAll());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var buses = await _service.Get(id.Value);
            return buses == null ? NotFound() : View(buses);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BusID,BusNumber,BusType,TotalSeats,OperatorName")] Bus buses)
        {
            if (ModelState.IsValid)
            {
                await _service.Create(buses);
                return RedirectToAction(nameof(Index));
            }
            return View(buses);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var bus = await _service.Get(id.Value);
            return bus == null ? NotFound() : View(bus);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("BusID,BusNumber,BusType,TotalSeats,OperatorName")] Bus bus)
        {
            if (id == null || bus == null || id != bus.BusID) return NotFound();

            if (ModelState.IsValid)
            {
                var res = await _service.Edit(id.Value, bus);
                return bus == null ? NotFound() : RedirectToAction(nameof(Index));
            }
            return View(bus);
        }

        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null) return NotFound();
            var bus = await _service.Get(id.Value);
            return bus == null ? NotFound() : View(bus);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {

            if (id == null) return NotFound();

            var res = await _service.DeleteConfirmed(id.Value);
            return res ? RedirectToAction(nameof(Index)) : NotFound();
        }

        private bool BusesExists(int id)=> _service.BusExists(id);
    }
}
