Shader "CameraFilterPack/Pixelisation_Dot"
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
      uniform float _Value;
      uniform float _Value2;
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
          float3 color_1;
          float3 texColor_2;
          float2 q_3;
          float3 tmpvar_4;
          tmpvar_4.x = _Value2;
          tmpvar_4.y = _Value2;
          tmpvar_4.z = _Value2;
          q_3 = in_f.xlv_TEXCOORD0;
          float2 tmpvar_5;
          tmpvar_5 = (floor((in_f.xlv_TEXCOORD0 / _Value)) * _Value);
          float3 tmpvar_6;
          tmpvar_6 = tex2D(_MainTex, tmpvar_5).xyz;
          texColor_2 = tmpvar_6;
          float3 tmpvar_7;
          tmpvar_7 = (texColor_2 - float3(0, 1, 0));
          float tmpvar_8;
          tmpvar_8 = clamp((pow(sqrt(dot(tmpvar_7, tmpvar_7)), 8) / 1.5), 0, 1);
          float3 tmpvar_9;
          float _tmp_dvx_59 = (tmpvar_8 * (tmpvar_8 * (3 - (2 * tmpvar_8))));
          tmpvar_9 = lerp(float3(_Value2, _Value2, _Value2), texColor_2, float3(_tmp_dvx_59, _tmp_dvx_59, _tmp_dvx_59));
          texColor_2 = tmpvar_9;
          float tmpvar_10;
          tmpvar_10 = dot(float3(0.2126, 0.7152, 0.0722), tmpvar_9);
          color_1 = tmpvar_4;
          float2 tmpvar_11;
          tmpvar_11 = ((q_3 - tmpvar_5) / float2(_Value, _Value));
          float tmpvar_12;
          tmpvar_12 = ((tmpvar_10 * tmpvar_10) * 16);
          float tmpvar_13;
          tmpvar_13 = (pow(abs((tmpvar_11.x - 0.5)), tmpvar_12) + pow(abs((tmpvar_11.y - 0.5)), tmpvar_12));
          float tmpvar_14;
          tmpvar_14 = pow(0.5, tmpvar_12);
          if((tmpvar_13<tmpvar_14))
          {
              color_1 = tmpvar_9;
          }
          float4 tmpvar_15;
          tmpvar_15.w = 1;
          tmpvar_15.xyz = float3(color_1);
          out_f.color = tmpvar_15;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
