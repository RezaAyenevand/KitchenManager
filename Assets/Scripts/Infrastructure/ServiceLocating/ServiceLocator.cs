using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ServiceLocating
{
    public class ServiceLocator
    {
        private static ServiceLocator instance;

        private List<Service> services = new List<Service>();

        public static void Init()
        {
            if (instance == null)
            {
                instance = new ServiceLocator();
            }
        }

        public static bool IsInited()
        {
            return instance != null;
        }

        public static void Clear()
        {
            instance = null;
        }

        public static void Register(Service service)
        {
            if (!instance.services.Any(x => x.GetType() == service.GetType()))
            {
                instance.services.Add(service);
            }
        }

        public static void RegisterMultiInstance(Service service)
        {
            instance.services.Add(service);
        }

        public static void UnRegister<T>() where T : Service
        {
            T val = Find<T>();
            instance.services.Remove(val);
        }

        public static void Replace<T>(T service) where T : Service
        {
            UnRegister<T>();
            Register(service);
        }

        public static T Find<T>() where T : Service
        {
            foreach (Service service in instance.services)
            {
                if (service is T)
                {
                    return (T)service;
                }
            }

            throw new Exception($"Service of type '{typeof(T).ToString()}' could not be found.");
        }

        public static List<T> FindList<T>() where T : Service
        {
            List<T> servicelist = new List<T>();
            foreach (Service service in instance.services)
            {
                if (service is T)
                {
                    servicelist.Add((T)service);
                }
            }
            return servicelist;
        }

        public static T FindServiceOfType<T>(Type serviceType) where T : Service
        {
            var servicesList = FindList<T>();
            return servicesList.First(x => x.GetType() == serviceType);
        }
    }
}
