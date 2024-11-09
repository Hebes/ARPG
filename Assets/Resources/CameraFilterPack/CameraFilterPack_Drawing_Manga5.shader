Shader "CameraFilterPack/Drawing_Manga5"
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
          edge0_6 = ((0.4 + (_DotSize / 16)) - 0.6);
          float tmpvar_7;
          tmpvar_7 = clamp(((-edge0_6) / (((0.7 + (_DotSize / 8)) - 0.6) - edge0_6)), 0, 1);
          color_3 = (color2_2.xyz + (tmpvar_7 * (tmpvar_7 * (3 - (2 * tmpvar_7)))));
          float tmpvar_8;
          float2 sh_9;
          sh_9 = in_f.xlv_TEXCOORD0;
          float d_10;
          d_10 = ((2136.281 / _DotSize) / 0.6);
          float2 tmpvar_11;
          tmpvar_11 = (sh_9 * 0.7071064);
          tmpvar_8 = ((0.5 + (0.35 * cos(((tmpvar_11.x + tmpvar_11.y) * d_10)))) + (0.25 * cos(((tmpvar_11.x - tmpvar_11.y) * d_10))));
          rasterPattern_1 = tmpvar_8;
          if((color_3.x>0.4))
          {
              color_3 = (color_3 + ((color2_2.xyz * 2) - rasterPattern_1));
          }
          float3 col_12;
          float4 tmpvar_13;
          float2 P_14;
          P_14 = (uv_4 + float2(-0.00390625, (-0.00390625)));
          tmpvar_13 = tex2D(_MainTex, P_14);
          float tmpvar_15;
          tmpvar_15 = dot(tmpvar_13, float4(0.3, 0.59, 0.11, 0.25));
          float4 tmpvar_16;
          float2 P_17;
          P_17 = (uv_4 + float2(0.00390625, (-0.00390625)));
          tmpvar_16 = tex2D(_MainTex, P_17);
          float tmpvar_18;
          tmpvar_18 = dot(tmpvar_16, float4(0.3, 0.59, 0.11, 0.25));
          float4 tmpvar_19;
          float2 P_20;
          P_20 = (uv_4 + float2(-0.00390625, 0));
          tmpvar_19 = tex2D(_MainTex, P_20);
          float tmpvar_21;
          tmpvar_21 = dot(tmpvar_19, float4(0.3, 0.59, 0.11, 0.25));
          float4 tmpvar_22;
          float2 P_23;
          P_23 = (uv_4 + float2(-0.00390625, 0.00390625));
          tmpvar_22 = tex2D(_MainTex, P_23);
          float tmpvar_24;
          tmpvar_24 = dot(tmpvar_22, float4(0.3, 0.59, 0.11, 0.25));
          float4 tmpvar_25;
          float2 P_26;
          P_26 = (uv_4 + float2(0, 0.00390625));
          tmpvar_25 = tex2D(_MainTex, P_26);
          float4 tmpvar_27;
          float2 P_28;
          P_28 = (uv_4 + float2(0.00390625, 0.00390625));
          tmpvar_27 = tex2D(_MainTex, P_28);
          float tmpvar_29;
          tmpvar_29 = dot(tmpvar_27, float4(0.3, 0.59, 0.11, 0.25));
          float tmpvar_30;
          tmpvar_30 = (((((tmpvar_18 + tmpvar_29) + (2 * tmpvar_21)) - tmpvar_15) - (2 * tmpvar_21)) - tmpvar_24);
          float tmpvar_31;
          tmpvar_31 = (((((tmpvar_24 + (2 * dot(tmpvar_25, float4(0.3, 0.59, 0.11, 0.25)))) + tmpvar_29) - tmpvar_15) - (2 * tmpvar_15)) - tmpvar_18);
          if((((tmpvar_30 * tmpvar_30) + (tmpvar_31 * tmpvar_31))>0.04))
          {
              col_12 = float3(-1, (-1), (-1));
          }
          else
          {
              col_12 = float3(0, 0, 0);
          }
          color_3 = (color_3 + ((col_12.z * rasterPattern_1) * 2));
          float4 tmpvar_32;
          tmpvar_32.w = 1;
          tmpvar_32.xyz = float3(color_3);
          out_f.color = tmpvar_32;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
