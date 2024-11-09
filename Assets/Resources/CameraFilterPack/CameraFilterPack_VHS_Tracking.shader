Shader "CameraFilterPack/VHS_Tracking"
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
          float colx_1;
          float yt_2;
          float y_3;
          float x_4;
          float2 uv_org_5;
          float2 uv_6;
          uv_6 = in_f.xlv_TEXCOORD0;
          uv_org_5 = uv_6;
          float tmpvar_7;
          tmpvar_7 = floor((_TimeX * 0.6));
          float tmpvar_8;
          float tmpvar_9;
          tmpvar_9 = dot(float2(_TimeX, _TimeX), float2(12.98, 78.13));
          tmpvar_8 = ((abs(cos(_TimeX)) * frac((sin(tmpvar_9) * 43858.55))) * 100);
          float tmpvar_10;
          tmpvar_10 = ((uv_6.y * 32) - tmpvar_8);
          x_4 = (uv_6.x - ((sin(frac((sin(tmpvar_9) * 43858.55))) * 0.1) * exp(((-(tmpvar_10 * tmpvar_10)) / 24))));
          y_3 = uv_6.y;
          uv_6.x = x_4;
          uv_6.y = y_3;
          yt_2 = (0.5 * cos(tmpvar_8));
          float tmpvar_11;
          tmpvar_11 = (0.1 * cos(yt_2));
          colx_1 = 0;
          int tmpvar_12;
          if((uv_org_5.y>yt_2))
          {
              float2 tmpvar_13;
              tmpvar_13.x = tmpvar_7;
              tmpvar_13.y = _TimeX;
              float tmpvar_14;
              tmpvar_14 = frac((sin(dot(tmpvar_13, float2(12.98, 78.13))) * 43858.55));
              tmpvar_12 = (uv_org_5.y<(yt_2 + (tmpvar_14 * 0.25)));
          }
          else
          {
              tmpvar_12 = int(0);
          }
          if(tmpvar_12)
          {
              float tmpvar_15;
              float x_16;
              x_16 = (x_4 * 100);
              tmpvar_15 = (x_16 - (floor((x_16 * 0.1)) * 10));
              float tmpvar_17;
              tmpvar_17 = sin(_TimeX);
              float tmpvar_18;
              tmpvar_18 = sin((tmpvar_11 * 360));
              int tmpvar_19;
              if(((tmpvar_15 * tmpvar_17)>tmpvar_18))
              {
                  tmpvar_19 = int(1);
              }
              else
              {
                  float tmpvar_20;
                  tmpvar_20 = frac((sin(dot(float2(tmpvar_15, tmpvar_15), float2(12.98, 78.13))) * 43858.55));
                  tmpvar_19 = (tmpvar_20>0.4);
              }
              if(tmpvar_19)
              {
                  colx_1 = (frac((sin(dot(float2(tmpvar_7, tmpvar_7), float2(12.98, 78.13))) * 43858.55)) * _Value);
              }
          }
          float2 tmpvar_21;
          tmpvar_21 = lerp(uv_org_5, uv_6, float2(_Value, _Value));
          uv_6 = tmpvar_21;
          float4 tmpvar_22;
          tmpvar_22 = tex2D(_MainTex, tmpvar_21);
          float4 tmpvar_23;
          tmpvar_23.w = 1;
          tmpvar_23.xyz = (tmpvar_22.xyz + colx_1);
          out_f.color = tmpvar_23;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
