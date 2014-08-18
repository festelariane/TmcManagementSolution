using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tmc.Core.Infrastructure
{
    public class Singleton
    {
        static Singleton()
        {
            allSingletons = new Dictionary<Type, object>();
        }
        static readonly IDictionary<Type, object> allSingletons;
        public static IDictionary<Type, object> AllSingletons
        {
            get { return allSingletons; }
        }
    }

    public class Singleton<T> : Singleton
    {
        static T _instance;
        public static T Instance 
        {
            get
            {
                return _instance;
            }
            set
            {
                _instance = value;
                AllSingletons[typeof(T)] = value;
            }
        }
    }
}
