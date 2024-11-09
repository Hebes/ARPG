Shader "CameraFilterPack/Vision_Rainbow"
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
      uniform float _Value3;
      uniform float _Value4;
      uniform float _Value5;
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
          float4 src_1;
          float2 uv_2;
          uv_2 = in_f.xlv_TEXCOORD0;
          float vec_y_3;
          vec_y_3 = (uv_2.y - _Value3);
          float vec_x_4;
          vec_x_4 = (uv_2.x - _Value2);
          float tmpvar_5;
          float tmpvar_6;
          tmpvar_6 = (min(abs((vec_y_3 / vec_x_4)), 1) / max(abs((vec_y_3 / vec_x_4)), 1));
          float tmpvar_7;
          tmpvar_7 = (tmpvar_6 * tmpvar_6);
          tmpvar_7 = (((((((((((-0.01213232 * tmpvar_7) + 0.05368138) * tmpvar_7) - 0.1173503) * tmpvar_7) + 0.1938925) * tmpvar_7) - 0.3326756) * tmpvar_7) + 0.9999793) * tmpvar_6);
          tmpvar_7 = (tmpvar_7 + (float((abs((vec_y_3 / vec_x_4))>1)) * ((tmpvar_7 * (-2)) + 1.570796)));
          tmpvar_5 = (tmpvar_7 * sign((vec_y_3 / vec_x_4)));
          if((abs(vec_x_4)>(1E-08 * abs(vec_y_3))))
          {
              if((vec_x_4<0))
              {
                  if((vec_y_3>=0))
                  {
                      tmpvar_5 = (tmpvar_5 + 3.141593);
                  }
                  else
                  {
                      tmpvar_5 = (tmpvar_5 - 3.141593);
                  }
              }
          }
          else
          {
              tmpvar_5 = (sign(vec_y_3) * 1.570796);
          }
          float3 tmpvar_8;
          float _tmp_dvx_71 = ((((((_TimeX * _Value) + 20) + (_Value4 * tmpvar_5)) * 6) + float3(0, 4, 2)) / float3(6, 6, 6));
          tmpvar_8 = float3(_tmp_dvx_71, _tmp_dvx_71, _tmp_dvx_71);
          float3 tmpvar_9;
          tmpvar_9 = (frac(abs(tmpvar_8)) * float3(6, 6, 6));
          float tmpvar_10;
          if((tmpvar_8.x>=0))
          {
              tmpvar_10 = tmpvar_9.x;
          }
          else
          {
              tmpvar_10 = (-tmpvar_9.x);
          }
          float tmpvar_11;
          if((tmpvar_8.y>=0))
          {
              tmpvar_11 = tmpvar_9.y;
          }
          else
          {
              tmpvar_11 = (-tmpvar_9.y);
          }
          float tmpvar_12;
          if((tmpvar_8.z>=0))
          {
              tmpvar_12 = tmpvar_9.z;
          }
          else
          {
              tmpvar_12 = (-tmpvar_9.z);
          }
          float3 tmpvar_13;
          tmpvar_13.x = tmpvar_10;
          tmpvar_13.y = tmpvar_11;
          tmpvar_13.z = tmpvar_12;
          float3 tmpvar_14;
          tmpvar_14 = clamp((abs((tmpvar_13 - 3)) - 1), float3(0, 0, 0), float3(1, 1, 1));
          float4 tmpvar_15;
          tmpvar_15 = tex2D(_MainTex, uv_2);
          src_1 = tmpvar_15;
          float2 x_16;
          x_16 = (float2(0.5, 0.5) - uv_2);
          float tmpvar_17;
          tmpvar_17 = clamp(((sqrt(dot(x_16, x_16)) - _Value5) / ((_Value5 - 0.35) - _Value5)), 0, 1);
          float4 tmpvar_18;
          tmpvar_18.w = 1;
          float _tmp_dvx_72 = (1 - (tmpvar_17 * (tmpvar_17 * (3 - (2 * tmpvar_17)))));
          tmpvar_18.xyz = lerp(src_1.xyz, ((tmpvar_14 * tmpvar_14) * (3 - (2 * tmpvar_14))), float3(_tmp_dvx_72, _tmp_dvx_72, _tmp_dvx_72));
          out_f.color = tmpvar_18;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
