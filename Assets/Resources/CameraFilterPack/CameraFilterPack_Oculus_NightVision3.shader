Shader "CameraFilterPack/Oculus_NightVision3"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _TimeX ("Time", Range(0, 1)) = 1
    _Distortion ("_Distortion", Range(0, 1)) = 0.3
    _ScreenResolution ("_ScreenResolution", Vector) = (0,0,0,0)
    _BinocularSize ("_BinocularSize", Range(0, 1)) = 0.5
    _BinocularDistance ("_BinocularDistance", Range(0, 1)) = 0.5
    _Greenness ("_Greenness", Range(0, 1)) = 0.4
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
      uniform float _Greenness;
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
          float2 uv_2;
          float2 tmpvar_3;
          tmpvar_3 = (0.5 + (in_f.xlv_TEXCOORD0 - 0.5));
          uv_2 = tmpvar_3;
          float3 video_4;
          float2 look_5;
          look_5.y = uv_2.y;
          look_5.x = (uv_2.x + (((sin(((uv_2.y * 20) + _TimeX)) / 250) * float((sin((_TimeX + (2 * cos((_TimeX * 2)))))>=0.9))) * (1 + cos((_TimeX * 80)))));
          float3 tmpvar_6;
          tmpvar_6 = tex2D(_MainTex, look_5).xyz;
          video_4 = tmpvar_6;
          float xlat_varsample_7;
          float2 tmpvar_8;
          tmpvar_8.x = 1;
          tmpvar_8.y = (2 * cos(_TimeX));
          float2 P_9;
          P_9 = (((tmpvar_8 * _TimeX) * 8) + ((uv_2 * float2(0.5, 1)) + float2(1, 3)));
          float tmpvar_10;
          tmpvar_10 = tex2D(_MainTex, P_9).x;
          xlat_varsample_7 = tmpvar_10;
          xlat_varsample_7 = (xlat_varsample_7 * xlat_varsample_7);
          float tmpvar_11;
          float x_12;
          x_12 = (((uv_2.y * 2) + (_TimeX / 4)) + sin((_TimeX + sin((_TimeX * 0.23)))));
          tmpvar_11 = (x_12 - floor(x_12));
          float tmpvar_13;
          tmpvar_13 = (float((tmpvar_11>=0.4)) - float((tmpvar_11>=0.6)));
          col_1 = (video_4.xxx + (((1 - (((tmpvar_11 - 0.4) / 0.2) * tmpvar_13)) * tmpvar_13) * xlat_varsample_7));
          float x_14;
          x_14 = ((uv_2.y * 30) + _TimeX);
          col_1 = (col_1 * ((12 + (x_14 - floor(x_14))) / 13));
          col_1 = (col_1 * (0.5 + (((((6 * uv_2.x) * uv_2.y) * (1.5 - uv_2.x)) * (1.5 - uv_2.y)) * _Greenness)));
          float3 tmpvar_15;
          tmpvar_15.xz = float2(0.55, 0.55);
          tmpvar_15.y = (1.55 + (_Greenness / 4));
          col_1 = (col_1 * (tmpvar_15 * _Greenness));
          col_1 = (col_1 * (0.9 + (0.1 * sin(((10 * _TimeX) + (uv_2.y * 300))))));
          col_1 = (col_1 * (0.79 + (0.01 * sin((110 * _TimeX)))));
          float4 tmpvar_16;
          tmpvar_16.w = 1;
          tmpvar_16.xyz = float3(col_1);
          out_f.color = tmpvar_16;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
