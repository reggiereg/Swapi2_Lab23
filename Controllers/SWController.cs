using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Lab23_Swapi2.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab23_Swapi2.Controllers
{
    public class SWController : Controller
    {

        //returns both forms for looking up by people id or planet id
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        //one IActionResult to call them all (i.e. planet or person
        [HttpPost]
        //takes in parameters of id and the lookUp type i.e. person on planet
        public async Task<IActionResult> DisplayById(string lookUp, int id)
        {
            //configure HTTP for base url
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://swapi.co/api/");

            //if form for people is filled out and submitted
            if(lookUp == "people")
            {
                var response = await client.GetAsync($"{lookUp}/{id}/");
                var people = await response.Content.ReadAsAsync<SWPeople>();
                ViewBag.Message = "Results for Star Wars Characters";
                ViewBag.option = people;
                return View("PeopleResults", people);
            }

            //if form for planets is filleed out and submitted
            else if (lookUp == "planets")
            {
                var response = await client.GetAsync($"{lookUp}/{id}/");
                //Need to add newget package Microsoft.aspnet.webapi.client
                var planets = await response.Content.ReadAsAsync<SWPlanets>();
                ViewBag.Message = "Results for Star Wars Planets";
                ViewBag.option = planets;
                return View("PlanetResults", planets);
            }
            else
            {
                return View("Index");
            }

        }
    }
}