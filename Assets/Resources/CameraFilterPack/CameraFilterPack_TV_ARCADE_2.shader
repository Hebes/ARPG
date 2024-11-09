Shader "CameraFilterPack/TV_ARCADE_2"
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
      uniform float _Value;
      uniform float _Value2;
      uniform float _Value3;
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
          float4 text_2;
          float3 col_3;
          float2 uv_4;
          float2 q_5;
          q_5 = in_f.xlv_TEXCOORD0;
          float2 uv_6;
          uv_6 = ((q_5 - 0.5) * 2);
          uv_6 = (uv_6 * 1.1);
          float tmpvar_7;
          tmpvar_7 = (abs(uv_6.y) * 0.2);
          uv_6.x = (uv_6.x * (1 + (tmpvar_7 * tmpvar_7)));
          float tmpvar_8;
          tmpvar_8 = (abs(uv_6.x) * 0.25);
          uv_6.y = (uv_6.y * (1 + (tmpvar_8 * tmpvar_8)));
          uv_6 = ((uv_6 / 2) + 0.5);
          uv_6 = ((uv_6 * 0.92) + 0.04);
          float tmpvar_9;
          tmpvar_9 = (((sin(((0.3 * _TimeX) + (uv_6.y * 21))) * sin(((0.7 * _TimeX) + (uv_6.y * 29)))) * sin(((0.3 + (0.33 * _TimeX)) + (uv_6.y * 31)))) * 0.0017);
          float2 tmpvar_10;
          tmpvar_10.x = ((tmpvar_9 + uv_6.x) + 0.001);
          tmpvar_10.y = (uv_6.y + 0.001);
          float4 tmpvar_11;
          tmpvar_11 = tex2D(_MainTex, tmpvar_10);
          text_2 = tmpvar_11;
          col_3 = (text_2.xyz + 0.05);
          float2 tmpvar_12;
          tmpvar_12.x = (uv_6.x * (_ScreenResolution.x / _ScreenResolution.y));
          tmpvar_12.y = (uv_6.y * _Value);
          float tmpvar_13;
          tmpvar_13 = (clamp(((tmpvar_12.y - frac(((-_TimeX) * _Value2))) - 0.05), 0, 0.1) * 10);
          float2 tmpvar_14;
          tmpvar_14.y = 0;
          float tmpvar_15;
          tmpvar_15 = (tmpvar_13 - 0.5);
          tmpvar_14.x = ((sin((tmpvar_13 * 10)) * ((-4 * (tmpvar_15 * tmpvar_15)) + 1)) * 0.02);
          uv_4 = (uv_6 + tmpvar_14);
          float2 tmpvar_16;
          tmpvar_16.y = (-0.02);
          tmpvar_16.x = (tmpvar_9 + 0.025);
          float4 tmpvar_17;
          float2 P_18;
          float _tmp_dvx_101 = ((0.75 * tmpvar_16) + (uv_4 + float2(0.001, 0.001)));
          P_18 = float2(_tmp_dvx_101, _tmp_dvx_101);
          tmpvar_17 = tex2D(_MainTex, P_18);
          text_2 = tmpvar_17;
          col_3.x = (col_3.x + (0.08 * text_2.x));
          col_3.y = (col_3.y + (0.05 * text_2.y));
          col_3.z = (col_3.z + (0.08 * text_2.z));
          col_3 = (clamp(((col_3 * 0.6) + ((0.4 * col_3) * col_3)), float3(0, 0, 0), float3(1, 1, 1)) * pow(((((16 * uv_4.x) * uv_4.y) * (1 - uv_4.x)) * (1 - uv_4.y)), 0.3));
          col_3 = (col_3 * float3(2.66, 2.94, 2.66));
          col_3 = (col_3 * (0.4 + (0.7 * (pow(clamp((0.35 + (0.35 * sin(((3.5 * _TimeX) + ((uv_4.y * _ScreenResolution.y) * 1.5))))), 0, 1), 1.7) * _Value3))));
          col_3 = (col_3 * (1 + (0.01 * sin((110 * _TimeX)))));
          if(((((uv_4.x<0) || (uv_4.x>1)) || (uv_4.y<0)) || (uv_4.y>1)))
          {
              col_3 = float3(0, 0, 0);
          }
          float tmpvar_19;
          tmpvar_19 = (in_f.xlv_TEXCOORD0.x * _ScreenResolution.x);
          float tmpvar_20;
          tmpvar_20 = frac(abs(tmpvar_19));
          float tmpvar_21;
          if((tmpvar_19>=0))
          {
              tmpvar_21 = tmpvar_20;
          }
          else
          {
              tmpvar_21 = (-tmpvar_20);
          }
          col_3 = (col_3 * (1 - (0.65 * clamp((tmpvar_21 - 1), 0, 1))));
          float4 tmpvar_22;
          tmpvar_22.w = 1;
          tmpvar_22.xyz = float3(col_3);
          tmpvar_1 = tmpvar_22;
          out_f.color = tmpvar_1;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
