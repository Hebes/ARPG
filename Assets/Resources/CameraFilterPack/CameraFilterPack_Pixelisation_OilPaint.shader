Shader "CameraFilterPack/Pixelisation_OilPaint"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _TimeX ("Time", Range(0, 1)) = 1
    _Distortion ("_Distortion", Range(0, 1)) = 0.3
    _ScreenResolution ("_ScreenResolution", Vector) = (0,0,0,0)
    _Value ("_Value", Range(0, 5)) = 1
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
      uniform float4 _ScreenResolution;
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
          float sigma2_1;
          float min_sigma2_2;
          float4 ccolor_3;
          float3 c_4;
          float3 s2_5;
          float3 s0_6;
          float3 m2_7;
          float3 m0_8;
          float2 uv_9;
          float2 tmpvar_10;
          tmpvar_10 = (float2(_Value, _Value) / _ScreenResolution.xy);
          uv_9 = in_f.xlv_TEXCOORD0;
          float4 tmpvar_11;
          float2 P_12;
          P_12 = (uv_9 + (float2(-4, (-4)) * tmpvar_10));
          tmpvar_11 = tex2D(_MainTex, P_12);
          c_4 = tmpvar_11.xyz;
          m0_8 = c_4;
          s0_6 = (c_4 * c_4);
          float4 tmpvar_13;
          tmpvar_13 = tex2D(_MainTex, uv_9);
          c_4 = tmpvar_13.xyz;
          m2_7 = c_4;
          s2_5 = (c_4 * c_4);
          float4 tmpvar_14;
          float2 P_15;
          P_15 = (uv_9 + (float2(-3, (-3)) * tmpvar_10));
          tmpvar_14 = tex2D(_MainTex, P_15);
          c_4 = tmpvar_14.xyz;
          m0_8 = (m0_8 + c_4);
          s0_6 = (s0_6 + (c_4 * c_4));
          float4 tmpvar_16;
          float2 P_17;
          P_17 = (uv_9 + (float2(1, 0) * tmpvar_10));
          tmpvar_16 = tex2D(_MainTex, P_17);
          c_4 = tmpvar_16.xyz;
          m2_7 = (m2_7 + c_4);
          s2_5 = (s2_5 + (c_4 * c_4));
          float4 tmpvar_18;
          float2 P_19;
          P_19 = (uv_9 + (float2(-2, (-2)) * tmpvar_10));
          tmpvar_18 = tex2D(_MainTex, P_19);
          c_4 = tmpvar_18.xyz;
          m0_8 = (m0_8 + c_4);
          s0_6 = (s0_6 + (c_4 * c_4));
          float4 tmpvar_20;
          float2 P_21;
          P_21 = (uv_9 + (float2(2, 0) * tmpvar_10));
          tmpvar_20 = tex2D(_MainTex, P_21);
          c_4 = tmpvar_20.xyz;
          m2_7 = (m2_7 + c_4);
          s2_5 = (s2_5 + (c_4 * c_4));
          float4 tmpvar_22;
          float2 P_23;
          P_23 = (uv_9 + (float2(-4, (-3)) * tmpvar_10));
          tmpvar_22 = tex2D(_MainTex, P_23);
          c_4 = tmpvar_22.xyz;
          m0_8 = (m0_8 + c_4);
          s0_6 = (s0_6 + (c_4 * c_4));
          float4 tmpvar_24;
          float2 P_25;
          P_25 = (uv_9 + (float2(0, 1) * tmpvar_10));
          tmpvar_24 = tex2D(_MainTex, P_25);
          c_4 = tmpvar_24.xyz;
          m2_7 = (m2_7 + c_4);
          s2_5 = (s2_5 + (c_4 * c_4));
          float4 tmpvar_26;
          float2 P_27;
          P_27 = (uv_9 + (float2(-3, (-3)) * tmpvar_10));
          tmpvar_26 = tex2D(_MainTex, P_27);
          c_4 = tmpvar_26.xyz;
          m0_8 = (m0_8 + c_4);
          s0_6 = (s0_6 + (c_4 * c_4));
          float4 tmpvar_28;
          float2 P_29;
          P_29 = (uv_9 + tmpvar_10);
          tmpvar_28 = tex2D(_MainTex, P_29);
          c_4 = tmpvar_28.xyz;
          m2_7 = (m2_7 + c_4);
          s2_5 = (s2_5 + (c_4 * c_4));
          float4 tmpvar_30;
          float2 P_31;
          P_31 = (uv_9 + (float2(-2, (-3)) * tmpvar_10));
          tmpvar_30 = tex2D(_MainTex, P_31);
          c_4 = tmpvar_30.xyz;
          m0_8 = (m0_8 + c_4);
          s0_6 = (s0_6 + (c_4 * c_4));
          float4 tmpvar_32;
          float2 P_33;
          P_33 = (uv_9 + (float2(2, 1) * tmpvar_10));
          tmpvar_32 = tex2D(_MainTex, P_33);
          c_4 = tmpvar_32.xyz;
          m2_7 = (m2_7 + c_4);
          s2_5 = (s2_5 + (c_4 * c_4));
          float4 tmpvar_34;
          float2 P_35;
          P_35 = (uv_9 + (float2(-4, (-2)) * tmpvar_10));
          tmpvar_34 = tex2D(_MainTex, P_35);
          c_4 = tmpvar_34.xyz;
          m0_8 = (m0_8 + c_4);
          s0_6 = (s0_6 + (c_4 * c_4));
          float4 tmpvar_36;
          float2 P_37;
          P_37 = (uv_9 + (float2(0, 2) * tmpvar_10));
          tmpvar_36 = tex2D(_MainTex, P_37);
          c_4 = tmpvar_36.xyz;
          m2_7 = (m2_7 + c_4);
          s2_5 = (s2_5 + (c_4 * c_4));
          float4 tmpvar_38;
          float2 P_39;
          P_39 = (uv_9 + (float2(-3, (-2)) * tmpvar_10));
          tmpvar_38 = tex2D(_MainTex, P_39);
          c_4 = tmpvar_38.xyz;
          m0_8 = (m0_8 + c_4);
          s0_6 = (s0_6 + (c_4 * c_4));
          float4 tmpvar_40;
          float2 P_41;
          P_41 = (uv_9 + (float2(1, 2) * tmpvar_10));
          tmpvar_40 = tex2D(_MainTex, P_41);
          c_4 = tmpvar_40.xyz;
          m2_7 = (m2_7 + c_4);
          s2_5 = (s2_5 + (c_4 * c_4));
          float4 tmpvar_42;
          float2 P_43;
          P_43 = (uv_9 + (float2(-2, (-3)) * tmpvar_10));
          tmpvar_42 = tex2D(_MainTex, P_43);
          c_4 = tmpvar_42.xyz;
          m0_8 = (m0_8 + c_4);
          s0_6 = (s0_6 + (c_4 * c_4));
          float4 tmpvar_44;
          float2 P_45;
          P_45 = (uv_9 + (float2(2, 2) * tmpvar_10));
          tmpvar_44 = tex2D(_MainTex, P_45);
          c_4 = tmpvar_44.xyz;
          m2_7 = (m2_7 + c_4);
          s2_5 = (s2_5 + (c_4 * c_4));
          ccolor_3 = float4(0, 0, 0, 0);
          min_sigma2_2 = 100;
          m0_8 = (m0_8 / 9);
          float3 tmpvar_46;
          tmpvar_46 = abs(((s0_6 / 9) - (m0_8 * m0_8)));
          s0_6 = tmpvar_46;
          float tmpvar_47;
          tmpvar_47 = ((tmpvar_46.x + tmpvar_46.y) + tmpvar_46.z);
          sigma2_1 = tmpvar_47;
          if((tmpvar_47<100))
          {
              min_sigma2_2 = tmpvar_47;
              float4 tmpvar_48;
              tmpvar_48.w = 1;
              tmpvar_48.xyz = float3(m0_8);
              ccolor_3 = tmpvar_48;
          }
          m2_7 = (m2_7 / 9);
          float3 tmpvar_49;
          tmpvar_49 = abs(((s2_5 / 9) - (m2_7 * m2_7)));
          s2_5 = tmpvar_49;
          sigma2_1 = ((tmpvar_49.x + tmpvar_49.y) + tmpvar_49.z);
          if((sigma2_1<min_sigma2_2))
          {
              min_sigma2_2 = sigma2_1;
              float4 tmpvar_50;
              tmpvar_50.w = 1;
              tmpvar_50.xyz = float3(m2_7);
              ccolor_3 = tmpvar_50;
          }
          out_f.color = ccolor_3;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
