Shader "CameraFilterPack/Vision_Aura"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _TimeX ("Time", Range(0, 1)) = 1
    _ScreenResolution ("_ScreenResolution", Vector) = (0,0,0,0)
    _Value2 ("_ColorRGB", Color) = (1,1,1,1)
    _Value5 ("Speed", Range(0, 1)) = 1
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
      uniform float4 _Value2;
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
          float3 color_2;
          float a_4;
          float dst_5;
          float ang_6;
          float time_7;
          float2 uv_8;
          float2 tmpvar_9;
          tmpvar_9.x = _Value3;
          tmpvar_9.y = _Value4;
          float2 tmpvar_10;
          tmpvar_10 = (in_f.xlv_TEXCOORD0 - tmpvar_9);
          uv_8 = tmpvar_10;
          float tmpvar_11;
          tmpvar_11 = (_TimeX * _Value5);
          time_7 = tmpvar_11;
          float tmpvar_12;
          float tmpvar_13;
          tmpvar_13 = (min(abs((tmpvar_10.y / tmpvar_10.x)), 1) / max(abs((tmpvar_10.y / tmpvar_10.x)), 1));
          float tmpvar_14;
          tmpvar_14 = (tmpvar_13 * tmpvar_13);
          tmpvar_14 = (((((((((((-0.01213232 * tmpvar_14) + 0.05368138) * tmpvar_14) - 0.1173503) * tmpvar_14) + 0.1938925) * tmpvar_14) - 0.3326756) * tmpvar_14) + 0.9999793) * tmpvar_13);
          tmpvar_14 = (tmpvar_14 + (float((abs((tmpvar_10.y / tmpvar_10.x))>1)) * ((tmpvar_14 * (-2)) + 1.570796)));
          tmpvar_12 = (tmpvar_14 * sign((tmpvar_10.y / tmpvar_10.x)));
          if((abs(tmpvar_10.x)>(1E-08 * abs(tmpvar_10.y))))
          {
              if((tmpvar_10.x<0))
              {
                  if((tmpvar_10.y>=0))
                  {
                      tmpvar_12 = (tmpvar_12 + 3.141593);
                  }
                  else
                  {
                      tmpvar_12 = (tmpvar_12 - 3.141593);
                  }
              }
          }
          else
          {
              tmpvar_12 = (sign(tmpvar_10.y) * 1.570796);
          }
          ang_6 = tmpvar_12;
          float tmpvar_15;
          tmpvar_15 = (sqrt(dot(tmpvar_10, tmpvar_10)) * _Value);
          dst_5 = tmpvar_15;
          float tmpvar_16;
          tmpvar_16 = clamp((((tmpvar_15 * 40) - 3) + (cos(((tmpvar_12 + cos((tmpvar_12 * 6))) + (tmpvar_11 * 2))) * 0.68)), 0, 1);
          a_4 = 0;
          int q_3 = 3;
          while((q_3<6))
          {
              float tmpvar_17;
              tmpvar_17 = float(q_3);
              float2 tmpvar_18;
              tmpvar_18 = (uv_8 + (cos((((((ang_6 - dst_5) * tmpvar_17) + time_7) + uv_8) + tmpvar_17)) * 0.2));
              float x_19;
              float d_20;
              float2 sp_21;
              float2 lp_22;
              float2 tmpvar_23;
              tmpvar_23 = (abs(tmpvar_18) * 10);
              sp_21 = (frac(tmpvar_23) - 0.5);
              lp_22 = floor(tmpvar_23);
              x_19 = (-1);
              float y_24;
              y_24 = (-1);
              float2 tmpvar_25;
              tmpvar_25.x = x_19;
              tmpvar_25.y = y_24;
              float2 tmpvar_26;
              tmpvar_26 = (lp_22 + tmpvar_25);
              float2 x_27;
              x_27 = ((sp_21 + ((cos((tmpvar_26.x + _TimeX)) + cos((tmpvar_26.y + _TimeX))) * 0.3)) - tmpvar_25);
              d_20 = min(1, sqrt(dot(x_27, x_27)));
              y_24 = 0;
              float2 tmpvar_28;
              tmpvar_28.x = x_19;
              tmpvar_28.y = y_24;
              float2 tmpvar_29;
              tmpvar_29 = (lp_22 + tmpvar_28);
              float2 x_30;
              x_30 = ((sp_21 + ((cos((tmpvar_29.x + _TimeX)) + cos((tmpvar_29.y + _TimeX))) * 0.3)) - tmpvar_28);
              d_20 = min(d_20, sqrt(dot(x_30, x_30)));
              y_24 = 1;
              float2 tmpvar_31;
              tmpvar_31.x = x_19;
              tmpvar_31.y = y_24;
              float2 tmpvar_32;
              tmpvar_32 = (lp_22 + tmpvar_31);
              float2 x_33;
              x_33 = ((sp_21 + ((cos((tmpvar_32.x + _TimeX)) + cos((tmpvar_32.y + _TimeX))) * 0.3)) - tmpvar_31);
              d_20 = min(d_20, sqrt(dot(x_33, x_33)));
              y_24 = 2;
              x_19 = 0;
              float y_34;
              y_34 = (-1);
              float2 tmpvar_35;
              tmpvar_35.x = x_19;
              tmpvar_35.y = y_34;
              float2 tmpvar_36;
              tmpvar_36 = (lp_22 + tmpvar_35);
              float2 x_37;
              x_37 = ((sp_21 + ((cos((tmpvar_36.x + _TimeX)) + cos((tmpvar_36.y + _TimeX))) * 0.3)) - tmpvar_35);
              d_20 = min(d_20, sqrt(dot(x_37, x_37)));
              y_34 = 0;
              float2 tmpvar_38;
              tmpvar_38.x = x_19;
              tmpvar_38.y = y_34;
              float2 tmpvar_39;
              tmpvar_39 = (lp_22 + tmpvar_38);
              float2 x_40;
              x_40 = ((sp_21 + ((cos((tmpvar_39.x + _TimeX)) + cos((tmpvar_39.y + _TimeX))) * 0.3)) - tmpvar_38);
              d_20 = min(d_20, sqrt(dot(x_40, x_40)));
              y_34 = 1;
              float2 tmpvar_41;
              tmpvar_41.x = x_19;
              tmpvar_41.y = y_34;
              float2 tmpvar_42;
              tmpvar_42 = (lp_22 + tmpvar_41);
              float2 x_43;
              x_43 = ((sp_21 + ((cos((tmpvar_42.x + _TimeX)) + cos((tmpvar_42.y + _TimeX))) * 0.3)) - tmpvar_41);
              d_20 = min(d_20, sqrt(dot(x_43, x_43)));
              y_34 = 2;
              x_19 = 1;
              float y_44;
              y_44 = (-1);
              float2 tmpvar_45;
              tmpvar_45.x = x_19;
              tmpvar_45.y = y_44;
              float2 tmpvar_46;
              tmpvar_46 = (lp_22 + tmpvar_45);
              float2 x_47;
              x_47 = ((sp_21 + ((cos((tmpvar_46.x + _TimeX)) + cos((tmpvar_46.y + _TimeX))) * 0.3)) - tmpvar_45);
              d_20 = min(d_20, sqrt(dot(x_47, x_47)));
              y_44 = 0;
              float2 tmpvar_48;
              tmpvar_48.x = x_19;
              tmpvar_48.y = y_44;
              float2 tmpvar_49;
              tmpvar_49 = (lp_22 + tmpvar_48);
              float2 x_50;
              x_50 = ((sp_21 + ((cos((tmpvar_49.x + _TimeX)) + cos((tmpvar_49.y + _TimeX))) * 0.3)) - tmpvar_48);
              d_20 = min(d_20, sqrt(dot(x_50, x_50)));
              y_44 = 1;
              float2 tmpvar_51;
              tmpvar_51.x = x_19;
              tmpvar_51.y = y_44;
              float2 tmpvar_52;
              tmpvar_52 = (lp_22 + tmpvar_51);
              float2 x_53;
              x_53 = ((sp_21 + ((cos((tmpvar_52.x + _TimeX)) + cos((tmpvar_52.y + _TimeX))) * 0.3)) - tmpvar_51);
              d_20 = min(d_20, sqrt(dot(x_53, x_53)));
              y_44 = 2;
              x_19 = 2;
              a_4 = (a_4 + (d_20 * (0.7 + ((cos((tmpvar_18.x * 14.234)) + cos((tmpvar_18.y * 16.234))) * 0.4))));
              q_3 = (q_3 + 1);
          }
          float4 tmpvar_54;
          tmpvar_54 = tex2D(_MainTex, in_f.xlv_TEXCOORD0);
          v_1 = tmpvar_54;
          color_2 = ((_Value2.xyz * a_4) * (tmpvar_16 * _Value2.w));
          color_2 = (color_2 + v_1.xyz);
          float4 tmpvar_55;
          tmpvar_55.w = 1;
          tmpvar_55.xyz = float3(color_2);
          out_f.color = tmpvar_55;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
