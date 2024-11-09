Shader "CameraFilterPack/EyesVision_1"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _MainTex2 ("Base (RGB)", 2D) = "white" {}
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
      uniform sampler2D _MainTex2;
      uniform float _TimeX;
      uniform float _Value;
      uniform float _Value2;
      uniform float _Value3;
      uniform float _Value4;
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
          float3 col_2;
          float3 oldfilm_3;
          float2 suv_4;
          float t_5;
          float2 uv_6;
          uv_6 = in_f.xlv_TEXCOORD0;
          float tmpvar_7;
          tmpvar_7 = float(int((_TimeX * _Value3)));
          t_5 = (tmpvar_7 + tmpvar_7);
          float2 tmpvar_8;
          tmpvar_8.x = (frac((sin(dot(float2(t_5, t_5), float2(12.9898, 78.233))) * 43758.55)) * (-6));
          float _tmp_dvx_89 = (t_5 + 23);
          tmpvar_8.y = (frac((sin(dot(float2(_tmp_dvx_89, _tmp_dvx_89), float2(12.9898, 78.233))) * 43758.55)) * 4);
          suv_4 = ((uv_6 + (0.004 * tmpvar_8)) * 0.8);
          suv_4 = (suv_4 + float2(0.075, 0.05));
          float time_9;
          time_9 = (_Value2 * _TimeX);
          float tmpvar_10;
          tmpvar_10 = (1 + (0.5 * sin((time_9 + (uv_6.x * _Value)))));
          float tmpvar_11;
          tmpvar_11 = (1 + (0.5 * cos((time_9 + (uv_6.y * _Value)))));
          float2 tmpvar_12;
          tmpvar_12.x = (tmpvar_10 + sin(tmpvar_11));
          tmpvar_12.y = (tmpvar_11 + cos(tmpvar_10));
          suv_4 = (suv_4 + (0.02 * tmpvar_12));
          float3 tmpvar_13;
          tmpvar_13 = tex2D(_MainTex2, suv_4).xyz;
          oldfilm_3 = tmpvar_13;
          uv_6 = in_f.xlv_TEXCOORD0;
          uv_6 = (uv_6 + (oldfilm_3.xy / 8));
          float3 tmpvar_14;
          tmpvar_14 = tex2D(_MainTex, uv_6).xyz;
          col_2 = tmpvar_14;
          col_2.xy = (col_2.xy + float2(0.08, 0.08));
          col_2.z = (col_2.z - 0.03);
          float tmpvar_15;
          float tmpvar_16;
          tmpvar_16 = (_TimeX / 2);
          tmpvar_15 = (t_5 - ((sin(tmpvar_16) * sin(tmpvar_16)) * t_5));
          float2 x_17;
          x_17 = (float2(0.5, 0.5) - uv_6.y);
          float tmpvar_18;
          tmpvar_18 = clamp(((sqrt(dot(x_17, x_17)) - tmpvar_15) / ((tmpvar_15 - 0.6) - tmpvar_15)), 0, 1);
          float3 tmpvar_19;
          float _tmp_dvx_90 = ((1 - (tmpvar_18 * (tmpvar_18 * (3 - (2 * tmpvar_18))))) * _Value4);
          tmpvar_19 = ((oldfilm_3 + col_2) * (float3(1, 1, 1) - float3(_tmp_dvx_90, _tmp_dvx_90, _tmp_dvx_90)));
          col_2 = tmpvar_19;
          float4 tmpvar_20;
          tmpvar_20.w = 1;
          tmpvar_20.xyz = float3(tmpvar_19);
          tmpvar_1 = tmpvar_20;
          out_f.color = tmpvar_1;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
