Shader "CameraFilterPack/AAA_FBSuper_Hexagon"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _NoiseTex ("Noise (RGB)", 2D) = "white" {}
    _NoiseSpeed ("Noise speed", Range(1, 10)) = 1
    _NoiseAlpha ("Noise Alpha", Range(0, 1)) = 0.3
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
      //uniform float4 _Time;
      uniform sampler2D _MainTex;
      uniform sampler2D _NoiseTex;
      uniform float _NoiseSpeed;
      uniform float _NoiseAlpha;
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
          float4 texel2_2;
          float4 texel_3;
          float2 uv_4;
          uv_4 = in_f.xlv_TEXCOORD0;
          float tmpvar_5;
          tmpvar_5 = ((_Value * _ScreenResolution.x) / 160);
          float2 tmpvar_6;
          float2 st_7;
          st_7 = (in_f.xlv_TEXCOORD0 * _ScreenResolution.xy);
          float2 coord_8;
          float tmpvar_9;
          tmpvar_9 = (0.5 * tmpvar_5);
          float tmpvar_10;
          tmpvar_10 = (0.8660254 * tmpvar_5);
          float tmpvar_11;
          tmpvar_11 = (tmpvar_9 / tmpvar_10);
          float2 tmpvar_12;
          tmpvar_12.x = (2 * tmpvar_10);
          tmpvar_12.y = (tmpvar_9 + tmpvar_5);
          float2 tmpvar_13;
          tmpvar_13 = (st_7 / tmpvar_12);
          float2 tmpvar_14;
          tmpvar_14.x = (2 * tmpvar_10);
          tmpvar_14.y = (tmpvar_9 + tmpvar_5);
          float2 tmpvar_15;
          tmpvar_15 = (st_7 / tmpvar_14);
          float2 tmpvar_16;
          tmpvar_16 = (frac(abs(tmpvar_15)) * tmpvar_14);
          float tmpvar_17;
          if((tmpvar_15.x>=0))
          {
              tmpvar_17 = tmpvar_16.x;
          }
          else
          {
              tmpvar_17 = (-tmpvar_16.x);
          }
          float tmpvar_18;
          if((tmpvar_15.y>=0))
          {
              tmpvar_18 = tmpvar_16.y;
          }
          else
          {
              tmpvar_18 = (-tmpvar_16.y);
          }
          float tmpvar_19;
          tmpvar_19 = (floor(tmpvar_13.y) / 2);
          float tmpvar_20;
          tmpvar_20 = (frac(abs(tmpvar_19)) * 2);
          float tmpvar_21;
          if((tmpvar_19>=0))
          {
              tmpvar_21 = tmpvar_20;
          }
          else
          {
              tmpvar_21 = (-tmpvar_20);
          }
          float2 tmpvar_22;
          tmpvar_22 = floor(tmpvar_13);
          coord_8 = tmpvar_22;
          if((tmpvar_21>0))
          {
              if((tmpvar_18<(tmpvar_9 - (tmpvar_17 * tmpvar_11))))
              {
                  coord_8 = (tmpvar_22 - 1);
              }
              else
              {
                  if((tmpvar_18<((-tmpvar_9) + (tmpvar_17 * tmpvar_11))))
                  {
                      coord_8.y = (coord_8.y - 1);
                  }
              }
          }
          else
          {
              if((tmpvar_17>tmpvar_10))
              {
                  if((tmpvar_18<((2 * tmpvar_9) - (tmpvar_17 * tmpvar_11))))
                  {
                      coord_8.y = (coord_8.y - 1);
                  }
              }
              else
              {
                  if((tmpvar_18<(tmpvar_17 * tmpvar_11)))
                  {
                      coord_8.y = (coord_8.y - 1);
                  }
                  else
                  {
                      coord_8.x = (coord_8.x - 1);
                  }
              }
          }
          float tmpvar_23;
          tmpvar_23 = (coord_8.y / 2);
          float tmpvar_24;
          tmpvar_24 = (frac(abs(tmpvar_23)) * 2);
          float tmpvar_25;
          if((tmpvar_23>=0))
          {
              tmpvar_25 = tmpvar_24;
          }
          else
          {
              tmpvar_25 = (-tmpvar_24);
          }
          float2 tmpvar_26;
          tmpvar_26.x = (((coord_8.x * 2) * tmpvar_10) - (tmpvar_25 * tmpvar_10));
          tmpvar_26.y = (coord_8.y * (tmpvar_9 + tmpvar_5));
          float2 tmpvar_27;
          tmpvar_27.x = (tmpvar_10 * 2);
          tmpvar_27.y = tmpvar_5;
          tmpvar_6 = (tmpvar_26 + tmpvar_27);
          float4 tmpvar_28;
          tmpvar_28.xyz = float3(1, 1, 1);
          tmpvar_28.w = _NoiseAlpha;
          texel_3.w = tmpvar_28.w;
          texel_3.xyz = float3(_NoiseAlpha, _NoiseAlpha, _NoiseAlpha);
          float2 uv_29;
          uv_29 = ((tmpvar_6 / _ScreenResolution.xy) + (_Time.x * _NoiseSpeed));
          float4 tmpvar_30;
          tmpvar_30 = tex2D(_NoiseTex, uv_29);
          float4 tmpvar_31;
          tmpvar_31 = tex2D(_MainTex, uv_29);
          float4 tmpvar_32;
          tmpvar_32 = ((tmpvar_30 * _NoiseAlpha) + (tmpvar_31 * (1 - _NoiseAlpha)));
          float tmpvar_33;
          tmpvar_33 = (((((tmpvar_32.x + tmpvar_32.y) + tmpvar_32.z) / 3) * 10) / 6);
          float tmpvar_34;
          tmpvar_34 = (frac(abs(tmpvar_33)) * 6);
          float tmpvar_35;
          if((tmpvar_33>=0))
          {
              tmpvar_35 = tmpvar_34;
          }
          else
          {
              tmpvar_35 = (-tmpvar_34);
          }
          texel_3 = ((texel_3 * tmpvar_35) * 0.6);
          float4 tmpvar_36;
          tmpvar_36 = tex2D(_MainTex, uv_4);
          texel2_2 = tmpvar_36;
          float2 uv_37;
          uv_37 = (uv_4 - float2(0.5, 0.5));
          uv_37 = ((uv_37 * 1.2) * (0.8333333 + ((2 * uv_37.x) * ((uv_37.x * uv_37.y) * uv_37.y))));
          uv_37 = (uv_37 + float2(0.5, 0.5));
          uv_4 = uv_37;
          float2 tmpvar_38;
          tmpvar_38.x = _PositionX;
          tmpvar_38.y = _PositionY;
          float2 x_39;
          x_39 = (tmpvar_38 - uv_37);
          float tmpvar_40;
          tmpvar_40 = clamp(((sqrt(dot(x_39, x_39)) - _Radius) / ((_Radius + (0.15 * _SpotSize)) - _Radius)), 0, 1);
          float2 tmpvar_41;
          tmpvar_41 = abs((tmpvar_6 - (in_f.xlv_TEXCOORD0 * _ScreenResolution.xy)));
          float tmpvar_42;
          tmpvar_42 = (tmpvar_5 * _BorderSize);
          float edge0_43;
          edge0_43 = (tmpvar_42 - 1);
          float tmpvar_44;
          tmpvar_44 = clamp((((max(((0.5 * tmpvar_41.x) + (0.8660254 * tmpvar_41.y)), tmpvar_41.x) / 0.8660254) - edge0_43) / (tmpvar_42 - edge0_43)), 0, 1);
          result_1.w = 0;
          float _tmp_dvx_130 = (1 - (tmpvar_44 * (tmpvar_44 * (3 - (2 * tmpvar_44)))));
          result_1.xyz = lerp(_BorderColor.xyz, texel_3.xyz, float3(_tmp_dvx_130, _tmp_dvx_130, _tmp_dvx_130));
          result_1.xyz = (result_1.xyz + float3(0.5, 0.5, 0.5));
          result_1.xyz = (result_1.xyz * _HexaColor.xyz);
          result_1 = (result_1 / (((1 - ((1.804371 * (uv_37.y - 0.5)) * (uv_37.y - 0.5))) * (1 - ((1.804371 * (uv_37.x - 0.5)) * (uv_37.x - 0.5)))) * 2));
          float _tmp_dvx_131 = (1 - (tmpvar_40 * (tmpvar_40 * (3 - (2 * tmpvar_40)))));
          result_1.xyz = lerp(result_1.xyz, texel2_2.xyz, float3(_tmp_dvx_131, _tmp_dvx_131, _tmp_dvx_131));
          float _tmp_dvx_132 = (1 - _AlphaHexa);
          result_1.xyz = lerp(result_1.xyz, texel2_2.xyz, float3(_tmp_dvx_132, _tmp_dvx_132, _tmp_dvx_132));
          result_1.w = 1;
          out_f.color = result_1;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
