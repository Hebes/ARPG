Shader "CameraFilterPack/TV_CompressionFX"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _TimeX ("Time", Range(0, 1)) = 1
    _Parasite ("_Parasite", Range(1, 10)) = 1
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
      uniform float _Parasite;
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
          float4 col1_1;
          float2 uv_2;
          uv_2 = in_f.xlv_TEXCOORD0;
          float tmpvar_3;
          tmpvar_3 = (_TimeX * 12);
          float2 tmpvar_4;
          tmpvar_4.y = 0;
          tmpvar_4.x = ((((pow(frac((sin(dot(((floor((uv_2 * float2(24, 19))) * 4) * floor(tmpvar_3)), float2(127.1, 311.7))) * 43758.55)), 3) * _Parasite) * pow(frac((sin(dot(((floor((uv_2 * float2(38, 14))) * 4) * floor(tmpvar_3)), float2(127.1, 311.7))) * 43758.55)), 3)) * 0.02) * frac((sin(dot((float2(2, 1) * floor(tmpvar_3)), float2(127.1, 311.7))) * 43758.55)));
          float4 tmpvar_5;
          float2 P_6;
          P_6 = (uv_2 + tmpvar_4);
          tmpvar_5 = tex2D(_MainTex, P_6);
          col1_1 = tmpvar_5;
          float4 tmpvar_7;
          tmpvar_7.w = 1;
          tmpvar_7.xyz = col1_1.xyz;
          out_f.color = tmpvar_7;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
