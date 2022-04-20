using AutoMapper;
using Ninject;
using Ninject.Extensions.Factory;
using Ninject.Web.Common;
using Project.Service.Repository;
using Project.Service.Repository.IRepository;

namespace Project.Service.Bindings
{
    static class DI
    {

        static public void Initialize()
        {
            IKernel _standardKernel = new StandardKernel();
            _standardKernel.Bind<IVehicleModelRepository>().To<VehicleModelRepository>();
            _standardKernel.Bind<IVehicleMakeRepository>().To<VehicleMakeRepository>();
            _standardKernel.Bind<IVehicleModelRepository>().ToFactory();
            _standardKernel.Bind<IMapper>().To<Mapper>();
            _standardKernel.Load(new Bindings());
            _standardKernel.GetAll<IVehicleModelRepository>();
            //IVehicleModelRepository _vmRepo = _standardKernel.Get<IVehicleModelRepository>();

        }

        //static public T Create<T>()
        //{
        //    return _standardKernel.Get<T>();
        //}
    }
}
