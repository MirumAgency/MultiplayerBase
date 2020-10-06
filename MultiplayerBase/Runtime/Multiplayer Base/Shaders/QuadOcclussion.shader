Shader "Custom/QuadOcclussion"
{
	Properties
	{
		_Alpha("Alpha", Range(0, 1)) = 1
		_Color("Color", Color) = (0, 0, 0, 1)
	}
		SubShader
	{
		Tags
		{
			"RenderPipeline" = "UniversalPipeline"
			"RenderType" = "Transparent"
			"Queue" = "Transparent+0"
		}

		Pass
		{
			Name "Pass"
			Tags
			{
		// LightMode: <None>
	}

	// Render State
	Blend SrcAlpha OneMinusSrcAlpha, One OneMinusSrcAlpha
	Cull Off
	ZTest Always
	ZWrite Off
		// ColorMask: <None>


		HLSLPROGRAM
		#pragma vertex vert
		#pragma fragment frag

		// Debug
		// <None>

		// --------------------------------------------------
		// Pass
		#pragma prefer_hlslcc gles
		#pragma exclude_renderers d3d11_9x
		#pragma target 2.0
		#pragma multi_compile_fog
		#pragma multi_compile_instancing
		// Keywords
		#pragma multi_compile _ LIGHTMAP_ON
		#pragma multi_compile _ DIRLIGHTMAP_COMBINED
		#pragma shader_feature _ _SAMPLE_GI		

		// GraphKeywords: <None>

		// Defines
		#define _SURFACE_TYPE_TRANSPARENT 1
		#define _AlphaClip 1
		#define ATTRIBUTES_NEED_NORMAL
		#define ATTRIBUTES_NEED_TANGENT
		#define SHADERPASS_UNLIT

		// Includes
		#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
		#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
		#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
		#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
		#include "Packages/com.unity.shadergraph/ShaderGraphLibrary/ShaderVariablesFunctions.hlsl"

		// --------------------------------------------------
		// Graph

		// Graph Properties
		CBUFFER_START(UnityPerMaterial)
		half _Alpha;
		half4 _Color;
		CBUFFER_END

			// Graph Functions

			void Unity_Multiply_half(half A, half B, out half Out)
			{
				Out = A * B;
			}

		// Graph Vertex
		// GraphVertex: <None>

		// Graph Pixel
		struct SurfaceDescriptionInputs
		{
		};

		struct SurfaceDescription
		{
			float3 Color;
			float Alpha;
			float AlphaClipThreshold;
		};

		SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
		{
			SurfaceDescription surface = (SurfaceDescription)0;
			half4 _Property_561A9394_Out_0 = _Color;
			half _Split_3A71A765_R_1 = _Property_561A9394_Out_0[0];
			half _Split_3A71A765_G_2 = _Property_561A9394_Out_0[1];
			half _Split_3A71A765_B_3 = _Property_561A9394_Out_0[2];
			half _Split_3A71A765_A_4 = _Property_561A9394_Out_0[3];
			half _Property_F13439A4_Out_0 = _Alpha;
			half _Multiply_54F5B891_Out_2;
			Unity_Multiply_half(_Split_3A71A765_A_4, _Property_F13439A4_Out_0, _Multiply_54F5B891_Out_2);
			surface.Color = (_Property_561A9394_Out_0.xyz);
			surface.Alpha = _Multiply_54F5B891_Out_2;
			surface.AlphaClipThreshold = 0.5;
			return surface;
		}

		// --------------------------------------------------
		// Structs and Packing

		// Generated Type: Attributes
		struct Attributes
		{
			float3 positionOS : POSITION;
			float3 normalOS : NORMAL;
			float4 tangentOS : TANGENT;
			#if UNITY_ANY_INSTANCING_ENABLED
			uint instanceID : INSTANCEID_SEMANTIC;
			#endif
		};

		// Generated Type: Varyings
		struct Varyings
		{
			float4 positionCS : SV_POSITION;
			#if UNITY_ANY_INSTANCING_ENABLED
			uint instanceID : CUSTOM_INSTANCE_ID;
			#endif
			#if (defined(UNITY_STEREO_INSTANCING_ENABLED))
			uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
			#endif
			#if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
			uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
			#endif
			#if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
			FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
			#endif
		};

		// Generated Type: PackedVaryings
		struct PackedVaryings
		{
			float4 positionCS : SV_POSITION;
			#if UNITY_ANY_INSTANCING_ENABLED
			uint instanceID : CUSTOM_INSTANCE_ID;
			#endif
			#if (defined(UNITY_STEREO_INSTANCING_ENABLED))
			uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
			#endif
			#if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
			uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
			#endif
			#if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
			FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
			#endif
		};

		// Packed Type: Varyings
		PackedVaryings PackVaryings(Varyings input)
		{
			PackedVaryings output = (PackedVaryings)0;
			output.positionCS = input.positionCS;
			#if UNITY_ANY_INSTANCING_ENABLED
			output.instanceID = input.instanceID;
			#endif
			#if (defined(UNITY_STEREO_INSTANCING_ENABLED))
			output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
			#endif
			#if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
			output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
			#endif
			#if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
			output.cullFace = input.cullFace;
			#endif
			return output;
		}

		// Unpacked Type: Varyings
		Varyings UnpackVaryings(PackedVaryings input)
		{
			Varyings output = (Varyings)0;
			output.positionCS = input.positionCS;
			#if UNITY_ANY_INSTANCING_ENABLED
			output.instanceID = input.instanceID;
			#endif
			#if (defined(UNITY_STEREO_INSTANCING_ENABLED))
			output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
			#endif
			#if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
			output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
			#endif
			#if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
			output.cullFace = input.cullFace;
			#endif
			return output;
		}

		// --------------------------------------------------
		// Build Graph Inputs

		SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
		{
			SurfaceDescriptionInputs output;
			ZERO_INITIALIZE(SurfaceDescriptionInputs, output);





		#if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
		#define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
		#else
		#define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
		#endif
		#undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN

			return output;
		}


		// --------------------------------------------------
		// Main

		#include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
		#include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/UnlitPass.hlsl"

		ENDHLSL
	}
	}
		FallBack "Hidden/Shader Graph/FallbackError"
}
