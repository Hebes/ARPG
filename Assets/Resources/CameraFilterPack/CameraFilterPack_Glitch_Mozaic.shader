Shader "CameraFilterPack/Glitch_Mozaic"
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
          float4 colMove_1;
          float4 colSnap_2;
          float2 uv_3;
          uv_3 = in_f.xlv_TEXCOORD0;
          float tmpvar_4;
          tmpvar_4 = (0.04 * _Value);
          float tmpvar_5;
          tmpvar_5 = (1 / tmpvar_4);
          float tmpvar_6;
          tmpvar_6 = (floor(((uv_3.x * tmpvar_5) + 0.5)) * tmpvar_4);
          float tmpvar_7;
          tmpvar_7 = (floor(((uv_3.y * tmpvar_5) + 0.5)) * tmpvar_4);
          float2 tmpvar_8;
          tmpvar_8.x = tmpvar_6;
          tmpvar_8.y = tmpvar_7;
          float4 tmpvar_9;
          tmpvar_9 = tex2D(_MainTex, tmpvar_8);
          colSnap_2 = tmpvar_9;
          float tmpvar_10;
          tmpvar_10 = pow((1 - (((colSnap_2.x + colSnap_2.y) + colSnap_2.z) / 3)), _Value);
          float tmpvar_11;
          tmpvar_11 = (tmpvar_4 * tmpvar_10);
          float tmpvar_12;
          tmpvar_12 = (1 / tmpvar_11);
          float2 tmpvar_13;
          tmpvar_13.x = (((tmpvar_6 - (floor(((uv_3.x * tmpvar_12) + 0.5)) * tmpvar_11)) * tmpvar_10) + uv_3.x);
          tmpvar_13.y = (((tmpvar_7 - (floor(((uv_3.y * tmpvar_12) + 0.5)) * tmpvar_11)) * tmpvar_10) + uv_3.y);
          float4 tmpvar_14;
          tmpvar_14 = tex2D(_MainTex, tmpvar_13);
          colMove_1 = tmpvar_14;
          out_f.color = colMove_1;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
