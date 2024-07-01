using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SchedulerCrudRazor.Models;
using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchedulerCrudRazor.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;


        public required List<ScheduleEvent> DataSource { get; set; }
        private ScheduleDataContext _context;



        public IndexModel(ScheduleDataContext _context)
        {
            this._context = _context;
            DataSource = _context.ScheduleEventsData.ToList();
        }
        public void OnGet()
        {

        }

        public IActionResult GetData()  // Here we return data to Schedule
        {
            var data = _context.ScheduleEventsData.ToList();
            return new JsonResult(data);
        }

        public IActionResult OnPostSave([FromBody] EventData Data)
        {
            foreach (var app in Data.eventToAdd)
            {
                app.Id = 0;
                _context.ScheduleEventsData.Add(app);
            }
            _context.SaveChanges();
            return GetData();
        }

        public IActionResult OnPostUpdate([FromBody] EventData Data)
        {
            foreach (var app in Data.eventToAdd)
            {
                var entity = _context.ScheduleEventsData.Find(app.Id);
                if (entity != null)
                {
                    _context.Entry(entity).CurrentValues.SetValues(app);
                }
            }
            _context.SaveChanges();
            return GetData();
        }

        public IActionResult OnPostDelete([FromBody] EventData Data)
        {
            foreach (var app in Data.eventToAdd)
            {
                var entity = _context.ScheduleEventsData.Find(app.Id);
                if (entity != null)
                {
                    _context.ScheduleEventsData.Remove(entity);
                }
            }
            _context.SaveChanges();
            return GetData();

        }

        public class EventData
        {
            public List<ScheduleEvent> eventToAdd { get; set; }
        }

    }
}
