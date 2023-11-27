using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AndMoney.SptObj {
    [CreateAssetMenu(fileName = "ListPos", menuName = "MyData/ListPos")]
    public class ListPos : ScriptableObject {
        public List<Vector3> pos;
    }
}

