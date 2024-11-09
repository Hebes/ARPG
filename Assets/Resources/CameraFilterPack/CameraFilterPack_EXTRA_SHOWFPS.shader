// Upgrade NOTE: commented out 'float4 unity_DynamicLightmapST', a built-in variable
// Upgrade NOTE: commented out 'float4 unity_LightmapST', a built-in variable
// Upgrade NOTE: commented out 'float4 unity_ShadowFadeCenterAndType', a built-in variable
// Upgrade NOTE: commented out 'sampler2D unity_DynamicLightmap', a built-in variable
// Upgrade NOTE: commented out 'sampler2D unity_Lightmap', a built-in variable
// Upgrade NOTE: commented out 'sampler2D unity_LightmapInd', a built-in variable

Shader "CameraFilterPack/EXTRA_SHOWFPS"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _TimeX ("Time", Range(0, 1)) = 1
    _ScreenResolution ("_ScreenResolution", Vector) = (0,0,0,0)
  }
  SubShader
  {
    Tags
    { 
    }
    Pass // ind: 1, name: 
    {
      Tags
      { 
      }
      ZClip Off
      ZTest Always
      // m_ProgramMask = 6
      CGPROGRAM
// Upgrade NOTE: excluded shader from DX11; has structs without semantics (struct v2f_vertex_lit members uv,diff,spec)
#pragma exclude_renderers d3d11
      //#pragma target 4.0
      
      #pragma vertex vert
      #pragma fragment frag
      
      #include "UnityCG.cginc"
      
      
      #define CODE_BLOCK_VERTEX
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_MatrixVP;
      //uniform float4 _Time;
      //uniform float4 _SinTime;
      //uniform float4 _CosTime;
      //uniform float4 unity_DeltaTime;
      //uniform float3 _WorldSpaceCameraPos;
      //uniform float4 _ProjectionParams;
      //uniform float4 _ScreenParams;
      //uniform float4 _ZBufferParams;
      //uniform float4 unity_OrthoParams;
      //uniform float4 unity_CameraWorldClipPlanes[6];
      //uniform float4x4 unity_CameraProjection;
      //uniform float4x4 unity_CameraInvProjection;
      //uniform float4x4 unity_WorldToCamera;
      //uniform float4x4 unity_CameraToWorld;
      //uniform float4 _WorldSpaceLightPos0;
      //uniform float4 _LightPositionRange;
      //uniform float4 unity_4LightPosX0;
      //uniform float4 unity_4LightPosY0;
      //uniform float4 unity_4LightPosZ0;
      //uniform float4 unity_4LightAtten0;
      //uniform float4 unity_LightColor[8];
      //uniform float4 unity_LightPosition[8];
      //uniform float4 unity_LightAtten[8];
      //uniform float4 unity_SpotDirection[8];
      //uniform float4 unity_SHAr;
      //uniform float4 unity_SHAg;
      //uniform float4 unity_SHAb;
      //uniform float4 unity_SHBr;
      //uniform float4 unity_SHBg;
      //uniform float4 unity_SHBb;
      //uniform float4 unity_SHC;
      //uniform float4 unity_OcclusionMaskSelector;
      uniform float4 unity_ProbesOcclusion;
      uniform float3 unity_LightColor0;
      uniform float3 unity_LightColor1;
      uniform float3 unity_LightColor2;
      uniform float3 unity_LightColor3;
      uniform float4x4 unity_ShadowSplitSpheres;
      uniform float4 unity_ShadowSplitSqRadii;
      //uniform float4 unity_LightShadowBias;
      //uniform float4 _LightSplitsNear;
      //uniform float4 _LightSplitsFar;
      //uniform float4x4x4 unity_WorldToShadow;
      //uniform float4 _LightShadowData;
      // uniform float4 unity_ShadowFadeCenterAndType;
      //uniform float4x4 unity_WorldToObject;
      //uniform float4 unity_LODFade;
      //uniform float4 unity_WorldTransformParams;
      //uniform float4x4 glstate_matrix_transpose_modelview0;
      //uniform float4 glstate_lightmodel_ambient;
      //uniform float4 unity_AmbientSky;
      //uniform float4 unity_AmbientEquator;
      //uniform float4 unity_AmbientGround;
      uniform float4 unity_IndirectSpecColor;
      //uniform float4x4 UNITY_MATRIX_P;
      //uniform float4x4 unity_MatrixV;
      //uniform float4x4 unity_MatrixInvV;
      uniform float4 unity_StereoScaleOffset;
      //uniform int unity_StereoEyeIndex;
      uniform float4 unity_ShadowColor;
      //uniform float4 unity_FogColor;
      //uniform float4 unity_FogParams;
      // uniform sampler2D unity_Lightmap;
      // uniform sampler2D unity_LightmapInd;
      // uniform sampler2D unity_DynamicLightmap;
      uniform sampler2D unity_DynamicDirectionality;
      uniform sampler2D unity_DynamicNormal;
      // uniform float4 unity_LightmapST;
      // uniform float4 unity_DynamicLightmapST;
      //uniform samplerCUBE unity_SpecCube0;
      //uniform samplerCUBE unity_SpecCube1;
      SamplerState samplerunity_SpecCube1;
      //uniform float4 unity_SpecCube0_BoxMax;
      //uniform float4 unity_SpecCube0_BoxMin;
      //uniform float4 unity_SpecCube0_ProbePosition;
      //uniform float4 unity_SpecCube0_HDR;
      //uniform float4 unity_SpecCube1_BoxMax;
      //uniform float4 unity_SpecCube1_BoxMin;
      //uniform float4 unity_SpecCube1_ProbePosition;
      //uniform float4 unity_SpecCube1_HDR;
      //uniform float4 unity_Lightmap_HDR;
      uniform float4 unity_DynamicLightmap_HDR;
      uniform sampler2D _MainTex;
      uniform float _TimeX;
      uniform float _Value;
      uniform float _Value2;
      uniform float4 _ScreenResolution;
      struct appdata_t
      {
          float4 vertex :POSITION;
          float4 color :COLOR;
          float4 texcoord :TEXCOORD0;
      };
      
      struct OUT_Data_Vert
      {
          float2 xlv_TEXCOORD0 :TEXCOORD0;
          float4 xlv_COLOR :COLOR;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float2 xlv_TEXCOORD0 :TEXCOORD0;
          float4 xlv_COLOR :COLOR;
      };
      
      struct OUT_Data_Frag
      {
          float4 color :SV_Target0;
      };
      
      OUT_Data_Vert vert(appdata_t in_v)
      {
          OUT_Data_Vert out_v;
          float4 tmpvar_1;
          tmpvar_1 = in_v.color;
          float2 tmpvar_2;
          tmpvar_2 = in_v.texcoord.xy;
          float2 tmpvar_3;
          float4 tmpvar_4;
          float4 tmpvar_5;
          tmpvar_5.w = 1;
          tmpvar_5.xyz = in_v.vertex.xyz;
          tmpvar_3 = tmpvar_2;
          tmpvar_4 = tmpvar_1;
          out_v.xlv_TEXCOORD0 = tmpvar_3;
          out_v.vertex = mul(unity_MatrixVP, mul(unity_ObjectToWorld, tmpvar_5));
          out_v.xlv_COLOR = tmpvar_4;
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      struct v2f_vertex_lit
      {
          float2 uv;
          float4 diff;
          float4 spec;
      };
      
      struct v2f_img
      {
          float4 pos;
          float2 uv;
      };
      
      struct appdata_img
      {
          float4 vertex;
          float2 texcoord;
      };
      
      struct v2f
      {
          float2 texcoord;
          float4 vertex;
          float4 color;
      };
      
      struct appdata_t
      {
          float4 vertex;
          float4 color;
          float2 texcoord;
      };
      
      int D(in float2 p, in float n )
      {
          int i = int(p.y);
          int b = int(pow(2, floor(((30 - p.x) - (n * 3)))));
          i = (((p.x<0) || (p.x>3)))?(0):(((i==5))?(972980223):(((i==4))?(690407533):(((i==3))?(704642687):(((i==2))?(696556137):(((i==1))?(972881535):(0))))));
          return {((i / b) - (2 * ((i / b) / 2)))};
      }
      
      float mod(in float x, in float modu )
      {
          return {(x - (floor((x * (1 / modu))) * modu))};
      }
      
      float4 frag(in v2f i )
      {
          OUT_Data_Frag out_f;
          float2 uv = i.texcoord;
          float4 fps = float4(0, 0, 0, 0);
          uv = (uv * 512);
          uv = (uv / _Value);
          float c = 1000;
          uv.x = (uv.x * 2);
          int n = 0;
          while((n<4))
          {
              uv.x = (uv.x - (4<3));
          }
      
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          float4 xl_retval;
          v2f xlt_i;
          xlt_i.texcoord = float2(in_f.xlv_TEXCOORD0);
          xlt_i.vertex = float4(0, 0, 0, 0);
          xlt_i.color = float4(in_f.xlv_COLOR);
          xl_retval = frag(xlt_i);
          out_f.color = float4(xl_retval);
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
