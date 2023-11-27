using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefUnits : RefBase {

    public static Dictionary<int, RefUnits> cacheMap = new Dictionary<int, RefUnits>();

    public int UnitsID;
    public string Part;

    public override string GetFirstKeyName() {
        return "UnitsID";
    }

    public override void LoadByLine(Dictionary<string, string> _value, int _line) {
        base.LoadByLine(_value, _line);
        UnitsID = GetInt("UnitsID");
        Part = GetString("Part");
    }

    public static RefUnits GetRef(int taskId) {
        RefUnits data = null;
        if (cacheMap.TryGetValue(taskId, out data)) {
            return data;
        }

        if (data == null) {
            Debug.LogError("error RefUnits key:" + taskId);
        }
        return data;
    }

}
