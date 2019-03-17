﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Entities
{
    public static class CityInfoContextExtensions
    {
        public static void EnsureSeedDataForDB(this CityInfoContext cityInfoContext)
        {
            if (cityInfoContext.Cities.Any())
            {
                return;
            }

            // init dummy data
            List<City> Cities = new List<City>()
            {
                new City()
                {
                     Name = "New York City",
                     Description = "The one with that big park.",
                     PointOfInterest = new List<PointOfInterest>()
                     {
                         new PointOfInterest() {
                             Name = "Central Park",
                             Description = "The most visited urban park in the United States." },
                          new PointOfInterest() {
                             Name = "Empire State Building",
                             Description = "A 102-story skyscraper located in Midtown Manhattan." },
                     }
                },
                new City()
                {
                    Name = "Antwerp",
                    Description = "The one with the cathedral that was never really finished.",
                    PointOfInterest = new List<PointOfInterest>()
                     {
                         new PointOfInterest() {
                             Name = "Cathedral of Our Lady",
                             Description = "A Gothic style cathedral, conceived by architects Jan and Pieter Appelmans." },
                          new PointOfInterest() {
                              Name = "Antwerp Central Station",
                             Description = "The the finest example of railway architecture in Belgium." },
                     }
                },
                new City()
                {
                    Name = "Paris",
                    Description = "The one with that big tower.",
                    PointOfInterest = new List<PointOfInterest>()
                     {
                         new PointOfInterest() {
                              Name = "Eiffel Tower",
                             Description = "A wrought iron lattice tower on the Champ de Mars, named after engineer Gustave Eiffel." },
                          new PointOfInterest() {
                              Name = "The Louvre",
                             Description = "The world's largest museum." },
                     }
                }
            };

            cityInfoContext.Cities.AddRange(Cities);
            cityInfoContext.SaveChanges();
        }
    }
}
