Shader "CameraFilterPack/Edge_Sigmoid"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _TimeX ("Time", Range(0, 1)) = 1
    _Distortion ("_Distortion", Range(0, 1)) = 0.3
    _ScreenResolution ("_ScreenResolution", Vector) = (0,0,0,0)
    _Gain ("_Gain", Range(1, 10)) = 3
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
      uniform float4 _ScreenResolution;
      uniform float _Gain;
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
              float3 c_1;
              float4 colorSum_3;
              float2 p_4;
              float2 uv_5;
              uv_5 = in_f.xlv_TEXCOORD0;
              p_4 = (1 / _ScreenResolution.xy);
              colorSum_3 = float4(0, 0, 0, 0);
              int a_2 = (-1);
              while((float(a_2)<=1))
              {
                  int b_6 = (-1);
                  while((float(b_6)<=1))
                  {
                      if(((float(b_6)==0) && (float(a_2)==0)))
                      {
                          float2 tmpvar_7;
                          tmpvar_7.x = float(b_6);
                          tmpvar_7.y = float(a_2);
                          float4 tmpvar_8;
                          tmpvar_8.zw = float2(0, 0);
                          tmpvar_8.xy = float2((uv_5 + (p_4 * tmpvar_7)));
                          colorSum_3 = (colorSum_3 + (impl_low_texture2DLodEXT(_MainTex, tmpvar_8.xy, 0) * 8));
                      }
                      else
                      {
                          float2 tmpvar_9;
                          tmpvar_9.x = float(b_6);
                          tmpvar_9.y = float(a_2);
                          float4 tmpvar_10;
                          tmpvar_10.zw = float2(0, 0);
                          tmpvar_10.xy = float2((uv_5 + (p_4 * tmpvar_9)));
                          colorSum_3 = (colorSum_3 - impl_low_texture2DLodEXT(_MainTex, tmpvar_10.xy, 0));
                      }
                      b_6 = (b_6 + 1);
                  }
                  a_2 = (a_2 + 1);
              }
              float3 tmpvar_11;
              tmpvar_11 = colorSum_3.xyz;
              c_1 = tmpvar_11;
              float tmpvar_12;
              tmpvar_12 = ((1 - (1 / (1 + exp((-9 * ((((c_1.x + c_1.y) + c_1.z) / (10 - _Gain)) - 0.18)))))) * 2);
              float4 tmpvar_13;
              tmpvar_13.w = 1;
              tmpvar_13.x = tmpvar_12;
              tmpvar_13.y = tmpvar_12;
              tmpvar_13.z = tmpvar_12;
              out_f.color = tmpvar_13;
          }
      
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
