Shader "CameraFilterPack/Blend2Camera_BlueScreen"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _MainTex2 ("Base (RGB)", 2D) = "white" {}
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
      uniform sampler2D _MainTex2;
      uniform float _Value;
      uniform float _Value2;
      uniform float _Value3;
      uniform float _Value4;
      uniform float _Value5;
      uniform float _Value6;
      uniform float _Value7;
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
          float4 tex_2;
          float2 uv_3;
          uv_3 = in_f.xlv_TEXCOORD0;
          float4 tmpvar_4;
          tmpvar_4 = tex2D(_MainTex, uv_3);
          tex_2 = tmpvar_4;
          float4 tmpvar_5;
          tmpvar_5 = tex2D(_MainTex2, uv_3);
          tex2_1 = tmpvar_5;
          float3 d_6;
          d_6.xy = tex2_1.xy;
          float tmpvar_7;
          tmpvar_7 = max(tex2_1.x, tex2_1.y);
          d_6.z = min((tex2_1.z - _Value2), (tmpvar_7 * 0.8));
          d_6 = (d_6 + ((tex2_1.z - d_6.z) - _Value4));
          d_6.x = (d_6.x + _Value5);
          d_6.z = (d_6.z + _Value6);
          d_6.y = (d_6.y + _Value7);
          float4 tmpvar_8;
          tmpvar_8.w = 1;
          float _tmp_dvx_22 = clamp((((tex2_1.z - tmpvar_7) - _Value3) * 3), 0, 1);
          tmpvar_8.xyz = lerp(d_6, tex_2.xyz, float3(_tmp_dvx_22, _tmp_dvx_22, _tmp_dvx_22));
          float4 tmpvar_9;
          tmpvar_9 = lerp(tex_2, tmpvar_8, float4(_Value, _Value, _Value, _Value));
          tex_2 = tmpvar_9;
          float4 tmpvar_10;
          tmpvar_10.w = 1;
          tmpvar_10.xyz = tmpvar_9.xyz;
          out_f.color = tmpvar_10;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
