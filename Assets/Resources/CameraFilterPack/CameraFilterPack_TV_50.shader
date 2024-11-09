Shader "CameraFilterPack/TV_50"
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
          float2 q_4;
          q_4 = in_f.xlv_TEXCOORD0;
          float2 uv_5;
          uv_5 = ((q_4 - 0.5) * 2);
          uv_5 = (uv_5 * 1.1);
          float tmpvar_6;
          tmpvar_6 = (abs(uv_5.y) * 0.2);
          uv_5.x = (uv_5.x * (1 + (tmpvar_6 * tmpvar_6)));
          float tmpvar_7;
          tmpvar_7 = (abs(uv_5.x) * 0.25);
          uv_5.y = (uv_5.y * (1 + (tmpvar_7 * tmpvar_7)));
          uv_5 = ((uv_5 / 2) + 0.5);
          uv_5 = ((uv_5 * 0.92) + 0.04);
          float tmpvar_8;
          tmpvar_8 = (((sin(((0.3 * _TimeX) + (uv_5.y * 21))) * sin(((0.7 * _TimeX) + (uv_5.y * 29)))) * sin(((0.3 + (0.33 * _TimeX)) + (uv_5.y * 31)))) * 0.0017);
          float2 tmpvar_9;
          tmpvar_9.x = ((tmpvar_8 + uv_5.x) + 0.001);
          tmpvar_9.y = (uv_5.y + 0.001);
          float4 tmpvar_10;
          tmpvar_10 = tex2D(_MainTex, tmpvar_9);
          text_2 = tmpvar_10;
          col_3 = (text_2.xyz + 0.05);
          float2 tmpvar_11;
          tmpvar_11.y = (-0.02);
          tmpvar_11.x = (tmpvar_8 + 0.025);
          float4 tmpvar_12;
          float2 P_13;
          float _tmp_dvx_13 = ((0.75 * tmpvar_11) + (uv_5 + float2(0.001, 0.001)));
          P_13 = float2(_tmp_dvx_13, _tmp_dvx_13);
          tmpvar_12 = tex2D(_MainTex, P_13);
          text_2 = tmpvar_12;
          col_3.x = (col_3.x + (0.08 * text_2.x));
          col_3.y = (col_3.y + (0.05 * text_2.y));
          col_3.z = (col_3.z + (0.08 * text_2.z));
          col_3 = (clamp(((col_3 * 0.6) + ((0.4 * col_3) * col_3)), float3(0, 0, 0), float3(1, 1, 1)) * pow(((((16 * uv_5.x) * uv_5.y) * (1 - uv_5.x)) * (1 - uv_5.y)), 0.3));
          col_3 = (col_3 * float3(3.66, 2.94, 2.66));
          col_3 = (col_3 * (0.4 + (0.7 * pow(clamp((0.35 + (0.35 * sin(((3.5 * _TimeX) + ((uv_5.y * _ScreenResolution.y) * 1.5))))), 0, 1), 1.7))));
          float _tmp_dvx_14 = dot(col_3, float3(0.222, 0.707, 0.071));
          col_3 = float3(_tmp_dvx_14, _tmp_dvx_14, _tmp_dvx_14);
          float _tmp_dvx_15 = clamp(((frac((sin(dot(((floor((uv_5 * float2(250, 250))) * _TimeX) / 1000), float2(12.9898, 78.233))) * 43758.55)) + 1) - 0.75), 0, 1);
          col_3 = (col_3 * ((1 + (0.01 * sin((110 * _TimeX)))) + (float3(_tmp_dvx_15, _tmp_dvx_15, _tmp_dvx_15) * clamp(floor((sin(((uv_5.y * 6) + (_TimeX * 2.6))) + 1.95)), 0, 1.1))));
          if(((((uv_5.x<0) || (uv_5.x>1)) || (uv_5.y<0)) || (uv_5.y>1)))
          {
              col_3 = float3(0, 0, 0);
          }
          float4 tmpvar_14;
          tmpvar_14.w = 1;
          tmpvar_14.xyz = float3(col_3);
          tmpvar_1 = tmpvar_14;
          out_f.color = tmpvar_1;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
