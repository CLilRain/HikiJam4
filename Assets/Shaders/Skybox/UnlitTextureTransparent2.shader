Shader "Custom/UnlitTextureTransparent2"
{
    Properties
    {
        // The base texture property
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _Offset ("Offset", Vector) = (0,0,0,0) // Correct default offset format
    }

    SubShader
    {
        // Tag indicating that this is an opaque shader
        Tags { "RenderType"="Opaque" }

        // The default shader pass
        Pass
        {
            Tags { "RenderType"="Opaque" }
            
            // Vertex and fragment shaders are defined here
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            // Input structure for the vertex shader
            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            // Output structure for the fragment shader
            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            // Texture property
            sampler2D _MainTex;
            float4 _MainTex_ST; // Texture Scale and Offset
            float2 _Offset;     // Custom Offset

            // Vertex shader
            v2f vert(appdata_t v)
            {
                v2f o;
                // Apply the object's transformation and pass the UV coordinates
                o.vertex = UnityObjectToClipPos(v.vertex);

                // Apply UV scaling and offset
                o.uv = TRANSFORM_TEX(v.uv, _MainTex); 

                // Add custom offset to UVs in the vertex shader
                o.uv += _Offset;

                return o;
            }

            // Fragment shader
            half4 frag(v2f i) : SV_Target
            {
                // Sample the texture
                half4 col = tex2D(_MainTex, i.uv);
                return col; // Return the texture color
            }
            ENDCG
        }
    }

    // Fallback shader if this shader cannot be rendered
    FallBack "Diffuse"
}
