// Made with Amplify Shader Editor v1.9.8.1
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "DarkShader_bkup1"
{
	Properties
	{
		_TargetPos("TargetPos", Vector) = (0,0,0,0)
		_ColorClose("ColorClose", Color) = (1,0.4361805,0,1)
		_ColorDistant("ColorDistant", Color) = (0,0.4981887,1,1)
		_MaxColorLerpDistance("MaxColorLerpDistance", Float) = 0
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#pragma target 3.5
		#define ASE_VERSION 19801
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float3 worldPos;
		};

		uniform float4 _ColorClose;
		uniform float4 _ColorDistant;
		uniform float3 _TargetPos;
		uniform float _MaxColorLerpDistance;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float3 ase_positionWS = i.worldPos;
			float clampResult13 = clamp( distance( _TargetPos , ase_positionWS ) , 0.0 , _MaxColorLerpDistance );
			float4 lerpResult8 = lerp( _ColorClose , _ColorDistant , ( clampResult13 / _MaxColorLerpDistance ));
			o.Albedo = lerpResult8.rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "AmplifyShaderEditor.MaterialInspector"
}
/*ASEBEGIN
Version=19801
Node;AmplifyShaderEditor.WorldPosInputsNode;7;-1120,208;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.Vector3Node;5;-1120,48;Inherit;False;Property;_TargetPos;TargetPos;0;0;Create;True;0;0;0;False;0;False;0,0,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.DistanceOpNode;6;-880,128;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;11;-912,272;Inherit;False;Property;_MaxColorLerpDistance;MaxColorLerpDistance;3;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;13;-720,128;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;12;-544,224;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;10;-544,-128;Inherit;False;Property;_ColorDistant;ColorDistant;2;0;Create;True;0;0;0;False;0;False;0,0.4981887,1,1;0,0,0,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.ColorNode;9;-544,-320;Inherit;False;Property;_ColorClose;ColorClose;1;0;Create;True;0;0;0;False;0;False;1,0.4361805,0,1;0,0,0,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.LerpOp;8;-256,32;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;48,-32;Float;False;True;-1;3;AmplifyShaderEditor.MaterialInspector;0;0;Standard;DarkShader_bkup1;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;;0;False;;False;0;False;;0;False;;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;12;all;True;True;True;True;0;False;;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;2;15;10;25;False;0.5;True;0;0;False;;0;False;;0;0;False;;0;False;;0;False;;0;False;;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;;-1;0;False;;0;0;0;False;0.1;False;;0;False;;False;17;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;16;FLOAT4;0,0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;6;0;5;0
WireConnection;6;1;7;0
WireConnection;13;0;6;0
WireConnection;13;2;11;0
WireConnection;12;0;13;0
WireConnection;12;1;11;0
WireConnection;8;0;9;0
WireConnection;8;1;10;0
WireConnection;8;2;12;0
WireConnection;0;0;8;0
ASEEND*/
//CHKSM=B94322460413C71BD01D3807BA59CD9659636E7A