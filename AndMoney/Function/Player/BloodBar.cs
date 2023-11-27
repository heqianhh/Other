using AndMoney;
using UnityEngine;
using UnityEngine.UI;

namespace AndMoney.SimpleView {
    public abstract class BloodBar<T> {
        public T barCdt;
        public GameObject ObjGo;
        public Transform transObj;
        Transform transAim;
        float offset = 0f;
        public BloodBar() {
        }

        public virtual void SetData(T bloodBarCdt, Transform transAim, float offset = 0f) {
            this.barCdt = bloodBarCdt;
            this.transAim = transAim;
            this.offset = offset;
            if (transObj == null) {
                transObj = GetModel();
            }
            ObjGo = transObj.gameObject;
            //spriteRenderer = ObjGo.GetChildControl<SpriteRenderer>("Square (1)");
            transObj.position = transAim.position + new Vector3(0f, offset, 0f);
            transObj.gameObject.SetActive(false);
        }
        public virtual Transform GetModel() {
            switch (barCdt) {

            }
            return null; 
        }
        public void ClearData() {
            if (transObj != null) {
                ObjectPool.Instance.Recycle(ObjGo);
                transObj = null;
            }
        }
        public virtual void SetFillAmount(float value) {
            //spriteRenderer.transform.SetPosX(EMath.Map(0f, 1f, -0.6f, 0f, value));
            //spriteRenderer.size = new Vector2(EMath.Map(0f, 1f, 0.1f, 10f, value), spriteRenderer.size.y);
        }

        public void Update(bool show = true) {
            if (!show) {
                return;
            }
            if (transObj != null && transAim != null) {
                transObj.position = transAim.position + new Vector3(0f, offset, 0f);
                transObj.gameObject.SetActive(show);
            }
        }
    }

    public class BloodBarImg<T> : BloodBar<T> {
        public Image image;
        public override void SetData(T bloodBarCdt, Transform transAim, float offset = 0) {
            base.SetData(bloodBarCdt, transAim, offset);
            SetImage();
        }
        public virtual void SetImage() {
            image = ObjGo.GetChildControl<Image>("");
        }

        public override void SetFillAmount(float value) {
            image.fillAmount = value;
        }
    }

    public class BloodBarRender<T> : BloodBar<T>{
        public SpriteRenderer spriteRenderer;
        public override void SetData(T bloodBarCdt, Transform transAim, float offset = 0) {
            base.SetData(bloodBarCdt, transAim, offset);
            SetRender();
        }
        public virtual void SetRender() {
            spriteRenderer = ObjGo.GetChildControl<SpriteRenderer>("");
        }
        public override void SetFillAmount(float value) {
            spriteRenderer.transform.SetPosX(EMath.Map(0f, 1f, -0.6f, 0f, value));
            spriteRenderer.size = new Vector2(EMath.Map(0f, 1f, 0.1f, 10f, value), spriteRenderer.size.y);
        }

    }
}