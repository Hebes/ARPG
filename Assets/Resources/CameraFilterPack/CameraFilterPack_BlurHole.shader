Shader "CameraFilterPack/BlurHole"
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
      uniform float _Radius;
      uniform float _SpotSize;
      uniform float _CenterX;
      uniform float _CenterY;
      uniform float _Alpha;
      uniform float _Alpha2;
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
          float3 cm_2;
          float Z_4;
          float3 final_colour_5;
          float kernel_6[6];
          final_colour_5 = float3(0, 0, 0);
          kernel_6[0] = 0;
          kernel_6[1] = 0;
          kernel_6[2] = 0;
          kernel_6[3] = 0;
          kernel_6[4] = 0;
          kernel_6[5] = 0;
          kernel_6[2] = 0.4;
          kernel_6[2] = 0.4;
          kernel_6[(2 - 1)] = 0.4;
          kernel_6[(2 + 1)] = 0.4;
          kernel_6[(2 - 2)] = 0.4;
          kernel_6[(2 + 2)] = 0.4;
          Z_4 = kernel_6[0];
          Z_4 = (Z_4 + kernel_6[1]);
          Z_4 = (Z_4 + kernel_6[2]);
          Z_4 = (Z_4 + kernel_6[3]);
          Z_4 = (Z_4 + kernel_6[4]);
          Z_4 = (Z_4 + kernel_6[5]);
          int u_3 = (-2);
          while((u_3<=2))
          {
              float2 tmpvar_7;
              tmpvar_7.x = (float(u_3) * _Distortion);
              tmpvar_7.y = (-2 * _Distortion);
              float4 tmpvar_8;
              float2 P_9;
              P_9 = (((tmpvar_1 * _ScreenResolution.xy) + tmpvar_7) / _ScreenResolution.xy);
              tmpvar_8 = tex2D(_MainTex, P_9);
              final_colour_5 = (final_colour_5 + ((kernel_6[0] * kernel_6[(2 + u_3)]) * tmpvar_8.xyz));
              float2 tmpvar_10;
              tmpvar_10.x = (float(u_3) * _Distortion);
              tmpvar_10.y = (-_Distortion);
              float4 tmpvar_11;
              float2 P_12;
              P_12 = (((tmpvar_1 * _ScreenResolution.xy) + tmpvar_10) / _ScreenResolution.xy);
              tmpvar_11 = tex2D(_MainTex, P_12);
              final_colour_5 = (final_colour_5 + ((kernel_6[1] * kernel_6[(2 + u_3)]) * tmpvar_11.xyz));
              float2 tmpvar_13;
              tmpvar_13.x = (float(u_3) * _Distortion);
              tmpvar_13.y = 0;
              float4 tmpvar_14;
              float2 P_15;
              P_15 = (((tmpvar_1 * _ScreenResolution.xy) + tmpvar_13) / _ScreenResolution.xy);
              tmpvar_14 = tex2D(_MainTex, P_15);
              final_colour_5 = (final_colour_5 + ((kernel_6[2] * kernel_6[(2 + u_3)]) * tmpvar_14.xyz));
              float2 tmpvar_16;
              tmpvar_16.x = (float(u_3) * _Distortion);
              tmpvar_16.y = _Distortion;
              float4 tmpvar_17;
              float2 P_18;
              P_18 = (((tmpvar_1 * _ScreenResolution.xy) + tmpvar_16) / _ScreenResolution.xy);
              tmpvar_17 = tex2D(_MainTex, P_18);
              final_colour_5 = (final_colour_5 + ((kernel_6[3] * kernel_6[(2 + u_3)]) * tmpvar_17.xyz));
              float2 tmpvar_19;
              tmpvar_19.x = (float(u_3) * _Distortion);
              tmpvar_19.y = (2 * _Distortion);
              float4 tmpvar_20;
              float2 P_21;
              P_21 = (((tmpvar_1 * _ScreenResolution.xy) + tmpvar_19) / _ScreenResolution.xy);
              tmpvar_20 = tex2D(_MainTex, P_21);
              final_colour_5 = (final_colour_5 + ((kernel_6[4] * kernel_6[(2 + u_3)]) * tmpvar_20.xyz));
              u_3 = (u_3 + 1);
          }
          float2 tmpvar_22;
          tmpvar_22.x = _CenterX;
          tmpvar_22.y = _CenterY;
          float2 x_23;
          x_23 = (tmpvar_22 - in_f.xlv_TEXCOORD0);
          float tmpvar_24;
          tmpvar_24 = clamp(((sqrt(dot(x_23, x_23)) - _Radius) / ((_Radius + (0.15 * _SpotSize)) - _Radius)), 0, 1);
          float3 tmpvar_25;
          tmpvar_25 = (final_colour_5 / (Z_4 * Z_4));
          float3 tmpvar_26;
          tmpvar_26 = tex2D(_MainTex, in_f.xlv_TEXCOORD0).xyz;
          cm_2 = tmpvar_26;
          float4 tmpvar_27;
          tmpvar_27.w = 1;
          float _tmp_dvx_104 = ((1 - (1 - (tmpvar_24 * (tmpvar_24 * (3 - (2 * tmpvar_24)))))) * _Alpha);
          tmpvar_27.xyz = float3(lerp(lerp(cm_2, tmpvar_25, float3(_Alpha2, _Alpha2, _Alpha2)), tmpvar_25, float3(_tmp_dvx_104, _tmp_dvx_104, _tmp_dvx_104)));
          out_f.color = tmpvar_27;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
