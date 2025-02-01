// Made with Amplify Shader Editor v1.9.8.1
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "DarkShader2"
{
	Properties
	{
		[HideInInspector]_TargetPos("TargetPos", Vector) = (0,0,0,0)
		_Scale("Scale", Range( 0 , 2)) = 0.7
		_Offset("Offset", Range( 0 , 2)) = 0.4
		_ToonRamp("Toon Ramp", 2D) = "white" {}
		_MaxLightDistance("MaxLightDistance", Float) = 23
		_Main("Main", 2D) = "white" {}
		_Int0("Int 0", Int) = 3
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" }
		Cull Back
		CGINCLUDE
		#include "UnityPBSLighting.cginc"
		#include "UnityCG.cginc"
		#include "Lighting.cginc"
		#pragma target 3.5
		#define ASE_VERSION 19801
		struct Input
		{
			float3 worldPos;
			float3 worldNormal;
			float2 uv_texcoord;
		};

		struct SurfaceOutputCustomLightingCustom
		{
			half3 Albedo;
			half3 Normal;
			half3 Emission;
			half Metallic;
			half Smoothness;
			half Occlusion;
			half Alpha;
			Input SurfInput;
			UnityGIInput GIData;
		};

		uniform float3 _TargetPos;
		uniform float _MaxLightDistance;
		uniform int _Int0;
		uniform sampler2D _ToonRamp;
		uniform float _Scale;
		uniform float _Offset;
		uniform sampler2D _Main;
		uniform float4 _Main_ST;

		inline half4 LightingStandardCustomLighting( inout SurfaceOutputCustomLightingCustom s, half3 viewDir, UnityGI gi )
		{
			UnityGIInput data = s.GIData;
			Input i = s.SurfInput;
			half4 c = 0;
			float3 ase_positionWS = i.worldPos;
			float clampResult13 = clamp( distance( _TargetPos , ase_positionWS ) , 0.0 , _MaxLightDistance );
			float3 ase_normalWS = i.worldNormal;
			#if defined(LIGHTMAP_ON) && UNITY_VERSION < 560 //aseld
			float3 ase_lightDirWS = 0;
			#else //aseld
			float3 ase_lightDirWS = normalize( UnityWorldSpaceLightDir( ase_positionWS ) );
			#endif //aseld
			float dotResult33 = dot( ase_normalWS , ase_lightDirWS );
			float2 temp_cast_0 = ((dotResult33*_Scale + _Offset)).xx;
			float4 temp_output_81_0 = ( ( trunc( ( ( 1.0 - ( clampResult13 / _MaxLightDistance ) ) * _Int0 ) ) / _Int0 ) * tex2D( _ToonRamp, temp_cast_0 ) );
			float2 temp_cast_2 = ((dotResult33*_Scale + _Offset)).xx;
			float2 uv_Main = i.uv_texcoord * _Main_ST.xy + _Main_ST.zw;
			c.rgb = ( temp_output_81_0 * tex2D( _Main, uv_Main ) ).rgb;
			c.a = temp_output_81_0.r;
			return c;
		}

		inline void LightingStandardCustomLighting_GI( inout SurfaceOutputCustomLightingCustom s, UnityGIInput data, inout UnityGI gi )
		{
			s.GIData = data;
		}

		void surf( Input i , inout SurfaceOutputCustomLightingCustom o )
		{
			o.SurfInput = i;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf StandardCustomLighting alpha:fade keepalpha fullforwardshadows 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.5
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			sampler3D _DitherMaskLOD;
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
				float3 worldNormal : TEXCOORD3;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.worldNormal = worldNormal;
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = IN.worldPos;
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.worldPos = worldPos;
				surfIN.worldNormal = IN.worldNormal;
				SurfaceOutputCustomLightingCustom o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputCustomLightingCustom, o )
				surf( surfIN, o );
				UnityGI gi;
				UNITY_INITIALIZE_OUTPUT( UnityGI, gi );
				o.Alpha = LightingStandardCustomLighting( o, worldViewDir, gi ).a;
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				half alphaRef = tex3D( _DitherMaskLOD, float3( vpos.xy * 0.25, o.Alpha * 0.9375 ) ).a;
				clip( alphaRef - 0.01 );
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "AmplifyShaderEditor.MaterialInspector"
}
/*ASEBEGIN
Version=19801
Node;AmplifyShaderEditor.WorldPosInputsNode;7;-432,240;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.Vector3Node;5;-432,80;Inherit;False;Property;_TargetPos;TargetPos;0;1;[HideInInspector];Create;True;0;0;0;False;0;False;0,0,0;4.65,11.52,-1.77;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.DistanceOpNode;6;-192,160;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;11;-320,400;Inherit;False;Property;_MaxLightDistance;MaxLightDistance;6;0;Create;True;0;0;0;False;0;False;23;23.12;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;13;-32,160;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;12;144,256;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;78;272,256;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.IntNode;90;288,96;Inherit;False;Property;_Int0;Int 0;9;0;Create;True;0;0;0;False;0;False;3;3;False;0;1;INT;0
Node;AmplifyShaderEditor.WorldNormalVector;30;-1040,512;Inherit;False;False;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.WorldSpaceLightDirHlpNode;31;-1056,672;Inherit;False;False;1;0;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;87;480,0;Inherit;False;2;2;0;FLOAT;0;False;1;INT;3;False;1;FLOAT;0
Node;AmplifyShaderEditor.DotProductOpNode;33;-848,576;Inherit;True;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;66;-528,640;Float;False;Property;_Scale;Scale;1;0;Create;True;0;0;0;False;0;False;0.7;0.7;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;67;-528,720;Float;False;Property;_Offset;Offset;2;0;Create;True;0;0;0;False;0;False;0.4;0.4;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.TruncOpNode;88;640,16;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ScaleAndOffsetNode;37;-208,576;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;1;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;89;816,16;Inherit;False;2;0;FLOAT;0;False;1;INT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;46;96,576;Inherit;True;Property;_ToonRamp;Toon Ramp;3;0;Create;True;0;0;0;False;0;False;-1;36460174ce81e654599f241ce7dd9b2d;36460174ce81e654599f241ce7dd9b2d;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.SamplerNode;68;-464,880;Inherit;True;Property;_Main;Main;7;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;81;640,464;Inherit;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode;91;-497.2136,545.1749;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;10;-544,-128;Inherit;False;Property;_ColorDistant;ColorDistant;5;0;Create;True;0;0;0;False;0;False;0,0.4981887,1,1;0,0.4981886,1,1;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.ColorNode;9;-544,-320;Inherit;False;Property;_ColorClose;ColorClose;4;0;Create;True;0;0;0;False;0;False;1,0.4361805,0,1;1,0.4361804,0,1;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;83;928,848;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;70;976,1408;Inherit;False;Property;_InnerShadowAmount;InnerShadowAmount;8;0;Create;True;0;0;0;False;0;False;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1200,272;Float;False;True;-1;3;AmplifyShaderEditor.MaterialInspector;0;0;CustomLighting;DarkShader2;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;;0;False;;False;0;False;;0;False;;False;0;Transparent;0.5;True;True;0;False;Transparent;;Transparent;All;12;all;True;True;True;True;0;False;;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;2;15;10;25;False;0.5;True;2;5;False;;10;False;;0;0;False;;0;False;;0;False;;0;False;;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;0;-1;-1;-1;0;False;0;0;False;;-1;0;False;;0;0;0;False;0.1;False;;0;False;;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;16;FLOAT4;0,0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;6;0;5;0
WireConnection;6;1;7;0
WireConnection;13;0;6;0
WireConnection;13;2;11;0
WireConnection;12;0;13;0
WireConnection;12;1;11;0
WireConnection;78;0;12;0
WireConnection;87;0;78;0
WireConnection;87;1;90;0
WireConnection;33;0;30;0
WireConnection;33;1;31;0
WireConnection;88;0;87;0
WireConnection;37;0;33;0
WireConnection;37;1;66;0
WireConnection;37;2;67;0
WireConnection;89;0;88;0
WireConnection;89;1;90;0
WireConnection;46;1;37;0
WireConnection;81;0;89;0
WireConnection;81;1;46;0
WireConnection;91;0;33;0
WireConnection;83;0;81;0
WireConnection;83;1;68;0
WireConnection;0;9;81;0
WireConnection;0;13;83;0
ASEEND*/
//CHKSM=535F86E9006E2A998DE04BD438B6F2EF4A0E53FD