using EFDataAccessLibrary.DataAccess;
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
                .Where(x => ApprovedAge(x.Age)) // this is a valid c# code but won't be converted to T-SQL and work on sql server
                //.Where(x => x.Age >= 18 && x.Age <= 60) // this would work on sqls server
                .ToList();

            // if we use c# functionality the entity framework would fetch all data first and load it RAM then apply the filtration
            // the filtration should be applied on sql server so put extra care of doing it always check xprofiler
        }

        private bool ApprovedAge(int age)
        {
            return (age >= 18 && age <= 60);
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
