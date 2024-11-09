Shader "CameraFilterPack/TV_Vcr"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _TimeX ("Time", Range(0, 1)) = 1
    _Distortion ("_Distortion", Range(1, 10)) = 1
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
          float3 video_1;
          float2 uv_2;
          uv_2 = in_f.xlv_TEXCOORD0;
          float2 uv_3;
          uv_3 = (uv_2 - float2(0.5, 0.5));
          uv_3 = ((uv_3 * 1.2) * (0.8333333 + ((2 * uv_3.x) * ((uv_3.x * uv_3.y) * uv_3.y))));
          uv_3 = (uv_3 + float2(0.5, 0.5));
          uv_2 = uv_3;
          float3 video_4;
          float2 look_5;
          float tmpvar_6;
          tmpvar_6 = (_TimeX / 4);
          look_5.x = (uv_3.x + ((((sin(((uv_3.y * 10) + _TimeX)) / 50) * float((sin((_TimeX + (4 * cos((_TimeX * 4)))))>=0.3))) * (1 + cos((_TimeX * 80)))) * (1 / (1 + ((20 * (uv_3.y - (tmpvar_6 - floor(tmpvar_6)))) * (uv_3.y - (tmpvar_6 - floor(tmpvar_6))))))));
          float x_7;
          float tmpvar_8;
          tmpvar_8 = cos(_TimeX);
          x_7 = (uv_3.y + ((0.4 * float((sin((_TimeX + (2 * cos((_TimeX * 3)))))>=0.9))) * ((sin(_TimeX) * sin((_TimeX * 20))) + (0.5 + ((0.1 * sin((_TimeX * 200))) * tmpvar_8)))));
          look_5.y = (x_7 - floor(x_7));
          float3 tmpvar_9;
          tmpvar_9 = tex2D(_MainTex, look_5).xyz;
          video_4 = tmpvar_9;
          float tmpvar_10;
          tmpvar_10 = (3 + (0.3 * sin((_TimeX + (5 * cos((_TimeX * 5)))))));
          float s_11;
          float2 tmpvar_12;
          tmpvar_12.x = 1;
          tmpvar_12.y = (2 * tmpvar_8);
          float2 P_13;
          P_13 = (((tmpvar_12 * _TimeX) * 8) + ((uv_3 * float2(0.5, 1)) + float2(1, 3)));
          float tmpvar_14;
          tmpvar_14 = tex2D(_MainTex, P_13).x;
          s_11 = tmpvar_14;
          s_11 = (s_11 * s_11);
          float tmpvar_15;
          float x_16;
          x_16 = (((uv_3.y * 4) + (_TimeX / 2)) + sin((_TimeX + sin((_TimeX * 0.63)))));
          tmpvar_15 = (x_16 - floor(x_16));
          float tmpvar_17;
          tmpvar_17 = (float((tmpvar_15>=0.5)) - float((tmpvar_15>=0.6)));
          video_1 = (video_4 + (((1 - (((tmpvar_15 - 0.5) / 0.1) * tmpvar_17)) * tmpvar_17) * s_11));
          float s_18;
          float2 tmpvar_19;
          tmpvar_19.x = 1;
          tmpvar_19.y = (2 * tmpvar_8);
          float2 P_20;
          P_20 = (((tmpvar_19 * _TimeX) * 8) + (uv_3 * 2));
          float tmpvar_21;
          tmpvar_21 = tex2D(_MainTex, P_20).x;
          s_18 = tmpvar_21;
          s_18 = (s_18 * s_18);
          video_1 = (video_1 + (s_18 / 2));
          video_1 = (video_1 * ((1 - ((tmpvar_10 * (uv_3.y - 0.5)) * (uv_3.y - 0.5))) * (1 - ((tmpvar_10 * (uv_3.x - 0.5)) * (uv_3.x - 0.5)))));
          float x_22;
          x_22 = ((uv_3.y * 30) + _TimeX);
          video_1 = (video_1 * ((12 + (x_22 - floor(x_22))) / 13));
          float4 tmpvar_23;
          tmpvar_23.w = 1;
          tmpvar_23.xyz = float3(video_1);
          out_f.color = tmpvar_23;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
