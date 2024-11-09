Shader "CameraFilterPack/Drawing_CellShading2"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _TimeX ("Time", Range(0, 1)) = 1
    _Distortion ("_Distortion", Range(0, 1)) = 0.3
    _ScreenResolution ("_ScreenResolution", Vector) = (0,0,0,0)
    _EdgeSize ("_EdgeSize", Range(0, 1)) = 0
    _ColorLevel ("_ColorLevel", Range(0, 10)) = 7
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
      uniform float _Distortion;
      uniform float4 _ScreenResolution;
      uniform float _EdgeSize;
      uniform float _ColorLevel;
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
      float4 impl_low_texture2DLodEXT(sampler2D sampler, float2 coord, float lod)
      {
          #if defined( GL_EXT_shader_texture_lod)
          {
              return tex2Dlod(sampler, float4(coord, lod));
              #else
              return tex2D(sampler, coord, lod);
              #endif
          }
      
          OUT_Data_Frag frag(v2f in_f)
          {
              float4 tmpvar_1;
              float2 tmpvar_2;
              tmpvar_2 = in_f.xlv_TEXCOORD0;
              float4 sum_3;
              float4 color_4;
              float4 rgbx_5;
              float Z_7;
              float3 final_colour_8;
              float kernel_9[6];
              final_colour_8 = float3(0, 0, 0);
              kernel_9[0] = 0;
              kernel_9[1] = 0;
              kernel_9[2] = 0;
              kernel_9[3] = 0;
              kernel_9[4] = 0;
              kernel_9[5] = 0;
              kernel_9[2] = 0.4;
              kernel_9[2] = 0.4;
              kernel_9[(2 - 1)] = 0.4;
              kernel_9[(2 + 1)] = 0.4;
              kernel_9[(2 - 2)] = 0.4;
              kernel_9[(2 + 2)] = 0.4;
              Z_7 = kernel_9[0];
              Z_7 = (Z_7 + kernel_9[1]);
              Z_7 = (Z_7 + kernel_9[2]);
              Z_7 = (Z_7 + kernel_9[3]);
              Z_7 = (Z_7 + kernel_9[4]);
              Z_7 = (Z_7 + kernel_9[5]);
              int u_6 = (-2);
              while((u_6<=2))
              {
                  float2 tmpvar_10;
                  tmpvar_10.x = ((float(u_6) * _Distortion) * 2);
                  tmpvar_10.y = (-4 * _Distortion);
                  float4 tmpvar_11;
                  tmpvar_11.zw = float2(0, 0);
                  tmpvar_11.xy = (((tmpvar_2 * _ScreenResolution.xy) + tmpvar_10) / _ScreenResolution.xy);
                  float4 tmpvar_12;
                  tmpvar_12 = impl_low_texture2DLodEXT(_MainTex, tmpvar_11.xy, 0);
                  final_colour_8 = (final_colour_8 + ((kernel_9[0] * kernel_9[(2 + u_6)]) * tmpvar_12.xyz));
                  float2 tmpvar_13;
                  tmpvar_13.x = ((float(u_6) * _Distortion) * 2);
                  tmpvar_13.y = ((-_Distortion) * 2);
                  float4 tmpvar_14;
                  tmpvar_14.zw = float2(0, 0);
                  tmpvar_14.xy = (((tmpvar_2 * _ScreenResolution.xy) + tmpvar_13) / _ScreenResolution.xy);
                  float4 tmpvar_15;
                  tmpvar_15 = impl_low_texture2DLodEXT(_MainTex, tmpvar_14.xy, 0);
                  final_colour_8 = (final_colour_8 + ((kernel_9[1] * kernel_9[(2 + u_6)]) * tmpvar_15.xyz));
                  float2 tmpvar_16;
                  tmpvar_16.x = ((float(u_6) * _Distortion) * 2);
                  tmpvar_16.y = 0;
                  float4 tmpvar_17;
                  tmpvar_17.zw = float2(0, 0);
                  tmpvar_17.xy = (((tmpvar_2 * _ScreenResolution.xy) + tmpvar_16) / _ScreenResolution.xy);
                  float4 tmpvar_18;
                  tmpvar_18 = impl_low_texture2DLodEXT(_MainTex, tmpvar_17.xy, 0);
                  final_colour_8 = (final_colour_8 + ((kernel_9[2] * kernel_9[(2 + u_6)]) * tmpvar_18.xyz));
                  float2 tmpvar_19;
                  tmpvar_19.x = ((float(u_6) * _Distortion) * 2);
                  tmpvar_19.y = (_Distortion * 2);
                  float4 tmpvar_20;
                  tmpvar_20.zw = float2(0, 0);
                  tmpvar_20.xy = (((tmpvar_2 * _ScreenResolution.xy) + tmpvar_19) / _ScreenResolution.xy);
                  float4 tmpvar_21;
                  tmpvar_21 = impl_low_texture2DLodEXT(_MainTex, tmpvar_20.xy, 0);
                  final_colour_8 = (final_colour_8 + ((kernel_9[3] * kernel_9[(2 + u_6)]) * tmpvar_21.xyz));
                  float2 tmpvar_22;
                  tmpvar_22.x = ((float(u_6) * _Distortion) * 2);
                  tmpvar_22.y = (4 * _Distortion);
                  float4 tmpvar_23;
                  tmpvar_23.zw = float2(0, 0);
                  tmpvar_23.xy = (((tmpvar_2 * _ScreenResolution.xy) + tmpvar_22) / _ScreenResolution.xy);
                  float4 tmpvar_24;
                  tmpvar_24 = impl_low_texture2DLodEXT(_MainTex, tmpvar_23.xy, 0);
                  final_colour_8 = (final_colour_8 + ((kernel_9[4] * kernel_9[(2 + u_6)]) * tmpvar_24.xyz));
                  u_6 = (u_6 + 1);
              }
              float4 tmpvar_25;
              tmpvar_25.w = 1;
              tmpvar_25.xyz = float3((final_colour_8 / (Z_7 * Z_7)));
              rgbx_5 = tmpvar_25;
              float4 tmpvar_26;
              tmpvar_26.zw = float2(0, 0);
              float2 tmpvar_27;
              tmpvar_27 = (in_f.xlv_TEXCOORD0 * _ScreenResolution.xy);
              tmpvar_26.xy = ((tmpvar_27 + float2(1, 1)) / _ScreenResolution.xy);
              color_4 = impl_low_texture2DLodEXT(_MainTex, tmpvar_26.xy, 0);
              float4 tmpvar_28;
              tmpvar_28.zw = float2(0, 0);
              tmpvar_28.xy = ((tmpvar_27 + float2(0, 1)) / _ScreenResolution.xy);
              color_4 = (color_4 + impl_low_texture2DLodEXT(_MainTex, tmpvar_28.xy, 0));
              float4 tmpvar_29;
              tmpvar_29.zw = float2(0, 0);
              tmpvar_29.xy = ((tmpvar_27 + float2(-1, 1)) / _ScreenResolution.xy);
              color_4 = (color_4 + impl_low_texture2DLodEXT(_MainTex, tmpvar_29.xy, 0));
              float4 tmpvar_30;
              tmpvar_30.zw = float2(0, 0);
              tmpvar_30.xy = ((tmpvar_27 + float2(1, 0)) / _ScreenResolution.xy);
              color_4 = (color_4 + impl_low_texture2DLodEXT(_MainTex, tmpvar_30.xy, 0));
              float4 tmpvar_31;
              tmpvar_31.zw = float2(0, 0);
              tmpvar_31.xy = (tmpvar_27 / _ScreenResolution.xy);
              color_4 = (color_4 + impl_low_texture2DLodEXT(_MainTex, tmpvar_31.xy, 0));
              float4 tmpvar_32;
              tmpvar_32.zw = float2(0, 0);
              tmpvar_32.xy = ((tmpvar_27 + float2(-1, 0)) / _ScreenResolution.xy);
              color_4 = (color_4 + impl_low_texture2DLodEXT(_MainTex, tmpvar_32.xy, 0));
              float4 tmpvar_33;
              tmpvar_33.zw = float2(0, 0);
              tmpvar_33.xy = ((tmpvar_27 + float2(1, (-1))) / _ScreenResolution.xy);
              color_4 = (color_4 + impl_low_texture2DLodEXT(_MainTex, tmpvar_33.xy, 0));
              float4 tmpvar_34;
              tmpvar_34.zw = float2(0, 0);
              tmpvar_34.xy = ((tmpvar_27 + float2(0, (-1))) / _ScreenResolution.xy);
              color_4 = (color_4 + impl_low_texture2DLodEXT(_MainTex, tmpvar_34.xy, 0));
              float4 tmpvar_35;
              tmpvar_35.zw = float2(0, 0);
              tmpvar_35.xy = ((tmpvar_27 + float2(-1, (-1))) / _ScreenResolution.xy);
              color_4 = (color_4 + impl_low_texture2DLodEXT(_MainTex, tmpvar_35.xy, 0));
              color_4 = (color_4 / 9);
              float _tmp_dvx_58 = (floor((7 * color_4)) / _ColorLevel);
              color_4 = float4(_tmp_dvx_58, _tmp_dvx_58, _tmp_dvx_58, _tmp_dvx_58);
              float4 tmpvar_36;
              float4 color_37;
              float4 tmpvar_38;
              tmpvar_38.zw = float2(0, 0);
              tmpvar_38.xy = ((tmpvar_27 + float2(1, 2)) / _ScreenResolution.xy);
              color_37 = impl_low_texture2DLodEXT(_MainTex, tmpvar_38.xy, 0);
              float4 tmpvar_39;
              tmpvar_39.zw = float2(0, 0);
              tmpvar_39.xy = ((tmpvar_27 + float2(0, 2)) / _ScreenResolution.xy);
              color_37 = (color_37 + impl_low_texture2DLodEXT(_MainTex, tmpvar_39.xy, 0));
              float4 tmpvar_40;
              tmpvar_40.zw = float2(0, 0);
              tmpvar_40.xy = ((tmpvar_27 + float2(-1, 2)) / _ScreenResolution.xy);
              color_37 = (color_37 + impl_low_texture2DLodEXT(_MainTex, tmpvar_40.xy, 0));
              float4 tmpvar_41;
              tmpvar_41.zw = float2(0, 0);
              tmpvar_41.xy = ((tmpvar_27 + float2(1, 1)) / _ScreenResolution.xy);
              color_37 = (color_37 + impl_low_texture2DLodEXT(_MainTex, tmpvar_41.xy, 0));
              float4 tmpvar_42;
              tmpvar_42.zw = float2(0, 0);
              tmpvar_42.xy = ((tmpvar_27 + float2(0, 1)) / _ScreenResolution.xy);
              color_37 = (color_37 + impl_low_texture2DLodEXT(_MainTex, tmpvar_42.xy, 0));
              float4 tmpvar_43;
              tmpvar_43.zw = float2(0, 0);
              tmpvar_43.xy = ((tmpvar_27 + float2(-1, 1)) / _ScreenResolution.xy);
              color_37 = (color_37 + impl_low_texture2DLodEXT(_MainTex, tmpvar_43.xy, 0));
              float4 tmpvar_44;
              tmpvar_44.zw = float2(0, 0);
              tmpvar_44.xy = ((tmpvar_27 + float2(1, 0)) / _ScreenResolution.xy);
              color_37 = (color_37 + impl_low_texture2DLodEXT(_MainTex, tmpvar_44.xy, 0));
              float4 tmpvar_45;
              tmpvar_45.zw = float2(0, 0);
              tmpvar_45.xy = (tmpvar_27 / _ScreenResolution.xy);
              color_37 = (color_37 + impl_low_texture2DLodEXT(_MainTex, tmpvar_45.xy, 0));
              float4 tmpvar_46;
              tmpvar_46.zw = float2(0, 0);
              tmpvar_46.xy = ((tmpvar_27 + float2(-1, 0)) / _ScreenResolution.xy);
              color_37 = (color_37 + impl_low_texture2DLodEXT(_MainTex, tmpvar_46.xy, 0));
              tmpvar_36 = (color_37 / 9);
              float4 tmpvar_47;
              float4 color_48;
              float4 tmpvar_49;
              tmpvar_49.zw = float2(0, 0);
              tmpvar_49.xy = ((tmpvar_27 + float2(1, 0)) / _ScreenResolution.xy);
              color_48 = impl_low_texture2DLodEXT(_MainTex, tmpvar_49.xy, 0);
              float4 tmpvar_50;
              tmpvar_50.zw = float2(0, 0);
              tmpvar_50.xy = (tmpvar_27 / _ScreenResolution.xy);
              color_48 = (color_48 + impl_low_texture2DLodEXT(_MainTex, tmpvar_50.xy, 0));
              float4 tmpvar_51;
              tmpvar_51.zw = float2(0, 0);
              tmpvar_51.xy = ((tmpvar_27 + float2(-1, 0)) / _ScreenResolution.xy);
              color_48 = (color_48 + impl_low_texture2DLodEXT(_MainTex, tmpvar_51.xy, 0));
              float4 tmpvar_52;
              tmpvar_52.zw = float2(0, 0);
              tmpvar_52.xy = ((tmpvar_27 + float2(1, (-1))) / _ScreenResolution.xy);
              color_48 = (color_48 + impl_low_texture2DLodEXT(_MainTex, tmpvar_52.xy, 0));
              float4 tmpvar_53;
              tmpvar_53.zw = float2(0, 0);
              tmpvar_53.xy = ((tmpvar_27 + float2(0, (-1))) / _ScreenResolution.xy);
              color_48 = (color_48 + impl_low_texture2DLodEXT(_MainTex, tmpvar_53.xy, 0));
              float4 tmpvar_54;
              tmpvar_54.zw = float2(0, 0);
              tmpvar_54.xy = ((tmpvar_27 + float2(-1, (-1))) / _ScreenResolution.xy);
              color_48 = (color_48 + impl_low_texture2DLodEXT(_MainTex, tmpvar_54.xy, 0));
              float4 tmpvar_55;
              tmpvar_55.zw = float2(0, 0);
              tmpvar_55.xy = ((tmpvar_27 + float2(1, (-2))) / _ScreenResolution.xy);
              color_48 = (color_48 + impl_low_texture2DLodEXT(_MainTex, tmpvar_55.xy, 0));
              float4 tmpvar_56;
              tmpvar_56.zw = float2(0, 0);
              tmpvar_56.xy = ((tmpvar_27 + float2(0, (-2))) / _ScreenResolution.xy);
              color_48 = (color_48 + impl_low_texture2DLodEXT(_MainTex, tmpvar_56.xy, 0));
              float4 tmpvar_57;
              tmpvar_57.zw = float2(0, 0);
              tmpvar_57.xy = ((tmpvar_27 + float2(-1, (-2))) / _ScreenResolution.xy);
              color_48 = (color_48 + impl_low_texture2DLodEXT(_MainTex, tmpvar_57.xy, 0));
              tmpvar_47 = (color_48 / 9);
              float4 tmpvar_58;
              tmpvar_58 = abs((tmpvar_36 - tmpvar_47));
              sum_3 = tmpvar_58;
              float4 tmpvar_59;
              float4 color_60;
              float4 tmpvar_61;
              tmpvar_61.zw = float2(0, 0);
              tmpvar_61.xy = ((tmpvar_27 + float2(2, 1)) / _ScreenResolution.xy);
              color_60 = impl_low_texture2DLodEXT(_MainTex, tmpvar_61.xy, 0);
              float4 tmpvar_62;
              tmpvar_62.zw = float2(0, 0);
              tmpvar_62.xy = ((tmpvar_27 + float2(1, 1)) / _ScreenResolution.xy);
              color_60 = (color_60 + impl_low_texture2DLodEXT(_MainTex, tmpvar_62.xy, 0));
              float4 tmpvar_63;
              tmpvar_63.zw = float2(0, 0);
              tmpvar_63.xy = ((tmpvar_27 + float2(0, 1)) / _ScreenResolution.xy);
              color_60 = (color_60 + impl_low_texture2DLodEXT(_MainTex, tmpvar_63.xy, 0));
              float4 tmpvar_64;
              tmpvar_64.zw = float2(0, 0);
              tmpvar_64.xy = ((tmpvar_27 + float2(2, 0)) / _ScreenResolution.xy);
              color_60 = (color_60 + impl_low_texture2DLodEXT(_MainTex, tmpvar_64.xy, 0));
              float4 tmpvar_65;
              tmpvar_65.zw = float2(0, 0);
              tmpvar_65.xy = ((tmpvar_27 + float2(1, 0)) / _ScreenResolution.xy);
              color_60 = (color_60 + impl_low_texture2DLodEXT(_MainTex, tmpvar_65.xy, 0));
              float4 tmpvar_66;
              tmpvar_66.zw = float2(0, 0);
              tmpvar_66.xy = (tmpvar_27 / _ScreenResolution.xy);
              color_60 = (color_60 + impl_low_texture2DLodEXT(_MainTex, tmpvar_66.xy, 0));
              float4 tmpvar_67;
              tmpvar_67.zw = float2(0, 0);
              tmpvar_67.xy = ((tmpvar_27 + float2(2, (-1))) / _ScreenResolution.xy);
              color_60 = (color_60 + impl_low_texture2DLodEXT(_MainTex, tmpvar_67.xy, 0));
              float4 tmpvar_68;
              tmpvar_68.zw = float2(0, 0);
              tmpvar_68.xy = ((tmpvar_27 + float2(1, (-1))) / _ScreenResolution.xy);
              color_60 = (color_60 + impl_low_texture2DLodEXT(_MainTex, tmpvar_68.xy, 0));
              float4 tmpvar_69;
              tmpvar_69.zw = float2(0, 0);
              tmpvar_69.xy = ((tmpvar_27 + float2(0, (-1))) / _ScreenResolution.xy);
              color_60 = (color_60 + impl_low_texture2DLodEXT(_MainTex, tmpvar_69.xy, 0));
              tmpvar_59 = (color_60 / 9);
              float4 tmpvar_70;
              float4 color_71;
              float4 tmpvar_72;
              tmpvar_72.zw = float2(0, 0);
              tmpvar_72.xy = ((tmpvar_27 + float2(0, 1)) / _ScreenResolution.xy);
              color_71 = impl_low_texture2DLodEXT(_MainTex, tmpvar_72.xy, 0);
              float4 tmpvar_73;
              tmpvar_73.zw = float2(0, 0);
              tmpvar_73.xy = ((tmpvar_27 + float2(-1, 1)) / _ScreenResolution.xy);
              color_71 = (color_71 + impl_low_texture2DLodEXT(_MainTex, tmpvar_73.xy, 0));
              float4 tmpvar_74;
              tmpvar_74.zw = float2(0, 0);
              tmpvar_74.xy = ((tmpvar_27 + float2(-2, 1)) / _ScreenResolution.xy);
              color_71 = (color_71 + impl_low_texture2DLodEXT(_MainTex, tmpvar_74.xy, 0));
              float4 tmpvar_75;
              tmpvar_75.zw = float2(0, 0);
              tmpvar_75.xy = (tmpvar_27 / _ScreenResolution.xy);
              color_71 = (color_71 + impl_low_texture2DLodEXT(_MainTex, tmpvar_75.xy, 0));
              float4 tmpvar_76;
              tmpvar_76.zw = float2(0, 0);
              tmpvar_76.xy = ((tmpvar_27 + float2(-1, 0)) / _ScreenResolution.xy);
              color_71 = (color_71 + impl_low_texture2DLodEXT(_MainTex, tmpvar_76.xy, 0));
              float4 tmpvar_77;
              tmpvar_77.zw = float2(0, 0);
              tmpvar_77.xy = ((tmpvar_27 + float2(-2, 0)) / _ScreenResolution.xy);
              color_71 = (color_71 + impl_low_texture2DLodEXT(_MainTex, tmpvar_77.xy, 0));
              float4 tmpvar_78;
              tmpvar_78.zw = float2(0, 0);
              tmpvar_78.xy = ((tmpvar_27 + float2(0, (-1))) / _ScreenResolution.xy);
              color_71 = (color_71 + impl_low_texture2DLodEXT(_MainTex, tmpvar_78.xy, 0));
              float4 tmpvar_79;
              tmpvar_79.zw = float2(0, 0);
              tmpvar_79.xy = ((tmpvar_27 + float2(-1, (-1))) / _ScreenResolution.xy);
              color_71 = (color_71 + impl_low_texture2DLodEXT(_MainTex, tmpvar_79.xy, 0));
              float4 tmpvar_80;
              tmpvar_80.zw = float2(0, 0);
              tmpvar_80.xy = ((tmpvar_27 + float2(-2, (-1))) / _ScreenResolution.xy);
              color_71 = (color_71 + impl_low_texture2DLodEXT(_MainTex, tmpvar_80.xy, 0));
              tmpvar_70 = (color_71 / 9);
              float4 tmpvar_81;
              tmpvar_81 = abs((tmpvar_59 - tmpvar_70));
              sum_3 = (sum_3 + tmpvar_81);
              sum_3 = (sum_3 / 2);
              float4 tmpvar_82;
              float4 y_83;
              y_83 = (rgbx_5 * 2);
              tmpvar_82 = lerp(color_4, y_83, float4(_Distortion, _Distortion, _Distortion, _Distortion));
              color_4 = tmpvar_82;
              float tmpvar_84;
              tmpvar_84 = (_EdgeSize + 0.05);
              float tmpvar_85;
              tmpvar_85 = sqrt(dot(sum_3, sum_3));
              if((tmpvar_85>tmpvar_84))
              {
                  color_4.xyz = float3(0, 0, 0);
              }
              tmpvar_1 = color_4;
              out_f.color = tmpvar_1;
          }
      
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
