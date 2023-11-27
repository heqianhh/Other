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
        // 创建一个随机数生成器
        static System.Random random = new System.Random();

        #region GameObject Component
        //GameObject上添加组件，如果组件存在直接返回
        static public T AddMissingComponent<T>(this GameObject go) where T : Component {
            T comp = go.GetComponent<T>();
            if (comp == null) {
                comp = go.AddComponent<T>();
            }
            return comp;
        }

        //Component上添加组件，如果组件存在直接返回
        static public T AddMissingComponent<T>(this Component _comp) where T : Component {
            T result = null;
            if (_comp != null) {
                result = _comp.gameObject.AddMissingComponent<T>();
            }
            return result;
        }
        //获取GameObject下的子物体组件
        static public T GetChildControl<T>(this GameObject _obj, string _target) where T : Component {
            Transform child = _obj.transform.Find(_target);
            if (child != null) {
                return child.GetComponent<T>();
            }
            return null;
        }
        /// <summary>
        /// 延迟显示物体
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
        //设置Text的值
        static public void SetText(this Text text, float str) {
            if (text != null) {
                text.text = str.ToString();
            }
        }

        //设置带参数的Text的值
        static public void SetTextFormat(this Text text, string str, params object[] objs) {
            if (text != null) {
                text.text = string.Format(str, objs);
            }
        }

        //设置翻译后的Text的值
        //static public void SetText(this Text text, string str) {
        //    if (text != null) {
        //        text.text = RefLanguage.GetValue(str);
        //    }
        //}

        //设置Text显示的日期值为 y-m-d 的格式
        static public void SetText(this Text text, DateTime str) {
            if (text != null) {
                text.text = string.Format("{0}-{1}-{2}", str.Year, str.Month, str.Day);
            }
        }
        #endregion
        #region 修改Transform的本地单个坐标
        //设置Transform的本地坐标的Y值
        static public void SetPosY(this Transform trans, float y) {
            trans.localPosition = new Vector3(trans.localPosition.x, y, trans.localPosition.z);
        }

        //设置Transform的本地坐标的X值
        static public void SetPosX(this Transform trans, float x) {
            trans.localPosition = new Vector3(x, trans.localPosition.y, trans.localPosition.z);
        }

        //设置Transform的本地坐标的X值
        static public void SetPosZ(this Transform trans, float z) {
            trans.localPosition = new Vector3(trans.localPosition.x, trans.localPosition.y, z);
        }

        //设置Transform的本地欧拉角的X值
        static public void SetRotX(this GameObject go, float angle) {
            go.transform.localEulerAngles = new Vector3(angle, go.transform.localEulerAngles.y, go.transform.localEulerAngles.z);
        }

        //设置Transform的本地欧拉角的Y值
        static public void SetRotY(this GameObject go, float angle) {
            go.transform.localEulerAngles = new Vector3(go.transform.localEulerAngles.x, angle, go.transform.localEulerAngles.z);
        }

        //设置Transform的本地欧拉角的Z值
        static public void SetRotZ(this GameObject go, float angle) {
            go.transform.localEulerAngles = new Vector3(go.transform.localEulerAngles.x, go.transform.localEulerAngles.y, angle);
        }
        #endregion
        #region 朝向角度
        public static Quaternion GetLookAtQua(this Transform originalObj, Vector3 targetPoint) {
            //计算物体在朝向某个向量后的正前方
            Vector3 forwardDir = targetPoint - originalObj.position;
            //计算朝向这个正前方时的物体四元数值
            Quaternion lookAtRot = Quaternion.LookRotation(forwardDir);
            return lookAtRot;
        }
        public static Quaternion GetLookAtQuaIgnoreY(this Transform originalObj, Vector3 targetPoint) {
            //计算物体在朝向某个向量后的正前方
            Vector3 forwardDir = targetPoint - originalObj.position;
            forwardDir.y = targetPoint.y;
            //计算朝向这个正前方时的物体四元数值
            Quaternion lookAtRot = Quaternion.LookRotation(forwardDir);
            return lookAtRot;
        }
        public static Quaternion GetLookAtQuaIgnoreYF(this Transform originalObj, Vector3 targetPoint) {
            //计算物体在朝向某个向量后的正前方
            Vector3 forwardDir = targetPoint - originalObj.position;
            forwardDir.y = targetPoint.y;
            //计算朝向这个正前方时的物体四元数值
            Quaternion lookAtRot = Quaternion.LookRotation(-forwardDir);
            return lookAtRot;
        }
        #endregion
        #region 图片和颜色
        //设置Image的图片
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

        //设置SpriteRenderer的图片
        static public void SetSprite(this SpriteRenderer sr, string spriteName, string atlasName = "UI") {
            if (sr == null)
                return;
            Sprite sprite = LocalAssetMgr.Instance.Load_UISprite(atlasName, spriteName);
            if (sprite == null) {
                Debug.LogError($"Sprite is null {atlasName}/{spriteName}");
            }
            sr.sprite = sprite;
        }

        //设置Image的alpha值
        static public void SetAlpha(this Image img, float a) {
            img.color = new Color(img.color.r, img.color.g, img.color.b, a);
        }

        //设置Text的alpha值
        static public void SetAlpha(this Text txt, float a) {
            txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, a);
        }

        //设置RawImage的alpha值
        static public void SetAlpha(this RawImage img, float a) {
            img.color = new Color(img.color.r, img.color.g, img.color.b, a);
        }

        //设置Image的RBG值
        static public void SetRGB(this Image img, Color color) {
            img.color = new Color(color.r, color.g, color.b, img.color.a);
        }

        //设置Text的RBG值
        static public void SetRGB(this Text txt, Color color) {
            txt.color = new Color(color.r, color.g, color.b, txt.color.a);
        }

        //设置Transform的本地缩放值
        static public void SetScale(this Transform trans, float scale) {
            trans.localScale = new Vector3(scale, scale, scale);
        }
        #endregion
        #region EventTrigger
        //EventTrigger组件监听
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

        //EventTrigger组件反监听
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
        /// int型列表求和
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
        /// 值是int型的字典求和
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
        /// 获得bool型列表第一个符合的值
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
        /// 从List中随机返回值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="list">需要随机取值的List</param>
        /// <returns>随机返回的值</returns>
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
        /// 从List中随机返回值并移除
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="list">需要随机取值的List</param>
        /// <returns>随机返回的值</returns>
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
        /// 从指定列表中随机获取指定数量的元素，并返回一个包含这些元素的新列表。
        /// </summary>
        /// <typeparam name="T">列表元素的类型。</typeparam>
        /// <param name="list">要从中获取元素的列表。</param>
        /// <param name="count">需要获取的元素的数量。</param>
        /// <returns>包含随机元素的新列表。</returns>
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
        /// 从List中根据权重随机返回值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="list">需要随机取值的List</param>
        /// <param name="weightList">权重List</param>
        /// <returns>根据权重随机处的值</returns>
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
        /// 反转权重
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
        /// 反转权重
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

            // 生成一个随机数作为索引
            int index = random.Next(dict.Count);

            // 获取指定索引的元素
            KeyValuePair<TKey, TValue> item = dict.ElementAt(index);

            // 返回结果
            return item;
        }
        /// <summary>
        /// 排序，从小到大
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputList"></param>
        public static void SortList<T>(this List<T> inputList) where T : IIntClass {
            inputList.Sort(new IdComparer<T>());
        }
        /// <summary>
        ///这是一种使用双指针算法对整数列表进行排序的方法。首先，获取列表长度并计算中间位置（如果列表长度为偶数，取中间两个值的平均数向下取整作为中间位置）。然后，创建一个新的空列表作为输出结果。
        ///接下来，设置两个指针分别指向列表的第一个和最后一个元素，并通过比较左右指针所指元素与中间位置之间的差的绝对值大小来选择更靠近中间位置的元素，并将其添加到输出列表中。每次选择后，相应的指针会向内移动一位，直到两个指针在中间位置相遇。
        ///最后，返回输出列表以完成排序。
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
            // 双指针遍历列表
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
        /// 排序，越中部越大
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputList"></param>
        public static void SortListMid<T>(this List<T> inputList) where T : IIntClass {
            inputList.Sort(new IdComparer<T>());
            inputList.SortListForMid();
        }
        /// <summary>
        /// 打乱列表
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
        /// 将a均分给类的b
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="classes"></param>
        /// <param name="a"></param>
        public static void AssignValues<T>(this List<T> classes, int a) where T : IIntClass {
            int count = classes.Count;

            // 计算总值并平均分配到每个类
            int totalB = a / count;
            int remainder = a % count;

            // 如果有余数，则逐个类递增其b值，直到余数用尽
            for (int i = 0; i < count && remainder > 0; i++, remainder--) {
                classes[i].SetValue(totalB + 1 + classes[i].GetValue());
            }

            // 将平均值分配给剩余的类
            for (int i = remainder; i < count; i++) {
                classes[i].SetValue(totalB + classes[i].GetValue());
            }
        }
        /// <summary>
        /// 去除末尾的0
        /// </summary>
        /// <param name="list"></param>
        public static void RemoveEndZero(this List<int> list) {
            while (list.Count > 0 && list[list.Count - 1] == 0) {
                list.RemoveAt(list.Count - 1);
            }
        }

        public static Color GetMostCommonColor(this List<Color> colors) {
            Dictionary<Color, int> colorCount = new Dictionary<Color, int>();

            // 计算每种颜色在列表中出现的次数
            foreach (Color color in colors) {
                if (colorCount.ContainsKey(color)) {
                    colorCount[color]++;
                }
                else {
                    colorCount[color] = 1;
                }
            }

            // 查找出现次数最多的颜色
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
        #region 坐标相关
        /// <summary>
        /// 获取最近坐标
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="posList">坐标列表</param>
        /// <param name="pos">对比坐标</param>
        /// <param name="minLen">限制的距离</param>
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
        /// 获取最近坐标
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="posList">坐标列表</param>
        /// <param name="pos">对比坐标</param>
        /// <param name="num">数量</param>
        /// <param name="minLen">限制的距离</param>
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
        #region 方向相关
        public static Vector3 GetRandomVertical(this Vector3 rot) {
            return Vector3.Cross(rot, Random.insideUnitSphere).normalized;
        }
        public static Vector3 GetRandomVerticalUp(this Vector3 referenceDirection) {
            // 将随机方向向量与参考方向向量叉乘，得到与参考方向垂直的向量
            Vector3 perpendicularDirection = Vector3.Cross(referenceDirection, Random.insideUnitSphere).normalized;

            // 保留向上方向的部分
            Vector3 upDirection = perpendicularDirection;
            if (Vector3.Dot(perpendicularDirection, Vector3.up) < 0) {
                upDirection = -perpendicularDirection;
            }

            return upDirection;
        }
        #endregion
        #region 时间格式转换
        /// <summary>
        /// 将秒格式为{min}:{second}格式
        /// </summary>
        /// <param name="_second">需要转化的秒</param>
        /// <param name="containZero">是否显示0点</param>
        /// <returns>格式后的时间值</returns>
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
        #region 类型转换
        /// <summary>
        /// Color转16进制
        /// </summary>
        /// <param name="color">颜色</param>
        /// <returns>16进制的值</returns>
        static public string ColorToHex(this Color color) {
            return ColorUtility.ToHtmlStringRGB(color);
        }

        /// <summary>
        /// 16进制转Color
        /// </summary>
        /// <param name="hex">16进制的值例如 "#DE66FFFF"</param>
        /// <returns>颜色</returns>
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
        /// 获取uv点y轴坐标的最大值
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
        /// 获取物体上的mesh组件
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



