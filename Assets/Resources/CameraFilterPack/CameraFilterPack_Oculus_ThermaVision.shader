Shader "CameraFilterPack/Oculus_ThermaVision"
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
          float4 thermal_1;
          float4 pixcol_2;
          float4 tmpvar_3;
          tmpvar_3 = tex2D(_MainTex, in_f.xlv_TEXCOORD0);
          pixcol_2 = tmpvar_3;
          float tmpvar_4;
          tmpvar_4 = (((pixcol_2.x + pixcol_2.y) + pixcol_2.z) / 3);
          if((tmpvar_4<_Value))
          {
              float _tmp_dvx_62 = (tmpvar_4 / _Value);
              thermal_1 = lerp(float4(0, 0, 1, 1), float4(1, 0, 0, 1), float4(_tmp_dvx_62, _tmp_dvx_62, _tmp_dvx_62, _tmp_dvx_62));
          }
          if((tmpvar_4>=_Value))
          {
              float _tmp_dvx_63 = ((tmpvar_4 - _Value) / _Value);
              thermal_1 = lerp(float4(1, 1, 0, 1), float4(1, 0, 0, 1), float4(_tmp_dvx_63, _tmp_dvx_63, _tmp_dvx_63, _tmp_dvx_63));
          }
          out_f.color = thermal_1;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
