Shader "CameraFilterPack/TV_Tiles"
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
          float3 col_2;
          float4 txt_3;
          float2 pos_4;
          pos_4 = ((256 * in_f.xlv_TEXCOORD0) + _TimeX);
          float4 tmpvar_5;
          tmpvar_5 = tex2D(_MainTex, in_f.xlv_TEXCOORD0);
          txt_3 = tmpvar_5;
          float tmpvar_6;
          float2 x_7;
          x_7 = (float2(0.5, 0.5) - in_f.xlv_TEXCOORD0);
          tmpvar_6 = sqrt(dot(x_7, x_7));
          float tmpvar_8;
          tmpvar_8 = clamp(((tmpvar_6 - _Value3) / ((_Value3 - _Value4) - _Value3)), 0, 1);
          float tmpvar_9;
          tmpvar_9 = (1 - (tmpvar_8 * (tmpvar_8 * (3 - (2 * tmpvar_8)))));
          col_2 = float3(0, 0, 0);
          int i_1_1 = 0;
          while((i_1_1<6))
          {
              float2 tmpvar_10;
              tmpvar_10 = floor(pos_4);
              float2 tmpvar_11;
              tmpvar_11 = frac(pos_4);
              float4 tmpvar_12;
              tmpvar_12 = frac(((sin((((tmpvar_10.x * 7) + (31 * tmpvar_10.y)) + (0.01 * _TimeX))) + float4(0.035, 0.01, 0, 0.7)) * 13.54532));
              float tmpvar_13;
              tmpvar_13 = clamp(((tmpvar_12.w - 0.45) / 0.1), 0, 1);
              col_2 = (col_2 + ((tmpvar_12.xyz * (tmpvar_13 * (tmpvar_13 * (3 - (2 * tmpvar_13))))) * sqrt(((((16 * tmpvar_11.x) * tmpvar_11.y) * (1 - tmpvar_11.x)) * (1 - tmpvar_11.y)))));
              pos_4 = (pos_4 / (2 * _Value));
              col_2 = (col_2 / 2);
              i_1_1 = (i_1_1 + 1);
          }
          col_2 = (txt_3.xyz * (pow((2.5 * col_2), float3(1, 1, 0.7)) * _Value2));
          float4 tmpvar_14;
          tmpvar_14.w = 1;
          tmpvar_14.xyz = lerp(txt_3.xyz, col_2, float3(tmpvar_9, tmpvar_9, tmpvar_9));
          out_f.color = tmpvar_14;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
