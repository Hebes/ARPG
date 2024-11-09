Shader "CameraFilterPack/Blend2Camera_SplitScreen"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _MainTex2 ("Base (RGB)", 2D) = "white" {}
    _TimeX ("Time", Range(0, 1)) = 1
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
      uniform float _Value;
      uniform float _Value2;
      uniform float _Value3;
      uniform float _Value4;
      uniform float _Value5;
      uniform float _Value6;
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
          float4 tex2_1;
          float2 uv2_2;
          float4 tex_3;
          float2 uv_4;
          uv_4 = in_f.xlv_TEXCOORD0;
          float4 tmpvar_5;
          tmpvar_5 = tex2D(_MainTex, uv_4);
          tex_3 = tmpvar_5;
          float2 tmpvar_6;
          tmpvar_6.x = _Value3;
          tmpvar_6.y = _Value6;
          float2 tmpvar_7;
          tmpvar_7 = (uv_4 - tmpvar_6);
          float tmpvar_8;
          tmpvar_8 = cos(_Value5);
          float tmpvar_9;
          tmpvar_9 = sin(_Value5);
          float2 tmpvar_10;
          tmpvar_10.x = ((tmpvar_7.x * tmpvar_8) - (tmpvar_7.y * tmpvar_9));
          tmpvar_10.y = ((tmpvar_7.x * tmpvar_9) + (tmpvar_7.y * tmpvar_8));
          uv_4 = tmpvar_10;
          uv2_2 = in_f.xlv_TEXCOORD0;
          float4 tmpvar_11;
          tmpvar_11 = tex2D(_MainTex2, uv2_2);
          tex2_1 = tmpvar_11;
          float2 x_12;
          float _tmp_dvx_9 = (-max(tmpvar_10.y, 0));
          x_12 = float2(_tmp_dvx_9, _tmp_dvx_9);
          float tmpvar_13;
          tmpvar_13 = clamp((sqrt(dot(x_12, x_12)) / _Value4), 0, 1);
          float4 tmpvar_14;
          tmpvar_14.w = 1;
          float _tmp_dvx_10 = ((1 - (tmpvar_13 * (tmpvar_13 * (3 - (2 * tmpvar_13))))) * _Value);
          float _tmp_dvx_11 = (1 - _Value2);
          tmpvar_14.xyz = lerp(lerp(tex_3.xyz, tex2_1.xyz, float3(_Value2, _Value2, _Value2)), lerp(tex_3.xyz, tex2_1.xyz, float3(_tmp_dvx_11, _tmp_dvx_11, _tmp_dvx_11)), float3(_tmp_dvx_10, _tmp_dvx_10, _tmp_dvx_10));
          out_f.color = tmpvar_14;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
