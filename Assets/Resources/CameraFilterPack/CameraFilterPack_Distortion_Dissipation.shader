Shader "CameraFilterPack/Distortion_Dissipation"
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
          float2 uv_1;
          uv_1 = in_f.xlv_TEXCOORD0;
          float tmpvar_2;
          tmpvar_2 = max(0, ((((frac((_Value / 3)) * 6) - uv_1.x) - 1) + uv_1.y));
          float2 p_3;
          p_3 = uv_1;
          float t_4;
          t_4 = (tmpvar_2 * tmpvar_2);
          float3 col_6;
          col_6 = float3(0, 0, 0);
          int i_5 = 1;
          while((i_5<10))
          {
              float2 p_7;
              p_7 = (p_3 + float2(0.5, 0.5));
              float2 x_8;
              float4 tmpvar_9;
              float2 P_10;
              P_10 = (p_7 * 0.001);
              tmpvar_9 = tex2D(_MainTex, P_10);
              x_8 = (tmpvar_9.xy - 0.5);
              float4 tmpvar_11;
              float2 P_12;
              float _tmp_dvx_157 = (0.002 * p_7);
              P_12 = float2(_tmp_dvx_157, _tmp_dvx_157);
              tmpvar_11 = tex2D(_MainTex, P_12);
              x_8 = (x_8 + ((tmpvar_11.xy - 0.5) / 2));
              float4 tmpvar_13;
              float2 P_14;
              float _tmp_dvx_158 = (0.004 * p_7);
              P_14 = float2(_tmp_dvx_158, _tmp_dvx_158);
              tmpvar_13 = tex2D(_MainTex, P_14);
              x_8 = (x_8 + ((tmpvar_13.xy - 0.5) / 4));
              float4 tmpvar_15;
              float2 P_16;
              float _tmp_dvx_159 = (0.008 * p_7);
              P_16 = float2(_tmp_dvx_159, _tmp_dvx_159);
              tmpvar_15 = tex2D(_MainTex, P_16);
              x_8 = (x_8 + ((tmpvar_15.xy - 0.5) / 8));
              float4 tmpvar_17;
              float2 P_18;
              float _tmp_dvx_160 = (0.016 * p_7);
              P_18 = float2(_tmp_dvx_160, _tmp_dvx_160);
              tmpvar_17 = tex2D(_MainTex, P_18);
              x_8 = (x_8 + ((tmpvar_17.xy - 0.5) / 16));
              float4 tmpvar_19;
              float2 P_20;
              float _tmp_dvx_161 = (0.032 * p_7);
              P_20 = float2(_tmp_dvx_161, _tmp_dvx_161);
              tmpvar_19 = tex2D(_MainTex, P_20);
              x_8 = (x_8 + ((tmpvar_19.xy - 0.5) / 32));
              p_3 = (p_3 + (((x_8 * t_4) * 0.01) / float(i_5)));
              p_3.y = (p_3.y - (t_4 * 0.003));
              float3 col_21;
              float3 tmpvar_22;
              tmpvar_22 = tex2D(_MainTex, p_3).xyz;
              col_21 = tmpvar_22;
              col_6 = (col_6 + col_21);
              i_5 = (i_5 + 1);
          }
          float4 tmpvar_23;
          tmpvar_23.w = 1;
          tmpvar_23.xyz = float3((col_6 / 10));
          out_f.color = tmpvar_23;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
