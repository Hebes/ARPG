Shader "CameraFilterPack/FX_Scan"
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
          float r_1;
          float4 frg_2;
          float2 uv_3;
          uv_3 = in_f.xlv_TEXCOORD0;
          frg_2 = float4(0, 0, 0, 1);
          float tmpvar_4;
          tmpvar_4 = tex2D(_MainTex, uv_3).x;
          r_1 = tmpvar_4;
          float tmpvar_5;
          tmpvar_5 = (sin((_TimeX * _Value2)) / 1.5);
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
          tmpvar_8 = (((uv_3.x - 0.4) - tmpvar_7) * 4);
          if(((tmpvar_8 - _Value)>r_1))
          {
              float4 tmpvar_9;
              tmpvar_9 = tex2D(_MainTex, uv_3);
              frg_2 = tmpvar_9;
          }
          else
          {
              if(((tmpvar_8 + _Value)>r_1))
              {
                  float2 tmpvar_10;
                  tmpvar_10.x = (uv_3.x + sin(((_TimeX * _Value2) * 9)));
                  tmpvar_10.y = (uv_3.y + (_TimeX * 5));
                  float4 tmpvar_11;
                  tmpvar_11 = tex2D(_MainTex, tmpvar_10);
                  frg_2 = tmpvar_11;
                  frg_2.x = (frg_2.x + 1);
                  frg_2.yz = (frg_2.yz - float2(1, 1));
              }
              else
              {
                  float4 tmpvar_12;
                  tmpvar_12 = tex2D(_MainTex, uv_3);
                  frg_2 = tmpvar_12;
              }
          }
          out_f.color = frg_2;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
