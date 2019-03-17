using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.Repositories
{
    public class CityInfoRepository: ICityInfoRepository
    {
        private CityInfoContext cityInfoContext;
        public CityInfoRepository(CityInfoContext context)
        {
            cityInfoContext = context;
        }

        public void AddPointOfInterest(int cityId, PointOfInterest pointOfInterest)
        {
            var city = this.GetCity(cityId);
            if (city == null)
            {
                throw new Exception("Not found");
            }

            city.PointOfInterest.Add(pointOfInterest);
            cityInfoContext.SaveChanges();
        }

        public IQueryable<City> GetCities()
        {
            return cityInfoContext.Cities.Include(c => c.PointOfInterest);
        }

        public City GetCity(int id)
        {
            return cityInfoContext.Cities.Include(c=>c.PointOfInterest).FirstOrDefault(c=>c.Id == id);
        }

        public PointOfInterest GetPointOfInterest(int cityId, int id)
        {
            return cityInfoContext.PointsOfInterest.FirstOrDefault(p => p.CityId == cityId && p.Id==id);
        }

        public IEnumerable<PointOfInterest> GetPointsOfInterests(int cityId)
        {
            return cityInfoContext.PointsOfInterest.Where(p=>p.CityId==cityId);
        }
    }
}
