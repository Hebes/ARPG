Shader "CameraFilterPack/Edge_BlackLine"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _TimeX ("Time", Range(0, 1)) = 1
    _Distortion ("_Distortion", Range(0, 1)) = 0.3
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
          float4 color_3;
          float xlat_varfilter_5[25];
          float2 uv_6;
          uv_6 = in_f.xlv_TEXCOORD0;
          int a_4 = 0;
          while((a_4<25))
          {
              xlat_varfilter_5[a_4] = (-1);
              a_4 = (a_4 + 1);
          }
          xlat_varfilter_5[12] = 24;
          color_3 = float4(0, 0, 0, 0);
          int a_1_2 = 0;
          while((a_1_2<5))
          {
              float2 tmpvar_7;
              tmpvar_7.x = float((a_1_2 - 2));
              tmpvar_7.y = (-2);
              float2 P_8;
              P_8 = (uv_6 + (tmpvar_7 / 100));
              color_3 = (color_3 + (xlat_varfilter_5[(a_1_2 * 5)] * tex2D(_MainTex, P_8)));
              float2 tmpvar_9;
              tmpvar_9.x = float((a_1_2 - 2));
              tmpvar_9.y = (-1);
              float2 P_10;
              P_10 = (uv_6 + (tmpvar_9 / 100));
              color_3 = (color_3 + (xlat_varfilter_5[((a_1_2 * 5) + 1)] * tex2D(_MainTex, P_10)));
              float2 tmpvar_11;
              tmpvar_11.x = float((a_1_2 - 2));
              tmpvar_11.y = 0;
              float2 P_12;
              P_12 = (uv_6 + (tmpvar_11 / 100));
              color_3 = (color_3 + (xlat_varfilter_5[((a_1_2 * 5) + 2)] * tex2D(_MainTex, P_12)));
              float2 tmpvar_13;
              tmpvar_13.x = float((a_1_2 - 2));
              tmpvar_13.y = 1;
              float2 P_14;
              P_14 = (uv_6 + (tmpvar_13 / 100));
              color_3 = (color_3 + (xlat_varfilter_5[((a_1_2 * 5) + 3)] * tex2D(_MainTex, P_14)));
              float2 tmpvar_15;
              tmpvar_15.x = float((a_1_2 - 2));
              tmpvar_15.y = 2;
              float2 P_16;
              P_16 = (uv_6 + (tmpvar_15 / 100));
              color_3 = (color_3 + (xlat_varfilter_5[((a_1_2 * 5) + 4)] * tex2D(_MainTex, P_16)));
              a_1_2 = (a_1_2 + 1);
          }
          if(((((color_3.x + color_3.y) + color_3.z) / 3)<0.8))
          {
              color_3 = float4(0, 0, 0, 0);
          }
          tmpvar_1 = color_3;
          out_f.color = tmpvar_1;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
