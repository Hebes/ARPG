Shader "CameraFilterPack/FB_TV_50"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "red" {}
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
        "QUEUE" = "Geometry"
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
          float tmpvar_5;
          tmpvar_5 = (((sin(((0.3 * _TimeX) + (q_4.y * 21))) * sin(((0.7 * _TimeX) + (q_4.y * 29)))) * sin(((0.3 + (0.33 * _TimeX)) + (q_4.y * 31)))) * 0.0017);
          float2 tmpvar_6;
          tmpvar_6.x = ((tmpvar_5 + q_4.x) + 0.001);
          tmpvar_6.y = (q_4.y + 0.001);
          float4 tmpvar_7;
          tmpvar_7 = tex2D(_MainTex, tmpvar_6);
          text_2 = tmpvar_7;
          col_3 = (text_2.xyz + 0.05);
          float2 tmpvar_8;
          tmpvar_8.y = (-0.02);
          tmpvar_8.x = (tmpvar_5 + 0.025);
          float4 tmpvar_9;
          float2 P_10;
          float _tmp_dvx_83 = ((0.75 * tmpvar_8) + (q_4 + float2(0.001, 0.001)));
          P_10 = float2(_tmp_dvx_83, _tmp_dvx_83);
          tmpvar_9 = tex2D(_MainTex, P_10);
          text_2 = tmpvar_9;
          col_3.x = (col_3.x + (0.08 * text_2.x));
          col_3.y = (col_3.y + (0.05 * text_2.y));
          col_3.z = (col_3.z + (0.08 * text_2.z));
          col_3 = (clamp(((col_3 * 0.6) + ((0.4 * col_3) * col_3)), float3(0, 0, 0), float3(1, 1, 1)) * pow(((((16 * q_4.x) * q_4.y) * (1 - q_4.x)) * (1 - q_4.y)), 0.3));
          col_3 = (col_3 * float3(3.66, 2.94, 2.66));
          col_3 = (col_3 * col_3.zxy);
          col_3 = (col_3 * 1.1);
          float _tmp_dvx_84 = dot(col_3, float3(0.222, 0.707, 0.071));
          col_3 = float3(_tmp_dvx_84, _tmp_dvx_84, _tmp_dvx_84);
          float _tmp_dvx_85 = clamp(((frac((sin(dot(((floor((q_4 * float2(250, 250))) * _TimeX) / 1000), float2(12.9898, 78.233))) * 43758.55)) + 1) - 0.75), 0, 1);
          col_3 = (col_3 * ((1 + (0.01 * sin((110 * _TimeX)))) + float3(_tmp_dvx_85, _tmp_dvx_85, _tmp_dvx_85)));
          if(((((q_4.x<0) || (q_4.x>1)) || (q_4.y<0)) || (q_4.y>1)))
          {
              col_3 = float3(0, 0, 0);
          }
          float4 tmpvar_11;
          tmpvar_11.w = 1;
          tmpvar_11.xyz = float3(col_3);
          tmpvar_1 = tmpvar_11;
          out_f.color = tmpvar_1;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
