using System;

namespace AnyColorBall.Infrastructure
{
    public class AllServices
    {
        private static AllServices _container;

        public static AllServices Container => _container ?? (_container = new AllServices());

        public void RegisterSingle<TService>(TService service) where TService : IService
        {
            Implementation<TService>.Service = service;
        }

        public TService Single<TService>() where TService : IService
        {
            TService service = Implementation<TService>.Service;
            
            if (service != null)
            {
                return service;
            }
            else
            {
                throw new InvalidOperationException("Failture get service - " + typeof(TService) + ". Make sure is service registered before use");
            }
        }

        private static class Implementation<TService> where TService : IService
        {
            public static TService Service;
        }
    }
}