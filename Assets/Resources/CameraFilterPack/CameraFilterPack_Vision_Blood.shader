Shader "CameraFilterPack/Vision_Blood"
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
          float3 p_2;
          float3 c_3;
          float3 o_4;
          float3 d_5;
          float2 uv_6;
          float k_7;
          k_7 = 0;
          uv_6 = in_f.xlv_TEXCOORD0;
          float3 tmpvar_8;
          tmpvar_8.z = 1;
          tmpvar_8.xy = float2(uv_6);
          float3 tmpvar_9;
          tmpvar_9 = (tmpvar_8 - 0.5);
          o_4 = tmpvar_9;
          c_3 = float3(0, 0, 0);
          float4 tmpvar_10;
          tmpvar_10 = tex2D(_MainTex, float2(0.1, 0.5));
          d_5 = (tmpvar_9 + (tmpvar_10.xyz * 0.01));
          int i_1_1 = 0;
          while((i_1_1<12))
          {
              p_2 = (o_4 + (sin((_TimeX / 2)) / 2));
              int j_11 = 0;
              while((j_11<10))
              {
                  float3 tmpvar_12;
                  tmpvar_12 = (abs((p_2.zyx - 0.4)) - 0.9);
                  p_2 = tmpvar_12;
                  k_7 = (k_7 + exp((-1.25 * abs(dot(o_4, tmpvar_12)))));
                  j_11 = (j_11 + 1);
              }
              k_7 = (k_7 / 3);
              o_4 = (o_4 + ((d_5 * 0.4) * k_7));
              float3 tmpvar_13;
              tmpvar_13.z = (-0.01);
              tmpvar_13.x = ((k_7 * k_7) * (k_7 * 3.173));
              tmpvar_13.y = ((k_7 * k_7) * (-0.041));
              float _tmp_dvx_23 = ((0.97 * c_3) + ((0.1 * k_7) * tmpvar_13));
              c_3 = float3(_tmp_dvx_23, _tmp_dvx_23, _tmp_dvx_23);
              i_1_1 = (i_1_1 + 1);
          }
          float2 x_14;
          x_14 = (float2(0.5, 0.5) - uv_6);
          float tmpvar_15;
          tmpvar_15 = clamp(((sqrt(dot(x_14, x_14)) - _Value) / (((_Value - 0.05) - _Value2) - _Value)), 0, 1);
          float _tmp_dvx_24 = (0.4 * log((1 + c_3)));
          c_3 = float3(_tmp_dvx_24, _tmp_dvx_24, _tmp_dvx_24);
          c_3 = (c_3 - 0.5);
          float2 tmpvar_16;
          tmpvar_16.x = ((c_3.x / 2) * uv_6.x);
          tmpvar_16.y = ((c_3.x / 2) * uv_6.y);
          float4 tmpvar_17;
          tmpvar_17 = tex2D(_MainTex, tmpvar_16);
          c_3 = (c_3 + tmpvar_17.xyz);
          float4 tmpvar_18;
          tmpvar_18 = tex2D(_MainTex, uv_6);
          float4 tmpvar_19;
          tmpvar_19.w = 0;
          tmpvar_19.xyz = float3(c_3);
          float3 tmpvar_20;
          float _tmp_dvx_25 = (1 - (tmpvar_15 * (tmpvar_15 * (3 - (2 * tmpvar_15)))));
          tmpvar_20 = lerp(tmpvar_18, tmpvar_19, float4(_tmp_dvx_25, _tmp_dvx_25, _tmp_dvx_25, _tmp_dvx_25)).xyz;
          c_3 = tmpvar_20;
          float4 tmpvar_21;
          tmpvar_21.w = 1;
          tmpvar_21.xyz = float3(tmpvar_20);
          out_f.color = tmpvar_21;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
