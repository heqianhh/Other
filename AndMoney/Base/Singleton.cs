using System;
using System.Collections.Generic;
using UnityEngine;

namespace AndMoney {
    public class ISingleton {
    }

    public class Singleton<T> : ISingleton where T : ISingleton, new() {
        private static T _instance = null;

        public static T Instance {
            get {
                if (_instance == null)
                    _instance = new T();
                return _instance;
            }
        }
    }

}


