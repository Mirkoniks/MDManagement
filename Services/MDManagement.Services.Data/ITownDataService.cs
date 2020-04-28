namespace MDManagement.Services.Data
{
    using MDManagement.Services.Models.Town;
    using System.Collections.Generic;
    using System.Runtime.InteropServices.ComTypes;

    public interface ITownDataService
    {
        public bool Exists(string townName);

        public TownServiceModel FindTownByName(string townName);

        public void Create(string name, int postCode);

        public IEnumerable<TownServiceModel> GetAllTowns();

        public void Delete(string townName);

        public void Update(int townId, string newTownName);

        public void Update(int townId, string newTownName, int newPostCode);

        public TownServiceModel FindTownById(int id);
    }
}
