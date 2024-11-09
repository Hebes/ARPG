Shader "CameraFilterPack/TV_Artefact"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _TimeX ("Time", Range(0, 1)) = 1
    _Colorisation ("_Colorisation", Range(1, 10)) = 1
    _Parasite ("_Parasite", Range(1, 10)) = 1
    _Noise ("_Noise", Range(1, 10)) = 1
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
      uniform float _Colorisation;
      uniform float _Parasite;
      uniform float _Noise;
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
          float4 col3_1;
          float4 col2_2;
          float4 col1_3;
          float2 uv_4;
          uv_4 = in_f.xlv_TEXCOORD0;
          float tmpvar_5;
          float tmpvar_6;
          tmpvar_6 = (_TimeX * 12);
          tmpvar_5 = frac((sin(dot((uv_4 * floor(tmpvar_6)), float2(127.1, 311.7))) * 43758.55));
          float3 tmpvar_7;
          tmpvar_7.x = tmpvar_5;
          tmpvar_7.y = (1 - (tmpvar_5 * _Colorisation));
          tmpvar_7.z = ((tmpvar_5 / 2) + 0.5);
          float tmpvar_8;
          tmpvar_8 = (((pow(frac((sin(dot((floor((uv_4 * float2(24, 9))) * floor(tmpvar_6)), float2(127.1, 311.7))) * 43758.55)), 8) * _Parasite) * pow(frac((sin(dot((floor((uv_4 * float2(8, 4))) * floor(tmpvar_6)), float2(127.1, 311.7))) * 43758.55)), 3)) - (pow(frac((sin(dot((float2(7.2341, 1) * floor(tmpvar_6)), float2(127.1, 311.7))) * 43758.55)), 17) * 2));
          float4 tmpvar_9;
          tmpvar_9 = tex2D(_MainTex, uv_4);
          col1_3 = tmpvar_9;
          float2 tmpvar_10;
          tmpvar_10.y = 0;
          tmpvar_10.x = ((tmpvar_8 * 0.05) * frac((sin(dot((float2(5, 1) * floor(tmpvar_6)), float2(127.1, 311.7))) * 43758.55)));
          float4 tmpvar_11;
          float2 P_12;
          P_12 = (uv_4 + tmpvar_10);
          tmpvar_11 = tex2D(_MainTex, P_12);
          col2_2 = tmpvar_11;
          float2 tmpvar_13;
          tmpvar_13.y = 0;
          tmpvar_13.x = ((tmpvar_8 * 0.05) * frac((sin(dot((float2(31, 1) * floor(tmpvar_6)), float2(127.1, 311.7))) * 43758.55)));
          float4 tmpvar_14;
          float2 P_15;
          P_15 = (uv_4 - tmpvar_13);
          tmpvar_14 = tex2D(_MainTex, P_15);
          col3_1 = tmpvar_14;
          float3 tmpvar_16;
          tmpvar_16.x = col1_3.x;
          tmpvar_16.y = col2_2.y;
          tmpvar_16.z = col3_1.z;
          float4 tmpvar_17;
          tmpvar_17.w = 0.2;
          tmpvar_17.xyz = float3((tmpvar_16 + (((tmpvar_7 * _Noise) - 2) * 0.08)));
          out_f.color = tmpvar_17;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
