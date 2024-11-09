Shader "CameraFilterPack/Oculus_NightVision1"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _TimeX ("Time", Range(0, 1)) = 1
    _Distortion ("_Distortion", Range(1, 10)) = 1
    _ScreenResolution ("_ScreenResolution", Vector) = (0,0,0,0)
    _Vignette ("_Vignette", Range(0, 100)) = 1.5
    _Linecount ("_Linecount", Range(1, 150)) = 90
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
      uniform float _Vignette;
      uniform float _Linecount;
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
      float2 xlat_mutablepos;
      float2 xlat_mutableuv;
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          xlat_mutableuv = in_f.xlv_TEXCOORD0;
          xlat_mutablepos = in_f.xlv_TEXCOORD0;
          xlat_mutableuv.y = (floor((xlat_mutableuv.y * _Linecount)) / _Linecount);
          float lum_1;
          float4 tmpvar_2;
          float4 v_3;
          float4 tmpvar_4;
          float2 P_5;
          float2 tmpvar_6;
          tmpvar_6 = (_TimeX * float2(9, 7));
          P_5 = (xlat_mutableuv + tmpvar_6);
          tmpvar_4 = tex2D(_MainTex, P_5);
          v_3 = tmpvar_4;
          float4 tmpvar_7;
          float2 P_8;
          P_8 = (xlat_mutableuv + (float2(0.01, 0) * v_3.x));
          tmpvar_7 = tex2D(_MainTex, P_8);
          tmpvar_2 = tmpvar_7;
          float4 v_9;
          float4 tmpvar_10;
          float2 P_11;
          P_11 = (xlat_mutableuv + tmpvar_6);
          tmpvar_10 = tex2D(_MainTex, P_11);
          v_9 = tmpvar_10;
          lum_1 = ((((0.2 * tmpvar_2.x) + (0.5 * tmpvar_2.y)) + (0.3 * tmpvar_2.z)) * ((0.3 * v_9.x) + 0.7));
          float phase_12;
          phase_12 = (xlat_mutablepos.y * _Linecount);
          phase_12 = (phase_12 * 2);
          float tmpvar_13;
          tmpvar_13 = (phase_12 / 2);
          float tmpvar_14;
          tmpvar_14 = (frac(abs(tmpvar_13)) * 2);
          float tmpvar_15;
          if((tmpvar_13>=0))
          {
              tmpvar_15 = tmpvar_14;
          }
          else
          {
              tmpvar_15 = (-tmpvar_14);
          }
          float i_16;
          i_16 = (pow((1 - abs((tmpvar_15 - 1))), ((2 * (1 - lum_1)) + 0.5)) * lum_1);
          float4 tmpvar_17;
          i_16 = (clamp(i_16, 0, 1) * 2);
          if((i_16<1))
          {
              float _tmp_dvx_87 = (((1 - i_16) * float4(0, 0.1, 0, 1)) + (i_16 * float4(0.2, 0.5, 0.1, 1)));
              tmpvar_17 = float4(_tmp_dvx_87, _tmp_dvx_87, _tmp_dvx_87, _tmp_dvx_87);
          }
          else
          {
              i_16 = (i_16 - 1);
              float _tmp_dvx_88 = (((1 - i_16) * float4(0.2, 0.5, 0.1, 1)) + (i_16 * float4(0.9, 1, 0.6, 1)));
              tmpvar_17 = float4(_tmp_dvx_88, _tmp_dvx_88, _tmp_dvx_88, _tmp_dvx_88);
          }
          float tmpvar_18;
          tmpvar_18 = (_Vignette * abs((xlat_mutablepos.x - 0.5)));
          float tmpvar_19;
          tmpvar_19 = (_Vignette * abs((xlat_mutablepos.y - 0.5)));
          float4 tmpvar_20;
          tmpvar_20 = (tmpvar_17 * ((1 - (tmpvar_18 * tmpvar_18)) - (tmpvar_19 * tmpvar_19)));
          out_f.color = tmpvar_20;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
