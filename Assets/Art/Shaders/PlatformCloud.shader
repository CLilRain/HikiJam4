Shader "Custom/PlatformCloud"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,0.8) // Define as Color for easier manipulation in the inspector
        _Alpha("Alpha", float) = 1
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha // Enable proper transparency blending
        ZWrite Off                      // Disable depth writing for proper transparency rendering
        Lighting Off                    // Disable lighting since it's unlit

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            // Define uniform properties
            float4 _Color;
            float _Alpha;

            // Vertex to Fragment structure
            struct v2f
            {
                float4 pos : SV_POSITION; // Screen-space position
            };

            // Vertex shader
            v2f vert(appdata_base v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex); // Transform vertices to clip space
                return o;
            }

            // Fragment shader
            half4 frag(v2f i) : SV_Target
            {
                _Color.a *= _Alpha;
                return _Color; // Output the color
            }
            ENDCG
        }
    }

    FallBack "Diffuse"
}
