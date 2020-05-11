namespace MDManagement.Services.Data
{
    using MDManagement.Services.Models.Town;
    using System.Collections.Generic;
    using System.Runtime.InteropServices.ComTypes;

    public interface ITownDataService
    {
        public bool Exists(string townName);

        public void Create(CreateTownServiceModel town);

        public TownServiceModel FindById(int id);

        public TownServiceModel FindByName(string name);
    }
}
