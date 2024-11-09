Shader "CameraFilterPack/TV_WideScreenHV"
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
          float4 tex_1;
          float2 uv_2;
          uv_2 = in_f.xlv_TEXCOORD0;
          float4 tmpvar_3;
          tmpvar_3 = tex2D(_MainTex, uv_2);
          tex_1 = tmpvar_3;
          float2 x_4;
          x_4 = (float2(0.5, 0.5) - uv_2.x);
          float tmpvar_5;
          float tmpvar_6;
          tmpvar_6 = (_Value - _Value2);
          tmpvar_5 = clamp(((sqrt(dot(x_4, x_4)) - _Value) / (tmpvar_6 - _Value)), 0, 1);
          float2 x_7;
          x_7 = (float2(0.5, 0.5) - uv_2.y);
          float tmpvar_8;
          tmpvar_8 = clamp(((sqrt(dot(x_7, x_7)) - _Value) / (tmpvar_6 - _Value)), 0, 1);
          float4 tmpvar_9;
          tmpvar_9.w = 1;
          float _tmp_dvx_81 = ((1 - (tmpvar_5 * (tmpvar_5 * (3 - (2 * tmpvar_5))))) + (1 - (tmpvar_8 * (tmpvar_8 * (3 - (2 * tmpvar_8))))));
          tmpvar_9.xyz = (tex_1 * (float4(1, 1, 1, 1) - float4(_tmp_dvx_81, _tmp_dvx_81, _tmp_dvx_81, _tmp_dvx_81))).xyz;
          out_f.color = tmpvar_9;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}