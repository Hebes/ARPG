Shader "CameraFilterPack/TV_Distorted"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _TimeX ("Time", Range(0, 1)) = 1
    _Distortion ("_Distortion", Range(1, 10)) = 1
    _RGB ("_RGB", Range(1, 10)) = 1
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
      uniform float _Distortion;
      uniform float _RGB;
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
          float blue_1;
          float green_2;
          float red_3;
          float2 uv_4;
          uv_4 = in_f.xlv_TEXCOORD0;
          float2 tmpvar_5;
          tmpvar_5.x = (_TimeX * 15);
          tmpvar_5.y = (uv_4.y * 80);
          float2 tmpvar_6;
          tmpvar_6.x = _TimeX;
          tmpvar_6.y = (uv_4.y * 25);
          float tmpvar_7;
          tmpvar_7 = ((((sin(((_TimeX * tmpvar_5.y) / 35)) * frac((sin(dot(tmpvar_5, float2(12.9898, 78.233))) * 43758.55))) * 0.003) * _Distortion) + (((sin(((_TimeX * tmpvar_6.y) / 35)) * frac((sin(dot(tmpvar_6, float2(12.9898, 78.233))) * 43758.55))) * 0.004) * _Distortion));
          float2 tmpvar_8;
          tmpvar_8.x = ((uv_4.x + tmpvar_7) - _RGB);
          tmpvar_8.y = uv_4.y;
          float tmpvar_9;
          tmpvar_9 = tex2D(_MainTex, tmpvar_8).x;
          red_3 = tmpvar_9;
          float2 tmpvar_10;
          tmpvar_10.x = (uv_4.x + tmpvar_7);
          tmpvar_10.y = uv_4.y;
          float tmpvar_11;
          tmpvar_11 = tex2D(_MainTex, tmpvar_10).y;
          green_2 = tmpvar_11;
          float2 tmpvar_12;
          tmpvar_12.x = ((uv_4.x + tmpvar_7) + _RGB);
          tmpvar_12.y = uv_4.y;
          float tmpvar_13;
          tmpvar_13 = tex2D(_MainTex, tmpvar_12).z;
          blue_1 = tmpvar_13;
          float3 tmpvar_14;
          tmpvar_14.x = red_3;
          tmpvar_14.y = green_2;
          tmpvar_14.z = blue_1;
          float4 tmpvar_15;
          tmpvar_15.w = 1;
          tmpvar_15.xyz = (tmpvar_14 - (sin((uv_4.y * 800)) * 0.04));
          out_f.color = tmpvar_15;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
