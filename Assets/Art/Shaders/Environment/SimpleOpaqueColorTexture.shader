Shader "Custom/SimpleOpaqueColorWithTexture"
{
    Properties
    {
        _Color ("Color", Color) = (1, 1, 1, 1)
        _MainTex ("Base (RGB)", 2D) = "white" { }
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            // Input structure for the vertex shader
            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0; // Texture coordinates
            };

            // Output structure for the fragment shader
            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0; // Texture coordinates passed to the fragment shader
            };

            // Declare the color and texture properties
            float4 _Color;
            sampler2D _MainTex;
            float4 _MainTex_ST; // Texture scaling and translation

            // Vertex shader
            v2f vert (appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            // Fragment shader
            half4 frag (v2f i) : SV_Target
            {
                // Sample the texture
                half4 texColor = tex2D(_MainTex, i.uv);

                // Combine the texture color with the _Color property
                return texColor * _Color;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
