using UnityEngine;

public class MixedRealityView : MonoBehaviour
{
    private OVRPassthroughLayer passthroughLayer;
    public Transform passthroughAreaCenter;
    public float passthroughAreaRadius = 1.0f;
    public Material passthroughMaterial;

    void Start()
    {
        // OVRPassthroughLayerの追加と設定
        passthroughLayer = gameObject.AddComponent<OVRPassthroughLayer>();
        passthroughLayer.textureOpacity = 1.0f;

        if (passthroughLayer != null)
        {
            Debug.Log("PassthroughLayer initialized successfully.");
        }
        else
        {
            Debug.LogError("Failed to initialize PassthroughLayer.");
        }

        // パススルー用のマテリアル設定
        passthroughMaterial.SetVector("_PassthroughCenter", passthroughAreaCenter.position);
        passthroughMaterial.SetFloat("_PassthroughRadius", passthroughAreaRadius);
    }

    void Update()
    {
        // パススルー領域を更新
        passthroughMaterial.SetVector("_PassthroughCenter", passthroughAreaCenter.position);
        passthroughMaterial.SetFloat("_PassthroughRadius", passthroughAreaRadius);
    }
}
