Shader "CameraFilterPack/Colors_HSV"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _HueShift ("HueShift", Range(0, 360)) = 0
    _Sat ("Saturation", float) = 1
    _Val ("Value", float) = 1
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
      uniform float _HueShift;
      uniform float _Sat;
      uniform float _Val;
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
          float3 tmpvar_1;
          tmpvar_1 = tex2D(_MainTex, in_f.xlv_TEXCOORD0).xyz;
          float3 RGB_2;
          RGB_2 = tmpvar_1;
          float3 RESULT_3;
          float tmpvar_4;
          float tmpvar_5;
          tmpvar_5 = (_HueShift * 3.141593);
          float tmpvar_6;
          tmpvar_6 = (_Val * _Sat);
          tmpvar_4 = (tmpvar_6 * cos((tmpvar_5 / 180)));
          float tmpvar_7;
          tmpvar_7 = (tmpvar_6 * sin((tmpvar_5 / 180)));
          float tmpvar_8;
          tmpvar_8 = (0.299 * _Val);
          float tmpvar_9;
          tmpvar_9 = (0.587 * _Val);
          float tmpvar_10;
          tmpvar_10 = (0.114 * _Val);
          RESULT_3.x = (((((tmpvar_8 + (0.701 * tmpvar_4)) + (0.168 * tmpvar_7)) * RGB_2.x) + (((tmpvar_9 - (0.587 * tmpvar_4)) + (0.33 * tmpvar_7)) * RGB_2.y)) + (((tmpvar_10 - (0.114 * tmpvar_4)) - (0.497 * tmpvar_7)) * RGB_2.z));
          RESULT_3.y = (((((tmpvar_8 - (0.299 * tmpvar_4)) - (0.328 * tmpvar_7)) * RGB_2.x) + (((tmpvar_9 + (0.413 * tmpvar_4)) + (0.035 * tmpvar_7)) * RGB_2.y)) + (((tmpvar_10 - (0.114 * tmpvar_4)) + (0.292 * tmpvar_7)) * RGB_2.z));
          RESULT_3.z = (((((tmpvar_8 - (0.3 * tmpvar_4)) + (1.25 * tmpvar_7)) * RGB_2.x) + (((tmpvar_9 - (0.588 * tmpvar_4)) - (1.05 * tmpvar_7)) * RGB_2.y)) + (((tmpvar_10 + (0.886 * tmpvar_4)) - (0.203 * tmpvar_7)) * RGB_2.z));
          float4 tmpvar_11;
          tmpvar_11.w = 1;
          tmpvar_11.xyz = float3(RESULT_3);
          out_f.color = tmpvar_11;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
