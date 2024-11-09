Shader "CameraFilterPack/Vision_Warp2"
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
      uniform float _TimeX;
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
          float3 fc_1;
          float4 fColor_2;
          float2 uv_3;
          uv_3 = in_f.xlv_TEXCOORD0;
          float2 tmpvar_4;
          tmpvar_4 = abs((uv_3 - 0.5));
          float tmpvar_5;
          tmpvar_5 = sin(((sqrt(dot(tmpvar_4, tmpvar_4)) - (_TimeX / 3)) * 5));
          float r_6;
          r_6 = (tmpvar_5 * tmpvar_5);
          float tmpvar_7;
          float tmpvar_8;
          tmpvar_8 = sin((r_6 / 0.01));
          float tmpvar_9;
          tmpvar_9 = (r_6 / 0.01);
          float tmpvar_10;
          tmpvar_10 = abs(tmpvar_9);
          if((tmpvar_10<=0.1))
          {
              tmpvar_7 = 1;
          }
          else
          {
              tmpvar_7 = abs((tmpvar_8 / tmpvar_9));
          }
          float4 tmpvar_11;
          tmpvar_11.w = 1;
          tmpvar_11.x = tmpvar_7;
          tmpvar_11.y = tmpvar_7;
          tmpvar_11.z = tmpvar_7;
          fColor_2.w = tmpvar_11.w;
          float2 x_12;
          x_12 = (float2(0.5, 0.5) - uv_3);
          float tmpvar_13;
          tmpvar_13 = clamp(((sqrt(dot(x_12, x_12)) - _Value) / (((_Value - 0.05) - _Value2) - _Value)), 0, 1);
          float _tmp_dvx_135 = (1 - (tmpvar_13 * (tmpvar_13 * (3 - (2 * tmpvar_13)))));
          fColor_2.xyz = float3((float3(tmpvar_7, tmpvar_7, tmpvar_7) * float3(_tmp_dvx_135, _tmp_dvx_135, _tmp_dvx_135)));
          float2 tmpvar_14;
          tmpvar_14.x = (fColor_2.x * _Value3);
          tmpvar_14.y = (fColor_2.x * _Value3);
          float2 P_15;
          P_15 = (uv_3 - tmpvar_14);
          float3 tmpvar_16;
          tmpvar_16 = tex2D(_MainTex, P_15).xyz;
          fc_1 = tmpvar_16;
          float4 tmpvar_17;
          tmpvar_17.w = 1;
          tmpvar_17.xyz = float3(fc_1);
          out_f.color = tmpvar_17;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
