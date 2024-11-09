Shader "CameraFilterPack/FX_Grid"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _TimeX ("Time", Range(0, 1)) = 1
    _Distortion ("_Distortion", Range(0, 5)) = 1
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
      uniform float _Distortion;
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
          float lit_1;
          float yb_2;
          float xb_3;
          float2 uv_4;
          float tmpvar_5;
          tmpvar_5 = (0.3 * _Distortion);
          uv_4 = in_f.xlv_TEXCOORD0;
          float2 tmpvar_6;
          tmpvar_6 = frac((uv_4 * float2(60, 35.3)));
          float2 tmpvar_7;
          tmpvar_7 = (floor((uv_4 * float2(60, 35.3))) / float2(60, 35.3));
          float2 tmpvar_8;
          tmpvar_8.x = tmpvar_7.x;
          tmpvar_8.y = uv_4.y;
          float tmpvar_9;
          tmpvar_9 = dot(tex2D(_MainTex, tmpvar_8).xyz, float3(0.33, 0.33, 0.33));
          xb_3 = tmpvar_9;
          float2 tmpvar_10;
          tmpvar_10.x = uv_4.x;
          tmpvar_10.y = tmpvar_7.y;
          float tmpvar_11;
          tmpvar_11 = dot(tex2D(_MainTex, tmpvar_10).xyz, float3(0.33, 0.33, 0.33));
          yb_2 = tmpvar_11;
          float tmpvar_12;
          tmpvar_12 = abs(((tmpvar_6.y - (tmpvar_5 / 2)) - ((1 - tmpvar_5) * yb_2)));
          int tmpvar_13;
          if((tmpvar_12<(tmpvar_5 / 2)))
          {
              tmpvar_13 = int(1);
          }
          else
          {
              float tmpvar_14;
              tmpvar_14 = abs(((tmpvar_6.x - (tmpvar_5 / 2)) - ((1 - tmpvar_5) * xb_3)));
              tmpvar_13 = (tmpvar_14<(tmpvar_5 / 2));
          }
          float tmpvar_15;
          tmpvar_15 = float(tmpvar_13);
          lit_1 = tmpvar_15;
          float tmpvar_16;
          tmpvar_16 = ((yb_2 + xb_3) / 2);
          float4 tmpvar_17;
          tmpvar_17.xzw = float3(0, 0, 1);
          tmpvar_17.y = ((lit_1 * tmpvar_16) + (((1 - lit_1) * tmpvar_16) * 0.3));
          out_f.color = tmpvar_17;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
