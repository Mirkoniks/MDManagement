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

        /// <summary>
        /// Creates a town
        /// </summary>
        /// <param name="town">CreateTownServiceModel which is a DTO which contains the needed info for this operations</param>
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

        /// <summary>
        /// Checks if town exists
        /// </summary>
        /// <param name="townName">Town name</param>
        /// <returns></returns>
        public bool Exists(string townName)
        {
            return data.Towns.Any(t => t.Name.ToLower() == townName.ToLower());
        }

        /// <summary>
        /// Finds a town by id
        /// </summary>
        /// <param name="id">Town id</param>
        /// <returns>TownServiceModel which is a DTO which contains the needed info for this operations</returns>
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

        /// <summary>
        /// Finds a tonw by name
        /// </summary>
        /// <param name="name">Town name</param>
        /// <returns>TownServiceModel which is a DTO which contains the needed info for this operations</returns>
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
