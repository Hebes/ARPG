Shader "CameraFilterPack/FX_Glitch3"
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
          float3 sample_yuv_1;
          float4 xlat_varsample_2;
          float2 uv_nm_3;
          float vt_rnd_4;
          float2 uv_5;
          uv_5 = in_f.xlv_TEXCOORD0;
          float tmpvar_6;
          tmpvar_6 = (16 * frac((sin(dot(((floor((uv_5.yy * float2(16, 16))) / float2(16, 16)) + (150 * (floor((_TimeX * 4)) / 4))), float2(12.9898, 78.233))) * 43758.55)));
          float tmpvar_7;
          tmpvar_7 = (5 * (floor((_TimeX * tmpvar_6)) / tmpvar_6));
          vt_rnd_4 = ((0.5 * frac((sin(dot((floor(((uv_5.yy + tmpvar_7) * float2(11, 11))) / float2(11, 11)), float2(12.9898, 78.233))) * 43758.55))) + (1.5 * frac((sin(dot((floor(((uv_5.yy + tmpvar_7) * float2(7, 7))) / float2(7, 7)), float2(12.9898, 78.233))) * 43758.55))));
          vt_rnd_4 = ((vt_rnd_4 * 2) - 1);
          vt_rnd_4 = (sign(vt_rnd_4) * clamp(((abs(vt_rnd_4) - 0.6) / 0.4), 0, 1));
          float2 tmpvar_8;
          tmpvar_8.y = 0;
          tmpvar_8.x = (0.1 * vt_rnd_4);
          float2 tmpvar_9;
          tmpvar_9 = clamp((uv_5 + tmpvar_8), float2(0, 0), float2(1, 1));
          uv_nm_3 = tmpvar_9;
          float tmpvar_10;
          float _tmp_dvx_123 = (floor((_TimeX * 8)) / 8);
          tmpvar_10 = frac((sin(dot(float2(_tmp_dvx_123, _tmp_dvx_123), float2(12.9898, 78.233))) * 43758.55));
          float tmpvar_11;
          tmpvar_11 = lerp(1, 0.975, clamp(0.4, 0, 1));
          float tmpvar_12;
          if((tmpvar_10>tmpvar_11))
          {
              tmpvar_12 = (1 - tmpvar_9.y);
          }
          else
          {
              tmpvar_12 = tmpvar_9.y;
          }
          uv_nm_3.y = tmpvar_12;
          float4 tmpvar_13;
          tmpvar_13 = tex2D(_MainTex, uv_nm_3);
          xlat_varsample_2 = tmpvar_13;
          float3 yuv_14;
          yuv_14.x = dot(xlat_varsample_2.xyz, float3(-0.299, 0.587, 0.114));
          yuv_14.y = dot(xlat_varsample_2.xyz, float3(-0.14713, (-0.28886), 0.436));
          yuv_14.z = dot(xlat_varsample_2.xyz, float3(0.615, (-0.51499), (-0.10001)));
          sample_yuv_1.x = yuv_14.x;
          sample_yuv_1.y = (yuv_14.y / (1 - ((3 * abs(vt_rnd_4)) * clamp((10.5 - vt_rnd_4), 0, 1))));
          sample_yuv_1.z = (yuv_14.z + ((1.125 * vt_rnd_4) * clamp((vt_rnd_4 - 0.5), 0, 1)));
          float3 rgb_15;
          rgb_15.x = (yuv_14.x + (sample_yuv_1.z * 1.13983));
          rgb_15.y = (yuv_14.x + dot(float2(-0.39465, (-0.5806)), sample_yuv_1.yz));
          rgb_15.z = (yuv_14.x + (sample_yuv_1.y * 2.03211));
          float4 tmpvar_16;
          tmpvar_16.xyz = float3(rgb_15);
          tmpvar_16.w = xlat_varsample_2.w;
          xlat_varsample_2 = tmpvar_16;
          float4 tmpvar_17;
          float _tmp_dvx_124 = (frac((sin(dot((in_f.xlv_TEXCOORD0 * (_TimeX * 100)), float2(12.9898, 78.233))) * 43758.55)) / 2);
          tmpvar_17 = (lerp(tmpvar_16, float4(0, 0, 0, 1), float4(_tmp_dvx_124, _tmp_dvx_124, _tmp_dvx_124, _tmp_dvx_124)) * 2);
          out_f.color = tmpvar_17;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
