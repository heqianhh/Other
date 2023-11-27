using System.Collections;
using System.Globalization;

public partial class AcNumMgr : Singleton<AcNumMgr> {

    public static string ACNumToRound(double num, int unitCount = 3) {
        double unitValue = 10;
        if (unitCount == 3) {
            unitValue = 1000;
        }
        else
            unitValue = System.Math.Pow(unitValue, unitCount);

        int units = 0;
        while (num >= unitValue) {
            num /= 1000;
            units++;
        }
        string str = num.ToString("0", CultureInfo.InvariantCulture);
        return str + GetUnit(units);
    }

    public static string ACNum(double num, AcNumCdt acNumCdt = AcNumCdt.Auto, int unitCount = 3) {
        double unitValue = 10;
        unitValue = System.Math.Pow(unitValue, unitCount);

        int units = 0;
        while (num >= unitValue) {
            num /= 1000;
            units++;
        }
        if (units < 1) {
            switch (acNumCdt) {
                case AcNumCdt.Auto:
                    return num.ToString("#0", CultureInfo.InvariantCulture);
                case AcNumCdt.UnFloat:
                    return num.ToString("#0", CultureInfo.InvariantCulture);
                case AcNumCdt.Float:
                    return num.ToString("#0.0", CultureInfo.InvariantCulture);
                default:
                    return null;
            }
        }
        switch (acNumCdt) {
            case AcNumCdt.Auto:
                return num.ToString("##.#", CultureInfo.InvariantCulture) + GetUnit(units);
            case AcNumCdt.UnFloat:
                return num.ToString("##", CultureInfo.InvariantCulture) + GetUnit(units);
            case AcNumCdt.Float:
                return num.ToString("##.#", CultureInfo.InvariantCulture) + GetUnit(units);
            default:
                return null;
        }

    }
    public static string ACNum(int num, bool getFloat = false, int unitCount = 3) {
        double unitValue = 10;
        unitValue = System.Math.Pow(unitValue, unitCount);

        int units = 0;
        while (num >= unitValue) {
            num /= 1000;
            units++;
        }
        if (units < 1) {
            return num.ToString(getFloat ? "#0.0" : "#0", CultureInfo.InvariantCulture);
        }
        return num.ToString("##.#", CultureInfo.InvariantCulture) + GetUnit(units);
    }

    public static string GetNumToUnit(double num) {
        int units = 0;
        while (num > 1000) {
            num /= 1000;
            units++;
        }
        return units >= 1 ? GetUnit(units) : "";
    }

    public static string GetUnit(int Units) {
        string empty = string.Empty;
        if (Units > 0 && Units < RefUnits.cacheMap.Values.Count) {
            empty = RefUnits.GetRef(Units - 1).Part;
        }
        return empty;
    }
}

public enum AcNumCdt {
    Auto,
    UnFloat,
    Float,
}
