using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class PassthroughController : MonoBehaviour
{
    public Camera passthroughCamera;
    public RawImage passthroughImage;
    public ARAnchorManager anchorManager;
    public Transform anchorTransform;
    public float passthroughRadius = 1.0f;

    private Material passthroughMaterial;
    private ARAnchor anchor;

    void Start()
    {
        // PassthroughImageのマテリアルを取得
        passthroughMaterial = passthroughImage.material;

        // PassthroughカメラのRenderTextureを設定
        RenderTexture passthroughTexture = new RenderTexture(Screen.width, Screen.height, 24);
        passthroughCamera.targetTexture = passthroughTexture;
        passthroughImage.texture = passthroughTexture;

        // 初期パススルー領域の設定
        CreateAnchor(anchorTransform.position);
        SetPassthroughRegion(anchorTransform.position, passthroughRadius);
    }

    void CreateAnchor(Vector3 position)
    {
        // ARAnchorを作成し、指定した位置に配置
        anchor = anchorManager.AddAnchor(new Pose(position, Quaternion.identity));
        if (anchor == null)
        {
            Debug.LogError("Failed to create ARAnchor.");
        }
    }

    void SetPassthroughRegion(Vector3 worldPosition, float radius)
    {
        if (anchor == null)
            return;

        // ワールド座標をビューポート座標に変換
        Vector3 viewportPos = passthroughCamera.WorldToViewportPoint(worldPosition);
        passthroughMaterial.SetVector("_PassthroughCenter", new Vector4(viewportPos.x, viewportPos.y, 0, 0));
        passthroughMaterial.SetFloat("_PassthroughRadius", radius);

        // デバッグログで位置を確認
        Debug.Log("Anchor position: " + worldPosition);
        Debug.Log("Passthrough region center: " + viewportPos);
    }

    void Update()
    {
        if (anchor != null)
        {
            // アンカーの位置を取得してパススルー領域を更新
            SetPassthroughRegion(anchor.transform.position, passthroughRadius);
        }
    }
}
