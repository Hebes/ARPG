Shader "CameraFilterPack/FX_Dot_Circle"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _TimeX ("Time", Range(0, 1)) = 1
    _Distortion ("_Distortion", Range(0, 1)) = 0.3
    _ScreenResolution ("_ScreenResolution", Vector) = (0,0,0,0)
    _Value ("_Value", Range(4, 32)) = 7
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
      uniform float4 _ScreenResolution;
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
          float4 color_1;
          float4 texel_2;
          int tmpvar_3;
          tmpvar_3 = int(_Value);
          float tmpvar_4;
          tmpvar_4 = (0.375 * float(tmpvar_3));
          float2 tmpvar_5;
          float2 tmpvar_6;
          tmpvar_6 = (in_f.xlv_TEXCOORD0 * _ScreenResolution.xy);
          tmpvar_5 = (floor((tmpvar_6 / float(tmpvar_3))) * float(tmpvar_3));
          float2 tmpvar_7;
          tmpvar_7 = (tmpvar_5 / _ScreenResolution.xy);
          float tmpvar_8;
          float2 x_9;
          x_9 = ((tmpvar_5 + (float(tmpvar_3) / 2)) - tmpvar_6);
          tmpvar_8 = sqrt(dot(x_9, x_9));
          float4 tmpvar_10;
          tmpvar_10 = tex2D(_MainTex, tmpvar_7);
          texel_2 = tmpvar_10;
          color_1 = texel_2;
          if((tmpvar_8>tmpvar_4))
          {
              color_1 = float4(0.25, 0.25, 0.25, 0.25);
          }
          out_f.color = color_1;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
