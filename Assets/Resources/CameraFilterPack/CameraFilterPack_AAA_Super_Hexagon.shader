Shader "CameraFilterPack/AAA_Super_Hexagon"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _TimeX ("Time", Range(0, 1)) = 1
    _Distortion ("_Distortion", Range(0, 1)) = 0.3
    _ScreenResolution ("_ScreenResolution", Vector) = (0,0,0,0)
    _Value ("_Value", Range(0.2, 10)) = 1
    _HexaColor ("_HexaColor", Color) = (1,1,1,1)
    _BorderSize ("_BorderSize", Range(-0.5, 0.5)) = 0
    _BorderColor ("_BorderColor", Color) = (1,1,1,1)
    _SpotSize ("_SpotSize", Range(0, 1)) = 0.5
    _AlphaHexa ("_AlphaHexa", Range(0.2, 10)) = 1
    _PositionX ("_PositionX", Range(-0.5, 0.5)) = 0
    _PositionY ("_PositionY", Range(-0.5, 0.5)) = 0
    _Radius ("_Radius", Range(0, 1)) = 0.5
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
      uniform float _BorderSize;
      uniform float4 _BorderColor;
      uniform float4 _HexaColor;
      uniform float _AlphaHexa;
      uniform float _PositionX;
      uniform float _PositionY;
      uniform float _Radius;
      uniform float _SpotSize;
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
          float4 result_1;
          float3 video_2;
          float2 uv2_3;
          float4 texel2_4;
          float4 texel_5;
          float2 uv_6;
          uv_6 = in_f.xlv_TEXCOORD0;
          float tmpvar_7;
          tmpvar_7 = ((_Value * _ScreenResolution.x) / 160);
          float2 tmpvar_8;
          float2 st_9;
          st_9 = (in_f.xlv_TEXCOORD0 * _ScreenResolution.xy);
          float2 coord_10;
          float tmpvar_11;
          tmpvar_11 = (0.5 * tmpvar_7);
          float tmpvar_12;
          tmpvar_12 = (0.8660254 * tmpvar_7);
          float tmpvar_13;
          tmpvar_13 = (tmpvar_11 / tmpvar_12);
          float2 tmpvar_14;
          tmpvar_14.x = (2 * tmpvar_12);
          tmpvar_14.y = (tmpvar_11 + tmpvar_7);
          float2 tmpvar_15;
          tmpvar_15 = (st_9 / tmpvar_14);
          float2 tmpvar_16;
          tmpvar_16.x = (2 * tmpvar_12);
          tmpvar_16.y = (tmpvar_11 + tmpvar_7);
          float2 tmpvar_17;
          tmpvar_17 = (st_9 / tmpvar_16);
          float2 tmpvar_18;
          tmpvar_18 = (frac(abs(tmpvar_17)) * tmpvar_16);
          float tmpvar_19;
          if((tmpvar_17.x>=0))
          {
              tmpvar_19 = tmpvar_18.x;
          }
          else
          {
              tmpvar_19 = (-tmpvar_18.x);
          }
          float tmpvar_20;
          if((tmpvar_17.y>=0))
          {
              tmpvar_20 = tmpvar_18.y;
          }
          else
          {
              tmpvar_20 = (-tmpvar_18.y);
          }
          float tmpvar_21;
          tmpvar_21 = (floor(tmpvar_15.y) / 2);
          float tmpvar_22;
          tmpvar_22 = (frac(abs(tmpvar_21)) * 2);
          float tmpvar_23;
          if((tmpvar_21>=0))
          {
              tmpvar_23 = tmpvar_22;
          }
          else
          {
              tmpvar_23 = (-tmpvar_22);
          }
          float2 tmpvar_24;
          tmpvar_24 = floor(tmpvar_15);
          coord_10 = tmpvar_24;
          if((tmpvar_23>0))
          {
              if((tmpvar_20<(tmpvar_11 - (tmpvar_19 * tmpvar_13))))
              {
                  coord_10 = (tmpvar_24 - 1);
              }
              else
              {
                  if((tmpvar_20<((-tmpvar_11) + (tmpvar_19 * tmpvar_13))))
                  {
                      coord_10.y = (coord_10.y - 1);
                  }
              }
          }
          else
          {
              if((tmpvar_19>tmpvar_12))
              {
                  if((tmpvar_20<((2 * tmpvar_11) - (tmpvar_19 * tmpvar_13))))
                  {
                      coord_10.y = (coord_10.y - 1);
                  }
              }
              else
              {
                  if((tmpvar_20<(tmpvar_19 * tmpvar_13)))
                  {
                      coord_10.y = (coord_10.y - 1);
                  }
                  else
                  {
                      coord_10.x = (coord_10.x - 1);
                  }
              }
          }
          float tmpvar_25;
          tmpvar_25 = (coord_10.y / 2);
          float tmpvar_26;
          tmpvar_26 = (frac(abs(tmpvar_25)) * 2);
          float tmpvar_27;
          if((tmpvar_25>=0))
          {
              tmpvar_27 = tmpvar_26;
          }
          else
          {
              tmpvar_27 = (-tmpvar_26);
          }
          float2 tmpvar_28;
          tmpvar_28.x = (((coord_10.x * 2) * tmpvar_12) - (tmpvar_27 * tmpvar_12));
          tmpvar_28.y = (coord_10.y * (tmpvar_11 + tmpvar_7));
          float2 tmpvar_29;
          tmpvar_29.x = (tmpvar_12 * 2);
          tmpvar_29.y = tmpvar_7;
          tmpvar_8 = (tmpvar_28 + tmpvar_29);
          float4 tmpvar_30;
          float2 P_31;
          P_31 = (tmpvar_8 / _ScreenResolution.xy);
          tmpvar_30 = tex2D(_MainTex, P_31);
          texel_5 = tmpvar_30;
          float4 tmpvar_32;
          tmpvar_32 = tex2D(_MainTex, uv_6);
          texel2_4 = tmpvar_32;
          float2 uv_33;
          uv_33 = (uv_6 - float2(0.5, 0.5));
          uv_33 = ((uv_33 * 1.2) * (0.8333333 + ((2 * uv_33.x) * ((uv_33.x * uv_33.y) * uv_33.y))));
          uv_33 = (uv_33 + float2(0.5, 0.5));
          uv_6 = uv_33;
          float2 tmpvar_34;
          tmpvar_34.x = (0.5 * sin((_TimeX / 3)));
          tmpvar_34.y = (0.5 * sin((_TimeX / 5)));
          uv2_3 = (uv_33.yy + tmpvar_34);
          float3 tmpvar_35;
          tmpvar_35 = tex2D(_MainTex, uv2_3).xyz;
          video_2 = tmpvar_35;
          float tmpvar_36;
          tmpvar_36 = ((1 - ((1.804371 * (uv_33.y - 0.5)) * (uv_33.y - 0.5))) * (1 - ((1.804371 * (uv_33.x - 0.5)) * (uv_33.x - 0.5))));
          float tmpvar_37;
          tmpvar_37 = ((uv_33.y * 10) + _TimeX);
          float tmpvar_38;
          tmpvar_38 = frac(abs(tmpvar_37));
          float tmpvar_39;
          if((tmpvar_37>=0))
          {
              tmpvar_39 = tmpvar_38;
          }
          else
          {
              tmpvar_39 = (-tmpvar_38);
          }
          video_2 = (video_2 + ((12 + tmpvar_39) / 13));
          float2 tmpvar_40;
          tmpvar_40.x = _PositionX;
          tmpvar_40.y = _PositionY;
          float2 x_41;
          x_41 = (tmpvar_40 - uv_33);
          float tmpvar_42;
          tmpvar_42 = clamp(((sqrt(dot(x_41, x_41)) - _Radius) / ((_Radius + (0.15 * _SpotSize)) - _Radius)), 0, 1);
          float tmpvar_43;
          tmpvar_43 = (1 - (tmpvar_42 * (tmpvar_42 * (3 - (2 * tmpvar_42)))));
          float2 tmpvar_44;
          tmpvar_44 = abs((tmpvar_8 - (in_f.xlv_TEXCOORD0 * _ScreenResolution.xy)));
          float tmpvar_45;
          tmpvar_45 = (tmpvar_7 * _BorderSize);
          float edge0_46;
          edge0_46 = (tmpvar_45 - 1);
          float tmpvar_47;
          tmpvar_47 = clamp((((max(((0.5 * tmpvar_44.x) + (0.8660254 * tmpvar_44.y)), tmpvar_44.x) / 0.8660254) - edge0_46) / (tmpvar_45 - edge0_46)), 0, 1);
          float _tmp_dvx_29 = (1 - (tmpvar_47 * (tmpvar_47 * (3 - (2 * tmpvar_47)))));
          result_1.xyz = lerp(_BorderColor.xyz, texel_5.xyz, float3(_tmp_dvx_29, _tmp_dvx_29, _tmp_dvx_29));
          float tmpvar_48;
          tmpvar_48 = (((_TimeX / 4) + uv_33.x) + 0.5);
          float tmpvar_49;
          tmpvar_49 = frac(abs(tmpvar_48));
          float tmpvar_50;
          if((tmpvar_48>=0))
          {
              tmpvar_50 = tmpvar_49;
          }
          else
          {
              tmpvar_50 = (-tmpvar_49);
          }
          float tmpvar_51;
          tmpvar_51 = clamp((tmpvar_50 / 0.5), 0, 1);
          float _tmp_dvx_30 = (1 - (0.5 * (tmpvar_51 * (tmpvar_51 * (3 - (2 * tmpvar_51))))));
          result_1.xyz = (result_1.xyz + float3(_tmp_dvx_30, _tmp_dvx_30, _tmp_dvx_30));
          result_1.xyz = (result_1.xyz * _HexaColor.xyz);
          result_1.xyz = (result_1.xyz / (video_2 / 2));
          result_1 = (result_1 / (tmpvar_36 * 2));
          result_1.xyz = lerp(result_1.xyz, texel2_4.xyz, float3(tmpvar_43, tmpvar_43, tmpvar_43));
          float _tmp_dvx_31 = (1 - _AlphaHexa);
          result_1.xyz = lerp(result_1.xyz, texel2_4.xyz, float3(_tmp_dvx_31, _tmp_dvx_31, _tmp_dvx_31));
          result_1.w = 1;
          out_f.color = result_1;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
