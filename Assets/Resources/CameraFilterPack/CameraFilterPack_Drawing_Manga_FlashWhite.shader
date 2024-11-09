Shader "CameraFilterPack/Drawing_Manga_FlashWhite"
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
      uniform float _Value4;
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
          float4 v_1;
          float2 tmpvar_2;
          tmpvar_2.x = _Value3;
          tmpvar_2.y = _Value4;
          float2 tmpvar_3;
          tmpvar_3 = (in_f.xlv_TEXCOORD0 - tmpvar_2);
          float tmpvar_4;
          tmpvar_4 = (3.141593 * _Value);
          float tmpvar_5;
          tmpvar_5 = (sqrt(dot(tmpvar_3, tmpvar_3)) / 0.7071068);
          float tmpvar_6;
          float tmpvar_7;
          tmpvar_7 = (min(abs((tmpvar_3.y / tmpvar_3.x)), 1) / max(abs((tmpvar_3.y / tmpvar_3.x)), 1));
          float tmpvar_8;
          tmpvar_8 = (tmpvar_7 * tmpvar_7);
          tmpvar_8 = (((((((((((-0.01213232 * tmpvar_8) + 0.05368138) * tmpvar_8) - 0.1173503) * tmpvar_8) + 0.1938925) * tmpvar_8) - 0.3326756) * tmpvar_8) + 0.9999793) * tmpvar_7);
          tmpvar_8 = (tmpvar_8 + (float((abs((tmpvar_3.y / tmpvar_3.x))>1)) * ((tmpvar_8 * (-2)) + 1.570796)));
          tmpvar_6 = (tmpvar_8 * sign((tmpvar_3.y / tmpvar_3.x)));
          if((abs(tmpvar_3.x)>(1E-08 * abs(tmpvar_3.y))))
          {
              if((tmpvar_3.x<0))
              {
                  if((tmpvar_3.y>=0))
                  {
                      tmpvar_6 = (tmpvar_6 + 3.141593);
                  }
                  else
                  {
                      tmpvar_6 = (tmpvar_6 - 3.141593);
                  }
              }
          }
          else
          {
              tmpvar_6 = (sign(tmpvar_3.y) * 1.570796);
          }
          float tmpvar_9;
          float _tmp_dvx_56 = ((floor((((tmpvar_6 + tmpvar_4) / (2 * tmpvar_4)) * 700)) / 700) * max((floor((_TimeX * _Value2)) / _Value2), 0.1));
          tmpvar_9 = ((frac((sin(dot(float2(_tmp_dvx_56, _tmp_dvx_56), float2(12.9898, 78.233))) * 43758.55)) * 0.7) + 0.3);
          float tmpvar_10;
          if((tmpvar_5>tmpvar_9))
          {
              tmpvar_10 = abs((tmpvar_5 - tmpvar_9));
          }
          else
          {
              tmpvar_10 = 0;
          }
          float4 tmpvar_11;
          tmpvar_11 = tex2D(_MainTex, in_f.xlv_TEXCOORD0);
          v_1 = tmpvar_11;
          v_1 = (v_1 + tmpvar_10);
          out_f.color = v_1;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}