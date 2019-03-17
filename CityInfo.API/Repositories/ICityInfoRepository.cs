using CityInfo.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Repositories
{
    public interface ICityInfoRepository
    {
        IQueryable<City> GetCities();
        City GetCity(int id);
        IEnumerable<PointOfInterest> GetPointsOfInterests(int cityId);
        PointOfInterest GetPointOfInterest(int cityId, int id);

        void AddPointOfInterest(int cityId, PointOfInterest pointOfInterest);
    }
}
