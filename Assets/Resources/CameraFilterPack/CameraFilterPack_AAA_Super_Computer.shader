Shader "CameraFilterPack/AAA_Super_Computer"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _TimeX ("Time", Range(0, 1)) = 1
    _Distortion ("_Distortion", Range(0, 1)) = 0.3
    _ScreenResolution ("_ScreenResolution", Vector) = (0,0,0,0)
    _Value ("_Value", Range(0.2, 10)) = 8.1
    _BorderSize ("_BorderSize", Range(-0.5, 0.5)) = 0
    _BorderColor ("_BorderColor", Color) = (0,0.5,1,1)
    _SpotSize ("_SpotSize", Range(0, 1)) = 0.5
    _AlphaHexa ("_AlphaHexa", Range(0.2, 10)) = 0.608
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
      uniform float _Value;
      uniform float _Value2;
      uniform float _BorderSize;
      uniform float4 _BorderColor;
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
          float3 col_1;
          float f_3;
          float a_4;
          float v_5;
          float flicker_6;
          float4 tex_7;
          float2 uv_8;
          uv_8 = in_f.xlv_TEXCOORD0;
          float4 tmpvar_9;
          tmpvar_9 = tex2D(_MainTex, uv_8);
          tex_7 = tmpvar_9;
          uv_8 = ((uv_8 - 0.5) * 2);
          float p_10;
          p_10 = (_TimeX * 1.3);
          float tmpvar_11;
          tmpvar_11 = floor(p_10);
          flicker_6 = ((lerp(frac((sin(tmpvar_11) * 43758.55)), frac((sin((tmpvar_11 + 1)) * 43758.55)), frac((p_10 * 2345.12))) * 0.8) + 0.4);
          v_5 = 0;
          a_4 = 0.6;
          f_3 = 1;
          int i_1_2 = 0;
          while((i_1_2<3))
          {
              float v2_12;
              float v1_13;
              float tmpvar_14;
              float2 x_15;
              x_15 = ((uv_8 * f_3) + 5);
              float2 res_17;
              float2 f_18;
              float2 p_19;
              p_19 = floor(x_15);
              f_18 = frac(x_15);
              res_17 = float2(8, 8);
              int j_16 = (-1);
              while((j_16<=1))
              {
                  float2 tmpvar_20;
                  tmpvar_20.x = (-1);
                  tmpvar_20.y = float(j_16);
                  float2 p_21;
                  p_21 = (p_19 + tmpvar_20);
                  float2 tmpvar_22;
                  tmpvar_22.x = sin((((p_21.x * 591.32) + (p_21.y * 154.077)) + _TimeX));
                  tmpvar_22.y = cos((((p_21.x * 391.32) + (p_21.y * 49.077)) + _TimeX));
                  float2 tmpvar_23;
                  tmpvar_23 = ((tmpvar_20 - f_18) + (frac(tmpvar_22) * _BorderSize));
                  float tmpvar_24;
                  tmpvar_24 = max(abs(tmpvar_23.x), abs(tmpvar_23.y));
                  if((tmpvar_24<res_17.x))
                  {
                      res_17.y = res_17.x;
                      res_17.x = tmpvar_24;
                  }
                  else
                  {
                      if((tmpvar_24<res_17.y))
                      {
                          res_17.y = tmpvar_24;
                      }
                  }
                  float2 tmpvar_25;
                  tmpvar_25.x = 0;
                  tmpvar_25.y = float(j_16);
                  float2 p_26;
                  p_26 = (p_19 + tmpvar_25);
                  float2 tmpvar_27;
                  tmpvar_27.x = sin((((p_26.x * 591.32) + (p_26.y * 154.077)) + _TimeX));
                  tmpvar_27.y = cos((((p_26.x * 391.32) + (p_26.y * 49.077)) + _TimeX));
                  float2 tmpvar_28;
                  tmpvar_28 = ((tmpvar_25 - f_18) + (frac(tmpvar_27) * _BorderSize));
                  float tmpvar_29;
                  tmpvar_29 = max(abs(tmpvar_28.x), abs(tmpvar_28.y));
                  if((tmpvar_29<res_17.x))
                  {
                      res_17.y = res_17.x;
                      res_17.x = tmpvar_29;
                  }
                  else
                  {
                      if((tmpvar_29<res_17.y))
                      {
                          res_17.y = tmpvar_29;
                      }
                  }
                  float2 tmpvar_30;
                  tmpvar_30.x = 1;
                  tmpvar_30.y = float(j_16);
                  float2 p_31;
                  p_31 = (p_19 + tmpvar_30);
                  float2 tmpvar_32;
                  tmpvar_32.x = sin((((p_31.x * 591.32) + (p_31.y * 154.077)) + _TimeX));
                  tmpvar_32.y = cos((((p_31.x * 391.32) + (p_31.y * 49.077)) + _TimeX));
                  float2 tmpvar_33;
                  tmpvar_33 = ((tmpvar_30 - f_18) + (frac(tmpvar_32) * _BorderSize));
                  float tmpvar_34;
                  tmpvar_34 = max(abs(tmpvar_33.x), abs(tmpvar_33.y));
                  if((tmpvar_34<res_17.x))
                  {
                      res_17.y = res_17.x;
                      res_17.x = tmpvar_34;
                  }
                  else
                  {
                      if((tmpvar_34<res_17.y))
                      {
                          res_17.y = tmpvar_34;
                      }
                  }
                  j_16 = (j_16 + 1);
              }
              tmpvar_14 = (res_17.y - (res_17.x * _Value2));
              v1_13 = tmpvar_14;
              v2_12 = 0;
              if((i_1_2>0))
              {
                  float tmpvar_35;
                  float2 x_36;
                  x_36 = ((((uv_8 * f_3) * 0.5) + 50) + _TimeX);
                  int j_37;
                  float2 res_38;
                  float2 f_39;
                  float2 p_40;
                  p_40 = floor(x_36);
                  f_39 = frac(x_36);
                  res_38 = float2(8, 8);
                  j_37 = (-1);
                  while(true)
                  {
                      int i_41;
                      if((j_37>1))
                      {
                          break;
                      }
                      i_41 = (-1);
                      while(true)
                      {
                          if((i_41>1))
                          {
                              break;
                          }
                          float2 tmpvar_42;
                          tmpvar_42.x = float(i_41);
                          tmpvar_42.y = float(j_37);
                          float2 p_43;
                          p_43 = (p_40 + tmpvar_42);
                          float2 tmpvar_44;
                          tmpvar_44.x = sin((((p_43.x * 591.32) + (p_43.y * 154.077)) + _TimeX));
                          tmpvar_44.y = cos((((p_43.x * 391.32) + (p_43.y * 49.077)) + _TimeX));
                          float2 tmpvar_45;
                          tmpvar_45 = ((tmpvar_42 - f_39) + (frac(tmpvar_44) * _BorderSize));
                          float tmpvar_46;
                          tmpvar_46 = max(abs(tmpvar_45.x), abs(tmpvar_45.y));
                          if((tmpvar_46<res_38.x))
                          {
                              res_38.y = res_38.x;
                              res_38.x = tmpvar_46;
                          }
                          else
                          {
                              if((tmpvar_46<res_38.y))
                              {
                                  res_38.y = tmpvar_46;
                              }
                          }
                          i_41 = (i_41 + 1);
                      }
                      j_37 = (j_37 + 1);
                  }
                  tmpvar_35 = (res_38.y - (res_38.x * _Value2));
                  v2_12 = tmpvar_35;
                  float tmpvar_47;
                  tmpvar_47 = clamp((tmpvar_14 / 0.1), 0, 1);
                  float tmpvar_48;
                  tmpvar_48 = clamp((tmpvar_35 / 0.08), 0, 1);
                  float tmpvar_49;
                  tmpvar_49 = ((1 - (tmpvar_47 * (tmpvar_47 * (3 - (2 * tmpvar_47))))) * (0.5 + (1 - (tmpvar_48 * (tmpvar_48 * (3 - (2 * tmpvar_48)))))));
                  v_5 = (v_5 + (a_4 * (tmpvar_49 * tmpvar_49)));
              }
              float tmpvar_50;
              tmpvar_50 = clamp((tmpvar_14 / 0.3), 0, 1);
              v1_13 = (1 - (tmpvar_50 * (tmpvar_50 * (3 - (2 * tmpvar_50)))));
              float p_51;
              p_51 = ((v1_13 * 5.5) + 0.1);
              float tmpvar_52;
              tmpvar_52 = floor(p_51);
              v2_12 = (a_4 * lerp(frac((sin(tmpvar_52) * 43758.55)), frac((sin((tmpvar_52 + 1)) * 43758.55)), frac((p_51 * 2345.12))));
              if((i_1_2==0))
              {
                  v_5 = (v_5 + (v2_12 * flicker_6));
              }
              else
              {
                  v_5 = (v_5 + v2_12);
              }
              f_3 = (f_3 * 3);
              a_4 = (a_4 * 0.7);
              i_1_2 = (i_1_2 + 1);
          }
          v_5 = (v_5 * (exp((-0.6 * sqrt(dot(uv_8, uv_8)))) * 1.2));
          float3 tmpvar_53;
          tmpvar_53.x = pow(v_5, 2.5);
          tmpvar_53.y = pow(v_5, 2.5);
          tmpvar_53.z = pow(v_5, 2.5);
          col_1 = ((tmpvar_53 * _Value) * _BorderColor.xyz);
          float2 tmpvar_54;
          tmpvar_54.x = _PositionX;
          tmpvar_54.y = _PositionY;
          float2 x_55;
          x_55 = (tmpvar_54 - uv_8);
          float tmpvar_56;
          tmpvar_56 = clamp(((sqrt(dot(x_55, x_55)) - _Radius) / ((_Radius + (0.15 * _SpotSize)) - _Radius)), 0, 1);
          float _tmp_dvx_137 = (1 - (tmpvar_56 * (tmpvar_56 * (3 - (2 * tmpvar_56)))));
          col_1 = lerp((col_1 + tex_7.xyz), tex_7.xyz, float3(_tmp_dvx_137, _tmp_dvx_137, _tmp_dvx_137));
          float _tmp_dvx_138 = (1 - _AlphaHexa);
          col_1 = lerp(col_1, tex_7.xyz, float3(_tmp_dvx_138, _tmp_dvx_138, _tmp_dvx_138));
          float4 tmpvar_57;
          tmpvar_57.w = 1;
          tmpvar_57.xyz = float3(col_1);
          out_f.color = tmpvar_57;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
