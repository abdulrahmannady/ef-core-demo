﻿using EFDataAccessLibrary.DataAccess;
using EFDataAccessLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace EFDemoWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly PeopleContext _db;

        public IndexModel(ILogger<IndexModel> logger, PeopleContext db)
        {
            _logger = logger;
            _db = db;
        }

        public void OnGet()
        {
            LoadSampleData();

            var people = _db.People
                .Include(a => a.Addresses)
                .Include(e => e.EmailAddresses)
                .ToList();
        }

        private void LoadSampleData()
        {
            if (_db.People.Count() == 0)
            {
                //Load temp data
                string file = System.IO.File.ReadAllText("generated.json");
                // deserlize it into array of people model
                var people = JsonSerializer.Deserialize<List<Person>>(file);
                // insert query to add people
                _db.AddRange(people);
                _db.SaveChanges();
            }
        }
    }
}
