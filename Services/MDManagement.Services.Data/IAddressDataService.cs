namespace MDManagement.Services.Data
{
    using MDManagement.Services.Models.Address;

    public interface IAddressDataService
    {
        public void Create(CreateAddressServiceModel address);

        public AddressServiceModel FindById(int id);

        public AddressServiceModel FindByName(string address);

        public void AddEmployeeToAddress(AddEmployeeToAddressServiceModel info);
    }
}
