Shader "Custom/UnlitTexture"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {} // Main texture
    }
    SubShader
    {
        Tags { "Queue"="Background" "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            Cull Off // Disable backface culling (optional, useful for skybox objects)
            ZWrite On // Write to depth buffer for proper rendering
            ZTest Always // Always render (even if other objects are behind)

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
                return tex2D(_MainTex, i.uv); // Sample texture and output color
            }
            ENDCG
        }
    }

    FallBack "Diffuse" // Fallback shader if this one can't be rendered
}
