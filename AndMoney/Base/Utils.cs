using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static AndMoney.EMath;
using Random = UnityEngine.Random;

namespace AndMoney {
    static public partial class Utils {
        // ����һ�������������
        static System.Random random = new System.Random();

        #region GameObject Component
        //GameObject��������������������ֱ�ӷ���
        static public T AddMissingComponent<T>(this GameObject go) where T : Component {
            T comp = go.GetComponent<T>();
            if (comp == null) {
                comp = go.AddComponent<T>();
            }
            return comp;
        }

        //Component��������������������ֱ�ӷ���
        static public T AddMissingComponent<T>(this Component _comp) where T : Component {
            T result = null;
            if (_comp != null) {
                result = _comp.gameObject.AddMissingComponent<T>();
            }
            return result;
        }
        //��ȡGameObject�µ����������
        static public T GetChildControl<T>(this GameObject _obj, string _target) where T : Component {
            Transform child = _obj.transform.Find(_target);
            if (child != null) {
                return child.GetComponent<T>();
            }
            return null;
        }
        /// <summary>
        /// �ӳ���ʾ����
        /// </summary>
        /// <param name="go"></param>
        /// <param name="time"></param>
        static public void DelayShow(this GameObject go, float time = 1f) {
            go.SetActive(false);
            IEnumerator show(float time) {
                yield return new WaitForSeconds(time);
                go.SetActive(true);
            }
            CoDelegator.Coroutine(show(time));

        }
        #endregion
        #region Text
        //����Text��ֵ
        static public void SetText(this Text text, float str) {
            if (text != null) {
                text.text = str.ToString();
            }
        }

        //���ô�������Text��ֵ
        static public void SetTextFormat(this Text text, string str, params object[] objs) {
            if (text != null) {
                text.text = string.Format(str, objs);
            }
        }

        //���÷�����Text��ֵ
        //static public void SetText(this Text text, string str) {
        //    if (text != null) {
        //        text.text = RefLanguage.GetValue(str);
        //    }
        //}

        //����Text��ʾ������ֵΪ y-m-d �ĸ�ʽ
        static public void SetText(this Text text, DateTime str) {
            if (text != null) {
                text.text = string.Format("{0}-{1}-{2}", str.Year, str.Month, str.Day);
            }
        }
        #endregion
        #region �޸�Transform�ı��ص�������
        //����Transform�ı��������Yֵ
        static public void SetPosY(this Transform trans, float y) {
            trans.localPosition = new Vector3(trans.localPosition.x, y, trans.localPosition.z);
        }

        //����Transform�ı��������Xֵ
        static public void SetPosX(this Transform trans, float x) {
            trans.localPosition = new Vector3(x, trans.localPosition.y, trans.localPosition.z);
        }

        //����Transform�ı��������Xֵ
        static public void SetPosZ(this Transform trans, float z) {
            trans.localPosition = new Vector3(trans.localPosition.x, trans.localPosition.y, z);
        }

        //����Transform�ı���ŷ���ǵ�Xֵ
        static public void SetRotX(this GameObject go, float angle) {
            go.transform.localEulerAngles = new Vector3(angle, go.transform.localEulerAngles.y, go.transform.localEulerAngles.z);
        }

        //����Transform�ı���ŷ���ǵ�Yֵ
        static public void SetRotY(this GameObject go, float angle) {
            go.transform.localEulerAngles = new Vector3(go.transform.localEulerAngles.x, angle, go.transform.localEulerAngles.z);
        }

        //����Transform�ı���ŷ���ǵ�Zֵ
        static public void SetRotZ(this GameObject go, float angle) {
            go.transform.localEulerAngles = new Vector3(go.transform.localEulerAngles.x, go.transform.localEulerAngles.y, angle);
        }
        #endregion
        #region ����Ƕ�
        public static Quaternion GetLookAtQua(this Transform originalObj, Vector3 targetPoint) {
            //���������ڳ���ĳ�����������ǰ��
            Vector3 forwardDir = targetPoint - originalObj.position;
            //���㳯�������ǰ��ʱ��������Ԫ��ֵ
            Quaternion lookAtRot = Quaternion.LookRotation(forwardDir);
            return lookAtRot;
        }
        public static Quaternion GetLookAtQuaIgnoreY(this Transform originalObj, Vector3 targetPoint) {
            //���������ڳ���ĳ�����������ǰ��
            Vector3 forwardDir = targetPoint - originalObj.position;
            forwardDir.y = targetPoint.y;
            //���㳯�������ǰ��ʱ��������Ԫ��ֵ
            Quaternion lookAtRot = Quaternion.LookRotation(forwardDir);
            return lookAtRot;
        }
        public static Quaternion GetLookAtQuaIgnoreYF(this Transform originalObj, Vector3 targetPoint) {
            //���������ڳ���ĳ�����������ǰ��
            Vector3 forwardDir = targetPoint - originalObj.position;
            forwardDir.y = targetPoint.y;
            //���㳯�������ǰ��ʱ��������Ԫ��ֵ
            Quaternion lookAtRot = Quaternion.LookRotation(-forwardDir);
            return lookAtRot;
        }
        #endregion
        #region ͼƬ����ɫ
        //����Image��ͼƬ
        static public void SetSprite(this Image image, string spriteName, string atlasName = "UI", bool setNative = false) {
            if (image == null)
                return;
            Sprite sprite = LocalAssetMgr.Instance.Load_UISprite(atlasName, spriteName);
            if (sprite == null) {
                Debug.LogError($"Sprite is null {atlasName}/{spriteName}");
            }
            image.sprite = sprite;

            if (setNative) {
                image.SetNativeSize();
            }
        }

        //����SpriteRenderer��ͼƬ
        static public void SetSprite(this SpriteRenderer sr, string spriteName, string atlasName = "UI") {
            if (sr == null)
                return;
            Sprite sprite = LocalAssetMgr.Instance.Load_UISprite(atlasName, spriteName);
            if (sprite == null) {
                Debug.LogError($"Sprite is null {atlasName}/{spriteName}");
            }
            sr.sprite = sprite;
        }

        //����Image��alphaֵ
        static public void SetAlpha(this Image img, float a) {
            img.color = new Color(img.color.r, img.color.g, img.color.b, a);
        }

        //����Text��alphaֵ
        static public void SetAlpha(this Text txt, float a) {
            txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, a);
        }

        //����RawImage��alphaֵ
        static public void SetAlpha(this RawImage img, float a) {
            img.color = new Color(img.color.r, img.color.g, img.color.b, a);
        }

        //����Image��RBGֵ
        static public void SetRGB(this Image img, Color color) {
            img.color = new Color(color.r, color.g, color.b, img.color.a);
        }

        //����Text��RBGֵ
        static public void SetRGB(this Text txt, Color color) {
            txt.color = new Color(color.r, color.g, color.b, txt.color.a);
        }

        //����Transform�ı�������ֵ
        static public void SetScale(this Transform trans, float scale) {
            trans.localScale = new Vector3(scale, scale, scale);
        }
        #endregion
        #region EventTrigger
        //EventTrigger�������
        static public void AddListener(this EventTrigger eventTrigger, EventTriggerType eventType, UnityAction<BaseEventData> action) {
            for (int index = 0; index < eventTrigger.triggers.Count; index++) {
                if (eventTrigger.triggers[index].eventID == eventType) {
                    eventTrigger.triggers[index].callback.AddListener(action);
                    return;
                }
            }
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = eventType;
            entry.callback.AddListener(action);
            eventTrigger.triggers.Add(entry);
        }

        //EventTrigger���������
        static public void RemoveListener(this EventTrigger eventTrigger, EventTriggerType eventType, UnityAction<BaseEventData> action) {
            for (int index = 0; index < eventTrigger.triggers.Count; index++) {
                if (eventTrigger.triggers[index].eventID == eventType) {
                    eventTrigger.triggers[index].callback.RemoveListener(action);
                }
            }
        }
        #endregion
        #region List&Dic
        /// <summary>
        /// int���б����
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        static public int SumValue(this List<int> list) {
            int sum = 0;
            foreach (var temp in list) {
                sum += temp;
            }
            return sum;
        }



        /// <summary>
        /// ֵ��int�͵��ֵ����
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        static public int SumValue<T>(this Dictionary<T, int> dic) {
            int sum = 0;
            foreach (var temp in dic) {
                sum += temp.Value;
            }
            return sum;
        }
        static public int MaxValue(this List<int> list) {
            int maxValue = int.MinValue;
            foreach (var temp in list) {
                if (maxValue < temp) {
                    maxValue = temp;
                }
            }
            return maxValue;
        }

        static public int MinValue(this List<int> list) {
            int minValue = int.MaxValue;
            foreach (var temp in list) {
                if (minValue > temp) {
                    minValue = temp;
                }
            }
            return minValue;
        }

        /// <summary>
        /// ���bool���б��һ�����ϵ�ֵ
        /// </summary>
        /// <param name="list"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        static public int GetFirst(this List<bool> list, bool value = false) {
            if (list.Count == 0) {
                return -1;
            }
            for (int i = 0; i < list.Count; i++) {
                if (list[i] == value) {
                    return i;
                }
            }
            return -1;
        }

        static public void InitList(this List<bool> list, bool value = false) {
            for (int i = 0; i < list.Count; i++) {
                list[i] = value;
            }
        }
        /// <summary>
        /// ��List���������ֵ
        /// </summary>
        /// <typeparam name="T">����</typeparam>
        /// <param name="list">��Ҫ���ȡֵ��List</param>
        /// <returns>������ص�ֵ</returns>
        static public T RandomValue<T>(this List<T> list) {
            if (list.Count == 0) {
                Debug.LogError("get random list is null");
                return default(T);
            }
            int index = Random.Range(0, list.Count);
            T result = list[index];
            return result;
        }
        static public T RandomValue<T>(this T[] list) {
            if (list.Length == 0) {
                Debug.LogError("get random list is null");
                return default(T);
            }
            int index = Random.Range(0, list.Length);
            T result = list[index];
            return result;
        }
        /// <summary>
        /// ��List���������ֵ���Ƴ�
        /// </summary>
        /// <typeparam name="T">����</typeparam>
        /// <param name="list">��Ҫ���ȡֵ��List</param>
        /// <returns>������ص�ֵ</returns>
        static public T RandomAndRemove<T>(this List<T> list) {
            if (list.Count == 0) {
                Debug.LogError("get random list is null");
                return default(T);
            }
            int index = Random.Range(0, list.Count);
            T result = list[index];
            list.RemoveAt(index);
            return result;
        }
        /// <summary>
        /// ��ָ���б��������ȡָ��������Ԫ�أ�������һ��������ЩԪ�ص����б�
        /// </summary>
        /// <typeparam name="T">�б�Ԫ�ص����͡�</typeparam>
        /// <param name="list">Ҫ���л�ȡԪ�ص��б�</param>
        /// <param name="count">��Ҫ��ȡ��Ԫ�ص�������</param>
        /// <returns>�������Ԫ�ص����б�</returns>
        public static List<T> GetRandomList<T>(this List<T> list, int count) {
            if (list.Count <= count) {
                return list;
            }
            List<T> result = new List<T>();
            HashSet<T> hashSet = new HashSet<T>();
            while (result.Count < count) {
                T randomValue = list.RandomValue();
                if (!hashSet.Contains(randomValue)) {
                    hashSet.Add(randomValue);
                    result.Add(randomValue);
                }
            }
            return result;
        }
        public static List<T> GetRandomList<T>(this T[] list, int count) {
            if (list.Length <= count) {
                return list.ToList();
            }
            List<T> result = new List<T>();
            HashSet<T> hashSet = new HashSet<T>();
            while (result.Count < count) {
                T randomValue = list.RandomValue();
                if (!hashSet.Contains(randomValue)) {
                    hashSet.Add(randomValue);
                    result.Add(randomValue);
                }
            }
            return result;
        }
        /// <summary>
        /// ��List�и���Ȩ���������ֵ
        /// </summary>
        /// <typeparam name="T">����</typeparam>
        /// <param name="list">��Ҫ���ȡֵ��List</param>
        /// <param name="weightList">Ȩ��List</param>
        /// <returns>����Ȩ���������ֵ</returns>
        static public T RandomWithWeight<T>(this List<T> list, List<int> weightList) {
            if (list.Count == 0) {
                Debug.LogError("get random list is null");
                return default(T);
            }
            if (list.Count == 1) {
                return list[0];
            }
            if (list.Count != weightList.Count) {
                Debug.LogError("list and weight count is unique");
                return list[0];
            }

            T result = list[0];
            int totalWeight = weightList.Sum();
            int randomWeight = Random.Range(0, totalWeight);

            int curTotalWeight = 0;
            for (int index = 0; index < list.Count; index++) {
                int curWeight = weightList[index];
                curTotalWeight += curWeight;
                if (curTotalWeight > randomWeight) {
                    return list[index];
                }
            }

            return result;
        }
        /// <summary>
        /// ��תȨ��
        /// </summary>
        /// <param name="inputList"></param>
        static public void AdjustWeights(this List<float> inputList) {
            if (inputList.Count == 0 || inputList.Count == 1) {
                return;
            }
            float min = inputList[0];
            float max = min;

            for (int i = 0; i < inputList.Count; i++) {
                float value = inputList[i];
                if (value < min) {
                    min = value;
                }
                if (value > max) {
                    max = value;
                }
            }
            if (max == min) {
                return;
            }

            List<float> rate = new List<float>();
            for (int i = 0; i < inputList.Count; i++) {
                rate.Add(1 - (inputList[i] - min) / (max - min));
            }
            for (int i = 0; i < inputList.Count; i++) {
                inputList[i] = min + (max - min) * rate[i];
            }
        }
        /// <summary>
        /// ��תȨ��
        /// </summary>
        /// <param name="inputList"></param>
        static public void AdjustWeights(this List<int> inputList) {
            List<float> temp = inputList.Select(i => (float)i).ToList();
            temp.AdjustWeights();
            List<int> end = temp.Select(i => (int)i).ToList();
            for (int i = 0; i < inputList.Count; i++) {
                inputList[i] = end[i];
            }
        }

        public static KeyValuePair<TKey, TValue> GetRandomDictionaryItem<TKey, TValue>(this Dictionary<TKey, TValue> dict) {

            // ����һ���������Ϊ����
            int index = random.Next(dict.Count);

            // ��ȡָ��������Ԫ��
            KeyValuePair<TKey, TValue> item = dict.ElementAt(index);

            // ���ؽ��
            return item;
        }
        /// <summary>
        /// ���򣬴�С����
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputList"></param>
        public static void SortList<T>(this List<T> inputList) where T : IIntClass {
            inputList.Sort(new IdComparer<T>());
        }
        /// <summary>
        ///����һ��ʹ��˫ָ���㷨�������б��������ķ��������ȣ���ȡ�б��Ȳ������м�λ�ã�����б���Ϊż����ȡ�м�����ֵ��ƽ��������ȡ����Ϊ�м�λ�ã���Ȼ�󣬴���һ���µĿ��б���Ϊ��������
        ///����������������ָ��ֱ�ָ���б�ĵ�һ�������һ��Ԫ�أ���ͨ���Ƚ�����ָ����ָԪ�����м�λ��֮��Ĳ�ľ���ֵ��С��ѡ��������м�λ�õ�Ԫ�أ���������ӵ�����б��С�ÿ��ѡ�����Ӧ��ָ��������ƶ�һλ��ֱ������ָ�����м�λ��������
        ///��󣬷�������б����������
        /// </summary>
        /// <param name="inputList"></param>
        /// <returns></returns>
        private static void SortListForMid<T>(this List<T> inputList) where T : IIntClass {
            if (inputList.Count <= 2) {
                return;
            }
            int min = 0;
            int max = inputList.Count - 1;
            int left = min;
            int right = max;

            List<T> outputList = new List<T>(inputList);
            int first = 0;
            int second = 1;
            // ˫ָ������б�
            while (left <= right) {
                outputList[left] = inputList[first];
                outputList[right] = inputList[second];

                first = Mathf.Clamp(first + 2, min, max);
                second = Mathf.Clamp(second + 2, min, max);

                right--;
                left++;

            }
            for (int i = 0; i < outputList.Count; i++) {
                inputList[i] = outputList[i];
            }
        }
        /// <summary>
        /// ����Խ�в�Խ��
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputList"></param>
        public static void SortListMid<T>(this List<T> inputList) where T : IIntClass {
            inputList.Sort(new IdComparer<T>());
            inputList.SortListForMid();
        }
        /// <summary>
        /// �����б�
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void Shuffle<T>(this List<T> list) {
            int n = list.Count;
            while (n > 1) {
                int k = Random.Range(0, n--);
                T temp = list[n];
                list[n] = list[k];
                list[k] = temp;
            }
        }
        /// <summary>
        /// ��a���ָ����b
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="classes"></param>
        /// <param name="a"></param>
        public static void AssignValues<T>(this List<T> classes, int a) where T : IIntClass {
            int count = classes.Count;

            // ������ֵ��ƽ�����䵽ÿ����
            int totalB = a / count;
            int remainder = a % count;

            // �����������������������bֵ��ֱ�������þ�
            for (int i = 0; i < count && remainder > 0; i++, remainder--) {
                classes[i].SetValue(totalB + 1 + classes[i].GetValue());
            }

            // ��ƽ��ֵ�����ʣ�����
            for (int i = remainder; i < count; i++) {
                classes[i].SetValue(totalB + classes[i].GetValue());
            }
        }
        /// <summary>
        /// ȥ��ĩβ��0
        /// </summary>
        /// <param name="list"></param>
        public static void RemoveEndZero(this List<int> list) {
            while (list.Count > 0 && list[list.Count - 1] == 0) {
                list.RemoveAt(list.Count - 1);
            }
        }

        public static Color GetMostCommonColor(this List<Color> colors) {
            Dictionary<Color, int> colorCount = new Dictionary<Color, int>();

            // ����ÿ����ɫ���б��г��ֵĴ���
            foreach (Color color in colors) {
                if (colorCount.ContainsKey(color)) {
                    colorCount[color]++;
                }
                else {
                    colorCount[color] = 1;
                }
            }

            // ���ҳ��ִ���������ɫ
            int maxCount = 0;
            Color mostCommonColor = Color.white;
            foreach (KeyValuePair<Color, int> pair in colorCount) {
                if (pair.Value > maxCount) {
                    maxCount = pair.Value;
                    mostCommonColor = pair.Key;
                }
            }

            return mostCommonColor;
        }
        public static Transform NearOnce(this List<Transform> listTrans, Transform transAim) {
            float minlen = Mathf.Infinity;
            Transform transMin = null;
            foreach (var temp in listTrans) {

                float dis = Vector3.Distance(temp.position, transAim.position);
                if (dis < minlen) {
                    minlen = dis;
                    transMin = temp;
                }
            }
            return transMin;
        }
        #endregion
        #region �������
        /// <summary>
        /// ��ȡ�������
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="posList">�����б�</param>
        /// <param name="pos">�Ա�����</param>
        /// <param name="minLen">���Ƶľ���</param>
        /// <returns></returns>
        public static T GetNearPos<T>(this List<T> posList, Vector3 pos, float minLen = Mathf.Infinity) where T : IVector3Class {
            T nearest = default;
            float minDistance = Mathf.Infinity;
            foreach (T item in posList) {
                float distance = Vector3.Distance(item.GetPos(), pos);
                if (distance >= minLen) {
                    continue;
                }
                if (distance < minDistance) {
                    minDistance = distance;
                    nearest = item;
                }
            }
            return nearest;
        }
        /// <summary>
        /// ��ȡ�������
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="posList">�����б�</param>
        /// <param name="pos">�Ա�����</param>
        /// <param name="num">����</param>
        /// <param name="minLen">���Ƶľ���</param>
        /// <returns></returns>
        public static List<T> GetNearPos<T>(this List<T> posList, Vector3 pos, int num, float minLen = Mathf.Infinity) where T : IVector3Class {
            List<T> nearestList = new List<T>();
            SortedList<float, T> sortedList = new SortedList<float, T>();

            foreach (T item in posList) {
                float distance = Vector3.Distance(item.GetPos(), pos);
                if (distance >= minLen) {
                    continue;
                }
                sortedList.Add(distance, item);
            }

            for (int i = 0; i < Mathf.Min(num, sortedList.Count); i++) {
                nearestList.Add(sortedList.Values[i]);
            }

            return nearestList;
        }

        #endregion
        #region �������
        public static Vector3 GetRandomVertical(this Vector3 rot) {
            return Vector3.Cross(rot, Random.insideUnitSphere).normalized;
        }
        public static Vector3 GetRandomVerticalUp(this Vector3 referenceDirection) {
            // ���������������ο�����������ˣ��õ���ο�����ֱ������
            Vector3 perpendicularDirection = Vector3.Cross(referenceDirection, Random.insideUnitSphere).normalized;

            // �������Ϸ���Ĳ���
            Vector3 upDirection = perpendicularDirection;
            if (Vector3.Dot(perpendicularDirection, Vector3.up) < 0) {
                upDirection = -perpendicularDirection;
            }

            return upDirection;
        }
        #endregion
        #region ʱ���ʽת��
        /// <summary>
        /// �����ʽΪ{min}:{second}��ʽ
        /// </summary>
        /// <param name="_second">��Ҫת������</param>
        /// <param name="containZero">�Ƿ���ʾ0��</param>
        /// <returns>��ʽ���ʱ��ֵ</returns>
        static public string GetTimeDesc(this float _second, bool containZero = true) {
            if (_second == 0 && containZero == false) {
                return "--:--";
            }
            string desc = "";
            int second = (int)_second;
            int min = second / 60;
            second = second % 60;
            desc = string.Format("{0}:{1}", min < 10 ? ("0" + min) : min.ToString(),
                second < 10 ? ("0" + second) : second.ToString());
            return desc;
        }
        #endregion
        #region ����ת��
        /// <summary>
        /// Colorת16����
        /// </summary>
        /// <param name="color">��ɫ</param>
        /// <returns>16���Ƶ�ֵ</returns>
        static public string ColorToHex(this Color color) {
            return ColorUtility.ToHtmlStringRGB(color);
        }

        /// <summary>
        /// 16����תColor
        /// </summary>
        /// <param name="hex">16���Ƶ�ֵ���� "#DE66FFFF"</param>
        /// <returns>��ɫ</returns>
        static public Color HexToColor(this string hex) {
            Color color;
            if (!ColorUtility.TryParseHtmlString(hex, out color)) {
                Debug.LogError($"Hex {hex} To Color Failed!");
            }
            return color;
        }
        #endregion
        #region UV
        /// <summary>
        /// ��ȡuv��y����������ֵ
        /// </summary>
        /// <param name="mesh"></param>
        /// <returns></returns>
        static public float GetMeshUVHighest(Mesh mesh) {
            Vector2[] vectors = mesh.uv;
            float highest = vectors[0].y;
            foreach (Vector2 temp in vectors) {
                if (highest < temp.y) {
                    highest = temp.y;
                }
            }
            return highest;
        }
        /// <summary>
        /// ��ȡ�����ϵ�mesh���
        /// </summary>
        /// <param name="go"></param>
        /// <returns></returns>
        static public Mesh GetMesh(this Transform go) {
            Mesh curMesh = null;
            if (go) {
                MeshFilter curFilter = go.GetComponent<MeshFilter>();
                SkinnedMeshRenderer curSkinnned = go.GetComponent<SkinnedMeshRenderer>();
                if (curFilter && !curSkinnned) {
                    curMesh = curFilter.sharedMesh;
                }
                if (!curFilter && curSkinnned) {
                    curMesh = curSkinnned.sharedMesh;
                }
            }
            return curMesh;
        }
        #endregion
    }
}



