Shader "CameraFilterPack/Drawing_Manga2"
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
          float rasterPattern_1;
          float4 color2_2;
          float3 color_3;
          float2 uv_4;
          uv_4 = in_f.xlv_TEXCOORD0;
          float4 tmpvar_5;
          tmpvar_5 = tex2D(_MainTex, uv_4);
          color2_2 = tmpvar_5;
          float edge0_6;
          float tmpvar_7;
          tmpvar_7 = (_DotSize / 8);
          edge0_6 = ((0.4 + tmpvar_7) - 0.6);
          float tmpvar_8;
          tmpvar_8 = clamp(((-edge0_6) / (((0.7 + tmpvar_7) - 0.6) - edge0_6)), 0, 1);
          color_3 = (color2_2.xyz + (tmpvar_8 * (tmpvar_8 * (3 - (2 * tmpvar_8)))));
          float tmpvar_9;
          float2 sh_10;
          sh_10 = in_f.xlv_TEXCOORD0;
          float d_11;
          d_11 = ((2136.281 / _DotSize) / 0.6);
          float2 tmpvar_12;
          tmpvar_12 = (sh_10 * 0.7071064);
          tmpvar_9 = ((0.5 + (0.25 * cos(((tmpvar_12.x + tmpvar_12.y) * d_11)))) + (0.25 * cos(((tmpvar_12.x - tmpvar_12.y) * d_11))));
          rasterPattern_1 = tmpvar_9;
          if((color_3.x>0.3))
          {
              color_3 = (color_3 + rasterPattern_1);
          }
          float3 col_13;
          float4 tmpvar_14;
          float2 P_15;
          P_15 = (uv_4 + float2(-0.00390625, (-0.00390625)));
          tmpvar_14 = tex2D(_MainTex, P_15);
          float tmpvar_16;
          tmpvar_16 = dot(tmpvar_14, float4(0.3, 0.59, 0.11, 0.25));
          float4 tmpvar_17;
          float2 P_18;
          P_18 = (uv_4 + float2(0.00390625, (-0.00390625)));
          tmpvar_17 = tex2D(_MainTex, P_18);
          float tmpvar_19;
          tmpvar_19 = dot(tmpvar_17, float4(0.3, 0.59, 0.11, 0.25));
          float4 tmpvar_20;
          float2 P_21;
          P_21 = (uv_4 + float2(-0.00390625, 0));
          tmpvar_20 = tex2D(_MainTex, P_21);
          float tmpvar_22;
          tmpvar_22 = dot(tmpvar_20, float4(0.3, 0.59, 0.11, 0.25));
          float4 tmpvar_23;
          float2 P_24;
          P_24 = (uv_4 + float2(-0.00390625, 0.00390625));
          tmpvar_23 = tex2D(_MainTex, P_24);
          float tmpvar_25;
          tmpvar_25 = dot(tmpvar_23, float4(0.3, 0.59, 0.11, 0.25));
          float4 tmpvar_26;
          float2 P_27;
          P_27 = (uv_4 + float2(0, 0.00390625));
          tmpvar_26 = tex2D(_MainTex, P_27);
          float4 tmpvar_28;
          float2 P_29;
          P_29 = (uv_4 + float2(0.00390625, 0.00390625));
          tmpvar_28 = tex2D(_MainTex, P_29);
          float tmpvar_30;
          tmpvar_30 = dot(tmpvar_28, float4(0.3, 0.59, 0.11, 0.25));
          float tmpvar_31;
          tmpvar_31 = (((((tmpvar_19 + tmpvar_30) + (2 * tmpvar_22)) - tmpvar_16) - (2 * tmpvar_22)) - tmpvar_25);
          float tmpvar_32;
          tmpvar_32 = (((((tmpvar_25 + (2 * dot(tmpvar_26, float4(0.3, 0.59, 0.11, 0.25)))) + tmpvar_30) - tmpvar_16) - (2 * tmpvar_16)) - tmpvar_19);
          if((((tmpvar_31 * tmpvar_31) + (tmpvar_32 * tmpvar_32))>0.04))
          {
              col_13 = float3(-1, (-1), (-1));
          }
          else
          {
              col_13 = float3(0, 0, 0);
          }
          color_3 = (color_3 + (col_13.y * rasterPattern_1));
          float4 tmpvar_33;
          tmpvar_33.w = 1;
          tmpvar_33.xyz = float3(color_3);
          out_f.color = tmpvar_33;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
