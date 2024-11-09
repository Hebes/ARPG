Shader "CameraFilterPack/AAA_WaterDrop"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _MainTex2 ("Base (RGB)", 2D) = "white" {}
    _TimeX ("Time", Range(0, 1)) = 1
    _SizeX ("SizeX", Range(0, 1)) = 1
    _SizeY ("SizeY", Range(0, 1)) = 1
    _Speed ("Speed", Range(0, 10)) = 1
    _Distortion ("_Distortion", Range(0, 1)) = 0.87
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
      uniform sampler2D _MainTex2;
      uniform float _TimeX;
      uniform float _SizeX;
      uniform float _Speed;
      uniform float _SizeY;
      uniform float _Distortion;
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
          float3 col_2;
          float2 uv_3;
          uv_3 = in_f.xlv_TEXCOORD0;
          float2 tmpvar_4;
          tmpvar_4.x = ((uv_3.x * 1.3) * _SizeX);
          float tmpvar_5;
          tmpvar_5 = (_TimeX * _Speed);
          tmpvar_4.y = (((uv_3.y * _SizeY) * 1.4) + (tmpvar_5 * 0.125));
          float4 tmpvar_6;
          tmpvar_6 = tex2D(_MainTex2, tmpvar_4);
          float2 tmpvar_7;
          tmpvar_7.x = (((uv_3.x * 1.15) * _SizeX) - 0.1);
          tmpvar_7.y = (((uv_3.y * _SizeY) * 1.1) + (tmpvar_5 * 0.225));
          float4 tmpvar_8;
          tmpvar_8 = tex2D(_MainTex2, tmpvar_7);
          float2 tmpvar_9;
          tmpvar_9.x = ((uv_3.x * _SizeX) - 0.2);
          tmpvar_9.y = ((uv_3.y * _SizeY) + (tmpvar_5 * 0.025));
          float4 tmpvar_10;
          tmpvar_10 = tex2D(_MainTex2, tmpvar_9);
          float2 tmpvar_11;
          tmpvar_11 = (uv_3 - ((((tmpvar_6.xyz / _Distortion).xy - (tmpvar_8.xyz / _Distortion).xy) - (tmpvar_10.xyz / _Distortion).xy) / 3));
          float3 tmpvar_12;
          tmpvar_12 = tex2D(_MainTex, tmpvar_11).xyz;
          col_2 = tmpvar_12;
          float4 tmpvar_13;
          tmpvar_13.w = 1;
          tmpvar_13.xyz = float3(col_2);
          tmpvar_1 = tmpvar_13;
          out_f.color = tmpvar_1;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
