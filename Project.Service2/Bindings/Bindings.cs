using AutoMapper;
using Ninject.Modules;
using Project.Service.Repository;
using Project.Service.Repository.IRepository;

namespace Project.Service.Bindings
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IVehicleModelRepository>().To<VehicleModelRepository>();
            Bind<IVehicleMakeRepository>().To<VehicleMakeRepository>();
            Bind<IMapper>().To<Mapper>();
        }

    }
}
