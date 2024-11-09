Shader "CameraFilterPack/Edge_Edge_filter"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _TimeX ("Time", Range(0, 1)) = 1
    _Distortion ("_Distortion", Range(0, 1)) = 0.3
    _ScreenResolution ("_ScreenResolution", Vector) = (0,0,0,0)
    _RedAmplifier ("_RedAmplifier", Range(0, 10)) = 0
    _GreenAmplifier ("_GreenAmplifier", Range(0, 10)) = 2
    _BlueAmplifier ("_BlueAmplifier", Range(0, 10)) = 0
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
      uniform float _RedAmplifier;
      uniform float _GreenAmplifier;
      uniform float _BlueAmplifier;
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
          float4 color_2;
          float4 sum_3;
          float4 tmpvar_4;
          float4 tmpvar_5;
          float2 P_6;
          float2 tmpvar_7;
          tmpvar_7 = (in_f.xlv_TEXCOORD0 * _ScreenResolution.xy);
          P_6 = ((tmpvar_7 + float2(0, 1)) / _ScreenResolution.xy);
          tmpvar_5 = tex2D(_MainTex, P_6);
          tmpvar_4 = tmpvar_5;
          float4 tmpvar_8;
          float4 tmpvar_9;
          float2 P_10;
          P_10 = ((tmpvar_7 + float2(0, (-1))) / _ScreenResolution.xy);
          tmpvar_9 = tex2D(_MainTex, P_10);
          tmpvar_8 = tmpvar_9;
          float4 tmpvar_11;
          tmpvar_11 = abs((tmpvar_4 - tmpvar_8));
          sum_3 = tmpvar_11;
          float4 tmpvar_12;
          float4 tmpvar_13;
          float2 P_14;
          P_14 = ((tmpvar_7 + float2(1, 0)) / _ScreenResolution.xy);
          tmpvar_13 = tex2D(_MainTex, P_14);
          tmpvar_12 = tmpvar_13;
          float4 tmpvar_15;
          float4 tmpvar_16;
          float2 P_17;
          P_17 = ((tmpvar_7 + float2(-1, 0)) / _ScreenResolution.xy);
          tmpvar_16 = tex2D(_MainTex, P_17);
          tmpvar_15 = tmpvar_16;
          float4 tmpvar_18;
          tmpvar_18 = abs((tmpvar_12 - tmpvar_15));
          sum_3 = (sum_3 + tmpvar_18);
          sum_3 = (sum_3 / 2);
          float4 tmpvar_19;
          float4 tmpvar_20;
          float2 P_21;
          P_21 = (tmpvar_7 / _ScreenResolution.xy);
          tmpvar_20 = tex2D(_MainTex, P_21);
          tmpvar_19 = tmpvar_20;
          color_2 = tmpvar_19;
          color_2.x = (color_2.x + (sqrt(dot(sum_3, sum_3)) * _RedAmplifier));
          color_2.y = (color_2.y + (sqrt(dot(sum_3, sum_3)) * _GreenAmplifier));
          color_2.z = (color_2.z + (sqrt(dot(sum_3, sum_3)) * _BlueAmplifier));
          tmpvar_1 = color_2;
          out_f.color = tmpvar_1;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
