Shader "CameraFilterPack/Atmosphere_Snow_8bits"
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
      uniform float _TimeX;
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
          float3 col_1;
          float2 uv_2;
          uv_2 = in_f.xlv_TEXCOORD0;
          float3 tmpvar_3;
          tmpvar_3 = tex2D(_MainTex, uv_2).xyz;
          col_1 = tmpvar_3;
          uv_2 = (uv_2 * 2);
          uv_2 = ((uv_2 - 0.5) * float2(1.076716, 0.916887));
          float2 x_4;
          float2 tmpvar_5;
          tmpvar_5 = (float2(_TimeX, _TimeX) * float2(0.02, 0.501));
          x_4 = ((uv_2 * 1.01) + tmpvar_5);
          float tmpvar_6;
          tmpvar_6 = clamp(((((frac((sin(dot(floor(((x_4 - floor(x_4)) * _Value2)), float2(12.9898, 78.233))) * 43758.55)) * 0.9) * _Value) - 0.9) / 0.1), 0, 1);
          float2 x_7;
          x_7 = ((uv_2 * 1.07) + tmpvar_5);
          float tmpvar_8;
          tmpvar_8 = clamp((((frac((sin(dot(floor(((x_7 - floor(x_7)) * _Value2)), float2(12.9898, 78.233))) * 43758.55)) * _Value) - 0.9) / 0.1), 0, 1);
          float2 x_9;
          x_9 = (uv_2 + (float2(_TimeX, _TimeX) * float2(0.05, 0.5)));
          float tmpvar_10;
          tmpvar_10 = clamp(((((frac((sin(dot(floor(((x_9 - floor(x_9)) * _Value2)), float2(12.9898, 78.233))) * 43758.55)) * 0.98) * _Value) - 0.9) / 0.1), 0, 1);
          float2 x_11;
          x_11 = ((uv_2 * 0.9) + (float2(_TimeX, _TimeX) * float2(0.02, 0.51)));
          float tmpvar_12;
          tmpvar_12 = clamp(((((frac((sin(dot(floor(((x_11 - floor(x_11)) * _Value2)), float2(12.9898, 78.233))) * 43758.55)) * 0.99) * _Value) - 0.9) / 0.1), 0, 1);
          float2 x_13;
          x_13 = ((uv_2 * 0.75) + (float2(_TimeX, _TimeX) * float2(0.07, 0.493)));
          float tmpvar_14;
          tmpvar_14 = clamp((((frac((sin(dot(floor(((x_13 - floor(x_13)) * _Value2)), float2(12.9898, 78.233))) * 43758.55)) * _Value) - 0.9) / 0.1), 0, 1);
          float2 x_15;
          x_15 = ((uv_2 * 0.5) + (float2(_TimeX, _TimeX) * float2(0.03, 0.504)));
          float tmpvar_16;
          tmpvar_16 = clamp((((frac((sin(dot(floor(((x_15 - floor(x_15)) * _Value2)), float2(12.9898, 78.233))) * 43758.55)) * _Value) - 0.94) / 0.06), 0, 1);
          float2 x_17;
          x_17 = ((uv_2 * 0.3) + (float2(_TimeX, _TimeX) * float2(0.02, 0.497)));
          float tmpvar_18;
          tmpvar_18 = clamp((((frac((sin(dot(floor(((x_17 - floor(x_17)) * _Value2)), float2(12.9898, 78.233))) * 43758.55)) * _Value) - 0.95) / 0.05000001), 0, 1);
          float2 x_19;
          x_19 = ((uv_2 * 0.1) + (float2(_TimeX, _TimeX) * float2(0, 0.51)));
          float tmpvar_20;
          tmpvar_20 = clamp((((frac((sin(dot(floor(((x_19 - floor(x_19)) * _Value2)), float2(12.9898, 78.233))) * 43758.55)) * _Value) - 0.96) / 0.04000002), 0, 1);
          float2 x_21;
          x_21 = ((uv_2 * 0.03) + (float2(_TimeX, _TimeX) * float2(0, 0.523)));
          float tmpvar_22;
          tmpvar_22 = clamp((((frac((sin(dot(floor(((x_21 - floor(x_21)) * _Value2)), float2(12.9898, 78.233))) * 43758.55)) * _Value) - 0.99) / 0.00999999), 0, 1);
          float3 tmpvar_23;
          float _tmp_dvx_114 = (tmpvar_22 * (tmpvar_22 * (3 - (2 * tmpvar_22))));
          float _tmp_dvx_115 = (tmpvar_20 * (tmpvar_20 * (3 - (2 * tmpvar_20))));
          float _tmp_dvx_116 = (tmpvar_18 * (tmpvar_18 * (3 - (2 * tmpvar_18))));
          float _tmp_dvx_117 = (tmpvar_16 * (tmpvar_16 * (3 - (2 * tmpvar_16))));
          float _tmp_dvx_118 = (tmpvar_14 * (tmpvar_14 * (3 - (2 * tmpvar_14))));
          float _tmp_dvx_119 = (tmpvar_12 * (tmpvar_12 * (3 - (2 * tmpvar_12))));
          float _tmp_dvx_120 = (tmpvar_10 * (tmpvar_10 * (3 - (2 * tmpvar_10))));
          float _tmp_dvx_121 = (tmpvar_8 * (tmpvar_8 * (3 - (2 * tmpvar_8))));
          float _tmp_dvx_122 = (tmpvar_6 * (tmpvar_6 * (3 - (2 * tmpvar_6))));
          tmpvar_23 = lerp(lerp(lerp(lerp(lerp(lerp(lerp(lerp(lerp(col_1, float3(1, 1, 1), float3(_tmp_dvx_122, _tmp_dvx_122, _tmp_dvx_122)), float3(1, 1, 1), float3(_tmp_dvx_121, _tmp_dvx_121, _tmp_dvx_121)), float3(1, 1, 1), float3(_tmp_dvx_120, _tmp_dvx_120, _tmp_dvx_120)), float3(1, 1, 1), float3(_tmp_dvx_119, _tmp_dvx_119, _tmp_dvx_119)), float3(1, 1, 1), float3(_tmp_dvx_118, _tmp_dvx_118, _tmp_dvx_118)), float3(1, 1, 1), float3(_tmp_dvx_117, _tmp_dvx_117, _tmp_dvx_117)), float3(1, 1, 1), float3(_tmp_dvx_116, _tmp_dvx_116, _tmp_dvx_116)), float3(1, 1, 1), float3(_tmp_dvx_115, _tmp_dvx_115, _tmp_dvx_115)), float3(1, 1, 1), float3(_tmp_dvx_114, _tmp_dvx_114, _tmp_dvx_114));
          col_1 = tmpvar_23;
          float4 tmpvar_24;
          tmpvar_24.w = 1;
          tmpvar_24.xyz = float3(tmpvar_23);
          out_f.color = tmpvar_24;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
