Shader "CameraFilterPack/FX_Hexagon_Black"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _TimeX ("Time", Range(0, 1)) = 1
    _Distortion ("_Distortion", Range(0, 1)) = 0.3
    _ScreenResolution ("_ScreenResolution", Vector) = (0,0,0,0)
    _Value ("_Value", Range(0.2, 10)) = 1
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
          float4 texel_1;
          float tmpvar_2;
          tmpvar_2 = ((_Value * _ScreenResolution.x) / 160);
          float2 tmpvar_3;
          float2 st_4;
          st_4 = (in_f.xlv_TEXCOORD0 * _ScreenResolution.xy);
          float2 coord_5;
          float tmpvar_6;
          tmpvar_6 = (0.5 * tmpvar_2);
          float tmpvar_7;
          tmpvar_7 = (0.8660254 * tmpvar_2);
          float tmpvar_8;
          tmpvar_8 = (tmpvar_6 / tmpvar_7);
          float2 tmpvar_9;
          tmpvar_9.x = (2 * tmpvar_7);
          tmpvar_9.y = (tmpvar_6 + tmpvar_2);
          float2 tmpvar_10;
          tmpvar_10 = (st_4 / tmpvar_9);
          float2 tmpvar_11;
          tmpvar_11.x = (2 * tmpvar_7);
          tmpvar_11.y = (tmpvar_6 + tmpvar_2);
          float2 tmpvar_12;
          tmpvar_12 = (st_4 / tmpvar_11);
          float2 tmpvar_13;
          tmpvar_13 = (frac(abs(tmpvar_12)) * tmpvar_11);
          float tmpvar_14;
          if((tmpvar_12.x>=0))
          {
              tmpvar_14 = tmpvar_13.x;
          }
          else
          {
              tmpvar_14 = (-tmpvar_13.x);
          }
          float tmpvar_15;
          if((tmpvar_12.y>=0))
          {
              tmpvar_15 = tmpvar_13.y;
          }
          else
          {
              tmpvar_15 = (-tmpvar_13.y);
          }
          float tmpvar_16;
          tmpvar_16 = (floor(tmpvar_10.y) / 2);
          float tmpvar_17;
          tmpvar_17 = (frac(abs(tmpvar_16)) * 2);
          float tmpvar_18;
          if((tmpvar_16>=0))
          {
              tmpvar_18 = tmpvar_17;
          }
          else
          {
              tmpvar_18 = (-tmpvar_17);
          }
          float2 tmpvar_19;
          tmpvar_19 = floor(tmpvar_10);
          coord_5 = tmpvar_19;
          if((tmpvar_18>0))
          {
              if((tmpvar_15<(tmpvar_6 - (tmpvar_14 * tmpvar_8))))
              {
                  coord_5 = (tmpvar_19 - 1);
              }
              else
              {
                  if((tmpvar_15<((-tmpvar_6) + (tmpvar_14 * tmpvar_8))))
                  {
                      coord_5.y = (coord_5.y - 1);
                  }
              }
          }
          else
          {
              if((tmpvar_14>tmpvar_7))
              {
                  if((tmpvar_15<((2 * tmpvar_6) - (tmpvar_14 * tmpvar_8))))
                  {
                      coord_5.y = (coord_5.y - 1);
                  }
              }
              else
              {
                  if((tmpvar_15<(tmpvar_14 * tmpvar_8)))
                  {
                      coord_5.y = (coord_5.y - 1);
                  }
                  else
                  {
                      coord_5.x = (coord_5.x - 1);
                  }
              }
          }
          float tmpvar_20;
          tmpvar_20 = (coord_5.y / 2);
          float tmpvar_21;
          tmpvar_21 = (frac(abs(tmpvar_20)) * 2);
          float tmpvar_22;
          if((tmpvar_20>=0))
          {
              tmpvar_22 = tmpvar_21;
          }
          else
          {
              tmpvar_22 = (-tmpvar_21);
          }
          float2 tmpvar_23;
          tmpvar_23.x = (((coord_5.x * 2) * tmpvar_7) - (tmpvar_22 * tmpvar_7));
          tmpvar_23.y = (coord_5.y * (tmpvar_6 + tmpvar_2));
          float2 tmpvar_24;
          tmpvar_24.x = (tmpvar_7 * 2);
          tmpvar_24.y = tmpvar_2;
          tmpvar_3 = (tmpvar_23 + tmpvar_24);
          float4 tmpvar_25;
          float2 P_26;
          P_26 = (tmpvar_3 / _ScreenResolution.xy);
          tmpvar_25 = tex2D(_MainTex, P_26);
          texel_1 = tmpvar_25;
          float2 tmpvar_27;
          tmpvar_27 = abs((tmpvar_3 - (in_f.xlv_TEXCOORD0 * _ScreenResolution.xy)));
          float edge0_28;
          edge0_28 = (tmpvar_2 - 1);
          float tmpvar_29;
          tmpvar_29 = clamp((((max(((0.5 * tmpvar_27.x) + (0.8660254 * tmpvar_27.y)), tmpvar_27.x) / 0.8660254) - edge0_28) / (tmpvar_2 - edge0_28)), 0, 1);
          float4 tmpvar_30;
          tmpvar_30.w = 1;
          tmpvar_30.xyz = (texel_1.xyz * (1 - (tmpvar_29 * (tmpvar_29 * (3 - (2 * tmpvar_29))))));
          out_f.color = tmpvar_30;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
