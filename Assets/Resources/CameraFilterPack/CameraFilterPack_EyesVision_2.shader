Shader "CameraFilterPack/EyesVision_2"
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
          float dist2_2;
          float3 col_3;
          float3 oldfilm_4;
          float2 suv_5;
          float t_6;
          float2 uv_7;
          uv_7 = in_f.xlv_TEXCOORD0;
          float tmpvar_8;
          tmpvar_8 = float(int((_TimeX * _Value3)));
          t_6 = (tmpvar_8 + tmpvar_8);
          float2 tmpvar_9;
          tmpvar_9.x = (frac((sin(dot(float2(t_6, t_6), float2(12.9898, 78.233))) * 43758.55)) * (-6));
          float _tmp_dvx_74 = (t_6 + 23);
          tmpvar_9.y = (frac((sin(dot(float2(_tmp_dvx_74, _tmp_dvx_74), float2(12.9898, 78.233))) * 43758.55)) * 4);
          suv_5 = ((uv_7 + (0.004 * tmpvar_9)) * 0.8);
          suv_5 = (suv_5 + float2(0.075, 0.05));
          float time_10;
          time_10 = (_Value2 * _TimeX);
          float tmpvar_11;
          tmpvar_11 = (1 + (0.5 * sin((time_10 + (uv_7.x * _Value)))));
          float tmpvar_12;
          tmpvar_12 = (1 + (0.5 * cos((time_10 + (uv_7.y * _Value)))));
          float2 tmpvar_13;
          tmpvar_13.x = (tmpvar_11 + sin(tmpvar_12));
          tmpvar_13.y = (tmpvar_12 + cos(tmpvar_11));
          suv_5 = (suv_5 + (0.02 * tmpvar_13));
          float3 tmpvar_14;
          tmpvar_14 = tex2D(_MainTex2, suv_5).xyz;
          oldfilm_4 = tmpvar_14;
          uv_7 = in_f.xlv_TEXCOORD0;
          uv_7 = (uv_7 + (oldfilm_4.xy / 16));
          float3 tmpvar_15;
          tmpvar_15 = tex2D(_MainTex, uv_7).xyz;
          col_3 = tmpvar_15;
          float tmpvar_16;
          float tmpvar_17;
          tmpvar_17 = (_TimeX / 2);
          tmpvar_16 = (t_6 - ((sin(tmpvar_17) * sin(tmpvar_17)) * t_6));
          float2 x_18;
          x_18 = (float2(0.5, 0.5) - uv_7.y);
          float tmpvar_19;
          tmpvar_19 = clamp(((sqrt(dot(x_18, x_18)) - tmpvar_16) / ((tmpvar_16 - 0.6) - tmpvar_16)), 0, 1);
          float2 x_20;
          x_20 = (float2(0.6, 0.6) - (suv_5 * 1.2));
          float tmpvar_21;
          tmpvar_21 = clamp(((sqrt(dot(x_20, x_20)) - 0.4) / 0.2), 0, 1);
          dist2_2 = ((1 - (tmpvar_19 * (tmpvar_19 * (3 - (2 * tmpvar_19))))) + (tmpvar_21 * (tmpvar_21 * (3 - (2 * tmpvar_21)))));
          float3 tmpvar_22;
          float _tmp_dvx_75 = (dist2_2 * _Value4);
          tmpvar_22 = (oldfilm_4 + ((col_3 * (float3(1, 1, 1) - float3(_tmp_dvx_75, _tmp_dvx_75, _tmp_dvx_75))) - dist2_2));
          col_3 = tmpvar_22;
          float4 tmpvar_23;
          tmpvar_23.w = 1;
          tmpvar_23.xyz = float3(tmpvar_22);
          tmpvar_1 = tmpvar_23;
          out_f.color = tmpvar_1;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
