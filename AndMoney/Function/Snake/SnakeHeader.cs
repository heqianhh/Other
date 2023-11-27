using System.Collections.Generic;
using UnityEngine;

public class SnakeHeader {
    Transform transF;
    GameObject ObjGo;
    Transform transform;
    public float snakeSpeed;
    // 记录位置信息
    LinkedList<PathItem> previousPositions = new LinkedList<PathItem>();
    // 记录蛇身节点
    List<SnakeBody> m_SnakeBodys = new List<SnakeBody>();
    // 固定的距离
    float CONST_LENGTH = 0.85f;

    private float m_PreDistance = 0.0f;
    private bool m_ConsumeState = false;
    private bool removeFirst = false;
    GameObject SankeBodyGa;//蛇身预制体
    Transform SankeHeadTran;//蛇头父物体
    public bool ConsumeState//设置 是否移动
    {
        set {
            if (value) {
                if (m_ConsumeState == false) {
                    m_PreDistance = CONST_LENGTH;
                }
            }
            m_ConsumeState = value;
        }
        get {
            return m_ConsumeState;
        }
    }
    public void OnStart(Transform transF) {
        this.transF = transF;
        SankeHeadTran = new GameObject("Snake").transform;
        ObjGo = ObjectPool.Instance.Get("Player", "Snake");
        transform = ObjGo.transform;
        transform.parent = SankeHeadTran;
        SankeBodyGa = ObjGo;
        transform.position = transF.position;
        this.RecordPosition(transform, true);
    }

    public void OnUpdate() {
        transform.position = transF.position;
        transform.rotation = transF.rotation;
        if (!m_ConsumeState) {
            this.RecordPosition(transform);
        }
        else {
            /// 消耗状态下可以移动头部的特殊处理
            var preNodeInfo = this.previousPositions.First;
            var dis = Vector3.Distance(preNodeInfo.Value.position, transform.position);
            if (dis > 0.1f) {
                // 1. 必须增加头部节点的记录
                this.RecordPosition(transform);
                // 2. 维护身体移动相关变量
                //m_PreDistance += dis;
                if (m_PreDistance > CONST_LENGTH) {
                    m_PreDistance = CONST_LENGTH;
                }
            }
        }
        this.followHead(m_SnakeBodys, previousPositions, m_ConsumeState);
    }

    //排序蛇身
    void followHead(List<SnakeBody> tail, LinkedList<PathItem> path, bool isConsumeState) {
        // 包含父亲节点的路径的起点
        LinkedListNode<PathItem> startNode = path.First;
        // 父节点距离其路径片段的尾部的距离
        float distanceToEnd = startNode.Value.distance;
        removeFirst = false;
        int count = 0;
        foreach (SnakeBody body in tail) {

            bool isFirstBody = (count == 0);
            count++;
            //计算 body 位置
            if (startNode.Next == null) {
                // 已经是尾巴了
                break;
            }
            // 距离不足，无法设置位置
            var DISTANCE = CONST_LENGTH;
            if (isConsumeState && isFirstBody) {
                DISTANCE = m_PreDistance;
            }

            while (distanceToEnd < DISTANCE) {
                if (startNode.Next.Next == null) {
                    return;
                }
                startNode = startNode.Next;
                distanceToEnd = distanceToEnd + startNode.Value.distance;
            }

            if (distanceToEnd >= DISTANCE) {
                // body 应该在片段 startNode 开头的线段上
                var ds = distanceToEnd - DISTANCE;
                var rate = 1.0f - ds / startNode.Value.distance;
                var pos = Vector3.LerpUnclamped(startNode.Value.position, startNode.Next.Value.position, rate);
                var rot = Quaternion.LerpUnclamped(startNode.Value.rotation, startNode.Next.Value.rotation, rate);
                body.transform.position = new Vector3(pos.x, pos.y, pos.z);
                body.transform.rotation = rot;
                distanceToEnd = distanceToEnd - DISTANCE;
            }
            if (isConsumeState && isFirstBody) {
                m_PreDistance -= snakeSpeed;

                if (m_PreDistance < 0.001f) {

                    m_PreDistance = CONST_LENGTH;
                    // 标记移除第一个；
                    removeFirst = true;

                }
            }
        }


        while (startNode.Next != path.Last) {
            path.RemoveLast();
            path.Last.Value.distance = 0;
        }

    }

    //计入蛇身体位置 
    LinkedListNode<PathItem> RecordPosition(Transform pos, bool first = false) {
        if (first) {
            var item = new PathItem();
            item.position = pos.position;
            item.rotation = pos.rotation;
            item.distance = 0;
            return this.previousPositions.AddFirst(item);
        }
        else {
            var preItem = this.previousPositions.First.Value;
            var item = new PathItem();
            item.position = pos.position;
            item.rotation = pos.rotation;
            item.distance = Vector3.Distance(preItem.position, item.position);
            return this.previousPositions.AddFirst(item);
        }
    }
    /// <summary>
    /// 删除蛇身
    /// </summary>
    public void DesSnakeBody() {
        if (m_SnakeBodys.Count > 0) {
            var item = m_SnakeBodys[0];
            m_SnakeBodys.RemoveAt(0);
            GameObject.Destroy(item.gameObject);
        }

    }
    // 获取最后一个节点的位置，作为新的节点的初始位置
    Vector3 GetLastTailPosition() {
        if (m_SnakeBodys.Count == 0) {
            return this.transform.position;
        }
        else {
            return m_SnakeBodys[m_SnakeBodys.Count - 1].transform.position;
        }
    }
    // 添加一个蛇身
    public void AppendSnakeBody() {
        // 放在尾部节点位置放蛇的 body
        var lastPos = this.GetLastTailPosition();
        var gameObj = GameObject.Instantiate(SankeBodyGa);

        gameObj.transform.parent = this.SankeHeadTran.transform;//lastPos
        gameObj.transform.position = new Vector3(lastPos.x, lastPos.y, lastPos.z);
        gameObj.transform.rotation = transform.rotation;
        var body = gameObj.AddMissingComponent<SnakeBody>();
        this.m_SnakeBodys.Add(body);
    }
    public Transform GetSnakeTrans(int ID) {
        if (ID == 0) {
            return transform;
        }
        else {
            if (m_SnakeBodys.Count - 1 >= ID) {
                return m_SnakeBodys[ID - 1].transform;
            }
            else {
                int offset = ID - (m_SnakeBodys.Count - 1);
                for (int i = 0; i < offset; i++) {
                    AppendSnakeBody();
                }
                return m_SnakeBodys[ID - 1].transform;
            }
        }
    }
}
public class SnakeBody : MonoBehaviour {

   

}
class PathItem {
    public Vector3 position;
    public Quaternion rotation;
    public float distance;
}
