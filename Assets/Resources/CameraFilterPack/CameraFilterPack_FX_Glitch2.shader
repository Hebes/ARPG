Shader "CameraFilterPack/FX_Glitch2"
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
          float3 p_1;
          float fty_2;
          float fft_3;
          float _TimeX_1_4;
          float2 uv_5;
          uv_5 = in_f.xlv_TEXCOORD0;
          uv_5 = (uv_5 - 0.5);
          float4 tmpvar_6;
          tmpvar_6 = tex2D(_MainTex, float2(0.5, 0.5));
          _TimeX_1_4 = tmpvar_6.x;
          if((_TimeX_1_4>=0.2))
          {
              if((_TimeX_1_4<0.4))
              {
                  uv_5.x = (uv_5.x + 100.55);
                  uv_5 = (uv_5 * 5E-05);
              }
              else
              {
                  if((_TimeX_1_4<0.6))
                  {
                      uv_5 = (uv_5 * 0.00045);
                  }
                  else
                  {
                      if((_TimeX_1_4<0.8))
                      {
                          uv_5 = (uv_5 * 500000);
                      }
                      else
                      {
                          if((_TimeX_1_4<1))
                          {
                              uv_5 = (uv_5 * 4.5E-05);
                          }
                      }
                  }
              }
          }
          float2 tmpvar_7;
          tmpvar_7.y = 0.25;
          tmpvar_7.x = uv_5.x;
          float tmpvar_8;
          tmpvar_8 = tex2D(_MainTex, tmpvar_7).x;
          fft_3 = tmpvar_8;
          float2 tmpvar_9;
          tmpvar_9.y = 0.35;
          tmpvar_9.x = uv_5.x;
          float tmpvar_10;
          tmpvar_10 = tex2D(_MainTex, tmpvar_9).x;
          fty_2 = tmpvar_10;
          uv_5 = (uv_5 * (200 * sin((log(fft_3) * 10))));
          float tmpvar_11;
          tmpvar_11 = sin(fty_2);
          if((tmpvar_11<0.5))
          {
              uv_5.x = (uv_5.x + (sin(fty_2) * sin((cos(_TimeX_1_4) + (uv_5.y * 40005)))));
          }
          uv_5 = (uv_5 * sin((_TimeX_1_4 * 179)));
          p_1.xy = float2(uv_5);
          p_1.z = 0;
          float3 pf_12;
          float3 tmpvar_13;
          tmpvar_13 = floor(p_1);
          float3 tmpvar_14;
          tmpvar_14 = frac(p_1);
          float tmpvar_15;
          tmpvar_15 = ((tmpvar_13.x + (59 * tmpvar_13.y)) + (256 * tmpvar_13.z));
          pf_12.xy = ((tmpvar_14.xy * tmpvar_14.xy) * (float2(3, 3) - (float2(2, 2) * tmpvar_14.xy)));
          pf_12.z = sin(tmpvar_14.z);
          float tmpvar_16;
          tmpvar_16 = lerp(lerp(lerp(frac((sin(tmpvar_15) * 43812.18)), frac((sin((tmpvar_15 + 1)) * 43812.18)), pf_12.x), lerp(frac((sin((tmpvar_15 + 59)) * 43812.18)), frac((sin((60 + tmpvar_15)) * 43812.18)), pf_12.x), pf_12.y), lerp(lerp(frac((sin((tmpvar_15 + 256)) * 43812.18)), frac((sin((257 + tmpvar_15)) * 43812.18)), pf_12.x), lerp(frac((sin((315 + tmpvar_15)) * 43812.18)), frac((sin((316 + tmpvar_15)) * 43812.18)), pf_12.x), pf_12.y), pf_12.z);
          float3 tmpvar_17;
          tmpvar_17.x = frac((sin(((tmpvar_16 * 6.238) * cos(_TimeX_1_4))) * 43812.18));
          tmpvar_17.y = frac((sin(((tmpvar_16 * 6.2384) + (0.4 * cos((_TimeX_1_4 * 2.25))))) * 43812.18));
          tmpvar_17.z = frac((sin(((tmpvar_16 * 6.2384) + (0.8 * cos((_TimeX_1_4 * 0.8468))))) * 43812.18));
          float4 tmpvar_18;
          tmpvar_18 = tex2D(_MainTex, in_f.xlv_TEXCOORD0);
          float4 tmpvar_19;
          tmpvar_19.w = 1;
          tmpvar_19.xyz = (tmpvar_17 * tmpvar_18.xyz);
          out_f.color = tmpvar_19;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
