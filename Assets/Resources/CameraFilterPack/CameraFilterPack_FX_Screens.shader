Shader "CameraFilterPack/FX_Screens"
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
      uniform float _Value2;
      uniform float _Value3;
      uniform float _Value4;
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
          float2 r_1;
          float4 noise_2;
          float2 uv_3;
          uv_3 = in_f.xlv_TEXCOORD0;
          float4 tmpvar_4;
          float2 P_5;
          P_5 = (floor((uv_3 * _Value)) / _Value);
          tmpvar_4 = tex2D(_MainTex, P_5);
          noise_2 = tmpvar_4;
          float x_6;
          x_6 = (((noise_2.x + noise_2.y) + noise_2.z) + (_TimeX * _Value2));
          float tmpvar_7;
          float x_8;
          x_8 = (uv_3 * _Value).x;
          tmpvar_7 = (x_8 - floor(x_8));
          float2 tmpvar_9;
          float tmpvar_10;
          tmpvar_10 = (tmpvar_7 - 0.5);
          tmpvar_9.x = (tmpvar_10 * tmpvar_10);
          float tmpvar_11;
          tmpvar_11 = (tmpvar_7 - 0.5);
          tmpvar_9.y = (tmpvar_11 * tmpvar_11);
          float2 tmpvar_12;
          tmpvar_12.x = _Value3;
          tmpvar_12.y = _Value4;
          r_1 = (tmpvar_9 - tmpvar_12);
          float4 tmpvar_13;
          tmpvar_13 = tex2D(_MainTex, uv_3);
          float4 tmpvar_14;
          float tmpvar_15;
          tmpvar_15 = min(1, (12 * dot(r_1, r_1)));
          tmpvar_14 = ((float4(0.7, 1.6, 2.8, 1) * (min(max((((1 - (x_6 - floor(x_6))) * 3) - 1.8), 0.1), 2) * (1 - (tmpvar_15 * tmpvar_15)))) + tmpvar_13);
          out_f.color = tmpvar_14;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
