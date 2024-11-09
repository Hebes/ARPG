Shader "CameraFilterPack/Blend2Camera_VividLight"
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
          float3 tmpvar_6;
          tmpvar_6 = lerp(tex_2.xyz, tex2_1.xyz, float3(_Value2, _Value2, _Value2));
          float3 tmpvar_7;
          float _tmp_dvx_19 = (1 - _Value2);
          tmpvar_7 = lerp(tex_2.xyz, tex2_1.xyz, float3(_tmp_dvx_19, _tmp_dvx_19, _tmp_dvx_19));
          float3 c_8;
          float tmpvar_9;
          if((tmpvar_6.x<0.5))
          {
              tmpvar_9 = (1 - ((1 - tmpvar_7.x) / (2 * tmpvar_6.x)));
          }
          else
          {
              tmpvar_9 = (tmpvar_7.x / (2 * (1 - tmpvar_6.x)));
          }
          c_8.x = tmpvar_9;
          float tmpvar_10;
          if((tmpvar_6.y<0.5))
          {
              tmpvar_10 = (1 - ((1 - tmpvar_7.y) / (2 * tmpvar_6.y)));
          }
          else
          {
              tmpvar_10 = (tmpvar_7.y / (2 * (1 - tmpvar_6.y)));
          }
          c_8.y = tmpvar_10;
          float tmpvar_11;
          if((tmpvar_6.z<0.5))
          {
              tmpvar_11 = (1 - ((1 - tmpvar_7.z) / (2 * tmpvar_6.z)));
          }
          else
          {
              tmpvar_11 = (tmpvar_7.z / (2 * (1 - tmpvar_6.z)));
          }
          c_8.z = tmpvar_11;
          float4 tmpvar_12;
          tmpvar_12.w = 1;
          tmpvar_12.xyz = float3(lerp(tmpvar_6, c_8, float3(_Value, _Value, _Value)));
          out_f.color = tmpvar_12;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
