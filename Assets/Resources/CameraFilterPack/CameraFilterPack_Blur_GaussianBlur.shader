Shader "CameraFilterPack/Blur_GaussianBlur"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
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
          float2 tmpvar_1;
          tmpvar_1 = in_f.xlv_TEXCOORD0;
          float Z_3;
          float3 final_colour_4;
          float kernel_5[6];
          final_colour_4 = float3(0, 0, 0);
          kernel_5[0] = 0;
          kernel_5[1] = 0;
          kernel_5[2] = 0;
          kernel_5[3] = 0;
          kernel_5[4] = 0;
          kernel_5[5] = 0;
          kernel_5[2] = 0.4;
          kernel_5[2] = 0.4;
          kernel_5[(2 - 1)] = 0.4;
          kernel_5[(2 + 1)] = 0.4;
          kernel_5[(2 - 2)] = 0.4;
          kernel_5[(2 + 2)] = 0.4;
          Z_3 = kernel_5[0];
          Z_3 = (Z_3 + kernel_5[1]);
          Z_3 = (Z_3 + kernel_5[2]);
          Z_3 = (Z_3 + kernel_5[3]);
          Z_3 = (Z_3 + kernel_5[4]);
          Z_3 = (Z_3 + kernel_5[5]);
          int u_2 = (-2);
          while((u_2<=2))
          {
              float2 tmpvar_6;
              tmpvar_6.x = (float(u_2) * _Distortion);
              tmpvar_6.y = (-2 * _Distortion);
              float4 tmpvar_7;
              float2 P_8;
              P_8 = (((tmpvar_1 * _ScreenResolution.xy) + tmpvar_6) / _ScreenResolution.xy);
              tmpvar_7 = tex2D(_MainTex, P_8);
              final_colour_4 = (final_colour_4 + ((kernel_5[0] * kernel_5[(2 + u_2)]) * tmpvar_7.xyz));
              float2 tmpvar_9;
              tmpvar_9.x = (float(u_2) * _Distortion);
              tmpvar_9.y = (-_Distortion);
              float4 tmpvar_10;
              float2 P_11;
              P_11 = (((tmpvar_1 * _ScreenResolution.xy) + tmpvar_9) / _ScreenResolution.xy);
              tmpvar_10 = tex2D(_MainTex, P_11);
              final_colour_4 = (final_colour_4 + ((kernel_5[1] * kernel_5[(2 + u_2)]) * tmpvar_10.xyz));
              float2 tmpvar_12;
              tmpvar_12.x = (float(u_2) * _Distortion);
              tmpvar_12.y = 0;
              float4 tmpvar_13;
              float2 P_14;
              P_14 = (((tmpvar_1 * _ScreenResolution.xy) + tmpvar_12) / _ScreenResolution.xy);
              tmpvar_13 = tex2D(_MainTex, P_14);
              final_colour_4 = (final_colour_4 + ((kernel_5[2] * kernel_5[(2 + u_2)]) * tmpvar_13.xyz));
              float2 tmpvar_15;
              tmpvar_15.x = (float(u_2) * _Distortion);
              tmpvar_15.y = _Distortion;
              float4 tmpvar_16;
              float2 P_17;
              P_17 = (((tmpvar_1 * _ScreenResolution.xy) + tmpvar_15) / _ScreenResolution.xy);
              tmpvar_16 = tex2D(_MainTex, P_17);
              final_colour_4 = (final_colour_4 + ((kernel_5[3] * kernel_5[(2 + u_2)]) * tmpvar_16.xyz));
              float2 tmpvar_18;
              tmpvar_18.x = (float(u_2) * _Distortion);
              tmpvar_18.y = (2 * _Distortion);
              float4 tmpvar_19;
              float2 P_20;
              P_20 = (((tmpvar_1 * _ScreenResolution.xy) + tmpvar_18) / _ScreenResolution.xy);
              tmpvar_19 = tex2D(_MainTex, P_20);
              final_colour_4 = (final_colour_4 + ((kernel_5[4] * kernel_5[(2 + u_2)]) * tmpvar_19.xyz));
              u_2 = (u_2 + 1);
          }
          float4 tmpvar_21;
          tmpvar_21.w = 1;
          tmpvar_21.xyz = float3((final_colour_4 / (Z_3 * Z_3)));
          out_f.color = tmpvar_21;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
