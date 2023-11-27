using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AndMoney {
    public partial class EMath {
        public static string[] surnames = { "Smith", "Johnson", "Brown", "Taylor", "Miller", "Wilson", "Moore", "Clark", "Lee", "Walker", "Hall", "Baker", "Lewis", "Young", "Green", "Adams", "King", "Wright", "Allen", "Carter", "Collins", "Davis", "Evans", "Foster", "Garcia", "Harris", "Jackson", "Jones", "Kelly", "Lopez", "Martin", "Mitchell", "Nelson", "Parker", "Rivera", "Robinson", "Ross", "Sanchez", "Scott", "Stewart", "Thomas", "Turner", "White", "Williams", "Wood", "Wright", "Zhang" };

        public static Color[] colors = {
            new Color(1f, 0f, 0f), // ��ɫ
            new Color(0f, 1f, 0f), // ��ɫ
            new Color(0f, 0f, 1f), // ��ɫ
            new Color(1f, 1f, 0f), // ��ɫ
            new Color(1f, 0f, 1f), // ��ɫ
            new Color(0f, 1f, 1f), // ��ɫ
            new Color(1f, 0.5f, 0f), // ��ɫ
            new Color(0.5f, 0f, 1f), // ����ɫ
            new Color(0f, 0.5f, 1f), // ǳ��ɫ
            new Color(1f, 0f, 0.5f), // �ۺ�ɫ
            new Color(0.5f, 1f, 0f), // ����ɫ
            new Color(0f, 1f, 0.5f), // ����ɫ
            new Color(0.5f, 0f, 0.5f), // ����ɫ
            new Color(0.5f, 0.5f, 0f), // ���ɫ
            new Color(0f, 0.5f, 0.5f), // ����ɫ
            new Color(0.5f, 0.5f, 1f), // ǳ��ɫ
            new Color(0.5f, 1f, 0.5f), // ǳ��ɫ
            new Color(1f, 0.5f, 1f), // õ��ɫ
            new Color(1f, 1f, 0.5f), // ǳ��ɫ
            new Color(0.5f, 1f, 1f) // ǳ��ɫ
        };
        #region ��ɫ
        public static Color GetColorFromHtmlColor(string htmlColor) {
            int hexValue = int.Parse(htmlColor, System.Globalization.NumberStyles.HexNumber);
            Color color = new Color32((byte)((hexValue >> 16) & 0xFF), (byte)((hexValue >> 8) & 0xFF), (byte)(hexValue & 0xFF), 255);
            return color;
        }
        #endregion
        #region �������˶�
        public static Vector3 Parabola(Vector3 start, Vector3 end, float height, float t) {
            float Func(float x) => 4 * (-height * x * x + height * x);

            var mid = Vector3.Lerp(start, end, t);

            return new Vector3(mid.x, Func(t) + Mathf.Lerp(start.y, end.y, t), mid.z);
        }
        public static Vector2 Parabola(Vector2 start, Vector2 end, float height, float t) {
            float Func(float x) => 4 * (-height * x * x + height * x);

            var mid = Vector2.Lerp(start, end, t);

            return new Vector2(mid.x, Func(t) + Mathf.Lerp(start.y, end.y, t));
        }
        #endregion
        #region ���ֵ
        /// <summary>
        /// ����С�����ֵ֮���������ֵ(���������ֵ)
        /// </summary>
        /// <param name="min">��Сֵ</param>
        /// <param name="max">���ֵ</param>
        /// <returns>���ֵ</returns>
        static public T GetRandomValueBetweenMinMax<T>(T minValue, T maxValue) {
            switch (minValue, maxValue) {
                case (int minInt, int maxInt):
                    return (T)(object)Random.Range(minInt, maxInt);
                case (float minFloat, float maxFloat):
                    return (T)(object)Random.Range(minFloat, maxFloat);
                case (double minDouble, double maxDouble):
                    double randomDouble = minDouble + (maxDouble - minDouble) * new System.Random().NextDouble();
                    return (T)(object)randomDouble;
                default:
                    Debug.LogError("Unsupported data type!");
                    return default;
            }
        }
        /// <summary>
        /// ����������ֵ
        /// </summary>
        /// <returns>���ֵ</returns>
        static public bool RangeTrueOrFalse(float rate = 0.5f) {
            return Random.Range(0f, 1f) >= rate;
        }
        /// <summary>
        /// ��ȡ���ƫ�Ƶ�Vector3
        /// </summary>
        /// <param name="offset">ƫ����</param>
        /// <returns></returns>
        static public Vector3 GetRandomVec3(float offset) {
            return new Vector3(Random.Range(-offset, offset), 0f, Random.Range(-offset, offset));
        }
        /// <summary>
        /// �����ȡһ��������int����ֵ���Ҳ��ظ�
        /// </summary>
        /// <param name="count">Ҫ��ȡ����ֵ����</param>
        /// <param name="minValue">��ֵ����Сֵ��������</param>
        /// <param name="maxValue">��ֵ�����ֵ��������</param>
        /// <returns>���������ֵ���б�</returns>
        public static List<int> GetRandomIntList(int count, int minValue, int maxValue) {
            List<int> result = new List<int>();
            HashSet<int> hashSet = new HashSet<int>();

            // ���count����������ȡֵ��������count����Ϊ������ȡֵ����
            count = Mathf.Min(count, maxValue - minValue + 1);

            // �����ȡcount�����ظ���int����ֵ
            while (result.Count < count) {
                int randomValue = Random.Range(minValue, maxValue + 1);
                if (!hashSet.Contains(randomValue)) {
                    hashSet.Add(randomValue);
                    result.Add(randomValue);
                }
            }

            return result;
        }
        public static List<int> GetRandomIntList(int count, int minValue, int maxValue, List<int> excludedValues) {
            List<int> result = new List<int>();
            HashSet<int> hashSet = new HashSet<int>(excludedValues);

            count = Math.Min(count, maxValue - minValue + 1 - excludedValues.Count);

            while (result.Count < count) {
                int randomValue = Random.Range(minValue, maxValue + 1);
                if (!hashSet.Contains(randomValue)) {
                    hashSet.Add(randomValue);
                    result.Add(randomValue);
                }
            }

            return result;
        }
        /// <summary>
        /// ��ȡnum�����������Щ�����Ϊa
        /// </summary>
        /// <param name="a">����</param>
        /// <param name="num">����</param>
        /// <returns></returns>
        public static List<int> GetRandomInts(int a, int num) {
            List<int> result = new List<int>();
            int remaining = a;

            for (int i = 0; i < num - 1; i++) {
                int maxPossibleValue = remaining - (num - i - 1);
                int randomValue = Random.Range(1, maxPossibleValue + 1);
                result.Add(randomValue);
                remaining -= randomValue;
            }

            result.Add(remaining);
            result.Shuffle();
            return result;
        }
        public static Vector2 CircleRandomPoint(float outerRadius, float innerRadius) {
            float angle = Random.Range(0f, Mathf.PI * 2f);

            // ��������뾶
            float radius = Random.Range(innerRadius, outerRadius);

            // ��������
            float x = radius * Mathf.Cos(angle);
            float y = radius * Mathf.Sin(angle);

            return new Vector2(x, y);
        }
        #endregion
        #region ����������
        public List<Vector3> Bezier(Vector3 startPoint, Vector3 midPoint, Vector3 endPoint, float vertexCount) {
            List<Vector3> vertexsList = new List<Vector3>();
            float interval = 1 / vertexCount;
            for (int i = 0; i < vertexCount; i++) {
                vertexsList.Add(GetPoint(startPoint, midPoint, endPoint, i * interval));
            }
            vertexsList[vertexsList.Count - 1] = endPoint;
            return vertexsList;
        }
        private Vector3 GetPoint(Vector3 startPoint, Vector3 midPoint, Vector3 endPoint, float t) {
            float a = 1 - t;
            Vector3 target = startPoint * Mathf.Pow(a, 2) + 2 * midPoint * t * a + endPoint * Mathf.Pow(t, 2);
            return target;
        }
        /// <summary>
        /// ���ݱ༭����㱴��������·����
        /// </summary>
        /// <param name="points"></param>
        public List<Vector3> GetPathPoints(List<Vector3> points) {
            List<Vector3> Pathpoints = new List<Vector3>();
            int pointNumber = GetNnumber(points);
            for (int i = 0; i <= pointNumber; i++) {
                Pathpoints.Add(GetPoint(points, pointNumber, i));
            }
            return Pathpoints;
        }
        /// <summary>
        /// �õ����������ı�������
        /// </summary>
        /// <param name="points"></param>
        /// <param name="pointNumber"></param>
        /// <param name="Index"></param>
        /// <returns></returns>
        public Vector3 GetPoint(List<Vector3> points, float pointNumber, int Index) {
            List<Vector3> Temp = new List<Vector3>();
            Vector3 pos = new Vector3();
            if (points.Count > 2) {
                for (int i = 0; i < points.Count - 1; i++) {
                    pos = Vector3.Lerp(points[i], points[i + 1], Index / pointNumber);
                    Temp.Add(pos);
                }
            }
            else if (points.Count == 2) {
                pos = Vector3.Lerp(points[0], points[1], Index / pointNumber);
            }
            else {
                pos = points[0];
            }
            if (Temp.Count >= 2) {
                pos = GetPoint(Temp, pointNumber, Index);
            }
            return pos;
        }
        /// <summary>
        /// ���㱴������ĸ���
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public int GetNnumber(List<Vector3> points) {
            float Length = 0;
            for (int i = 0; i < points.Count - 2; i++) {
                Length += Vector3.Distance(points[i], points[i + 1]);
            }
            if (Length > points.Count)//���峤�ȼ�������Զ���
            {
                return (int)(Length / 1);
            }
            else {
                return (int)(Length / 0.5f);
            }
        }
        #endregion
        #region ���ĵ����
        /// <summary>
        /// ����Բ�ĺͰ뾶��ȡԲ�ϵĵ�
        /// </summary>
        /// <param name="center">Բ��</param>
        /// <param name="r">�뾶</param>
        /// <param name="count">��ĸ���</param>
        /// <returns>Բ�ϵĵ�</returns>
        static public List<Vector2> GetCirclePoint(Vector2 center, float r, int count) {
            return GetCirclePoint(center, r, 0f, 360f, count, false);
        }
        /// <summary>
        /// ����Բ�ĺͰ뾶��ȡԲ�ϵĵ�
        /// </summary>
        /// <param name="center">Բ��</param>
        /// <param name="r">�뾶</param>
        /// <param name="AngleMin">��С�Ƕ�</param>
        /// <param name="AngleMax">���Ƕ�</param>
        /// <param name="count">��ĸ���</param>
        /// <returns>�����ϵĵ�</returns>
        static public List<Vector2> GetCirclePoint(Vector2 center, float r, float AngleMin, float AngleMax, int count, bool haveTail = true) {
            List<Vector2> circlePos = new List<Vector2>();
            float addAngle = (float)(AngleMax - AngleMin) / (count - (haveTail ? 1 : 0));
            float curAngle = AngleMin;
            for (int i = 0; i < count; i++) {
                float x = center.x + r * Mathf.Sin(curAngle * Mathf.Deg2Rad);
                float y = center.y + r * Mathf.Cos(curAngle * Mathf.Deg2Rad);
                curAngle += addAngle;
                circlePos.Add(new Vector2(x, y));
            }
            return circlePos;
        }
        /// <summary>
        /// ����Բ�ĺͰ뾶�ͼ���ǶȻ�ȡ�Գ�ʼ�Ƕ�Ϊ���ĵ�Բ�ϵĵ�
        /// </summary>
        /// <param name="center">Բ��</param>
        /// <param name="r">�뾶</param>
        /// <param name="StartAngle">��ʼ�Ƕ�</param>
        /// <param name="AngleInterval">����Ƕ�</param>
        /// <param name="count">��ĸ���</param>
        /// <returns></returns>
        static public List<Vector2> GetCirclePointMid(Vector2 center, float r, float StartAngle, float AngleInterval, int count) {
            List<Vector2> circlePos = new List<Vector2>();
            for (int i = 0; i < count; i++) {
                int t = i;
                if (t % 2 == 0) {
                    t = t / 2;
                }
                else {
                    t = -(t + 1) / 2;
                }
                float curAngle = StartAngle + t * AngleInterval;
                float x = center.x + r * Mathf.Sin(curAngle * Mathf.Deg2Rad);
                float y = center.y + r * Mathf.Cos(curAngle * Mathf.Deg2Rad);
                circlePos.Add(new Vector2(x, y));
            }
            return circlePos;
        }
        static public List<Vector2> GetCirclePointMidRadian(Vector2 center, float r, float StartAngle, float ArcLen, int count) {
            List<Vector2> circlePos = new List<Vector2>();
            for (int i = 0; i < count; i++) {
                int t = i;
                if (t % 2 == 0) {
                    t = t / 2;
                }
                else {
                    t = -(t + 1) / 2;
                }
                float curAngle = StartAngle + t * ArcLen / r * Mathf.Rad2Deg;
                float x = center.x + r * Mathf.Sin(curAngle * Mathf.Deg2Rad);
                float y = center.y + r * Mathf.Cos(curAngle * Mathf.Deg2Rad);
                circlePos.Add(new Vector2(x, y));
            }
            return circlePos;
        }
        static public List<Vector2> GetCirclePointLeftRadian(Vector2 center, float r, float StartAngle, float ArcLen, int count) {
            List<Vector2> circlePos = new List<Vector2>();
            float addAngle = ArcLen / r * Mathf.Rad2Deg;
            float curAngle = -count / 2 * addAngle;
            for (int i = 0; i < count; i++) {
                float x = center.x + r * Mathf.Sin(curAngle * Mathf.Deg2Rad);
                float y = center.y + r * Mathf.Cos(curAngle * Mathf.Deg2Rad);
                curAngle += addAngle;
                circlePos.Add(new Vector2(x, y));
            }
            return circlePos;
        }
        /// <summary>
        /// ��ȡ��ĳ����������תһ���ǶȺ������
        /// </summary>
        /// <param name="pos1"></param>
        /// <param name="centerPos"></param>
        /// <param name="angle"></param>
        /// <param name="yAxis"></param>
        /// <returns></returns>
        static public Vector3 GetRotPos(Vector3 pos1, Vector3 centerPos, float angle, bool yAxis = true) {
            Vector3 pos = pos1;
            //��y����ת
            if (yAxis) {
                float x0 = (pos1.x - centerPos.x) * Mathf.Cos(angle * Mathf.Deg2Rad) - (pos1.z - centerPos.z) * Mathf.Sin(angle * Mathf.Deg2Rad) + centerPos.x;
                float z0 = (pos1.z - centerPos.z) * Mathf.Cos(angle * Mathf.Deg2Rad) + (pos1.x - centerPos.x) * Mathf.Sin(angle * Mathf.Deg2Rad) + centerPos.z;
                pos = new Vector3(x0, centerPos.y, z0);
            }
            //��z����ת
            else {
                float x0 = (pos1.x - centerPos.x) * Mathf.Cos(angle * Mathf.Deg2Rad) - (pos1.y - centerPos.y) * Mathf.Sin(angle * Mathf.Deg2Rad) + centerPos.x;
                float y0 = (pos1.y - centerPos.y) * Mathf.Cos(angle * Mathf.Deg2Rad) + (pos1.x - centerPos.x) * Mathf.Sin(angle * Mathf.Deg2Rad) + centerPos.y;
                pos = new Vector3(x0, y0, centerPos.z);
            }
            return pos;
        }
        static public List<Vector2> GetRectanglePoint(Vector2 start, float lineLen, int lineMaxCount, float rowLen, int count) {
            List<Vector2> rectanglePos = new List<Vector2>();
            for (int i = 0; i < count; i++) {
                float x = start.x + i % lineMaxCount * lineLen;
                float y = start.y + i / lineMaxCount * rowLen;
                rectanglePos.Add(new Vector2(x, y));
            }
            return rectanglePos;
        }
        /// <summary>
        /// ����Χ�����ĵĶ��Բ�ϵĵȻ���������
        /// </summary>
        /// <param name="num">��ĸ���</param>
        /// <param name="radiusStep">Բ�İ뾶�Ĳ�ֵ</param>
        /// <param name="arclength">���ڵ��Ļ���</param>
        /// <returns></returns>
        static public List<Vector2> GeneratePointsOnCircles(int num, float radiusStep, float arclength) {
            List<Vector2> points = new List<Vector2>();
            if (num == 0) {
                return points;
            }
            points.Add(Vector2.zero);
            if (num == 1) {
                return points;
            }
            num -= 1;
            float radius = 0f;
            int numPoints = 1;

            while (num > 0) {
                radius += radiusStep;
                numPoints = Mathf.RoundToInt(2 * Mathf.PI * radius / arclength);
                if (num >= numPoints) {
                    num -= numPoints;
                    for (int i = 0; i < numPoints; i++) {
                        float angle = i * 2 * Mathf.PI / numPoints;
                        float x = radius * Mathf.Cos(angle);
                        float z = radius * Mathf.Sin(angle);
                        Vector2 point = new Vector3(x, z);
                        points.Add(point);
                    }
                }
                else {
                    for (int i = 0; i < num; i++) {
                        float angle = i * 2 * Mathf.PI / numPoints;
                        float x = radius * Mathf.Cos(angle);
                        float z = radius * Mathf.Sin(angle);
                        Vector2 point = new Vector3(x, z);
                        points.Add(point);
                    }
                    num = 0;
                }
            }
            return points;
        }
        #endregion
        #region �����
        /// <summary>
        /// ���Ƿ��ڶ�����ڲ�
        /// </summary>
        /// <param name="p">��</param>
        /// <param name="vertexs">����εĸ�����</param>
        /// <returns></returns>
        static public bool IsPointInPolygon(Vector2 p, List<Vector2> vertexs) {
            int crossNum = 0;
            int vertexCount = vertexs.Count;

            for (int i = 0; i < vertexCount; i++) {
                Vector2 v1 = vertexs[i];
                Vector2 v2 = vertexs[(i + 1) % vertexCount];

                if (((v1.y <= p.y) && (v2.y > p.y))
                    || ((v1.y > p.y) && (v2.y <= p.y))) {
                    if (p.x < v1.x + (p.y - v1.y) / (v2.y - v1.y) * (v2.x - v1.x)) {
                        crossNum += 1;
                    }
                }
            }

            if (crossNum % 2 == 0) {
                return false;
            }
            else {
                return true;
            }
        }
        #endregion
        #region �ȱ�����
        static public float Map(float x, float y, float j, float k, float value, float speed = 1f) {
            float t = Mathf.Pow((value - x) / (y - x), speed);
            return j + (k - j) * t;
        }
        #endregion
        #region ����
        static public bool[,] FindMathces(int width, int height, int minMatchCount, int[,] blocks) {
            bool[,] matches = new bool[width, height];
            bool[,] matches_height = new bool[width, height];
            bool[,] matches_width = new bool[width, height];
            for (int i = 0; i < width; i++) {
                for (int j = 0; j < height; j++) {
                    if (matches_height[i, j]) {
                        continue;
                    }
                    int count = 1;
                    for (int k = i + 1; k < width; k++) {
                        if (blocks[k, j] == blocks[i, j]) {
                            count++;
                        }
                        else {
                            break;
                        }
                    }
                    if (count >= minMatchCount) {
                        for (int k = i; k < i + count; k++) {
                            matches_height[k, j] = true;
                        }
                    }
                }
            }
            for (int i = 0; i < width; i++) {
                for (int j = 0; j < height; j++) {
                    if (matches_width[i, j]) {
                        continue;
                    }
                    int count = 1;
                    for (int k = j + 1; k < height; k++) {
                        if (blocks[i, k] == blocks[i, j]) {
                            count++;
                        }
                        else {
                            break;
                        }
                    }
                    if (count >= minMatchCount) {
                        for (int k = j; k < j + count; k++) {
                            matches_width[i, k] = true;
                        }
                    }
                }
            }
            for (int i = 0; i < width; i++) {
                for (int j = 0; j < height; j++) {
                    if (matches_height[i, j] || matches_width[i, j]) {
                        matches[i, j] = true;
                    }
                }
            }
            return matches;
        }
        static public bool[,] FindMatchesAdvanced(int width, int height, int minMatchCount, int[,] blocks) {
            bool[,] matches = new bool[width, height];
            List<Vector2Int> matchList = new List<Vector2Int>();

            // ���ˮƽ�ʹ�ֱƥ��
            for (int i = 0; i < width; i++) {
                for (int j = 0; j < height; j++) {
                    if (matches[i, j]) {
                        continue;
                    }

                    // ���ˮƽƥ��
                    int hCount = 1;
                    for (int k = i + 1; k < width; k++) {
                        if (blocks[k, j] == blocks[i, j]) {
                            hCount++;
                        }
                        else {
                            break;
                        }
                    }
                    if (hCount >= minMatchCount) {
                        for (int k = i; k < i + hCount; k++) {
                            matches[k, j] = true;
                            matchList.Add(new Vector2Int(k, j));
                        }
                    }

                    // ��ⴹֱƥ��
                    int vCount = 1;
                    for (int k = j + 1; k < height; k++) {
                        if (blocks[i, k] == blocks[i, j]) {
                            vCount++;
                        }
                        else {
                            break;
                        }
                    }
                    if (vCount >= minMatchCount) {
                        for (int k = j; k < j + vCount; k++) {
                            matches[i, k] = true;
                            matchList.Add(new Vector2Int(i, k));
                        }
                    }
                }
            }

            // ��� L �κ� T ��ƥ��
            while (matchList.Count > 0) {
                Vector2Int match = matchList[0];
                matchList.RemoveAt(0);

                List<Vector2Int> adjacentBlocks = new List<Vector2Int>();
                adjacentBlocks.Add(new Vector2Int(match.x - 1, match.y));
                adjacentBlocks.Add(new Vector2Int(match.x + 1, match.y));
                adjacentBlocks.Add(new Vector2Int(match.x, match.y - 1));
                adjacentBlocks.Add(new Vector2Int(match.x, match.y + 1));

                foreach (Vector2Int block in adjacentBlocks) {
                    if (block.x >= 0 && block.x < width && block.y >= 0 && block.y < height && !matches[block.x, block.y] && blocks[block.x, block.y] == blocks[match.x, match.y]) {
                        matches[block.x, block.y] = true;
                        matchList.Add(block);
                    }
                }
            }

            return matches;
        }
        static public bool[,] FindMatchesOptimized(int width, int height, int minMatchCount, int[,] blocks) {
            byte[] matchArray = new byte[width * height / 8 + 1];
            int bitIndex = 0;

            for (int i = 0; i < width; i++) {
                for (int j = 0; j < height; j++) {
                    if (((matchArray[bitIndex / 8] >> (bitIndex % 8)) & 1) == 1) {
                        bitIndex++;
                        continue;
                    }

                    // ���ˮƽƥ��
                    int hCount = 1;
                    for (int k = i + 1; k < width; k++) {
                        if (blocks[k, j] == blocks[i, j]) {
                            hCount++;
                        }
                        else {
                            break;
                        }
                    }
                    if (hCount >= minMatchCount) {
                        for (int k = i; k < i + hCount; k++) {
                            matchArray[bitIndex / 8] |= (byte)(1 << (bitIndex % 8));
                            bitIndex++;
                        }
                    }

                    // ��ⴹֱƥ��
                    int vCount = 1;
                    for (int k = j + 1; k < height; k++) {
                        if (blocks[i, k] == blocks[i, j]) {
                            vCount++;
                        }
                        else {
                            break;
                        }
                    }
                    if (vCount >= minMatchCount) {
                        for (int k = j; k < j + vCount; k++) {
                            matchArray[bitIndex / 8] |= (byte)(1 << (bitIndex % 8));
                            bitIndex++;
                        }
                    }

                    bitIndex++;
                }
            }

            bool[,] matches = new bool[width, height];
            bitIndex = 0;
            for (int i = 0; i < width; i++) {
                for (int j = 0; j < height; j++) {
                    if (((matchArray[bitIndex / 8] >> (bitIndex % 8)) & 1) == 1) {
                        matches[i, j] = true;
                    }
                    bitIndex++;
                }
            }

            return matches;
        }
        #endregion
        #region ���߼��
        static public Collider RayCastLine(Vector3 pos, Vector3 dir, float len, int layerMask) {
            Ray ray = new Ray(pos, dir);
            RaycastHit raycastHit;
            if (Physics.Raycast(ray, out raycastHit, len, layerMask)) {
                return raycastHit.collider;
            }
            return null;
        }
        static public Collider RayCastLine(Vector3 pos, Vector3 dir, float len, LayerMask layerMask) {
            return RayCastLine(pos, dir, len, 1 << layerMask);
        }
        static public Collider RayCastLine(Vector3 pos, Vector3 dir, float len, List<LayerMask> layerMask) {
            return RayCastLine(pos, dir, len, GetLayerMask(layerMask));
        }
        static public List<Collider> RayCastLineAll(Vector3 pos, Vector3 dir, float len, int layerMask) {
            List<Collider> colliders = new List<Collider>();
            Ray ray = new Ray(pos, dir);
            RaycastHit[] raycastHits = Physics.RaycastAll(ray, len, layerMask);

            Debug.DrawLine(ray.origin, pos + len * dir, Color.red);

            foreach (var temp in raycastHits) {
                colliders.Add(temp.collider);
            }
            return colliders;
        }
        static public List<Collider> RayCastLineAll(Vector3 pos, Vector3 dir, float len, LayerMask layerMask) {
            return RayCastLineAll(pos, dir, len, 1 << layerMask);
        }
        static public List<Collider> RayCastLineAll(Vector3 pos, Vector3 dir, float len, List<LayerMask> layerMask) {
            return RayCastLineAll(pos, dir, len, GetLayerMask(layerMask));
        }
        static public List<Collider> RayCastSphere(Vector3 pos, float radius, int layerMask) {
            List<Collider> colliders = new List<Collider>();
            Collider[] collidersTemp = Physics.OverlapSphere(pos, radius, layerMask);
            foreach (var temp in collidersTemp) {
                colliders.Add(temp);
            }
            return colliders;
        }
        static public List<Collider> RayCastSphere(Vector3 pos, float radius, LayerMask layerMask) {
            return RayCastSphere(pos, radius, 1 << layerMask);
        }
        static public List<Collider> RayCastSphere(Vector3 pos, float radius, List<LayerMask> layerMask) {
            return RayCastSphere(pos, radius, GetLayerMask(layerMask));
        }
        /// <summary>
        /// ���ϲ�
        /// </summary>
        /// <param name="layerMasks"></param>
        /// <returns></returns>
        static public int GetLayerMask(List<LayerMask> layerMasks) {
            int layerMask = 0;
            foreach (LayerMask mask in layerMasks) {
                layerMask |= (1 << mask.value);
            }
            return layerMask;
        }
        /// <summary>
        /// ��һ����ײ�尴����ࡣ
        /// </summary>
        /// <param name="colliders">Ҫ�������ײ���б�</param>
        /// <returns>�Բ�����Ϊ�����ò���������ײ���б�Ϊֵ���ֵ䡣</returns>
        static public Dictionary<LayerMask, List<Collider>> CategorizeCollidersByLayer(List<Collider> colliders) {
            // ����һ�����ֵ䣬���ڴ洢��������ײ�塣
            Dictionary<LayerMask, List<Collider>> categorizedColliders = new Dictionary<LayerMask, List<Collider>>();

            // ����������ײ�塣
            foreach (Collider collider in colliders) {
                // ��ȡ����ײ�����ڲ�Ĳ����롣
                LayerMask layerMask = 1 << collider.gameObject.layer;

                // ����ò㻹û�б����࣬����һ���µ��б���ӵ��ֵ��С�
                if (!categorizedColliders.ContainsKey(layerMask)) {
                    categorizedColliders.Add(layerMask, new List<Collider>());
                }

                // ������ײ����ӵ���Ӧ����б��С�
                categorizedColliders[layerMask].Add(collider);
            }

            // ���ط�������ײ���ֵ䡣
            return categorizedColliders;
        }
        #endregion
        #region ��Ļ����ת��
        /// <summary>
        /// ��������תUI
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="camera"></param>
        /// <returns></returns>
        public static Vector3 WorldPosToUI(Vector3 pos, Camera camera) {
            return UIRootTwoD.Instance.systemCanvas.transform.TransformPoint(ScreenToUI(camera.WorldToScreenPoint(pos)));
        }
        /// <summary>
        /// ��Ļ����תUI
        /// </summary>
        /// <param name="screenPos"></param>
        /// <returns></returns>
        public static Vector2 ScreenToUI(Vector2 screenPos) {
            Vector2 pos = Vector2.zero;
            pos.x = screenPos.x * (windowScale.x / Screen.width) - windowScale.x / 2f;
            pos.y = screenPos.y * (curWindowScaleY / Screen.height) - curWindowScaleY / 2f;
            return pos;
        }
        private static Vector2 windowScale = new Vector2(750, 1334);
        private static float curWindowScaleY = (windowScale.x / (float)Screen.width) * Screen.height;
        /// <summary>
        /// ��Ļ����ת��������
        /// </summary>
        /// <param name="screenPos"></param>
        /// <param name="camera"></param>
        /// <returns></returns>
        public static Vector3 ScreenToWorldPos(Vector2 screenPos, Camera camera) {
            return camera.ScreenToWorldPoint(screenPos);
        }
        #endregion
        #region ������ֵ
        /// <summary>
        /// ��a����ֵƽ�����䵽k���ȵ��б�
        /// </summary>
        /// <param name="k">�б���</param>
        /// <param name="a">���������</param>
        /// <returns></returns>
        public static List<int> AssignValues(int k, int a) {
            int count = k;
            // ������ֵ��ƽ�����䵽ÿ����
            int totalB = a / count;
            int remainder = a % count;

            List<int> classes = new List<int>();
            for (int i = 0; i < k; i++) {
                classes.Add(totalB);
            }

            // �����������������������bֵ��ֱ�������þ�
            for (int i = 0; i < count && remainder > 0; i++, remainder--) {
                classes[i] += 1;
            }

            return classes;
        }
        /// <summary>
        /// һ������sum��ֳɶ��������������Щ����������һ���б��С��������ص��б���������б�ĳ�����ͬ���б��е�ÿ��Ԫ�ض�Ӧһ������Ĳ�ֽ��
        /// </summary>
        /// <param name="sum">���������</param>
        /// <param name="rule">����Ĺ���</param>
        /// <returns></returns>
        public static List<int> AssignValues(int sum, List<int> rule) {
            List<int> value = new List<int>(new int[rule.Count]);
            for (int i = rule.Count - 1; i >= 0; i--) {
                int num = sum / rule[i];
                if (num == 0) {
                    if (rule[i] > sum) {
                        continue;
                    }
                    value[i] = sum;
                    break;
                }
                else {
                    value[i] = num;
                    sum %= rule[i];
                }
            }
            return value;
        }
        #endregion
        #region �Զ���Ƚ���
        /// <summary>
        /// ��С�Ƚ���
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public class IdComparer<T> : IComparer<T> where T : IIntClass {
            public int Compare(T x, T y) {
                return x.GetValue().CompareTo(y.GetValue());
            }
        }

        #endregion
        #region ���
        public static bool JudgeInCamera(Transform trans, Camera camera) {
            Vector3 pos = trans.position;
            //ת��Ϊ�ӽ�����
            Vector3 viewPos = camera.WorldToViewportPoint(pos);
            // z<0�������������
            if (viewPos.z < 0)
                return false;
            //̫Զ�ˣ��������ˣ�
            if (viewPos.z > camera.farClipPlane)
                return false;
            // x,yȡֵ�� 0~1֮��ʱ�������ӽǷ�Χ�⣻
            if (viewPos.x < 0 || viewPos.y < 0 || viewPos.x > 1 || viewPos.y > 1)
                return false;
            return true;
        }
        #endregion
        #region ����
        public static double FormulaStraight(int x, double init, double upRate) {
            return init + (x * upRate);
        }
        public static double FormulaIntervalLimitIncrease(int x, double min, double max, double rate) {
            return (x * max + rate * min) / (x + rate);
        }
        public static double FormulaIntervalLimitDecrease(int x, double min, double max, double rate) {
            return (x * min + rate * max) / (x + rate);
        }
        #endregion
        #region ����
        private static int CollectorNumMax = 5;

        public static double GetlockCost(int nestID) {
            int grindRate = nestID == 1 ? 1 : (nestID - 1) % (CollectorNumMax + 1);
            grindRate = grindRate == 0 ? (CollectorNumMax + 1) : grindRate;
            return (2 * (grindRate + 1)) * Mathf.Pow(10, nestID + 1);
        }
        public static double GetUpLvCost(int nestID, int lv) {
            int grindRate = nestID % (CollectorNumMax + 1);
            grindRate = grindRate == 0 ? (CollectorNumMax + 1) : grindRate;
            return (4 * grindRate * lv) * Mathf.Pow(10, nestID);
        }
        private static List<int> stepList = new List<int>() { 1, 5, 10, 15, 25 };
        private static List<float> stepSpeedList = new List<float>() { 0.55f, 1f, 1.7f, 2.7f, 4f };
        public static int GetStepLv(int lv, out int preLv, out int nextLv) {
            int lvStep = 0;
            preLv = 0;
            nextLv = 0;
            for (int i = 0; i < stepList.Count; i++) {
                nextLv = stepList[i];
                if (lv >= nextLv) {
                    preLv = nextLv;
                    lvStep = i + 1;
                }
                else {
                    break;
                }
            }
            return lvStep;
        }
        public static float GetStepSpeed(int stepLv) {
            if (stepLv > 0 && stepSpeedList.Count >= stepLv) {
                return stepSpeedList[stepLv - 1];
            }
            return 1;
        }
        private static int nextLv = 0;
        private static int stepLv = 0;
        private static int preLv = 0;
        private static float baseDistance = 2;
        private static float newProductTime = 2;
        public static double GetCapacityMoney(int nestID, int Lv) {
            stepLv = GetStepLv(Lv, out preLv, out nextLv);
            double productTime = baseDistance / GetStepSpeed(stepLv);
            double secondIncome = Mathf.Pow(10, nestID) * (10 + (7 * (Lv - 1))) / productTime;
            return Mathf.Round((float)secondIncome * newProductTime);
        }
        #endregion
    }

}

