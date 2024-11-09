Shader "CameraFilterPack/Blur_Movie"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _TimeX ("Time", Range(0, 1)) = 1
    _Distortion ("_Distortion", Range(0, 1)) = 0.3
    _ScreenResolution ("_ScreenResolution", Vector) = (0,0,0,0)
    _Radius ("_Radius", Range(0, 1000)) = 700
    _Factor ("_Factor", Range(0, 1000)) = 200
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
      uniform float _Radius;
      uniform float _Factor;
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
              float2 tmpvar_1;
              tmpvar_1 = in_f.xlv_TEXCOORD0;
              float4 accumW_3;
              float4 accumColx_4;
              float radius_5;
              float factor_6;
              factor_6 = ((_Factor / _ScreenResolution.y) * 64);
              radius_5 = ((_Radius / _ScreenResolution.x) * 2);
              accumColx_4 = float4(0, 0, 0, 0);
              accumW_3 = float4(0, 0, 0, 0);
              float j_2 = (-5);
              while((j_2<5))
              {
                  float u_7 = (-5);
                  while((u_7<5))
                  {
                      float2 tmpvar_8;
                      tmpvar_8.x = (u_7 + j_2);
                      tmpvar_8.y = (j_2 - u_7);
                      float4 tmpvar_9;
                      tmpvar_9.zw = float2(0, 0);
                      tmpvar_9.xy = (tmpvar_1 + (((1 / _ScreenResolution.xy) * tmpvar_8) * radius_5));
                      float4 tmpvar_10;
                      float4 tmpvar_11;
                      tmpvar_11 = impl_low_texture2DLodEXT(_MainTex, tmpvar_9.xy, 0);
                      tmpvar_10 = tmpvar_11;
                      float4 tmpvar_12;
                      float _tmp_dvx_55 = (1 + ((tmpvar_10 * tmpvar_10) * (tmpvar_10 * factor_6)));
                      tmpvar_12 = float4(_tmp_dvx_55, _tmp_dvx_55, _tmp_dvx_55, _tmp_dvx_55);
                      accumColx_4 = (accumColx_4 + (tmpvar_10 * tmpvar_12));
                      accumW_3 = (accumW_3 + tmpvar_12);
                      u_7 = (u_7 + 1);
                  }
                  j_2 = (j_2 + 1);
              }
              accumColx_4 = (accumColx_4 / accumW_3);
              out_f.color = accumColx_4;
          }
      
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
