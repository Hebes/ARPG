Shader "CameraFilterPack/FX_DigitalMatrix"
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
          float4 v_1;
          int i_2;
          float4 o_3;
          float2 p_4;
          float2 u_5;
          u_5 = in_f.xlv_TEXCOORD0;
          float tmpvar_6;
          tmpvar_6 = (_TimeX * _Value5);
          float2 tmpvar_7;
          tmpvar_7 = (u_5 * (24 / _Value));
          u_5 = tmpvar_7;
          float2 tmpvar_8;
          float _tmp_dvx_133 = ((6 * frac(tmpvar_7)) - 0.5);
          tmpvar_8 = float2(_tmp_dvx_133, _tmp_dvx_133);
          p_4 = tmpvar_8;
          float tmpvar_9;
          tmpvar_9 = ((40000 * sin(dot(ceil(tmpvar_7.xx), float2(12, 7)))) / 10);
          float tmpvar_10;
          tmpvar_10 = (frac(abs(tmpvar_9)) * 10);
          float tmpvar_11;
          if((tmpvar_9>=0))
          {
              tmpvar_11 = tmpvar_10;
          }
          else
          {
              tmpvar_11 = (-tmpvar_10);
          }
          u_5.y = (tmpvar_7.y + ceil(((tmpvar_6 * 2) * tmpvar_11)));
          o_3 = float4(0, 0, 0, 0);
          int tmpvar_12;
          tmpvar_12 = int(tmpvar_8.y);
          i_2 = tmpvar_12;
          float tmpvar_13;
          tmpvar_13 = abs((tmpvar_8.x - 1.5));
          int tmpvar_14;
          if((tmpvar_13>1.1))
          {
              tmpvar_14 = 0;
          }
          else
          {
              int tmpvar_15;
              if((tmpvar_12==5))
              {
                  tmpvar_15 = 972980223;
              }
              else
              {
                  int tmpvar_16;
                  if((tmpvar_12==4))
                  {
                      tmpvar_16 = 690407533;
                  }
                  else
                  {
                      int tmpvar_17;
                      if((tmpvar_12==3))
                      {
                          tmpvar_17 = 704642687;
                      }
                      else
                      {
                          int tmpvar_18;
                          if((tmpvar_12==2))
                          {
                              tmpvar_18 = 696556137;
                          }
                          else
                          {
                              int tmpvar_19;
                              if((tmpvar_12==1))
                              {
                                  tmpvar_19 = 972881535;
                              }
                              else
                              {
                                  tmpvar_19 = 0;
                              }
                              tmpvar_18 = tmpvar_19;
                          }
                          tmpvar_17 = tmpvar_18;
                      }
                      tmpvar_16 = tmpvar_17;
                  }
                  tmpvar_15 = tmpvar_16;
              }
              tmpvar_14 = tmpvar_15;
          }
          float tmpvar_20;
          tmpvar_20 = ceil(tmpvar_8.x);
          float tmpvar_21;
          tmpvar_21 = ((40000 * sin(dot(ceil(u_5), float2(12, 7)))) / 10);
          float tmpvar_22;
          tmpvar_22 = (frac(abs(tmpvar_21)) * 10);
          float tmpvar_23;
          if((tmpvar_21>=0))
          {
              tmpvar_23 = tmpvar_22;
          }
          else
          {
              tmpvar_23 = (-tmpvar_22);
          }
          i_2 = (tmpvar_14 / int(exp2(((30 - tmpvar_20) - (3 * floor(tmpvar_23))))));
          float2 tmpvar_24;
          tmpvar_24 = (u_5 + 1);
          u_5 = tmpvar_24;
          float tmpvar_25;
          tmpvar_25 = ((40000 * sin(dot(ceil(tmpvar_24), float2(12, 7)))) / 10);
          float tmpvar_26;
          tmpvar_26 = (frac(abs(tmpvar_25)) * 10);
          float tmpvar_27;
          if((tmpvar_25>=0))
          {
              tmpvar_27 = tmpvar_26;
          }
          else
          {
              tmpvar_27 = (-tmpvar_26);
          }
          if((tmpvar_27<9.9))
          {
              o_3 = float4(1, 1, 1, 1);
          }
          if((i_2>((i_2 / 2) * 2)))
          {
              o_3 = (o_3 + float4(1, 0, 0, 1));
          }
          else
          {
              o_3 = float4(0, 0, 0, 0);
          }
          u_5 = in_f.xlv_TEXCOORD0;
          float2 tmpvar_28;
          tmpvar_28 = (u_5 * (24 / (_Value / 2)));
          u_5 = tmpvar_28;
          float _tmp_dvx_134 = ((6 * frac(tmpvar_28)) - 0.5);
          p_4 = float2(_tmp_dvx_134, _tmp_dvx_134);
          float tmpvar_29;
          tmpvar_29 = ((40000 * sin(dot(ceil(tmpvar_28.xx), float2(12, 7)))) / 10);
          float tmpvar_30;
          tmpvar_30 = (frac(abs(tmpvar_29)) * 10);
          float tmpvar_31;
          if((tmpvar_29>=0))
          {
              tmpvar_31 = tmpvar_30;
          }
          else
          {
              tmpvar_31 = (-tmpvar_30);
          }
          u_5.y = (tmpvar_28.y + ceil((((tmpvar_6 * 2) * tmpvar_31) / 2)));
          i_2 = int(p_4.y);
          float tmpvar_32;
          tmpvar_32 = abs((p_4.x - 1.5));
          int tmpvar_33;
          if((tmpvar_32>1.1))
          {
              tmpvar_33 = 0;
          }
          else
          {
              int tmpvar_34;
              if((i_2==5))
              {
                  tmpvar_34 = 972980223;
              }
              else
              {
                  int tmpvar_35;
                  if((i_2==4))
                  {
                      tmpvar_35 = 690407533;
                  }
                  else
                  {
                      int tmpvar_36;
                      if((i_2==3))
                      {
                          tmpvar_36 = 704642687;
                      }
                      else
                      {
                          int tmpvar_37;
                          if((i_2==2))
                          {
                              tmpvar_37 = 696556137;
                          }
                          else
                          {
                              int tmpvar_38;
                              if((i_2==1))
                              {
                                  tmpvar_38 = 972881535;
                              }
                              else
                              {
                                  tmpvar_38 = 0;
                              }
                              tmpvar_37 = tmpvar_38;
                          }
                          tmpvar_36 = tmpvar_37;
                      }
                      tmpvar_35 = tmpvar_36;
                  }
                  tmpvar_34 = tmpvar_35;
              }
              tmpvar_33 = tmpvar_34;
          }
          float tmpvar_39;
          tmpvar_39 = ceil(p_4.x);
          float tmpvar_40;
          tmpvar_40 = ((40000 * sin(dot(ceil(u_5), float2(12, 7)))) / 10);
          float tmpvar_41;
          tmpvar_41 = (frac(abs(tmpvar_40)) * 10);
          float tmpvar_42;
          if((tmpvar_40>=0))
          {
              tmpvar_42 = tmpvar_41;
          }
          else
          {
              tmpvar_42 = (-tmpvar_41);
          }
          i_2 = (tmpvar_33 / int(exp2(((30 - tmpvar_39) - (3 * floor(tmpvar_42))))));
          u_5 = (u_5 + 1);
          if((i_2>((i_2 / 2) * 2)))
          {
              o_3 = (o_3 + float4(1, 0, 0, 1));
          }
          o_3.x = (o_3.x * _Value2);
          o_3.y = (o_3.y * _Value3);
          o_3.z = (o_3.z * _Value4);
          float4 tmpvar_43;
          tmpvar_43 = tex2D(_MainTex, in_f.xlv_TEXCOORD0);
          v_1 = tmpvar_43;
          float4 tmpvar_44;
          tmpvar_44 = (v_1 + o_3);
          out_f.color = tmpvar_44;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
