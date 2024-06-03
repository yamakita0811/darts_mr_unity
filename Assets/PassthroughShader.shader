Shader "Custom/PassthroughShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _PassthroughCenter ("Passthrough Center", Vector) = (0.5, 0.5, 0, 0)
        _PassthroughRadius ("Passthrough Radius", Float) = 0.25
    }
    SubShader
    {
        Tags { "Queue"="Overlay" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _PassthroughCenter;
            float _PassthroughRadius;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                float2 passthroughCenter = _PassthroughCenter.xy;
                float dist = distance(i.uv, passthroughCenter);
                if (dist < _PassthroughRadius)
                {
                    // Passthrough領域（透明にする）
                    col.a = 0;
                }
                return col;
            }
            ENDCG
        }
    }
}
