Shader "Custom/UnlitTextureTransparent"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {} // Main texture
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100

        Blend SrcAlpha OneMinusSrcAlpha // Enable alpha blending
        ZWrite Off                     // Disable depth writes for transparency sorting

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
            float4 _MainTex_ST; // UV Transform (Scale, Offset)

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex); // Transform vertex to clip space
                o.uv = TRANSFORM_TEX(v.uv, _MainTex); // Apply UV offset and scale
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 color = tex2D(_MainTex, i.uv); // Sample texture color
                return color; // Output the sampled color
            }
            ENDCG
        }
    }

    FallBack "Diffuse" // Fallback shader if this one can't be rendered
}
