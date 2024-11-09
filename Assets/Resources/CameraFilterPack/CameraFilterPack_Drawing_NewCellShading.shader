Shader "CameraFilterPack/Drawing_NewCellShading"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _TimeX ("Time", Range(0, 1)) = 1
    _Distortion ("_Distortion", Range(0, 1)) = 0.3
    _ScreenResolution ("_ScreenResolution", Vector) = (0,0,0,0)
    _Threshold ("_Threshold", Range(0, 1)) = 0
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
      uniform float _Threshold;
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
              float4 color_1;
              color_1 = float4(0, 0, 0, 0);
              float tmpvar_2;
              tmpvar_2 = (in_f.xlv_TEXCOORD0.x * _ScreenResolution.x);
              float tmpvar_3;
              tmpvar_3 = (in_f.xlv_TEXCOORD0.y * _ScreenResolution.y);
              float tmpvar_4;
              float2 tmpvar_5;
              tmpvar_5.x = (tmpvar_2 - 1);
              tmpvar_5.y = (tmpvar_3 - 1);
              float4 tmpvar_6;
              tmpvar_6.zw = float2(0, 0);
              tmpvar_6.xy = (tmpvar_5 / _ScreenResolution.xy);
              float4 tmpvar_7;
              tmpvar_7 = impl_low_texture2DLodEXT(_MainTex, tmpvar_6.xy, 0);
              tmpvar_4 = tmpvar_7.x;
              float tmpvar_8;
              float2 tmpvar_9;
              tmpvar_9.x = (tmpvar_2 - 1);
              tmpvar_9.y = tmpvar_3;
              float4 tmpvar_10;
              tmpvar_10.zw = float2(0, 0);
              tmpvar_10.xy = (tmpvar_9 / _ScreenResolution.xy);
              float4 tmpvar_11;
              tmpvar_11 = impl_low_texture2DLodEXT(_MainTex, tmpvar_10.xy, 0);
              tmpvar_8 = tmpvar_11.x;
              float tmpvar_12;
              float2 tmpvar_13;
              tmpvar_13.x = (tmpvar_2 - 1);
              tmpvar_13.y = (tmpvar_3 + 1);
              float4 tmpvar_14;
              tmpvar_14.zw = float2(0, 0);
              tmpvar_14.xy = (tmpvar_13 / _ScreenResolution.xy);
              float4 tmpvar_15;
              tmpvar_15 = impl_low_texture2DLodEXT(_MainTex, tmpvar_14.xy, 0);
              tmpvar_12 = tmpvar_15.x;
              float tmpvar_16;
              float2 tmpvar_17;
              tmpvar_17.x = (tmpvar_2 + 1);
              tmpvar_17.y = (tmpvar_3 - 1);
              float4 tmpvar_18;
              tmpvar_18.zw = float2(0, 0);
              tmpvar_18.xy = (tmpvar_17 / _ScreenResolution.xy);
              float4 tmpvar_19;
              tmpvar_19 = impl_low_texture2DLodEXT(_MainTex, tmpvar_18.xy, 0);
              tmpvar_16 = tmpvar_19.x;
              float tmpvar_20;
              float2 tmpvar_21;
              tmpvar_21.x = (tmpvar_2 + 1);
              tmpvar_21.y = tmpvar_3;
              float4 tmpvar_22;
              tmpvar_22.zw = float2(0, 0);
              tmpvar_22.xy = (tmpvar_21 / _ScreenResolution.xy);
              float4 tmpvar_23;
              tmpvar_23 = impl_low_texture2DLodEXT(_MainTex, tmpvar_22.xy, 0);
              tmpvar_20 = tmpvar_23.x;
              float tmpvar_24;
              float2 tmpvar_25;
              tmpvar_25.x = (tmpvar_2 + 1);
              tmpvar_25.y = (tmpvar_3 + 1);
              float4 tmpvar_26;
              tmpvar_26.zw = float2(0, 0);
              tmpvar_26.xy = (tmpvar_25 / _ScreenResolution.xy);
              float4 tmpvar_27;
              tmpvar_27 = impl_low_texture2DLodEXT(_MainTex, tmpvar_26.xy, 0);
              tmpvar_24 = tmpvar_27.x;
              float tmpvar_28;
              float2 tmpvar_29;
              tmpvar_29.x = (tmpvar_2 - 1);
              tmpvar_29.y = (tmpvar_3 - 1);
              float4 tmpvar_30;
              tmpvar_30.zw = float2(0, 0);
              tmpvar_30.xy = (tmpvar_29 / _ScreenResolution.xy);
              float4 tmpvar_31;
              tmpvar_31 = impl_low_texture2DLodEXT(_MainTex, tmpvar_30.xy, 0);
              tmpvar_28 = tmpvar_31.x;
              float tmpvar_32;
              float2 tmpvar_33;
              tmpvar_33.x = tmpvar_2;
              tmpvar_33.y = (tmpvar_3 - 1);
              float4 tmpvar_34;
              tmpvar_34.zw = float2(0, 0);
              tmpvar_34.xy = (tmpvar_33 / _ScreenResolution.xy);
              float4 tmpvar_35;
              tmpvar_35 = impl_low_texture2DLodEXT(_MainTex, tmpvar_34.xy, 0);
              tmpvar_32 = tmpvar_35.x;
              float tmpvar_36;
              float2 tmpvar_37;
              tmpvar_37.x = (tmpvar_2 + 1);
              tmpvar_37.y = (tmpvar_3 - 1);
              float4 tmpvar_38;
              tmpvar_38.zw = float2(0, 0);
              tmpvar_38.xy = (tmpvar_37 / _ScreenResolution.xy);
              float4 tmpvar_39;
              tmpvar_39 = impl_low_texture2DLodEXT(_MainTex, tmpvar_38.xy, 0);
              tmpvar_36 = tmpvar_39.x;
              float tmpvar_40;
              float2 tmpvar_41;
              tmpvar_41.x = (tmpvar_2 - 1);
              tmpvar_41.y = (tmpvar_3 + 1);
              float4 tmpvar_42;
              tmpvar_42.zw = float2(0, 0);
              tmpvar_42.xy = (tmpvar_41 / _ScreenResolution.xy);
              float4 tmpvar_43;
              tmpvar_43 = impl_low_texture2DLodEXT(_MainTex, tmpvar_42.xy, 0);
              tmpvar_40 = tmpvar_43.x;
              float tmpvar_44;
              float2 tmpvar_45;
              tmpvar_45.x = tmpvar_2;
              tmpvar_45.y = (tmpvar_3 + 1);
              float4 tmpvar_46;
              tmpvar_46.zw = float2(0, 0);
              tmpvar_46.xy = (tmpvar_45 / _ScreenResolution.xy);
              float4 tmpvar_47;
              tmpvar_47 = impl_low_texture2DLodEXT(_MainTex, tmpvar_46.xy, 0);
              tmpvar_44 = tmpvar_47.x;
              float tmpvar_48;
              float2 tmpvar_49;
              tmpvar_49.x = (tmpvar_2 + 1);
              tmpvar_49.y = (tmpvar_3 + 1);
              float4 tmpvar_50;
              tmpvar_50.zw = float2(0, 0);
              tmpvar_50.xy = (tmpvar_49 / _ScreenResolution.xy);
              float4 tmpvar_51;
              tmpvar_51 = impl_low_texture2DLodEXT(_MainTex, tmpvar_50.xy, 0);
              tmpvar_48 = tmpvar_51.x;
              float2 tmpvar_52;
              tmpvar_52.x = ((((((-tmpvar_4) - (2 * tmpvar_8)) - tmpvar_12) + tmpvar_16) + (2 * tmpvar_20)) + tmpvar_24);
              tmpvar_52.y = (((((tmpvar_28 + (2 * tmpvar_32)) + tmpvar_36) - tmpvar_40) - (2 * tmpvar_44)) - tmpvar_48);
              float tmpvar_53;
              tmpvar_53 = sqrt(dot(tmpvar_52, tmpvar_52));
              if((tmpvar_53>_Threshold))
              {
                  color_1 = float4(0, 0, 0, 0);
              }
              else
              {
                  float2 tmpvar_54;
                  tmpvar_54.x = tmpvar_2;
                  tmpvar_54.y = tmpvar_3;
                  float4 tmpvar_55;
                  tmpvar_55.zw = float2(0, 0);
                  tmpvar_55.xy = (tmpvar_54 / _ScreenResolution.xy);
                  color_1 = impl_low_texture2DLodEXT(_MainTex, tmpvar_55.xy, 0);
              }
              out_f.color = color_1;
          }
      
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
