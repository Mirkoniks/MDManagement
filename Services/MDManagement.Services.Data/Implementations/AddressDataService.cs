using MDManagement.Data.Models;
using MDManagement.Services.Models.Address;
using MDManagement.Web.Data;
using System.Linq;

namespace MDManagement.Services.Data.Implementations
{
    public class AddressDataService : IAddressDataService
    {
        private readonly MDManagementDbContext data;

        public AddressDataService(MDManagementDbContext data)
        {
            this.data = data;
        }

        /// <summary>
        /// Adds address to a employee
        /// </summary>
        /// <param name="info">Service model which contains the needed info</param>
        public void AddEmployeeToAddress(AddEmployeeToAddressServiceModel info)
        {
            data.Addresses.Where(a => a.Id == info.AddressId).FirstOrDefault().EmployeeId = info.EmployeeId;

            data.SaveChanges();
        }


        /// <summary>
        /// Creates an address
        /// </summary>
        /// <param name="address">Service model which creastes an address</param>
        public void Create(CreateAddressServiceModel address)
        {
            var addressToAdd = new Address()
            {
                AddressText = address.AddressText,
                TownId = address.TownId
            };

            data.Addresses.Add(addressToAdd);

            data.SaveChanges();
        }

        /// <summary>
        /// Finds an addres 
        /// </summary>
        /// <param name="id">Address id</param>
        /// <returns>Address service model with the needen info</returns>
        public AddressServiceModel FindById(int id)
        {
            var address = data.Addresses.Where(a => a.Id == id)
                .Select(a => new AddressServiceModel
                {
                    AddressId = a.Id,
                    AddressText = a.AddressText,
                    TownId = a.TownId
                })
                .FirstOrDefault();

            return address;
        }
        /// <summary>
        /// Finds an address by name
        /// </summary>
        /// <param name="addressText">Address text</param>
        /// <returns>Address service model with the needen info</returns>
        public AddressServiceModel FindByName(string addressText)
        {
            return data.Addresses.Where(a => a.AddressText == addressText)
                .Select(a => new AddressServiceModel
                {
                    AddressId = a.Id,
                    AddressText = a.AddressText,
                    TownId = a.TownId
                })
                .FirstOrDefault();
        }
    }
}
