Shader "CameraFilterPack/Blur_Focus"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _TimeX ("Time", Range(0, 1)) = 1
    _Size ("Size", Range(0, 1)) = 1
    _Circle ("Circle", Range(0, 1)) = 1
    _Distortion ("_Distortion", Range(0, 1)) = 0.3
    _ScreenResolution ("_ScreenResolution", Vector) = (0,0,0,0)
    _CenterX ("_CenterX", Range(-1, 1)) = 0
    _CenterY ("_CenterY", Range(-1, 1)) = 0
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
      uniform float _CenterX;
      uniform float _CenterY;
      uniform float _Circle;
      uniform float _Size;
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
              float4 tmpvar_1;
              float4 tx_3;
              float2 uv_4;
              uv_4 = in_f.xlv_TEXCOORD0;
              tx_3 = float4(0, 0, 0, 0);
              float u_2 = 0;
              while((u_2<_Size))
              {
                  float2 tmpvar_5;
                  tmpvar_5.x = (0.5 + (_CenterX / 2));
                  tmpvar_5.y = (0.5 + (_CenterY / 2));
                  float2 tmpvar_6;
                  tmpvar_6 = (uv_4 - tmpvar_5);
                  float4 tmpvar_7;
                  tmpvar_7.zw = float2(0, 0);
                  tmpvar_7.xy = float2((uv_4 + ((tmpvar_6 * dot(tmpvar_6, tmpvar_6)) * (u_2 / _Circle))));
                  tx_3 = (tx_3 + impl_low_texture2DLodEXT(_MainTex, tmpvar_7.xy, 0));
                  u_2 = (u_2 + 0.2);
              }
              tx_3 = (tx_3 / (_Size * 5));
              float4 tmpvar_8;
              tmpvar_8.w = 1;
              tmpvar_8.xyz = tx_3.xyz;
              tmpvar_1 = tmpvar_8;
              out_f.color = tmpvar_1;
          }
      
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
