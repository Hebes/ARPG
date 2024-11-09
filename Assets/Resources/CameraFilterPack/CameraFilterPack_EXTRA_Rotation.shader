Shader "CameraFilterPack/EXTRA_Rotation"
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
          float2 uv_2;
          float2 tmpvar_3;
          tmpvar_3.x = _Value2;
          tmpvar_3.y = _Value3;
          float tmpvar_4;
          tmpvar_4 = (_Value * 0.01744444);
          float tmpvar_5;
          tmpvar_5 = sin(tmpvar_4);
          float tmpvar_6;
          tmpvar_6 = cos(tmpvar_4);
          float2x2 tmpvar_7;
          conv_mxt2x2_0(tmpvar_7).x = tmpvar_6;
          conv_mxt2x2_0(tmpvar_7).y = tmpvar_5;
          conv_mxt2x2_1(tmpvar_7).x = (-tmpvar_5);
          conv_mxt2x2_1(tmpvar_7).y = tmpvar_6;
          uv_2 = (mul((in_f.xlv_TEXCOORD0 - tmpvar_3), tmpvar_7) + float2(0.5, 0.5));
          float4 tmpvar_8;
          tmpvar_8 = tex2D(_MainTex, uv_2);
          tmpvar_1 = tmpvar_8;
          out_f.color = tmpvar_1;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
