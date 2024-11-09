Shader "CameraFilterPack/Drawing_CellShading"
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
              float4 sum_2;
              float4 color_3;
              float4 tmpvar_4;
              tmpvar_4.zw = float2(0, 0);
              float2 tmpvar_5;
              tmpvar_5 = (in_f.xlv_TEXCOORD0 * _ScreenResolution.xy);
              tmpvar_4.xy = ((tmpvar_5 + float2(1, 1)) / _ScreenResolution.xy);
              color_3 = impl_low_texture2DLodEXT(_MainTex, tmpvar_4.xy, 0);
              float4 tmpvar_6;
              tmpvar_6.zw = float2(0, 0);
              tmpvar_6.xy = ((tmpvar_5 + float2(0, 1)) / _ScreenResolution.xy);
              color_3 = (color_3 + impl_low_texture2DLodEXT(_MainTex, tmpvar_6.xy, 0));
              float4 tmpvar_7;
              tmpvar_7.zw = float2(0, 0);
              tmpvar_7.xy = ((tmpvar_5 + float2(-1, 1)) / _ScreenResolution.xy);
              color_3 = (color_3 + impl_low_texture2DLodEXT(_MainTex, tmpvar_7.xy, 0));
              float4 tmpvar_8;
              tmpvar_8.zw = float2(0, 0);
              tmpvar_8.xy = ((tmpvar_5 + float2(1, 0)) / _ScreenResolution.xy);
              color_3 = (color_3 + impl_low_texture2DLodEXT(_MainTex, tmpvar_8.xy, 0));
              float4 tmpvar_9;
              tmpvar_9.zw = float2(0, 0);
              tmpvar_9.xy = (tmpvar_5 / _ScreenResolution.xy);
              color_3 = (color_3 + impl_low_texture2DLodEXT(_MainTex, tmpvar_9.xy, 0));
              float4 tmpvar_10;
              tmpvar_10.zw = float2(0, 0);
              tmpvar_10.xy = ((tmpvar_5 + float2(-1, 0)) / _ScreenResolution.xy);
              color_3 = (color_3 + impl_low_texture2DLodEXT(_MainTex, tmpvar_10.xy, 0));
              float4 tmpvar_11;
              tmpvar_11.zw = float2(0, 0);
              tmpvar_11.xy = ((tmpvar_5 + float2(1, (-1))) / _ScreenResolution.xy);
              color_3 = (color_3 + impl_low_texture2DLodEXT(_MainTex, tmpvar_11.xy, 0));
              float4 tmpvar_12;
              tmpvar_12.zw = float2(0, 0);
              tmpvar_12.xy = ((tmpvar_5 + float2(0, (-1))) / _ScreenResolution.xy);
              color_3 = (color_3 + impl_low_texture2DLodEXT(_MainTex, tmpvar_12.xy, 0));
              float4 tmpvar_13;
              tmpvar_13.zw = float2(0, 0);
              tmpvar_13.xy = ((tmpvar_5 + float2(-1, (-1))) / _ScreenResolution.xy);
              color_3 = (color_3 + impl_low_texture2DLodEXT(_MainTex, tmpvar_13.xy, 0));
              color_3 = (color_3 / 9);
              float4 tmpvar_14;
              tmpvar_14.yzw = color_3.yzw;
              tmpvar_14.x = (floor((7 * color_3.x)) / _ColorLevel);
              float4 tmpvar_15;
              tmpvar_15.xzw = tmpvar_14.xzw;
              tmpvar_15.y = (floor((7 * color_3.y)) / _ColorLevel);
              float4 tmpvar_16;
              tmpvar_16.xyw = tmpvar_15.xyw;
              tmpvar_16.z = (floor((7 * color_3.z)) / _ColorLevel);
              color_3 = tmpvar_16;
              float4 tmpvar_17;
              float4 color_18;
              float4 tmpvar_19;
              tmpvar_19.zw = float2(0, 0);
              tmpvar_19.xy = ((tmpvar_5 + float2(1, 2)) / _ScreenResolution.xy);
              color_18 = impl_low_texture2DLodEXT(_MainTex, tmpvar_19.xy, 0);
              float4 tmpvar_20;
              tmpvar_20.zw = float2(0, 0);
              tmpvar_20.xy = ((tmpvar_5 + float2(0, 2)) / _ScreenResolution.xy);
              color_18 = (color_18 + impl_low_texture2DLodEXT(_MainTex, tmpvar_20.xy, 0));
              float4 tmpvar_21;
              tmpvar_21.zw = float2(0, 0);
              tmpvar_21.xy = ((tmpvar_5 + float2(-1, 2)) / _ScreenResolution.xy);
              color_18 = (color_18 + impl_low_texture2DLodEXT(_MainTex, tmpvar_21.xy, 0));
              float4 tmpvar_22;
              tmpvar_22.zw = float2(0, 0);
              tmpvar_22.xy = ((tmpvar_5 + float2(1, 1)) / _ScreenResolution.xy);
              color_18 = (color_18 + impl_low_texture2DLodEXT(_MainTex, tmpvar_22.xy, 0));
              float4 tmpvar_23;
              tmpvar_23.zw = float2(0, 0);
              tmpvar_23.xy = ((tmpvar_5 + float2(0, 1)) / _ScreenResolution.xy);
              color_18 = (color_18 + impl_low_texture2DLodEXT(_MainTex, tmpvar_23.xy, 0));
              float4 tmpvar_24;
              tmpvar_24.zw = float2(0, 0);
              tmpvar_24.xy = ((tmpvar_5 + float2(-1, 1)) / _ScreenResolution.xy);
              color_18 = (color_18 + impl_low_texture2DLodEXT(_MainTex, tmpvar_24.xy, 0));
              float4 tmpvar_25;
              tmpvar_25.zw = float2(0, 0);
              tmpvar_25.xy = ((tmpvar_5 + float2(1, 0)) / _ScreenResolution.xy);
              color_18 = (color_18 + impl_low_texture2DLodEXT(_MainTex, tmpvar_25.xy, 0));
              float4 tmpvar_26;
              tmpvar_26.zw = float2(0, 0);
              tmpvar_26.xy = (tmpvar_5 / _ScreenResolution.xy);
              color_18 = (color_18 + impl_low_texture2DLodEXT(_MainTex, tmpvar_26.xy, 0));
              float4 tmpvar_27;
              tmpvar_27.zw = float2(0, 0);
              tmpvar_27.xy = ((tmpvar_5 + float2(-1, 0)) / _ScreenResolution.xy);
              color_18 = (color_18 + impl_low_texture2DLodEXT(_MainTex, tmpvar_27.xy, 0));
              tmpvar_17 = (color_18 / 9);
              float4 tmpvar_28;
              float4 color_29;
              float4 tmpvar_30;
              tmpvar_30.zw = float2(0, 0);
              tmpvar_30.xy = ((tmpvar_5 + float2(1, 0)) / _ScreenResolution.xy);
              color_29 = impl_low_texture2DLodEXT(_MainTex, tmpvar_30.xy, 0);
              float4 tmpvar_31;
              tmpvar_31.zw = float2(0, 0);
              tmpvar_31.xy = (tmpvar_5 / _ScreenResolution.xy);
              color_29 = (color_29 + impl_low_texture2DLodEXT(_MainTex, tmpvar_31.xy, 0));
              float4 tmpvar_32;
              tmpvar_32.zw = float2(0, 0);
              tmpvar_32.xy = ((tmpvar_5 + float2(-1, 0)) / _ScreenResolution.xy);
              color_29 = (color_29 + impl_low_texture2DLodEXT(_MainTex, tmpvar_32.xy, 0));
              float4 tmpvar_33;
              tmpvar_33.zw = float2(0, 0);
              tmpvar_33.xy = ((tmpvar_5 + float2(1, (-1))) / _ScreenResolution.xy);
              color_29 = (color_29 + impl_low_texture2DLodEXT(_MainTex, tmpvar_33.xy, 0));
              float4 tmpvar_34;
              tmpvar_34.zw = float2(0, 0);
              tmpvar_34.xy = ((tmpvar_5 + float2(0, (-1))) / _ScreenResolution.xy);
              color_29 = (color_29 + impl_low_texture2DLodEXT(_MainTex, tmpvar_34.xy, 0));
              float4 tmpvar_35;
              tmpvar_35.zw = float2(0, 0);
              tmpvar_35.xy = ((tmpvar_5 + float2(-1, (-1))) / _ScreenResolution.xy);
              color_29 = (color_29 + impl_low_texture2DLodEXT(_MainTex, tmpvar_35.xy, 0));
              float4 tmpvar_36;
              tmpvar_36.zw = float2(0, 0);
              tmpvar_36.xy = ((tmpvar_5 + float2(1, (-2))) / _ScreenResolution.xy);
              color_29 = (color_29 + impl_low_texture2DLodEXT(_MainTex, tmpvar_36.xy, 0));
              float4 tmpvar_37;
              tmpvar_37.zw = float2(0, 0);
              tmpvar_37.xy = ((tmpvar_5 + float2(0, (-2))) / _ScreenResolution.xy);
              color_29 = (color_29 + impl_low_texture2DLodEXT(_MainTex, tmpvar_37.xy, 0));
              float4 tmpvar_38;
              tmpvar_38.zw = float2(0, 0);
              tmpvar_38.xy = ((tmpvar_5 + float2(-1, (-2))) / _ScreenResolution.xy);
              color_29 = (color_29 + impl_low_texture2DLodEXT(_MainTex, tmpvar_38.xy, 0));
              tmpvar_28 = (color_29 / 9);
              float4 tmpvar_39;
              tmpvar_39 = abs((tmpvar_17 - tmpvar_28));
              sum_2 = tmpvar_39;
              float4 tmpvar_40;
              float4 color_41;
              float4 tmpvar_42;
              tmpvar_42.zw = float2(0, 0);
              tmpvar_42.xy = ((tmpvar_5 + float2(2, 1)) / _ScreenResolution.xy);
              color_41 = impl_low_texture2DLodEXT(_MainTex, tmpvar_42.xy, 0);
              float4 tmpvar_43;
              tmpvar_43.zw = float2(0, 0);
              tmpvar_43.xy = ((tmpvar_5 + float2(1, 1)) / _ScreenResolution.xy);
              color_41 = (color_41 + impl_low_texture2DLodEXT(_MainTex, tmpvar_43.xy, 0));
              float4 tmpvar_44;
              tmpvar_44.zw = float2(0, 0);
              tmpvar_44.xy = ((tmpvar_5 + float2(0, 1)) / _ScreenResolution.xy);
              color_41 = (color_41 + impl_low_texture2DLodEXT(_MainTex, tmpvar_44.xy, 0));
              float4 tmpvar_45;
              tmpvar_45.zw = float2(0, 0);
              tmpvar_45.xy = ((tmpvar_5 + float2(2, 0)) / _ScreenResolution.xy);
              color_41 = (color_41 + impl_low_texture2DLodEXT(_MainTex, tmpvar_45.xy, 0));
              float4 tmpvar_46;
              tmpvar_46.zw = float2(0, 0);
              tmpvar_46.xy = ((tmpvar_5 + float2(1, 0)) / _ScreenResolution.xy);
              color_41 = (color_41 + impl_low_texture2DLodEXT(_MainTex, tmpvar_46.xy, 0));
              float4 tmpvar_47;
              tmpvar_47.zw = float2(0, 0);
              tmpvar_47.xy = (tmpvar_5 / _ScreenResolution.xy);
              color_41 = (color_41 + impl_low_texture2DLodEXT(_MainTex, tmpvar_47.xy, 0));
              float4 tmpvar_48;
              tmpvar_48.zw = float2(0, 0);
              tmpvar_48.xy = ((tmpvar_5 + float2(2, (-1))) / _ScreenResolution.xy);
              color_41 = (color_41 + impl_low_texture2DLodEXT(_MainTex, tmpvar_48.xy, 0));
              float4 tmpvar_49;
              tmpvar_49.zw = float2(0, 0);
              tmpvar_49.xy = ((tmpvar_5 + float2(1, (-1))) / _ScreenResolution.xy);
              color_41 = (color_41 + impl_low_texture2DLodEXT(_MainTex, tmpvar_49.xy, 0));
              float4 tmpvar_50;
              tmpvar_50.zw = float2(0, 0);
              tmpvar_50.xy = ((tmpvar_5 + float2(0, (-1))) / _ScreenResolution.xy);
              color_41 = (color_41 + impl_low_texture2DLodEXT(_MainTex, tmpvar_50.xy, 0));
              tmpvar_40 = (color_41 / 9);
              float4 tmpvar_51;
              float4 color_52;
              float4 tmpvar_53;
              tmpvar_53.zw = float2(0, 0);
              tmpvar_53.xy = ((tmpvar_5 + float2(0, 1)) / _ScreenResolution.xy);
              color_52 = impl_low_texture2DLodEXT(_MainTex, tmpvar_53.xy, 0);
              float4 tmpvar_54;
              tmpvar_54.zw = float2(0, 0);
              tmpvar_54.xy = ((tmpvar_5 + float2(-1, 1)) / _ScreenResolution.xy);
              color_52 = (color_52 + impl_low_texture2DLodEXT(_MainTex, tmpvar_54.xy, 0));
              float4 tmpvar_55;
              tmpvar_55.zw = float2(0, 0);
              tmpvar_55.xy = ((tmpvar_5 + float2(-2, 1)) / _ScreenResolution.xy);
              color_52 = (color_52 + impl_low_texture2DLodEXT(_MainTex, tmpvar_55.xy, 0));
              float4 tmpvar_56;
              tmpvar_56.zw = float2(0, 0);
              tmpvar_56.xy = (tmpvar_5 / _ScreenResolution.xy);
              color_52 = (color_52 + impl_low_texture2DLodEXT(_MainTex, tmpvar_56.xy, 0));
              float4 tmpvar_57;
              tmpvar_57.zw = float2(0, 0);
              tmpvar_57.xy = ((tmpvar_5 + float2(-1, 0)) / _ScreenResolution.xy);
              color_52 = (color_52 + impl_low_texture2DLodEXT(_MainTex, tmpvar_57.xy, 0));
              float4 tmpvar_58;
              tmpvar_58.zw = float2(0, 0);
              tmpvar_58.xy = ((tmpvar_5 + float2(-2, 0)) / _ScreenResolution.xy);
              color_52 = (color_52 + impl_low_texture2DLodEXT(_MainTex, tmpvar_58.xy, 0));
              float4 tmpvar_59;
              tmpvar_59.zw = float2(0, 0);
              tmpvar_59.xy = ((tmpvar_5 + float2(0, (-1))) / _ScreenResolution.xy);
              color_52 = (color_52 + impl_low_texture2DLodEXT(_MainTex, tmpvar_59.xy, 0));
              float4 tmpvar_60;
              tmpvar_60.zw = float2(0, 0);
              tmpvar_60.xy = ((tmpvar_5 + float2(-1, (-1))) / _ScreenResolution.xy);
              color_52 = (color_52 + impl_low_texture2DLodEXT(_MainTex, tmpvar_60.xy, 0));
              float4 tmpvar_61;
              tmpvar_61.zw = float2(0, 0);
              tmpvar_61.xy = ((tmpvar_5 + float2(-2, (-1))) / _ScreenResolution.xy);
              color_52 = (color_52 + impl_low_texture2DLodEXT(_MainTex, tmpvar_61.xy, 0));
              tmpvar_51 = (color_52 / 9);
              float4 tmpvar_62;
              tmpvar_62 = abs((tmpvar_40 - tmpvar_51));
              sum_2 = (sum_2 + tmpvar_62);
              sum_2 = (sum_2 / 2);
              float tmpvar_63;
              tmpvar_63 = (_EdgeSize + 0.05);
              float tmpvar_64;
              tmpvar_64 = sqrt(dot(sum_2, sum_2));
              if((tmpvar_64>tmpvar_63))
              {
                  color_3.xyz = float3(0, 0, 0);
              }
              tmpvar_1 = color_3;
              out_f.color = tmpvar_1;
          }
      
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
