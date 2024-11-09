Shader "CameraFilterPack/Drawing_Crosshatch"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _TimeX ("Time", Range(0, 1)) = 1
    _Distortion ("_Distortion", Range(0, 10)) = 1
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
      uniform float _Distortion;
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
          float g_1;
          float gy_2;
          float gx_3;
          float brightestChannel_4;
          float dimmestChannel_5;
          float brightness_6;
          float4 tex_7;
          float3 res_8;
          float2 tmpvar_9;
          tmpvar_9 = (1 / _ScreenResolution.xy);
          res_8 = float3(1, 1, 1);
          float4 tmpvar_10;
          tmpvar_10 = tex2D(_MainTex, in_f.xlv_TEXCOORD0);
          tex_7 = tmpvar_10;
          float tmpvar_11;
          tmpvar_11 = (((0.2126 * tmpvar_10.x) + (0.7152 * tmpvar_10.y)) + (0.0722 * tmpvar_10.z));
          brightness_6 = tmpvar_11;
          float tmpvar_12;
          tmpvar_12 = min(min(tmpvar_10.x, tmpvar_10.y), tmpvar_10.z);
          dimmestChannel_5 = tmpvar_12;
          float tmpvar_13;
          tmpvar_13 = max(max(tmpvar_10.x, tmpvar_10.y), tmpvar_10.z);
          brightestChannel_4 = tmpvar_13;
          float tmpvar_14;
          tmpvar_14 = (brightestChannel_4 - dimmestChannel_5);
          if((tmpvar_14>0.1))
          {
              tex_7 = (tmpvar_10 * (1 / brightestChannel_4));
          }
          else
          {
              tex_7.xyz = float3(1, 1, 1);
          }
          float2 tmpvar_15;
          tmpvar_15 = (in_f.xlv_TEXCOORD0 * _ScreenResolution.xy);
          float tmpvar_16;
          tmpvar_16 = (tmpvar_15.x + tmpvar_15.y);
          float tmpvar_17;
          tmpvar_17 = (tmpvar_15.x - tmpvar_15.y);
          if((brightness_6<0.8))
          {
              float tmpvar_18;
              tmpvar_18 = (tmpvar_16 - (floor((tmpvar_16 * 0.1)) * 10));
              if((tmpvar_18<=_Distortion))
              {
                  res_8 = (tex_7.xyz * 0.8);
              }
          }
          if((brightness_6<0.6))
          {
              float tmpvar_19;
              tmpvar_19 = (tmpvar_17 - (floor((tmpvar_17 * 0.1)) * 10));
              if((tmpvar_19<=_Distortion))
              {
                  res_8 = (tex_7.xyz * 0.6);
              }
          }
          if((brightness_6<0.3))
          {
              float tmpvar_20;
              float x_21;
              x_21 = (tmpvar_16 - 5);
              tmpvar_20 = (x_21 - (floor((x_21 * 0.1)) * 10));
              if((tmpvar_20<=_Distortion))
              {
                  res_8 = (tex_7.xyz * 0.3);
              }
          }
          if((brightness_6<0.15))
          {
              float tmpvar_22;
              float x_23;
              x_23 = (tmpvar_17 - 5);
              tmpvar_22 = (x_23 - (floor((x_23 * 0.1)) * 10));
              if((tmpvar_22<=_Distortion))
              {
                  res_8 = float3(0, 0, 0);
              }
          }
          float tmpvar_24;
          float4 tmpvar_25;
          float2 P_26;
          P_26 = ((tmpvar_15 + float2(-1, (-1))) * tmpvar_9);
          tmpvar_25 = tex2D(_MainTex, P_26);
          tmpvar_24 = (((0.2126 * tmpvar_25.x) + (0.7152 * tmpvar_25.y)) + (0.0722 * tmpvar_25.z));
          gx_3 = (-tmpvar_24);
          gy_2 = (-tmpvar_24);
          float tmpvar_27;
          float4 tmpvar_28;
          float2 P_29;
          P_29 = ((tmpvar_15 + float2(-1, 0)) * tmpvar_9);
          tmpvar_28 = tex2D(_MainTex, P_29);
          tmpvar_27 = (((0.2126 * tmpvar_28.x) + (0.7152 * tmpvar_28.y)) + (0.0722 * tmpvar_28.z));
          gx_3 = (gx_3 + (-2 * tmpvar_27));
          gx_3 = (gx_3 - tmpvar_27);
          float tmpvar_30;
          float4 tmpvar_31;
          float2 P_32;
          P_32 = ((tmpvar_15 + float2(1, (-1))) * tmpvar_9);
          tmpvar_31 = tex2D(_MainTex, P_32);
          tmpvar_30 = (((0.2126 * tmpvar_31.x) + (0.7152 * tmpvar_31.y)) + (0.0722 * tmpvar_31.z));
          gx_3 = (gx_3 + tmpvar_30);
          gy_2 = (gy_2 - tmpvar_30);
          float tmpvar_33;
          float4 tmpvar_34;
          float2 P_35;
          P_35 = ((tmpvar_15 + float2(1, 0)) * tmpvar_9);
          tmpvar_34 = tex2D(_MainTex, P_35);
          tmpvar_33 = (((0.2126 * tmpvar_34.x) + (0.7152 * tmpvar_34.y)) + (0.0722 * tmpvar_34.z));
          gx_3 = (gx_3 + (2 * tmpvar_33));
          float tmpvar_36;
          float4 tmpvar_37;
          float2 P_38;
          P_38 = ((tmpvar_15 + float2(1, 1)) * tmpvar_9);
          tmpvar_37 = tex2D(_MainTex, P_38);
          tmpvar_36 = (((0.2126 * tmpvar_37.x) + (0.7152 * tmpvar_37.y)) + (0.0722 * tmpvar_37.z));
          gx_3 = (gx_3 + tmpvar_36);
          float tmpvar_39;
          float4 tmpvar_40;
          float2 P_41;
          P_41 = ((tmpvar_15 + float2(1, 1)) * tmpvar_9);
          tmpvar_40 = tex2D(_MainTex, P_41);
          tmpvar_39 = (((0.2126 * tmpvar_40.x) + (0.7152 * tmpvar_40.y)) + (0.0722 * tmpvar_40.z));
          gy_2 = (gy_2 + tmpvar_39);
          float tmpvar_42;
          float4 tmpvar_43;
          float2 P_44;
          P_44 = ((tmpvar_15 + float2(0, (-1))) * tmpvar_9);
          tmpvar_43 = tex2D(_MainTex, P_44);
          tmpvar_42 = (((0.2126 * tmpvar_43.x) + (0.7152 * tmpvar_43.y)) + (0.0722 * tmpvar_43.z));
          gy_2 = (gy_2 + (-2 * tmpvar_42));
          float tmpvar_45;
          float4 tmpvar_46;
          float2 P_47;
          P_47 = ((tmpvar_15 + float2(-1, 1)) * tmpvar_9);
          tmpvar_46 = tex2D(_MainTex, P_47);
          tmpvar_45 = (((0.2126 * tmpvar_46.x) + (0.7152 * tmpvar_46.y)) + (0.0722 * tmpvar_46.z));
          gy_2 = (gy_2 + tmpvar_45);
          float tmpvar_48;
          float4 tmpvar_49;
          float2 P_50;
          P_50 = ((tmpvar_15 + float2(0, 1)) * tmpvar_9);
          tmpvar_49 = tex2D(_MainTex, P_50);
          tmpvar_48 = (((0.2126 * tmpvar_49.x) + (0.7152 * tmpvar_49.y)) + (0.0722 * tmpvar_49.z));
          gy_2 = (gy_2 + (2 * tmpvar_48));
          float tmpvar_51;
          tmpvar_51 = ((gx_3 * gx_3) + (gy_2 * gy_2));
          g_1 = tmpvar_51;
          res_8 = (res_8 * (1 - g_1));
          float4 tmpvar_52;
          tmpvar_52.w = 1;
          tmpvar_52.xyz = float3(res_8);
          out_f.color = tmpvar_52;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
