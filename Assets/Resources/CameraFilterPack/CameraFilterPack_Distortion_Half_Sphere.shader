Shader "CameraFilterPack/Distortion_Half_Sphere"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _TimeX ("Time", Range(0, 1)) = 1
    _Distortion ("_Distortion", Range(0, 1)) = 0.3
    _ScreenResolution ("_ScreenResolution", Vector) = (0,0,0,0)
    _SphereSize ("_SphereSize", Range(1, 10)) = 1
    _SpherePositionX ("_SpherePositionX", Range(-1, 1)) = 0
    _SpherePositionY ("_SpherePositionY", Range(-1, 1)) = 0
    _Strength ("_Strength", Range(1, 10)) = 5
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
      uniform float _SphereSize;
      uniform float _SpherePositionX;
      uniform float _SpherePositionY;
      uniform float _Strength;
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
          float4 tmpvar_1;
          float2 uv_lense_distorted_2;
          float2 tmpvar_3;
          tmpvar_3.x = 1;
          tmpvar_3.y = (_ScreenResolution.y / _ScreenResolution.x);
          float2 tmpvar_4;
          tmpvar_4 = (0.5 + ((in_f.xlv_TEXCOORD0 - 0.5) * tmpvar_3));
          float2 tmpvar_5;
          tmpvar_5.x = (0.5 + (_SpherePositionX / 2));
          tmpvar_5.y = (0.5 - (_SpherePositionY / 2));
          float radius_6;
          radius_6 = (0.1 * _SphereSize);
          float refractivity_7;
          refractivity_7 = (1.075 * _Strength);
          float rad_8;
          float2 tmpvar_9;
          tmpvar_9 = (tmpvar_4 - tmpvar_5);
          rad_8 = sqrt(dot(tmpvar_9, tmpvar_9));
          float tmpvar_10;
          if((tmpvar_9.y>0))
          {
              float y_over_x_11;
              y_over_x_11 = (tmpvar_9.x / tmpvar_9.y);
              float tmpvar_12;
              tmpvar_12 = (min(abs(y_over_x_11), 1) / max(abs(y_over_x_11), 1));
              float tmpvar_13;
              tmpvar_13 = (tmpvar_12 * tmpvar_12);
              tmpvar_13 = (((((((((((-0.01213232 * tmpvar_13) + 0.05368138) * tmpvar_13) - 0.1173503) * tmpvar_13) + 0.1938925) * tmpvar_13) - 0.3326756) * tmpvar_13) + 0.9999793) * tmpvar_12);
              tmpvar_13 = (tmpvar_13 + (float((abs(y_over_x_11)>1)) * ((tmpvar_13 * (-2)) + 1.570796)));
              tmpvar_10 = (tmpvar_13 * sign(y_over_x_11));
          }
          else
          {
              if(((tmpvar_9.x>=0) && (tmpvar_9.y<0)))
              {
                  float y_over_x_14;
                  y_over_x_14 = (tmpvar_9.x / tmpvar_9.y);
                  float tmpvar_15;
                  tmpvar_15 = (min(abs(y_over_x_14), 1) / max(abs(y_over_x_14), 1));
                  float tmpvar_16;
                  tmpvar_16 = (tmpvar_15 * tmpvar_15);
                  tmpvar_16 = (((((((((((-0.01213232 * tmpvar_16) + 0.05368138) * tmpvar_16) - 0.1173503) * tmpvar_16) + 0.1938925) * tmpvar_16) - 0.3326756) * tmpvar_16) + 0.9999793) * tmpvar_15);
                  tmpvar_16 = (tmpvar_16 + (float((abs(y_over_x_14)>1)) * ((tmpvar_16 * (-2)) + 1.570796)));
                  tmpvar_10 = ((tmpvar_16 * sign(y_over_x_14)) + 3.14);
              }
              else
              {
                  if(((tmpvar_9.x<0) && (tmpvar_9.y<0)))
                  {
                      float y_over_x_17;
                      y_over_x_17 = (tmpvar_9.x / tmpvar_9.y);
                      float tmpvar_18;
                      tmpvar_18 = (min(abs(y_over_x_17), 1) / max(abs(y_over_x_17), 1));
                      float tmpvar_19;
                      tmpvar_19 = (tmpvar_18 * tmpvar_18);
                      tmpvar_19 = (((((((((((-0.01213232 * tmpvar_19) + 0.05368138) * tmpvar_19) - 0.1173503) * tmpvar_19) + 0.1938925) * tmpvar_19) - 0.3326756) * tmpvar_19) + 0.9999793) * tmpvar_18);
                      tmpvar_19 = (tmpvar_19 + (float((abs(y_over_x_17)>1)) * ((tmpvar_19 * (-2)) + 1.570796)));
                      tmpvar_10 = ((tmpvar_19 * sign(y_over_x_17)) - 3.14);
                  }
                  else
                  {
                      if(((tmpvar_9.x>0) && (tmpvar_9.y==0)))
                      {
                          tmpvar_10 = 1.57;
                      }
                      else
                      {
                          if(((tmpvar_9.x<0) && (tmpvar_9.y==0)))
                          {
                              tmpvar_10 = (-1.57);
                          }
                          else
                          {
                              if(((tmpvar_9.x==0) && (tmpvar_9.y==0)))
                              {
                                  tmpvar_10 = 1.57;
                              }
                              else
                              {
                                  tmpvar_10 = 1.57;
                              }
                          }
                      }
                  }
              }
          }
          float tmpvar_20;
          tmpvar_20 = clamp((1 - (rad_8 / radius_6)), 0, 1);
          float tmpvar_21;
          float tmpvar_22;
          tmpvar_22 = (tmpvar_20 - 1);
          tmpvar_21 = sqrt((1 - (tmpvar_22 * tmpvar_22)));
          float y_23;
          y_23 = (1 - tmpvar_20);
          float tmpvar_24;
          if((tmpvar_21>0))
          {
              float y_over_x_25;
              y_over_x_25 = (y_23 / tmpvar_21);
              float tmpvar_26;
              tmpvar_26 = (min(abs(y_over_x_25), 1) / max(abs(y_over_x_25), 1));
              float tmpvar_27;
              tmpvar_27 = (tmpvar_26 * tmpvar_26);
              tmpvar_27 = (((((((((((-0.01213232 * tmpvar_27) + 0.05368138) * tmpvar_27) - 0.1173503) * tmpvar_27) + 0.1938925) * tmpvar_27) - 0.3326756) * tmpvar_27) + 0.9999793) * tmpvar_26);
              tmpvar_27 = (tmpvar_27 + (float((abs(y_over_x_25)>1)) * ((tmpvar_27 * (-2)) + 1.570796)));
              tmpvar_24 = (tmpvar_27 * sign(y_over_x_25));
          }
          else
          {
              if(((y_23>=0) && (tmpvar_21<0)))
              {
                  float y_over_x_28;
                  y_over_x_28 = (y_23 / tmpvar_21);
                  float tmpvar_29;
                  tmpvar_29 = (min(abs(y_over_x_28), 1) / max(abs(y_over_x_28), 1));
                  float tmpvar_30;
                  tmpvar_30 = (tmpvar_29 * tmpvar_29);
                  tmpvar_30 = (((((((((((-0.01213232 * tmpvar_30) + 0.05368138) * tmpvar_30) - 0.1173503) * tmpvar_30) + 0.1938925) * tmpvar_30) - 0.3326756) * tmpvar_30) + 0.9999793) * tmpvar_29);
                  tmpvar_30 = (tmpvar_30 + (float((abs(y_over_x_28)>1)) * ((tmpvar_30 * (-2)) + 1.570796)));
                  tmpvar_24 = ((tmpvar_30 * sign(y_over_x_28)) + 3.14);
              }
              else
              {
                  if(((y_23<0) && (tmpvar_21<0)))
                  {
                      float y_over_x_31;
                      y_over_x_31 = (y_23 / tmpvar_21);
                      float tmpvar_32;
                      tmpvar_32 = (min(abs(y_over_x_31), 1) / max(abs(y_over_x_31), 1));
                      float tmpvar_33;
                      tmpvar_33 = (tmpvar_32 * tmpvar_32);
                      tmpvar_33 = (((((((((((-0.01213232 * tmpvar_33) + 0.05368138) * tmpvar_33) - 0.1173503) * tmpvar_33) + 0.1938925) * tmpvar_33) - 0.3326756) * tmpvar_33) + 0.9999793) * tmpvar_32);
                      tmpvar_33 = (tmpvar_33 + (float((abs(y_over_x_31)>1)) * ((tmpvar_33 * (-2)) + 1.570796)));
                      tmpvar_24 = ((tmpvar_33 * sign(y_over_x_31)) - 3.14);
                  }
                  else
                  {
                      if(((y_23>0) && (tmpvar_21==0)))
                      {
                          tmpvar_24 = 1.57;
                      }
                      else
                      {
                          if(((y_23<0) && (tmpvar_21==0)))
                          {
                              tmpvar_24 = (-1.57);
                          }
                          else
                          {
                              if(((y_23==0) && (tmpvar_21==0)))
                              {
                                  tmpvar_24 = 1.57;
                              }
                              else
                              {
                                  tmpvar_24 = 1.57;
                              }
                          }
                      }
                  }
              }
          }
          float x_34;
          x_34 = (sin(tmpvar_24) / refractivity_7);
          float tmpvar_35;
          tmpvar_35 = (tmpvar_24 - (sign(x_34) * (1.570796 - (sqrt((1 - abs(x_34))) * (1.570796 + (abs(x_34) * (-0.2146018 + (abs(x_34) * (0.08656672 + (abs(x_34) * (-0.03102955)))))))))));
          float2 tmpvar_36;
          tmpvar_36.x = sin(tmpvar_10);
          tmpvar_36.y = cos(tmpvar_10);
          float2 x_37;
          x_37 = (tmpvar_4 - tmpvar_5);
          float t_38;
          t_38 = float((sqrt(dot(x_37, x_37))<radius_6));
          float _tmp_dvx_54 = (0.5 + ((((tmpvar_4 * (1 - t_38)) + ((tmpvar_5 + ((tmpvar_36 * ((1 - tmpvar_20) - ((sin(tmpvar_35) * tmpvar_21) / cos(tmpvar_35)))) * radius_6)) * t_38)) - 0.5) / tmpvar_3));
          uv_lense_distorted_2 = float2(_tmp_dvx_54, _tmp_dvx_54);
          float4 tmpvar_39;
          tmpvar_39 = tex2D(_MainTex, uv_lense_distorted_2);
          tmpvar_1 = tmpvar_39;
          out_f.color = tmpvar_1;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
