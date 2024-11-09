Shader "CameraFilterPack/FX_8bits_gb"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _TimeX ("Time", Range(0, 1)) = 1
    _ScreenX ("Time", Range(0, 2000)) = 1
    _ScreenY ("Time", Range(0, 2000)) = 1
    _Distortion ("_Distortion", Range(1, 10)) = 1
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
      uniform float _Distortion;
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
          float4 FragColor_1;
          float best1_2;
          float best0_3;
          float3 dst1_4;
          float3 dst0_5;
          float3 src_6;
          float2 q_7;
          q_7 = in_f.xlv_TEXCOORD0;
          float2 tmpvar_8;
          tmpvar_8 = ((q_7 / float2(0.00625, 0.00694)) * float2(0.00625, 0.00694));
          float3 tmpvar_9;
          tmpvar_9 = tex2D(_MainTex, tmpvar_8).xyz;
          src_6 = tmpvar_9;
          dst0_5 = float3(0, 0, 0);
          dst1_4 = float3(0, 0, 0);
          best0_3 = 1000;
          best1_2 = 1000;
          src_6 = (src_6 + _Distortion);
          float3 tmpvar_10;
          tmpvar_10 = ((src_6 * (src_6 * src_6)) - float3(2.7E-05, 0.004096, 0.035937));
          float tmpvar_11;
          tmpvar_11 = dot(tmpvar_10, tmpvar_10);
          if((tmpvar_11<1000))
          {
              best1_2 = 1000;
              dst1_4 = float3(0, 0, 0);
              best0_3 = tmpvar_11;
              dst0_5 = float3(0.03, 0.16, 0.33);
          }
          float3 tmpvar_12;
          tmpvar_12 = ((src_6 * (src_6 * src_6)) - float3(0.002197, 0.079507, 0.050653));
          float tmpvar_13;
          tmpvar_13 = dot(tmpvar_12, tmpvar_12);
          if((tmpvar_13<best0_3))
          {
              best1_2 = best0_3;
              dst1_4 = dst0_5;
              best0_3 = tmpvar_13;
              dst0_5 = float3(0.13, 0.43, 0.37);
          }
          float3 tmpvar_14;
          tmpvar_14 = ((src_6 * (src_6 * src_6)) - float3(0.103823, 0.328509, 0.07408799));
          float tmpvar_15;
          tmpvar_15 = dot(tmpvar_14, tmpvar_14);
          if((tmpvar_15<best0_3))
          {
              best1_2 = best0_3;
              dst1_4 = dst0_5;
              best0_3 = tmpvar_15;
              dst0_5 = float3(0.47, 0.69, 0.42);
          }
          float3 tmpvar_16;
          tmpvar_16 = ((src_6 * (src_6 * src_6)) - float3(0.314432, 0.493039, 0.019683));
          float tmpvar_17;
          tmpvar_17 = dot(tmpvar_16, tmpvar_16);
          if((tmpvar_17<best0_3))
          {
              best1_2 = best0_3;
              dst1_4 = dst0_5;
              best0_3 = tmpvar_17;
              dst0_5 = float3(0.68, 0.79, 0.27);
          }
          float tmpvar_18;
          float x_19;
          x_19 = (q_7.x + q_7.y);
          tmpvar_18 = (x_19 - (floor((x_19 * 0.5)) * 2));
          float2 p_20;
          float _tmp_dvx_155 = floor(_TimeX);
          p_20 = ((q_7 * 0.5) + frac(sin(float2(_tmp_dvx_155, _tmp_dvx_155))));
          float tmpvar_21;
          tmpvar_21 = frac(((10000 * sin(((17 * p_20.x) + (p_20.y * 0.1)))) * (0.1 + abs(sin(((p_20.y * 13) + p_20.x))))));
          float3 tmpvar_22;
          if((tmpvar_18>((tmpvar_21 * 0.75) + (best1_2 / (best0_3 + best1_2)))))
          {
              tmpvar_22 = dst1_4;
          }
          else
          {
              tmpvar_22 = dst0_5;
          }
          float4 tmpvar_23;
          tmpvar_23.w = 1;
          tmpvar_23.xyz = float3(tmpvar_22);
          FragColor_1 = tmpvar_23;
          if((tmpvar_22.x==0))
          {
              FragColor_1.xyz = float3(0.03, 0.16, 0.33);
          }
          out_f.color = FragColor_1;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
