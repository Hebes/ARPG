Shader "CameraFilterPack/FB_AAA_Blood"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _MainTex2 ("Base (RGB)", 2D) = "white" {}
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
      uniform sampler2D _MainTex2;
      uniform float _Value;
      uniform float _Value2;
      uniform float _Value3;
      uniform float _Value4;
      uniform float _Value5;
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
          float3 col2_2;
          float3 col_3;
          float2 uv2_4;
          float2 uv_5;
          uv_5 = in_f.xlv_TEXCOORD0;
          uv2_4 = uv_5;
          uv_5 = ((in_f.xlv_TEXCOORD0 - float2(0.5, 0.5)) * 0.8);
          float tmpvar_6;
          tmpvar_6 = ((_Value2 + _Value3) + (_Value4 + _Value5));
          float tmpvar_7;
          tmpvar_7 = (tmpvar_6 * 0.000744444);
          float tmpvar_8;
          tmpvar_8 = sin(tmpvar_7);
          float tmpvar_9;
          tmpvar_9 = cos(tmpvar_7);
          float2x2 tmpvar_10;
          conv_mxt2x2_0(tmpvar_10).x = tmpvar_9;
          conv_mxt2x2_0(tmpvar_10).y = tmpvar_8;
          conv_mxt2x2_1(tmpvar_10).x = (-tmpvar_8);
          conv_mxt2x2_1(tmpvar_10).y = tmpvar_9;
          uv_5 = (mul(uv_5, tmpvar_10) + float2(0.5, 0.5));
          uv_5 = (uv_5 / (2 + (tmpvar_6 / 400)));
          float2 tmpvar_11;
          tmpvar_11.x = 0;
          tmpvar_11.y = (tmpvar_6 / 1600);
          uv_5 = (uv_5 + tmpvar_11);
          float4 tmpvar_12;
          tmpvar_12 = tex2D(_MainTex2, uv_5);
          uv_5 = (uv_5 + float2(0.5, 0));
          float4 tmpvar_13;
          tmpvar_13 = tex2D(_MainTex2, uv_5);
          col_3 = ((tmpvar_12.xyz * _Value4) + (tmpvar_13.xyz * _Value3));
          uv_5 = (uv_5 + float2(0, 0.5));
          float4 tmpvar_14;
          tmpvar_14 = tex2D(_MainTex2, uv_5);
          col_3 = (col_3 + (tmpvar_14.xyz * _Value5));
          uv_5 = (uv_5 - float2(0.5, 0));
          float4 tmpvar_15;
          tmpvar_15 = tex2D(_MainTex2, uv_5);
          col_3 = (col_3 + (tmpvar_15.xyz * _Value2));
          uv2_4 = (uv2_4 + (col_3.xx / 512));
          float3 tmpvar_16;
          tmpvar_16 = tex2D(_MainTex, uv2_4).xyz;
          col2_2 = tmpvar_16;
          col2_2 = (col2_2 + (col_3 * _Value));
          col2_2 = (col2_2 - (0.1 * col_3));
          float4 tmpvar_17;
          tmpvar_17.w = 1;
          tmpvar_17.xyz = float3(col2_2);
          tmpvar_1 = tmpvar_17;
          out_f.color = tmpvar_1;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
