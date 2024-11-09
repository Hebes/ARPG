Shader "CameraFilterPack/TV_Old"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _TimeX ("Time", Range(0, 1)) = 1
    _Distortion ("_Distortion", Range(1, 10)) = 1
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
          float3 textur_2;
          float2 uv_3;
          uv_3 = in_f.xlv_TEXCOORD0;
          float3 tmpvar_4;
          tmpvar_4 = tex2D(_MainTex, uv_3).xyz;
          textur_2 = tmpvar_4;
          float2 tmpvar_5;
          tmpvar_5.x = floor(((uv_3.x * 200) * (uv_3.x / uv_3.y)));
          tmpvar_5.y = floor((uv_3.y * 200));
          float tmpvar_6;
          tmpvar_6 = clamp((frac((sin(dot(((tmpvar_5 * _TimeX) / 1000), float2(12.9898, 78.233))) * 43758.55)) + 0.5), 0, 1);
          float3 tmpvar_7;
          float _tmp_dvx_136 = clamp((sin(((uv_3.y * 6) + (_TimeX * 5.6))) + 1.25), 0, 1);
          tmpvar_7 = (textur_2 * lerp((float3(tmpvar_6, tmpvar_6, tmpvar_6) - 0.75), float3(tmpvar_6, tmpvar_6, tmpvar_6), float3(_tmp_dvx_136, _tmp_dvx_136, _tmp_dvx_136))).xxx;
          color_1.xy = tmpvar_7.xy;
          color_1.z = (tmpvar_7.z + 0.052);
          float2 tmpvar_8;
          tmpvar_8 = (uv_3 - float2(0.5, 0.5));
          color_1 = (color_1 * (1 - (pow(sqrt(dot(tmpvar_8, tmpvar_8)), 2.1) * 2.8)));
          float4 tmpvar_9;
          tmpvar_9.w = 1;
          tmpvar_9.xyz = float3(color_1);
          out_f.color = tmpvar_9;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
