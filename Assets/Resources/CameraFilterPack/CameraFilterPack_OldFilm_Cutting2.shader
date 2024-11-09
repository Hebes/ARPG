Shader "CameraFilterPack/OldFilm_Cutting2"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _MainTex2 ("Base (RGB)", 2D) = "white" {}
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
      uniform float _Speed;
      uniform float _Value;
      uniform float _Value2;
      uniform float _Value3;
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
          float3 oldfilm_2;
          float3 col_3;
          float2 uv_4;
          uv_4 = in_f.xlv_TEXCOORD0;
          float tmpvar_5;
          tmpvar_5 = float(int((_TimeX * 15)));
          float2 tmpvar_6;
          tmpvar_6.x = (frac((sin(dot(float2(tmpvar_5, tmpvar_5), float2(12.9898, 78.233))) * 43758.55)) * (-2));
          float _tmp_dvx_45 = (tmpvar_5 + 23);
          tmpvar_6.y = frac((sin(dot(float2(_tmp_dvx_45, _tmp_dvx_45), float2(12.9898, 78.233))) * 43758.55));
          float2 tmpvar_7;
          tmpvar_7 = (uv_4 + (0.004 * tmpvar_6));
          float3 tmpvar_8;
          tmpvar_8 = tex2D(_MainTex, tmpvar_7).xyz;
          col_3 = tmpvar_8;
          float2 tmpvar_9;
          tmpvar_9.x = frac((sin(dot(float2(tmpvar_5, tmpvar_5), float2(12.9898, 78.233))) * 43758.55));
          float _tmp_dvx_46 = (tmpvar_5 + 23);
          tmpvar_9.y = frac((sin(dot(float2(_tmp_dvx_46, _tmp_dvx_46), float2(12.9898, 78.233))) * 43758.55));
          uv_4.y = (uv_4 + (0.01 * tmpvar_9)).y;
          uv_4.x = (uv_4.x + (_TimeX * _Speed));
          float3 tmpvar_10;
          tmpvar_10 = tex2D(_MainTex2, uv_4).xyz;
          oldfilm_2 = tmpvar_10;
          uv_4 = in_f.xlv_TEXCOORD0;
          col_3 = (lerp(col_3, (1 - col_3), float3(_Value3, _Value3, _Value3)) * (pow(((((16 * uv_4.x) * (1 - uv_4.x)) * uv_4.y) * (1 - uv_4.y)), 0.4) + _Value2));
          float3 tmpvar_11;
          float _tmp_dvx_47 = dot(float3(0.2126, 0.7152, 0.0722), col_3);
          float _tmp_dvx_48 = (((2 * oldfilm_2) + float3(_tmp_dvx_47, _tmp_dvx_47, _tmp_dvx_47)) - _Value);
          tmpvar_11 = float3(_tmp_dvx_48, _tmp_dvx_48, _tmp_dvx_48);
          col_3 = tmpvar_11;
          float4 tmpvar_12;
          tmpvar_12.w = 1;
          tmpvar_12.xyz = float3(tmpvar_11);
          tmpvar_1 = tmpvar_12;
          out_f.color = tmpvar_1;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
