using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AndMoney{
    /// <summary>
    /// int
    /// </summary>
    public interface IIntClass {
        int GetValue();
        void SetValue(int num);
 
    }
    /// <summary>
    /// Vector3
    /// </summary>
    public interface IVector3Class {
        Vector3 GetPos();
        void SetPos(Vector3 pos);
    }
    public interface IFather {
        
    }
}

