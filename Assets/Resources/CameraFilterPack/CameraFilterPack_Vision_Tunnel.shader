Shader "CameraFilterPack/Vision_Tunnel"
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
      uniform float _Value2;
      uniform float _Value3;
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
          float3 col_1;
          float2 uv_2;
          float2 p_3;
          float2 tmpvar_4;
          tmpvar_4 = (-1 + (2 * in_f.xlv_TEXCOORD0));
          p_3 = tmpvar_4;
          float tmpvar_5;
          tmpvar_5 = sqrt(dot(p_3, p_3));
          float tmpvar_6;
          float tmpvar_7;
          tmpvar_7 = (min(abs((p_3.y / p_3.x)), 1) / max(abs((p_3.y / p_3.x)), 1));
          float tmpvar_8;
          tmpvar_8 = (tmpvar_7 * tmpvar_7);
          tmpvar_8 = (((((((((((-0.01213232 * tmpvar_8) + 0.05368138) * tmpvar_8) - 0.1173503) * tmpvar_8) + 0.1938925) * tmpvar_8) - 0.3326756) * tmpvar_8) + 0.9999793) * tmpvar_7);
          tmpvar_8 = (tmpvar_8 + (float((abs((p_3.y / p_3.x))>1)) * ((tmpvar_8 * (-2)) + 1.570796)));
          tmpvar_6 = (tmpvar_8 * sign((p_3.y / p_3.x)));
          if((abs(p_3.x)>(1E-08 * abs(p_3.y))))
          {
              if((p_3.x<0))
              {
                  if((p_3.y>=0))
                  {
                      tmpvar_6 = (tmpvar_6 + 3.141593);
                  }
                  else
                  {
                      tmpvar_6 = (tmpvar_6 - 3.141593);
                  }
              }
          }
          else
          {
              tmpvar_6 = (sign(p_3.y) * 1.570796);
          }
          uv_2.x = (0.8 - (0.1 / tmpvar_5));
          float x_9;
          x_9 = (tmpvar_6 / 3.1416);
          uv_2.y = (x_9 - (_Value3 * floor((x_9 / _Value3))));
          float3 tmpvar_10;
          tmpvar_10 = tex2D(_MainTex, uv_2).xyz;
          col_1 = tmpvar_10;
          float tmpvar_11;
          tmpvar_11 = clamp((tmpvar_5 / 0.5), 0, 1);
          col_1 = (col_1 * (tmpvar_11 * (tmpvar_11 * (3 - (2 * tmpvar_11)))));
          float tmpvar_12;
          float2 x_13;
          x_13 = (float2(0.5, 0.5) - in_f.xlv_TEXCOORD0);
          tmpvar_12 = sqrt(dot(x_13, x_13));
          float tmpvar_14;
          tmpvar_14 = clamp(((tmpvar_12 - _Value) / (((_Value - 0.05) - _Value2) - _Value)), 0, 1);
          float4 tmpvar_15;
          tmpvar_15 = tex2D(_MainTex, in_f.xlv_TEXCOORD0);
          float4 tmpvar_16;
          tmpvar_16.w = 0;
          tmpvar_16.xyz = float3(col_1);
          float4 tmpvar_17;
          tmpvar_17.w = 1;
          float _tmp_dvx_57 = (1 - (tmpvar_14 * (tmpvar_14 * (3 - (2 * tmpvar_14)))));
          tmpvar_17.xyz = lerp(tmpvar_15, tmpvar_16, float4(_tmp_dvx_57, _tmp_dvx_57, _tmp_dvx_57, _tmp_dvx_57)).xyz;
          out_f.color = tmpvar_17;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
