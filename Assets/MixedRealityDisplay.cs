using UnityEngine;

public class MixedRealityDisplay : MonoBehaviour
{
    public Camera passthroughCamera;
    public Camera vrCamera;
    public Rect passthroughRect = new Rect(0, 0, 0.5f, 1f); // 左半分をパススルーにする例

    private OVRManager ovrManager;

    void Start()
    {
        // パススルーカメラの初期設定
        passthroughCamera.rect = passthroughRect;
        passthroughCamera.clearFlags = CameraClearFlags.SolidColor;
        passthroughCamera.backgroundColor = new Color(0, 0, 0, 0); // 透明

        // VRカメラの初期設定
        vrCamera.rect = new Rect(0, 0, 1, 1);

        // OVRManagerの参照を取得
        ovrManager = FindObjectOfType<OVRManager>();
        if (ovrManager != null)
        {
            ovrManager.isInsightPassthroughEnabled = true;
        }
    }

    void Update()
    {
        // パススルーとVRの更新処理
    }
}
