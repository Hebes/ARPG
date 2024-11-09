Shader "CameraFilterPack/Blur_Noise"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _TimeX ("Time", Range(0, 1)) = 1
    _Distortion ("_Distortion", Range(0, 1)) = 0.3
    _ScreenResolution ("_ScreenResolution", Vector) = (0,0,0,0)
    _Level ("_Level", Range(1, 16)) = 4
    _Distance ("_Distance", Vector) = (30,0,0,0)
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
      //#pragma target 4.0
      
      #pragma vertex vert
      #pragma fragment frag
      
      #include "UnityCG.cginc"
      
      
      #define CODE_BLOCK_VERTEX
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_MatrixVP;
      uniform sampler2D _MainTex;
      uniform float _TimeX;
      uniform float4 _ScreenResolution;
      uniform float _Level;
      uniform float4 _Distance;
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
      float4 impl_low_texture2DLodEXT(sampler2D sampler, float2 coord, float lod)
      {
          #if defined( GL_EXT_shader_texture_lod)
          {
              return tex2Dlod(sampler, float4(coord, lod));
              #else
              return tex2D(sampler, coord, lod);
              #endif
          }
      
          OUT_Data_Frag frag(v2f in_f)
          {
              float4 sum_2;
              float2 p_3;
              float2 stepfloat_4;
              int num_samples_5;
              float2 uv_6;
              uv_6 = in_f.xlv_TEXCOORD0;
              int tmpvar_7;
              tmpvar_7 = int(_Level);
              num_samples_5 = tmpvar_7;
              float tmpvar_8;
              tmpvar_8 = float(tmpvar_7);
              float2 tmpvar_9;
              tmpvar_9 = (_Distance.xy / _ScreenResolution.xy);
              float2 tmpvar_10;
              tmpvar_10 = (uv_6 - (0.5 * tmpvar_9));
              float2 tmpvar_11;
              tmpvar_11 = (((uv_6 + (0.5 * tmpvar_9)) - tmpvar_10) / (tmpvar_8 - 1));
              stepfloat_4 = tmpvar_11;
              p_3 = (tmpvar_10 + ((frac((sin(dot((uv_6 + frac(_TimeX)), float2(12.9898, 78.233))) * 43758.55)) - 0.5) * tmpvar_11));
              float4 tmpvar_12;
              tmpvar_12 = impl_low_texture2DLodEXT(_MainTex, p_3, 0);
              float4 tmpvar_13;
              tmpvar_13 = tmpvar_12;
              sum_2 = tmpvar_13;
              int i_1_1 = 1;
              while((i_1_1<num_samples_5))
              {
                  p_3 = (p_3 + stepfloat_4);
                  float4 tmpvar_14;
                  tmpvar_14 = impl_low_texture2DLodEXT(_MainTex, p_3, 0);
                  sum_2 = (sum_2 + tmpvar_14);
                  i_1_1 = (i_1_1 + 1);
              }
              sum_2 = (sum_2 / tmpvar_8);
              out_f.color = sum_2;
          }
      
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
