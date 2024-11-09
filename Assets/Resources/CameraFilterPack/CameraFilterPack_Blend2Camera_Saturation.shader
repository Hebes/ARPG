Shader "CameraFilterPack/Blend2Camera_Saturation"
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
          float _tmp_dvx_148 = (1 - _Value2);
          tmpvar_7 = lerp(tex_2.xyz, tex2_1.xyz, float3(_tmp_dvx_148, _tmp_dvx_148, _tmp_dvx_148));
          float4 tmpvar_8;
          tmpvar_8.xy = tmpvar_7.zy;
          tmpvar_8.zw = float2(-1, 0.6666667);
          float4 tmpvar_9;
          tmpvar_9.xy = tmpvar_7.yz;
          tmpvar_9.zw = float2(0, (-0.3333333));
          float4 tmpvar_10;
          float _tmp_dvx_149 = float((tmpvar_7.y>=tmpvar_7.z));
          tmpvar_10 = lerp(tmpvar_8, tmpvar_9, float4(_tmp_dvx_149, _tmp_dvx_149, _tmp_dvx_149, _tmp_dvx_149));
          float4 tmpvar_11;
          tmpvar_11.xyz = tmpvar_10.xyw;
          tmpvar_11.w = tmpvar_7.x;
          float4 tmpvar_12;
          tmpvar_12.x = tmpvar_7.x;
          tmpvar_12.yzw = tmpvar_10.yzx;
          float4 tmpvar_13;
          float _tmp_dvx_150 = float((tmpvar_7.x>=tmpvar_10.x));
          tmpvar_13 = lerp(tmpvar_11, tmpvar_12, float4(_tmp_dvx_150, _tmp_dvx_150, _tmp_dvx_150, _tmp_dvx_150));
          float tmpvar_14;
          tmpvar_14 = (tmpvar_13.x - min(tmpvar_13.w, tmpvar_13.y));
          float3 tmpvar_15;
          tmpvar_15.x = abs((tmpvar_13.z + ((tmpvar_13.w - tmpvar_13.y) / ((6 * tmpvar_14) + 1E-10))));
          tmpvar_15.y = (tmpvar_14 / (tmpvar_13.x + 1E-10));
          tmpvar_15.z = tmpvar_13.x;
          float4 tmpvar_16;
          tmpvar_16.xy = tmpvar_6.zy;
          tmpvar_16.zw = float2(-1, 0.6666667);
          float4 tmpvar_17;
          tmpvar_17.xy = tmpvar_6.yz;
          tmpvar_17.zw = float2(0, (-0.3333333));
          float4 tmpvar_18;
          float _tmp_dvx_151 = float((tmpvar_6.y>=tmpvar_6.z));
          tmpvar_18 = lerp(tmpvar_16, tmpvar_17, float4(_tmp_dvx_151, _tmp_dvx_151, _tmp_dvx_151, _tmp_dvx_151));
          float4 tmpvar_19;
          tmpvar_19.xyz = tmpvar_18.xyw;
          tmpvar_19.w = tmpvar_6.x;
          float4 tmpvar_20;
          tmpvar_20.x = tmpvar_6.x;
          tmpvar_20.yzw = tmpvar_18.yzx;
          float4 tmpvar_21;
          float _tmp_dvx_152 = float((tmpvar_6.x>=tmpvar_18.x));
          tmpvar_21 = lerp(tmpvar_19, tmpvar_20, float4(_tmp_dvx_152, _tmp_dvx_152, _tmp_dvx_152, _tmp_dvx_152));
          float tmpvar_22;
          tmpvar_22 = (tmpvar_21.x - min(tmpvar_21.w, tmpvar_21.y));
          float3 tmpvar_23;
          tmpvar_23.x = abs((tmpvar_21.z + ((tmpvar_21.w - tmpvar_21.y) / ((6 * tmpvar_22) + 1E-10))));
          tmpvar_23.y = (tmpvar_22 / (tmpvar_21.x + 1E-10));
          tmpvar_23.z = tmpvar_21.x;
          float4 tmpvar_24;
          tmpvar_24.w = 1;
          tmpvar_24.xyz = lerp(tmpvar_6, (tmpvar_13.x * lerp(float3(1, 1, 1), clamp((abs(((frac((tmpvar_15.xxx + float3(1, 0.6666667, 0.3333333))) * 6) - float3(3, 3, 3))) - float3(1, 1, 1)), float3(0, 0, 0), float3(1, 1, 1)), tmpvar_23.yyy)), float3(_Value, _Value, _Value));
          out_f.color = tmpvar_24;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
