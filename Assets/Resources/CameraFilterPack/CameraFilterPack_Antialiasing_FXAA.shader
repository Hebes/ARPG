Shader "CameraFilterPack/Antialiasing_FXAA"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _TimeX ("Time", Range(0, 1)) = 1
    _Distortion ("_Distortion", Range(0, 1)) = 0.3
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
          float4 tmpvar_1;
          float lumaB_2;
          float rcpDirMin_3;
          float dirReduce_4;
          float2 dir_5;
          float lumaM_6;
          float lumaSE_7;
          float lumaSW_8;
          float lumaNE_9;
          float lumaNW_10;
          float2 tmpvar_11;
          tmpvar_11 = (1 / _ScreenResolution.xy);
          float2 P_12;
          P_12 = (in_f.xlv_TEXCOORD0 - tmpvar_11);
          float2 P_13;
          P_13 = (in_f.xlv_TEXCOORD0 + (float2(1, (-1)) * tmpvar_11));
          float2 P_14;
          P_14 = (in_f.xlv_TEXCOORD0 + (float2(-1, 1) * tmpvar_11));
          float2 P_15;
          P_15 = (in_f.xlv_TEXCOORD0 + tmpvar_11);
          float tmpvar_16;
          tmpvar_16 = dot(tex2D(_MainTex, P_12).xyz, float3(0.299, 0.587, 0.114));
          lumaNW_10 = tmpvar_16;
          float tmpvar_17;
          tmpvar_17 = dot(tex2D(_MainTex, P_13).xyz, float3(0.299, 0.587, 0.114));
          lumaNE_9 = tmpvar_17;
          float tmpvar_18;
          tmpvar_18 = dot(tex2D(_MainTex, P_14).xyz, float3(0.299, 0.587, 0.114));
          lumaSW_8 = tmpvar_18;
          float tmpvar_19;
          tmpvar_19 = dot(tex2D(_MainTex, P_15).xyz, float3(0.299, 0.587, 0.114));
          lumaSE_7 = tmpvar_19;
          float tmpvar_20;
          tmpvar_20 = dot(tex2D(_MainTex, in_f.xlv_TEXCOORD0).xyz, float3(0.299, 0.587, 0.114));
          lumaM_6 = tmpvar_20;
          float tmpvar_21;
          tmpvar_21 = min(min(lumaM_6, lumaNW_10), min(min(lumaNE_9, lumaSW_8), lumaSE_7));
          float tmpvar_22;
          tmpvar_22 = max(max(lumaM_6, lumaNW_10), max(max(lumaNE_9, lumaSW_8), lumaSE_7));
          dir_5.x = ((lumaSW_8 + lumaSE_7) - (lumaNW_10 + lumaNE_9));
          dir_5.y = ((lumaNW_10 + lumaSW_8) - (lumaNE_9 + lumaSE_7));
          float tmpvar_23;
          float x_24;
          x_24 = (((lumaNW_10 + lumaNE_9) + (lumaSW_8 + lumaSE_7)) * 0.03125);
          tmpvar_23 = max(x_24, 0.0078125);
          dirReduce_4 = tmpvar_23;
          float tmpvar_25;
          tmpvar_25 = (1 / (min(abs(dir_5.x), abs(dir_5.y)) + dirReduce_4));
          rcpDirMin_3 = tmpvar_25;
          dir_5 = (min(float2(8, 8), max(float2(-8, (-8)), (dir_5 * rcpDirMin_3))) * tmpvar_11);
          float2 P_26;
          P_26 = (in_f.xlv_TEXCOORD0 + (dir_5 * (-0.1666667)));
          float2 P_27;
          P_27 = (in_f.xlv_TEXCOORD0 + (dir_5 * 0.1666667));
          float3 tmpvar_28;
          tmpvar_28 = (0.5 * (tex2D(_MainTex, P_26).xyz + tex2D(_MainTex, P_27).xyz));
          float2 P_29;
          P_29 = (in_f.xlv_TEXCOORD0 + (dir_5 * (-0.5)));
          float2 P_30;
          P_30 = (in_f.xlv_TEXCOORD0 + (dir_5 * 0.5));
          float3 tmpvar_31;
          tmpvar_31 = ((tmpvar_28 * 0.5) + (0.25 * (tex2D(_MainTex, P_29).xyz + tex2D(_MainTex, P_30).xyz)));
          float tmpvar_32;
          tmpvar_32 = dot(tmpvar_31, float3(0.299, 0.587, 0.114));
          lumaB_2 = tmpvar_32;
          if(((lumaB_2<tmpvar_21) || (lumaB_2>tmpvar_22)))
          {
              float4 tmpvar_33;
              tmpvar_33.w = 1;
              tmpvar_33.xyz = float3(tmpvar_28);
              tmpvar_1 = tmpvar_33;
          }
          else
          {
              float4 tmpvar_34;
              tmpvar_34.w = 1;
              tmpvar_34.xyz = float3(tmpvar_31);
              tmpvar_1 = tmpvar_34;
          }
          out_f.color = tmpvar_1;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
