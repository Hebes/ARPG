Shader "CameraFilterPack/Distortion_FishEye"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _TimeX ("Time", Range(0, 1)) = 1
    _Distortion ("_Distortion", Range(0, 1)) = 0.87
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
          float bind_2;
          float2 p_3;
          p_3 = in_f.xlv_TEXCOORD0;
          float2 tmpvar_4;
          tmpvar_4 = (p_3 - float2(0.5, 0.5));
          float tmpvar_5;
          tmpvar_5 = sqrt(dot(tmpvar_4, tmpvar_4));
          float tmpvar_6;
          tmpvar_6 = (4.442894 * (_Distortion - 0.5));
          bind_2 = 0.5;
          if((tmpvar_6>0))
          {
              bind_2 = 0.7071068;
          }
          if((tmpvar_6>0))
          {
              float theta_7;
              theta_7 = (tmpvar_5 * tmpvar_6);
              float theta_8;
              theta_8 = (bind_2 * tmpvar_6);
              uv_1 = (float2(0.5, 0.5) + (((normalize(tmpvar_4) * (sin(theta_7) / cos(theta_7))) * bind_2) / (sin(theta_8) / cos(theta_8))));
          }
          else
          {
              if((tmpvar_6<0))
              {
                  float y_over_x_9;
                  y_over_x_9 = ((tmpvar_5 * (-tmpvar_6)) * 10);
                  float tmpvar_10;
                  tmpvar_10 = (min(abs(y_over_x_9), 1) / max(abs(y_over_x_9), 1));
                  float tmpvar_11;
                  tmpvar_11 = (tmpvar_10 * tmpvar_10);
                  tmpvar_11 = (((((((((((-0.01213232 * tmpvar_11) + 0.05368138) * tmpvar_11) - 0.1173503) * tmpvar_11) + 0.1938925) * tmpvar_11) - 0.3326756) * tmpvar_11) + 0.9999793) * tmpvar_10);
                  tmpvar_11 = (tmpvar_11 + (float((abs(y_over_x_9)>1)) * ((tmpvar_11 * (-2)) + 1.570796)));
                  float y_over_x_12;
                  y_over_x_12 = (((-tmpvar_6) * bind_2) * 10);
                  float tmpvar_13;
                  tmpvar_13 = (min(abs(y_over_x_12), 1) / max(abs(y_over_x_12), 1));
                  float tmpvar_14;
                  tmpvar_14 = (tmpvar_13 * tmpvar_13);
                  tmpvar_14 = (((((((((((-0.01213232 * tmpvar_14) + 0.05368138) * tmpvar_14) - 0.1173503) * tmpvar_14) + 0.1938925) * tmpvar_14) - 0.3326756) * tmpvar_14) + 0.9999793) * tmpvar_13);
                  tmpvar_14 = (tmpvar_14 + (float((abs(y_over_x_12)>1)) * ((tmpvar_14 * (-2)) + 1.570796)));
                  uv_1 = (float2(0.5, 0.5) + (((normalize(tmpvar_4) * (tmpvar_11 * sign(y_over_x_9))) * bind_2) / (tmpvar_14 * sign(y_over_x_12))));
              }
              else
              {
                  uv_1 = p_3;
              }
          }
          float4 tmpvar_15;
          tmpvar_15.w = 1;
          tmpvar_15.xyz = tex2D(_MainTex, uv_1).xyz;
          out_f.color = tmpvar_15;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
