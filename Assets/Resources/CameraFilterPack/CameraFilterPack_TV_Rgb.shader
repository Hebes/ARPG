Shader "CameraFilterPack/TV_Rgb"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _TimeX ("Time", Range(0, 1)) = 1
    _Distortion ("_Distortion", Range(1, 10)) = 1
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
      uniform float4 _ScreenResolution;
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
          float3 sum_1;
          float3 col_2;
          float2 tmpvar_3;
          tmpvar_3 = (in_f.xlv_TEXCOORD0 * _ScreenResolution.xy);
          float2 tmpvar_4;
          tmpvar_4 = (floor((tmpvar_3 / 6)) * 6);
          float2 tmpvar_5;
          tmpvar_5 = (tmpvar_3 / float2(6, 6));
          float2 tmpvar_6;
          tmpvar_6 = (frac(abs(tmpvar_5)) * float2(6, 6));
          float tmpvar_7;
          if((tmpvar_5.x>=0))
          {
              tmpvar_7 = tmpvar_6.x;
          }
          else
          {
              tmpvar_7 = (-tmpvar_6.x);
          }
          float tmpvar_8;
          if((tmpvar_5.y>=0))
          {
              tmpvar_8 = tmpvar_6.y;
          }
          else
          {
              tmpvar_8 = (-tmpvar_6.y);
          }
          col_2 = float3(0, 0, 0);
          float2 P_9;
          P_9 = (tmpvar_4 / _ScreenResolution.xy);
          float3 tmpvar_10;
          tmpvar_10 = tex2D(_MainTex, P_9).xyz;
          sum_1 = tmpvar_10;
          if((tmpvar_8<5))
          {
              if((tmpvar_7<2))
              {
                  col_2.x = sum_1.x;
              }
              else
              {
                  if((tmpvar_7<4))
                  {
                      col_2.y = sum_1.y;
                  }
                  else
                  {
                      col_2.z = sum_1.z;
                  }
              }
          }
          float4 tmpvar_11;
          tmpvar_11.w = 1;
          tmpvar_11.xyz = float3(col_2);
          out_f.color = tmpvar_11;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
