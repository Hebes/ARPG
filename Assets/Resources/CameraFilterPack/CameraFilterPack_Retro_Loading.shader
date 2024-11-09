Shader "CameraFilterPack/Retro_Loading"
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
          float2 vel_3;
          float2 st_4;
          st_4 = in_f.xlv_TEXCOORD0;
          st_4.x = st_4.x;
          st_4 = (st_4 * float2(50, 30));
          float tmpvar_5;
          tmpvar_5 = (_TimeX * _Value);
          float2 tmpvar_6;
          tmpvar_6 = floor(st_4);
          float2 tmpvar_7;
          tmpvar_7.x = (tmpvar_5 * 10);
          tmpvar_7.y = (_TimeX * 10);
          vel_3 = (floor(tmpvar_7) * float2(-1, 0));
          float tmpvar_8;
          tmpvar_8 = (tmpvar_6.y / 2);
          float tmpvar_9;
          tmpvar_9 = (frac(abs(tmpvar_8)) * 2);
          float tmpvar_10;
          if((tmpvar_8>=0))
          {
              tmpvar_10 = tmpvar_9;
          }
          else
          {
              tmpvar_10 = (-tmpvar_9);
          }
          vel_3 = (vel_3 * ((float((tmpvar_10>=1)) - 0.5) * 2));
          vel_3 = (vel_3 * frac((sin(tmpvar_6.y) * 10000)));
          float tmpvar_11;
          tmpvar_11 = (((tmpvar_5 * 50) + floor((1 + _TimeX))) / 1500);
          float tmpvar_12;
          tmpvar_12 = (frac(abs(tmpvar_11)) * 1500);
          float tmpvar_13;
          if((tmpvar_11>=0))
          {
              tmpvar_13 = tmpvar_12;
          }
          else
          {
              tmpvar_13 = (-tmpvar_12);
          }
          float tmpvar_14;
          tmpvar_14 = (tmpvar_13 / 50);
          float tmpvar_15;
          tmpvar_15 = (frac(abs(tmpvar_14)) * 50);
          float tmpvar_16;
          if((tmpvar_14>=0))
          {
              tmpvar_16 = tmpvar_15;
          }
          else
          {
              tmpvar_16 = (-tmpvar_15);
          }
          float tmpvar_17;
          tmpvar_17 = floor((tmpvar_13 / 50));
          float _tmp_dvx_32 = float((tmpvar_6.y>=(30 - tmpvar_17)));
          color_2 = float3(_tmp_dvx_32, _tmp_dvx_32, _tmp_dvx_32);
          color_2 = (color_2 + ((1 - float((tmpvar_6.x>=tmpvar_16))) * float(((tmpvar_6.y + 1)>=(30 - tmpvar_17)))));
          float3 tmpvar_18;
          tmpvar_18 = clamp(color_2, float3(0, 0, 0), float3(1, 1, 1));
          color_2.x = (tmpvar_18.x * frac((sin(dot(floor(((st_4 + vel_3) + float2(0.1, 0))), float2(12.9898, 78.233))) * 43758.55)));
          color_2.y = (tmpvar_18.y * frac((sin(dot(floor((st_4 + vel_3)), float2(12.9898, 78.233))) * 43758.55)));
          color_2.z = (tmpvar_18.z * frac((sin(dot(floor(((st_4 + vel_3) - float2(0.1, 0))), float2(12.9898, 78.233))) * 43758.55)));
          float3 tmpvar_19;
          tmpvar_19 = clamp(((color_2 * color_2) / float3(0.25, 0.25, 0.25)), 0, 1);
          color_2 = (float3(bool3((tmpvar_19 * (tmpvar_19 * (3 - (2 * tmpvar_19)))) >= float3(0.25, 0.25, 0.25))) * (float((frac((st_4.x + vel_3.x))>=0.1)) * float((frac((st_4.y + vel_3.y))>=0.1))));
          float4 tmpvar_20;
          tmpvar_20 = tex2D(_MainTex, in_f.xlv_TEXCOORD0);
          v_1 = tmpvar_20;
          float4 tmpvar_21;
          tmpvar_21.w = 1;
          tmpvar_21.xyz = (v_1.xyz + color_2);
          out_f.color = tmpvar_21;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
