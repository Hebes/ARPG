Shader "CameraFilterPack/FX_Drunk"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _TimeX ("Time", Range(0, 1)) = 1
    _Distortion ("_Distortion", Range(0, 1)) = 0.3
    _ScreenResolution ("_ScreenResolution", Vector) = (0,0,0,0)
    _Value ("_Value", Range(0, 20)) = 6
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
          float4 finalColor_1;
          float4 color2_2;
          float4 color_3;
          float2 normalizedCoord3_4;
          float2 normalizedCoord2_5;
          float2 normalizedCoord_6;
          float tmpvar_7;
          tmpvar_7 = (sin((_TimeX * 2)) * _Value);
          float tmpvar_8;
          tmpvar_8 = ((sin((_TimeX * 1.2)) + 1) / 2);
          float tmpvar_9;
          tmpvar_9 = ((sin((_TimeX * 1.8)) + 1) / 2);
          float2 tmpvar_10;
          tmpvar_10.x = 0;
          tmpvar_10.y = tmpvar_7;
          float2 tmpvar_11;
          tmpvar_11 = (in_f.xlv_TEXCOORD0 + (tmpvar_10 / _ScreenResolution.x));
          float2 tmpvar_12;
          tmpvar_12 = frac(abs(tmpvar_11));
          float tmpvar_13;
          if((tmpvar_11.x>=0))
          {
              tmpvar_13 = tmpvar_12.x;
          }
          else
          {
              tmpvar_13 = (-tmpvar_12.x);
          }
          float tmpvar_14;
          if((tmpvar_11.y>=0))
          {
              tmpvar_14 = tmpvar_12.y;
          }
          else
          {
              tmpvar_14 = (-tmpvar_12.y);
          }
          normalizedCoord_6.x = pow(tmpvar_13, lerp(1.25, 0.85, tmpvar_8));
          normalizedCoord_6.y = pow(tmpvar_14, lerp(0.85, 1.25, tmpvar_9));
          float2 tmpvar_15;
          tmpvar_15.y = 0;
          tmpvar_15.x = tmpvar_7;
          float2 tmpvar_16;
          tmpvar_16 = (in_f.xlv_TEXCOORD0 + (tmpvar_15 / _ScreenResolution.x));
          float2 tmpvar_17;
          tmpvar_17 = frac(abs(tmpvar_16));
          float tmpvar_18;
          if((tmpvar_16.x>=0))
          {
              tmpvar_18 = tmpvar_17.x;
          }
          else
          {
              tmpvar_18 = (-tmpvar_17.x);
          }
          float tmpvar_19;
          if((tmpvar_16.y>=0))
          {
              tmpvar_19 = tmpvar_17.y;
          }
          else
          {
              tmpvar_19 = (-tmpvar_17.y);
          }
          normalizedCoord2_5.x = pow(tmpvar_18, lerp(0.95, 1.1, tmpvar_9));
          normalizedCoord2_5.y = pow(tmpvar_19, lerp(1.1, 0.95, tmpvar_8));
          normalizedCoord3_4 = in_f.xlv_TEXCOORD0;
          color_3.yzw = tex2D(_MainTex, normalizedCoord_6).yzw;
          float4 tmpvar_20;
          tmpvar_20 = tex2D(_MainTex, normalizedCoord2_5);
          color2_2.yzw = tmpvar_20.yzw;
          float4 tmpvar_21;
          tmpvar_21 = tex2D(_MainTex, normalizedCoord3_4);
          color_3.x = sqrt(tmpvar_20.x);
          color2_2.x = color_3.x;
          float4 tmpvar_22;
          float _tmp_dvx_18 = lerp(0.4, 0.6, tmpvar_8);
          tmpvar_22 = lerp(lerp(color_3, color2_2, float4(_tmp_dvx_18, _tmp_dvx_18, _tmp_dvx_18, _tmp_dvx_18)), tmpvar_21, float4(0.4, 0.4, 0.4, 0.4));
          finalColor_1 = tmpvar_22;
          float tmpvar_23;
          tmpvar_23 = sqrt(dot(finalColor_1, finalColor_1));
          if((tmpvar_23>1.4))
          {
              float2 tmpvar_24;
              tmpvar_24 = lerp(finalColor_1.xy, normalizedCoord3_4, float2(0.5, 0.5));
              finalColor_1.xy = float2(tmpvar_24);
          }
          else
          {
              float tmpvar_25;
              tmpvar_25 = sqrt(dot(finalColor_1, finalColor_1));
              if((tmpvar_25<0.4))
              {
                  float2 tmpvar_26;
                  tmpvar_26 = lerp(finalColor_1.yz, normalizedCoord3_4, float2(0.5, 0.5));
                  finalColor_1.yz = tmpvar_26;
              }
          }
          out_f.color = finalColor_1;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
