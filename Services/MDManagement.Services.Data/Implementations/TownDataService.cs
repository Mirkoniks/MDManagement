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

        public void Create(CreateTownServiceModel town)
        {
            var townToAdd = new Town()
            {
                Name = town.Name,
                PostCode = town.PostCode
            };

            data.Towns.Add(townToAdd);

            data.SaveChanges();
        }

        public bool Exists(string townName)
        {
            return data.Towns.Any(t => t.Name.ToLower() == townName.ToLower());
        }

        public TownServiceModel FindById(int id)
        {
            var town = data.Towns
                .Where(t => t.Id == id)
                .Select(t => new TownServiceModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    PostCode = t.PostCode
                })
                .FirstOrDefault();

            return town;
        }

        public TownServiceModel FindByName(string name)
        {
            return data.Towns
                .Where(t => t.Name == name)
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
