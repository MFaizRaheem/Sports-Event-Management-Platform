using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using SEMP.Models;
using SEMP.Data;

namespace SEMP.Controllers
{
    public class EventController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int? eventId)
        {
            // Fetch all events
            var events = _context.Events.ToList();

            // If eventId is provided, filter events to include only that event
            var createdEvent = eventId.HasValue ? _context.Events.Find(eventId) : null;

            return View(createdEvent != null ? new List<Event> { createdEvent } : events);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Event @event)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(@event);
                    _context.SaveChanges();

                    // Redirect to Index with the ID of the created event
                    return RedirectToAction(nameof(Index), new { eventId = @event.Id });
                }
            }
            catch (Exception ex)
            {
                // Log the exception for troubleshooting
                Console.WriteLine($"Error saving changes: {ex.Message}");
                Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");
            }

            return View(@event);
        }

        [HttpGet]
        public IActionResult Update(int eventId)
        {
            // Retrieve the event to be updated
            var eventToUpdate = _context.Events.Find(eventId);

            if (eventToUpdate == null)
            {
                return NotFound(); // Return a 404 Not Found if the event is not found
            }

            return View(eventToUpdate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Event updatedEvent)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Update the event in the database
                    _context.Entry(updatedEvent).State = EntityState.Modified;
                    _context.SaveChanges();

                    // Redirect to Index after updating
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                // Log the exception for troubleshooting
                Console.WriteLine($"Error updating event: {ex.Message}");
                Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");
            }

            // If there is an error, return to the Update view with the model
            return View(updatedEvent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int eventId)
        {
            var eventToDelete = _context.Events.Find(eventId);

            if (eventToDelete == null)
            {
                return NotFound(); // Return a 404 Not Found if the event is not found
            }

            // Perform the actual deletion logic
            _context.Events.Remove(eventToDelete);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index)); // Redirect to the index page after deletion
        }




    }
}
