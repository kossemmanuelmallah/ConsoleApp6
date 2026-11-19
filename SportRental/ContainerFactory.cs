using Unity;
using SportRental.Forms;
using SportRental.Repositories.Interfaces;
using SportRental.Repositories.Stubs;

namespace SportRental
{
    public static class ContainerFactory
    {
        public static IUnityContainer Build()
        {
            var container = new UnityContainer();

            // Репозитории
            container.RegisterType<IEquipmentTypeRepository, EquipmentTypeRepositoryStub>();
            container.RegisterType<IEquipmentSizeRepository, EquipmentSizeRepositoryStub>();
            container.RegisterType<ITariffRepository, TariffRepositoryStub>();
            container.RegisterType<IClientRepository, ClientRepositoryStub>();
            container.RegisterType<IEquipmentRepository, EquipmentRepositoryStub>();
            container.RegisterType<IRentalRepository, RentalRepositoryStub>();
            container.RegisterType<IRentalItemRepository, RentalItemRepositoryStub>();

            // Формы
            container.RegisterType<MainForm>();
            container.RegisterType<EquipmentTypeListForm>();
            container.RegisterType<EquipmentSizeListForm>();
            container.RegisterType<TariffListForm>();
            container.RegisterType<ClientListForm>();
            container.RegisterType<EquipmentListForm>();
            container.RegisterType<RentalListForm>();

            return container;
        }
    }
}