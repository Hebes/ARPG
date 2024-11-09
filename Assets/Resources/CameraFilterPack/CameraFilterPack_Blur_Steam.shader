Shader "CameraFilterPack/Blur_Steam"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _TimeX ("Time", Range(0, 1)) = 1
    _Distortion ("_Distortion", Range(0, 1)) = 0.3
    _ScreenResolution ("_ScreenResolution", Vector) = (0,0,0,0)
    _Radius ("_Radius", Range(0, 1)) = 0.1
    _Quality ("_Quality", Range(0, 1)) = 0.75
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
      uniform float _Radius;
      uniform float _Quality;
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
      
          float xlat_mutable_Quality;
          OUT_Data_Frag frag(v2f in_f)
          {
              xlat_mutable_Quality = _Quality;
              float4 tmpvar_1;
              float2 uv_2;
              uv_2 = in_f.xlv_TEXCOORD0;
              float r_3;
              float4 col_4;
              col_4 = float4(0, 0, 0, 0);
              if((_Quality==1))
              {
                  xlat_mutable_Quality = 0.99;
              }
              r_3 = 0;
              while(true)
              {
                  float a_5;
                  if((r_3>=1))
                  {
                      break;
                  }
                  a_5 = 0;
                  while(true)
                  {
                      if((a_5>=1))
                      {
                          break;
                      }
                      float2 tmpvar_6;
                      tmpvar_6.x = cos((a_5 * 6.283184));
                      tmpvar_6.y = sin((a_5 * 6.283184));
                      float4 tmpvar_7;
                      tmpvar_7.zw = float2(0, 0);
                      tmpvar_7.xy = float2((uv_2 + (tmpvar_6 * (r_3 * _Radius))));
                      col_4 = (col_4 + impl_low_texture2DLodEXT(_MainTex, tmpvar_7.xy, 0));
                      a_5 = (a_5 + (1 - xlat_mutable_Quality));
                  }
                  r_3 = (r_3 + (1 - xlat_mutable_Quality));
              }
              col_4 = (col_4 * ((1 - xlat_mutable_Quality) * (1 - xlat_mutable_Quality)));
              tmpvar_1 = col_4;
              out_f.color = tmpvar_1;
          }
      
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
