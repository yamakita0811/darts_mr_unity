Shader "Custom/PassthroughShader"
{
    Properties
    {
        _PassthroughCenter ("Passthrough Center", Vector) = (0, 0, 0, 0)
        _PassthroughRadius ("Passthrough Radius", Float) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : POSITION;
                float3 worldPos : TEXCOORD0;
            };

            float4 _PassthroughCenter;
            float _PassthroughRadius;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                float distanceToCenter = distance(i.worldPos, _PassthroughCenter.xyz);
                if (distanceToCenter < _PassthroughRadius)
                {
                    return half4(0, 0, 0, 0); // パススルー領域
                }
                else
                {
                    return half4(1, 1, 1, 1); // VR領域
                }
            }
            ENDCG
        }
    }
}
