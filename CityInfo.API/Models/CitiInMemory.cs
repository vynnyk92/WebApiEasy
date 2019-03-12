using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Models
{
    public class CitiInMemory
    {
        public static CitiInMemory Current { get; } = new CitiInMemory();

        public List<CityDTO> Cities { get; set; }

        public CitiInMemory()
        {
            Cities = new List<CityDTO>()
            {
                new CityDTO()
                {
                    Id=1,
                    Name="Name 1",
                    Description = "Desc 1",
                    PointOfInterest = new List<PointOfInterestDTO>(){
                        new PointOfInterestDTO(){
                            Id = 11, 
                            Name ="Point 11",
                            Description ="Point 11",
                        },
                        new PointOfInterestDTO(){
                            Id = 12,
                            Name ="Point 12",
                            Description ="Point 12",
                        }
                    }
                },
                 new CityDTO()
                {
                    Id=2,
                    Name="Name 2",
                    Description = "Desc 2",
                    PointOfInterest = new List<PointOfInterestDTO>(){
                        new PointOfInterestDTO(){
                            Id = 21,
                            Name ="Point 21",
                            Description ="Point 21",
                        },
                        new PointOfInterestDTO(){
                            Id = 22,
                            Name ="Point 22",
                            Description ="Point 22",
                        }
                    }
                },
                  new CityDTO()
                {
                    Id=3,
                    Name="Name 3",
                    Description = "Desc 3"
                }
            };
        }
    }
}
