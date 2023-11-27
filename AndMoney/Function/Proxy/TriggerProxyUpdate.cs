using System;

public class TriggerProxyUpdate : TriggerProxy {
    public Action UpdateAction;

    private void Update() {
        UpdateAction?.Invoke();
    }
}
