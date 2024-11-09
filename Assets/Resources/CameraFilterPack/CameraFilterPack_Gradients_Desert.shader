Shader "CameraFilterPack/Gradients_Desert"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _TimeX ("Time", Range(0, 1)) = 1
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
      uniform float _Value;
      uniform float _Value2;
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
          float4 tc_1;
          float2 uv_2;
          uv_2 = in_f.xlv_TEXCOORD0;
          float4 tmpvar_3;
          tmpvar_3 = tex2D(_MainTex, uv_2);
          tc_1 = tmpvar_3;
          float tmpvar_4;
          tmpvar_4 = (((0.2126 * tc_1.x) + (0.7152 * tc_1.y)) + (0.0722 * tc_1.z));
          float tmpvar_5;
          tmpvar_5 = lerp(tmpvar_4, (1 - tmpvar_4), _Value);
          float tmpvar_6;
          tmpvar_6 = sqrt(clamp((1 - ((tmpvar_5 - 0.4) / 0.6)), 0, 1));
          float tmpvar_7;
          tmpvar_7 = clamp(((tmpvar_5 - 0.4) / 0.5), 0, 1);
          float3 tmpvar_8;
          tmpvar_8.z = 1;
          tmpvar_8.x = tmpvar_6;
          tmpvar_8.y = tmpvar_6;
          float3 tmpvar_9;
          float _tmp_dvx_1 = (tmpvar_7 * (tmpvar_7 * (3 - (2 * tmpvar_7))));
          float _tmp_dvx_2 = sqrt((lerp(float3(1, 1, 1), float3(0, 0.8, 1), float3(_tmp_dvx_1, _tmp_dvx_1, _tmp_dvx_1)) * tmpvar_8));
          tmpvar_9 = float3(_tmp_dvx_2, _tmp_dvx_2, _tmp_dvx_2);
          float3 tmpvar_10;
          tmpvar_10.xz = float2(0.85, 0.5);
          tmpvar_10.y = (0.75 + max((0.8 - (tmpvar_5 * 20)), 0));
          float s_11;
          s_11 = (tmpvar_5 / 0.4);
          float3 tmpvar_12;
          float _tmp_dvx_3 = (s_11 * s_11);
          tmpvar_12 = lerp(float3(0.7, 0.3, 0), tmpvar_10, float3(_tmp_dvx_3, _tmp_dvx_3, _tmp_dvx_3));
          float3 tmpvar_13;
          if((tmpvar_5>0.4))
          {
              tmpvar_13 = tmpvar_9;
          }
          else
          {
              tmpvar_13 = tmpvar_12;
          }
          float4 tmpvar_14;
          tmpvar_14.w = 0;
          tmpvar_14.xyz = float3((clamp(tmpvar_13, float3(0, 0, 0), float3(1, 1, 1)) * clamp((1.5 * (1 - abs((tmpvar_5 - 0.4)))), 0, 1)));
          float4 tmpvar_15;
          tmpvar_15.w = 1;
          tmpvar_15.xyz = lerp(tc_1, tmpvar_14, float4(_Value2, _Value2, _Value2, _Value2)).xyz;
          tc_1 = tmpvar_15;
          out_f.color = tmpvar_15;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}