Shader "CameraFilterPack/Drawing_Lines"
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
          float4 c_1;
          float2 uv_2;
          uv_2 = in_f.xlv_TEXCOORD0;
          float4 tmpvar_3;
          tmpvar_3 = tex2D(_MainTex, uv_2);
          c_1 = tmpvar_3;
          float tmpvar_4;
          tmpvar_4 = (max(c_1.x, max(c_1.y, c_1.z)) * _Value);
          float tmpvar_5;
          tmpvar_5 = (((uv_2.x + uv_2.y) + 500) * (345.678 + _Value2));
          float tmpvar_6;
          tmpvar_6 = frac(abs(tmpvar_5));
          float tmpvar_7;
          if((tmpvar_5>=0))
          {
              tmpvar_7 = tmpvar_6;
          }
          else
          {
              tmpvar_7 = (-tmpvar_6);
          }
          float tmpvar_8;
          tmpvar_8 = frac(abs(tmpvar_4));
          float tmpvar_9;
          if((tmpvar_4>=0))
          {
              tmpvar_9 = tmpvar_8;
          }
          else
          {
              tmpvar_9 = (-tmpvar_8);
          }
          if((tmpvar_9>tmpvar_7))
          {
              c_1.x = ceil(tmpvar_4);
          }
          else
          {
              c_1.x = floor(tmpvar_4);
          }
          c_1.x = (c_1.x / _Value);
          float4 tmpvar_10;
          tmpvar_10 = c_1.xxxw;
          out_f.color = tmpvar_10;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}