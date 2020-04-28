namespace MDManagement.Services.Data.Implementations
{
    using MDManagement.Data.Models;
    using MDManagement.Services.Models.Town;
    using MDManagement.Web.Data;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class TownDataService : ITownDataService
    {
        private readonly MDManagementDbContext data;

        public TownDataService(MDManagementDbContext data)
        {
            this.data = data;
        }

        public void Create(string name, int postCode)
        {
            if (!Exists(name))
            {
                Town town = new Town
                {
                    Name = name,
                    PostCode = postCode
                };

                data.Towns.Add(town);

                data.SaveChanges();
            }   
        }

        public bool Exists(string townName)
        {
            return data.Towns.Any(t => t.Name == townName);
        }

        public TownServiceModel FindTownByName(string townName)
        {
            return data.Towns
                   .Where(n => n.Name == townName)
                   .Select(t => new TownServiceModel
                   {
                       Name = t.Name,
                       PostCode = t.PostCode
                   }
                 ).FirstOrDefault();
        }

        public IEnumerable<TownServiceModel> GetAllTowns()
        {
            return data
                .Towns
                .Select(t => new TownServiceModel()
                {
                    Name = t.Name,
                    PostCode = t.PostCode
                })
                .ToArray();
        }

        public void Delete(string townName)
        {
            var town = data.Towns.Where(n => n.Name == townName).FirstOrDefault();

            if (town != null)
            {
                data.Towns.Remove(town);
                data.SaveChanges();
            }
        }

        public void Update(int townId, string newTownName)
        {
            var town = FindTownById(townId);

            if (town != null)
            {
                town.Name = newTownName;

                data.Update(town);

                data.SaveChanges();
            }
        }

        public void Update(int townId, string newTownName, int newPostCode)
        {
            var town = FindTownById(townId);

            if (town != null)
            {
                town.Name = newTownName;
                town.PostCode = newPostCode;

                data.Update(town);

                data.SaveChanges();
            }
        }

        public TownServiceModel FindTownById(int id)
        {
            return data.Towns
                .Where(t => t.Id == id)
                .Select(t => new TownServiceModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    PostCode = t.PostCode
                })
                .FirstOrDefault();
        } 
    }
}
