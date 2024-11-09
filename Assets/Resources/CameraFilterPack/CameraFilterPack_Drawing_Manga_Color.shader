Shader "CameraFilterPack/Drawing_Manga_Color"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _TimeX ("Time", Range(0, 1)) = 1
    _Distortion ("_Distortion", Range(0, 1)) = 0.3
    _ScreenResolution ("_ScreenResolution", Vector) = (0,0,0,0)
    _DotSize ("_DotSize", Range(0, 1)) = 0
    _ColorLevel ("_ColorLevel", Range(0, 10)) = 7
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
      uniform float _DotSize;
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
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          float2 resText_1;
          float lum_2;
          float3 color_3;
          float2 uv_4;
          uv_4 = in_f.xlv_TEXCOORD0;
          float3 tmpvar_5;
          tmpvar_5 = (tex2D(_MainTex, uv_4) * 2).xyz;
          color_3 = tmpvar_5;
          float tmpvar_6;
          float2 sh_7;
          sh_7 = in_f.xlv_TEXCOORD0;
          float d_8;
          d_8 = ((2136.281 / _DotSize) / 1.6);
          float2 tmpvar_9;
          tmpvar_9 = (sh_7 * 0.7071064);
          tmpvar_6 = ((0.5 + (0.25 * cos(((tmpvar_9.x + tmpvar_9.y) * d_8)))) + (0.25 * cos(((tmpvar_9.x - tmpvar_9.y) * d_8))));
          float3 col_10;
          float tmpvar_11;
          tmpvar_11 = (0.001953125 * _DotSize);
          float2 tmpvar_12;
          tmpvar_12.x = (-tmpvar_11);
          tmpvar_12.y = (-tmpvar_11);
          float4 tmpvar_13;
          float2 P_14;
          P_14 = (uv_4 + tmpvar_12);
          tmpvar_13 = tex2D(_MainTex, P_14);
          float tmpvar_15;
          tmpvar_15 = dot(tmpvar_13, float4(0.1125, 0.22125, 0.04125, 0.25));
          float2 tmpvar_16;
          tmpvar_16.x = tmpvar_11;
          tmpvar_16.y = (-tmpvar_11);
          float4 tmpvar_17;
          float2 P_18;
          P_18 = (uv_4 + tmpvar_16);
          tmpvar_17 = tex2D(_MainTex, P_18);
          float tmpvar_19;
          tmpvar_19 = dot(tmpvar_17, float4(0.1125, 0.22125, 0.04125, 0.25));
          float tmpvar_20;
          tmpvar_20 = (((((tmpvar_19 + tmpvar_19) + (2 * tmpvar_19)) - tmpvar_15) - (2 * tmpvar_15)) - tmpvar_15);
          float tmpvar_21;
          tmpvar_21 = (((((tmpvar_15 + (2 * tmpvar_15)) + tmpvar_19) - tmpvar_15) - (2 * tmpvar_15)) - tmpvar_19);
          if((((tmpvar_20 * tmpvar_20) + (tmpvar_21 * tmpvar_21))>0.04))
          {
              col_10 = float3(-1, (-1), (-1));
          }
          else
          {
              col_10 = float3(0, 0, 0);
          }
          color_3 = (color_3 + (col_10 * tmpvar_6));
          color_3 = (color_3 / 2);
          lum_2 = (floor((2.8 * color_3.x)) / 2.8);
          float2 tmpvar_22;
          tmpvar_22 = (in_f.xlv_TEXCOORD0 * 480);
          resText_1 = tmpvar_22;
          float tmpvar_23;
          tmpvar_23 = sin((((6.3 * (resText_1.x + resText_1.y)) * lum_2) / 2));
          float tmpvar_24;
          tmpvar_24 = sin((((6.3 * (resText_1.x - resText_1.y)) * lum_2) / 2));
          float tmpvar_25;
          if((color_3.y>color_3.x))
          {
              tmpvar_25 = tmpvar_24;
          }
          else
          {
              tmpvar_25 = tmpvar_23;
          }
          color_3 = (color_3 - (float3(tmpvar_25, tmpvar_25, tmpvar_25) / 8));
          float4 tmpvar_26;
          tmpvar_26.w = 1;
          tmpvar_26.xyz = float3(color_3);
          out_f.color = tmpvar_26;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
