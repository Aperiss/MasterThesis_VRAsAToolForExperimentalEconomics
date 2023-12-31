// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "GPUInstancer/IL3DN/Pine"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_AlphaCutoff("Alpha Cutoff", Range( 0 , 1)) = 0.5
		_MainTex("MainTex", 2D) = "white" {}
		[Toggle(_SNOW_ON)] _Snow("Snow", Float) = 1
		[Toggle(_WIND_ON)] _Wind("Wind", Float) = 1
		_WindStrenght("Wind Strenght", Range( 0 , 1)) = 0.5
		[Toggle(_WIGGLE_ON)] _Wiggle("Wiggle", Float) = 1
		_WiggleStrenght("Wiggle Strenght", Range( 0 , 1)) = 0.5
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "AlphaTest+0" }
		Cull Off
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		#pragma multi_compile __ _WIND_ON
		#pragma multi_compile __ _SNOW_ON
		#pragma multi_compile __ _WIGGLE_ON
		#include "VS_indirect.cginc"
		#pragma multi_compile GPU_FRUSTUM_ON__
		struct Input
		{
			float3 worldPos;
			float3 worldNormal;
			float2 uv_texcoord;
			float4 vertexColor : COLOR;
		};

		uniform float3 WindDirection;
		uniform sampler2D NoiseTextureFloat;
		uniform float WindSpeedFloat;
		uniform float WindTurbulenceFloat;
		uniform float _WindStrenght;
		uniform float WindStrenghtFloat;
		uniform float SnowPinesFloat;
		uniform float4 _Color;
		uniform sampler2D _MainTex;
		uniform float LeavesWiggleFloat;
		uniform float _WiggleStrenght;
		uniform float _AlphaCutoff;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float3 myWindDirection967 = WindDirection;
			float3 temp_output_952_0 = float3( (myWindDirection967).xz ,  0.0 );
			float3 ase_worldPos = mul( unity_ObjectToWorld, v.vertex );
			float2 panner956 = ( 1.0 * _Time.y * ( temp_output_952_0 * WindSpeedFloat * 10.0 ).xy + (ase_worldPos).xy);
			float4 worldNoise905 = ( tex2Dlod( NoiseTextureFloat, float4( ( ( panner956 * WindTurbulenceFloat ) / float2( 10,10 ) ), 0, 0.0) ) * _WindStrenght * WindStrenghtFloat );
			float4 transform886 = mul(unity_WorldToObject,( float4( myWindDirection967 , 0.0 ) * ( ( v.color.a * worldNoise905 ) + ( worldNoise905 * v.color.g ) ) ));
			#ifdef _WIND_ON
				float4 staticSwitch897 = ( transform886 + float4( 0,0,0,0 ) );
			#else
				float4 staticSwitch897 = float4( 0,0,0,0 );
			#endif
			v.vertex.xyz += staticSwitch897.xyz;
			v.vertex.w = 1;
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float3 ase_worldNormal = i.worldNormal;
			#ifdef _SNOW_ON
				float staticSwitch921 = ( saturate( pow( abs( ase_worldNormal.y ) , 10.0 ) ) * SnowPinesFloat );
			#else
				float staticSwitch921 = 0.0;
			#endif
			float3 myWindDirection967 = WindDirection;
			float3 temp_output_952_0 = float3( (myWindDirection967).xz ,  0.0 );
			float3 ase_worldPos = i.worldPos;
			float2 panner956 = ( 1.0 * _Time.y * ( temp_output_952_0 * WindSpeedFloat * 10.0 ).xy + (ase_worldPos).xy);
			float4 worldNoise905 = ( tex2D( NoiseTextureFloat, ( ( panner956 * WindTurbulenceFloat ) / float2( 10,10 ) ) ) * _WindStrenght * WindStrenghtFloat );
			float cos745 = cos( ( ( tex2D( NoiseTextureFloat, worldNoise905.rg ) * i.vertexColor.g ) * LeavesWiggleFloat * _WiggleStrenght ).r );
			float sin745 = sin( ( ( tex2D( NoiseTextureFloat, worldNoise905.rg ) * i.vertexColor.g ) * LeavesWiggleFloat * _WiggleStrenght ).r );
			float2 rotator745 = mul( i.uv_texcoord - float2( 0.25,0.25 ) , float2x2( cos745 , -sin745 , sin745 , cos745 )) + float2( 0.25,0.25 );
			#ifdef _WIGGLE_ON
				float2 staticSwitch898 = rotator745;
			#else
				float2 staticSwitch898 = i.uv_texcoord;
			#endif
			float4 tex2DNode97 = tex2D( _MainTex, staticSwitch898 );
			o.Albedo = saturate( ( staticSwitch921 + ( _Color * tex2DNode97 ) ) ).rgb;
			o.Alpha = 1;
			clip( tex2DNode97.a - _AlphaCutoff );
		}

		ENDCG
		CGPROGRAM
#include "UnityCG.cginc"
#include "./../../GPUInstancer/Shaders/Include/GPUInstancerInclude.cginc"
#pragma instancing_options procedural:setupGPUI
#pragma multi_compile_instancing
		#pragma exclude_renderers vulkan xbox360 psp2 n3ds wiiu 
		#pragma surface surf Lambert keepalpha fullforwardshadows nolightmap  nodirlightmap dithercrossfade vertex:vertexDataFunc 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
#include "UnityCG.cginc"
#include "./../../GPUInstancer/Shaders/Include/GPUInstancerInclude.cginc"
#pragma instancing_options procedural:setupGPUI
#pragma multi_compile_instancing
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
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
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
				float3 worldNormal : TEXCOORD3;
				half4 color : COLOR0;
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
				vertexDataFunc( v, customInputData );
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.worldNormal = worldNormal;
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				o.color = v.color;
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
				surfIN.vertexColor = IN.color;
				SurfaceOutput o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutput, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
}
/*ASEBEGIN
Version=18901
1251;81;2034;1453;-630.1859;-1051.34;2.105833;True;True
Node;AmplifyShaderEditor.Vector3Node;867;1743.65,80.67828;Float;False;Global;WindDirection;WindDirection;14;0;Create;True;0;0;0;False;0;False;0,0,0;-0.7071068,0,-0.7071068;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RegisterLocalVarNode;967;2053.49,79.48224;Inherit;False;myWindDirection;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;968;861.1199,1717.354;Inherit;False;967;myWindDirection;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.CommentaryNode;948;1190.904,1440.392;Inherit;False;1611.173;648.4346;World Noise;15;961;959;960;958;957;955;956;953;954;952;951;966;950;949;965;World Noise;1,0,0.02020931,1;0;0
Node;AmplifyShaderEditor.SwizzleNode;949;1220.541,1715.728;Inherit;False;FLOAT2;0;2;1;2;1;0;FLOAT3;0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;966;1515.448,1989.196;Inherit;False;Constant;_Float0;Float 0;9;0;Create;True;0;0;0;False;0;False;10;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TransformDirectionNode;952;1428.541,1715.728;Inherit;False;World;World;True;Fast;False;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.WorldPosInputsNode;951;1225.81,1501.016;Float;True;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;950;1385.846,1891.679;Float;False;Global;WindSpeedFloat;WindSpeedFloat;3;0;Create;False;0;0;0;False;0;False;0.5;0.4;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SwizzleNode;954;1488.099,1496.855;Inherit;False;FLOAT2;0;1;2;2;1;0;FLOAT3;0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;953;1694.364,1857.001;Inherit;False;3;3;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;955;1764.049,1665.584;Float;False;Global;WindTurbulenceFloat;WindTurbulenceFloat;4;0;Create;False;0;0;0;False;0;False;0.5;0.05;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;956;1862.387,1503.35;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;957;2063.385,1505.027;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;965;2220.706,1501.431;Inherit;False;2;0;FLOAT2;0,0;False;1;FLOAT2;10,10;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;959;2282.204,1876.771;Float;False;Global;WindStrenghtFloat;WindStrenghtFloat;3;0;Create;False;0;0;0;False;0;False;0.5;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;960;2360.082,1500.883;Inherit;True;Global;NoiseTextureFloat;NoiseTextureFloat;4;0;Create;False;0;0;0;False;0;False;-1;None;e5055e0d246bd1047bdb28057a93753c;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;958;2285.85,1770.491;Float;False;Property;_WindStrenght;Wind Strenght;6;0;Create;False;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;961;2658.999,1751.806;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;905;2847.26,1746.268;Float;False;worldNoise;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.CommentaryNode;899;1792.495,773.7919;Inherit;False;1014.989;570.0199;UV Animation;8;745;799;746;798;904;964;963;962;UV Animation;0.7678117,1,0,1;0;0
Node;AmplifyShaderEditor.GetLocalVarNode;907;1594.319,852.8303;Inherit;False;905;worldNoise;1;0;OBJECT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.VertexColorNode;963;2097.861,1022.605;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;938;1741.336,284.9232;Inherit;False;1075.409;358.2535;Snow;6;918;939;942;940;944;945;Snow;1,1,1,1;0;0
Node;AmplifyShaderEditor.SamplerNode;962;1964.868,828.5834;Inherit;True;Property;_TextureSample0;Texture Sample 0;4;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Instance;960;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.WorldNormalVector;940;1782.964,346.9032;Inherit;False;False;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;964;2275.338,1015.779;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;904;2009.502,1264.279;Float;False;Property;_WiggleStrenght;Wiggle Strenght;8;0;Create;True;0;0;0;False;0;False;0.5;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;798;2004.283,1185.988;Float;False;Global;LeavesWiggleFloat;LeavesWiggleFloat;5;0;Create;True;0;0;0;False;0;False;0.5;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;908;2188.38,2104.574;Inherit;False;619.4545;677.2183;Vertex Animation;5;857;854;855;853;856;Vertex Animation;0,1,0.8708036,1;0;0
Node;AmplifyShaderEditor.AbsOpNode;945;1992.992,390.9749;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.VertexColorNode;853;2241.329,2179.344;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;906;1870.66,2422.799;Inherit;False;905;worldNoise;1;0;OBJECT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;746;2302.538,835.3718;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.VertexColorNode;856;2246.027,2592.41;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;799;2405.604,1123.231;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;937;3326.629,1006.802;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;855;2514.767,2476.146;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.PowerNode;918;2126.164,391.3695;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;10;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;854;2511.766,2338.585;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RotatorNode;745;2559.531,976.8619;Inherit;True;3;0;FLOAT2;0,0;False;1;FLOAT2;0.25,0.25;False;2;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;939;1764.693,553.5909;Inherit;False;Global;SnowPinesFloat;SnowPinesFloat;4;0;Create;True;0;0;0;False;0;False;1;4;0;20;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;857;2663.208,2399.785;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode;944;2278.771,389.6975;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;898;3961.742,1187.799;Float;False;Property;_Wiggle;Wiggle;7;0;Create;True;0;0;0;False;0;False;1;1;1;True;_WIND_ON;Toggle;2;Key0;Key1;Create;False;True;9;1;FLOAT2;0,0;False;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT2;0,0;False;6;FLOAT2;0,0;False;7;FLOAT2;0,0;False;8;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;969;2848.428,1338.063;Inherit;False;967;myWindDirection;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SamplerNode;97;4222.893,1164.98;Inherit;True;Property;_MainTex;MainTex;2;0;Create;True;0;0;0;False;0;False;-1;None;6ab0f5f5ed2482e43a5ace7eeced19e6;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;942;2432.369,530.497;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;292;4218.407,964.0632;Float;False;Property;_Color;Color;0;0;Create;True;0;0;0;False;0;False;1,1,1,1;0.7058823,0.5882353,0.1843136,1;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;872;3207.503,1341.415;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StaticSwitch;921;3949.399,506.2074;Inherit;False;Property;_Snow;Snow;3;0;Create;True;0;0;0;False;0;False;1;1;1;True;;Toggle;2;Key0;Key1;Create;False;True;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WorldToObjectTransfNode;886;3401.475,1340.256;Inherit;False;1;0;FLOAT4;0,0,0,1;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;293;4553.004,1056.225;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;936;4687.28,866.9855;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;970;3728.866,1342.848;Inherit;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.CommentaryNode;971;1430.048,2830.871;Inherit;False;1376.448;561.1524;Whole tree swing animations;8;980;979;978;977;975;983;984;985;Swing;0.02349341,1,0,1;0;0
Node;AmplifyShaderEditor.SaturateNode;946;4847.991,1043.856;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;910;4237.173,1410.353;Float;False;Property;_AlphaCutoff;Alpha Cutoff;1;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;897;3969.67,1309.435;Float;False;Property;_Wind;Wind;5;0;Create;True;0;0;0;False;0;False;1;1;1;True;;Toggle;2;Key0;Key1;Create;False;True;9;1;FLOAT4;0,0,0,0;False;0;FLOAT4;0,0,0,0;False;2;FLOAT4;0,0,0,0;False;3;FLOAT4;0,0,0,0;False;4;FLOAT4;0,0,0,0;False;5;FLOAT4;0,0,0,0;False;6;FLOAT4;0,0,0,0;False;7;FLOAT4;0,0,0,0;False;8;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SinTimeNode;983;1488.133,2910.433;Inherit;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RotateAboutAxisNode;979;2233.529,2947.271;Inherit;False;False;4;0;FLOAT3;1,0,0;False;1;FLOAT;0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0.01,0.01,0.01;False;1;FLOAT3;0
Node;AmplifyShaderEditor.PosVertexDataNode;978;2281.21,3167.79;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;985;1958.462,2970.677;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TransformPositionNode;977;1881.341,3191.415;Inherit;False;World;Object;False;Fast;True;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.WorldPosInputsNode;975;1635.019,3202.134;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;984;1691.091,2977.095;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;982;1279.491,3108.995;Inherit;False;Property;_SwingSpeed;Swing Speed;10;0;Create;True;0;0;0;False;0;False;0.1;10;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;980;2635.201,2946.474;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;981;1246.147,2668.812;Inherit;False;Property;_SwingAmount;Swing Amount;9;0;Create;True;0;0;0;False;0;False;0.1;10;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;5087.896,1047.746;Float;False;True;-1;2;;0;0;Lambert;IL3DN/Pine;False;False;False;False;False;False;True;False;True;False;False;False;True;False;False;False;True;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Masked;0.5;True;True;0;False;TransparentCutout;;AlphaTest;All;9;d3d9;d3d11_9x;d3d11;glcore;gles;gles3;metal;xboxone;ps4;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;0;4;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;1;False;-1;1;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;892;-1;0;True;910;3;Include;VS_indirect.cginc;False;;Custom;Pragma;instancing_options procedural:setup;False;;Custom;Pragma;multi_compile GPU_FRUSTUM_ON__;False;;Custom;0;0;False;0.1;False;-1;0;False;-1;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;967;0;867;0
WireConnection;949;0;968;0
WireConnection;952;0;949;0
WireConnection;954;0;951;0
WireConnection;953;0;952;0
WireConnection;953;1;950;0
WireConnection;953;2;966;0
WireConnection;956;0;954;0
WireConnection;956;2;953;0
WireConnection;957;0;956;0
WireConnection;957;1;955;0
WireConnection;965;0;957;0
WireConnection;960;1;965;0
WireConnection;961;0;960;0
WireConnection;961;1;958;0
WireConnection;961;2;959;0
WireConnection;905;0;961;0
WireConnection;962;1;907;0
WireConnection;964;0;962;0
WireConnection;964;1;963;2
WireConnection;945;0;940;2
WireConnection;799;0;964;0
WireConnection;799;1;798;0
WireConnection;799;2;904;0
WireConnection;855;0;906;0
WireConnection;855;1;856;2
WireConnection;918;0;945;0
WireConnection;854;0;853;4
WireConnection;854;1;906;0
WireConnection;745;0;746;0
WireConnection;745;2;799;0
WireConnection;857;0;854;0
WireConnection;857;1;855;0
WireConnection;944;0;918;0
WireConnection;898;1;937;0
WireConnection;898;0;745;0
WireConnection;97;1;898;0
WireConnection;942;0;944;0
WireConnection;942;1;939;0
WireConnection;872;0;969;0
WireConnection;872;1;857;0
WireConnection;921;0;942;0
WireConnection;886;0;872;0
WireConnection;293;0;292;0
WireConnection;293;1;97;0
WireConnection;936;0;921;0
WireConnection;936;1;293;0
WireConnection;970;0;886;0
WireConnection;946;0;936;0
WireConnection;897;0;970;0
WireConnection;979;1;985;0
WireConnection;979;3;977;0
WireConnection;985;0;981;0
WireConnection;985;1;984;0
WireConnection;977;0;975;0
WireConnection;984;0;983;4
WireConnection;984;1;982;0
WireConnection;980;0;979;0
WireConnection;980;1;978;0
WireConnection;0;0;946;0
WireConnection;0;10;97;4
WireConnection;0;11;897;0
ASEEND*/
//CHKSM=36BEC9CEA3EF6D9E378B259D234AA4A13C89E5CF
