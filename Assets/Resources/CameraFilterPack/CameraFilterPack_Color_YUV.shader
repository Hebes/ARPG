Shader "CameraFilterPack/Color_YUV"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
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
      uniform float _Y;
      uniform float _U;
      uniform float _V;
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
          float4 col_2;
          float2 p_3;
          p_3 = in_f.xlv_TEXCOORD0;
          float4 tmpvar_4;
          tmpvar_4 = tex2D(_MainTex, p_3);
          col_2 = tmpvar_4;
          float Y_5;
          Y_5 = ((((0.299 * col_2.x) + (0.587 * col_2.y)) + (0.114 * col_2.z)) + _Y);
          float U_6;
          U_6 = ((((-0.147 * col_2.x) - (0.289 * col_2.y)) + (0.436 * col_2.z)) + _U);
          float V_7;
          V_7 = ((((0.615 * col_2.x) - (0.515 * col_2.y)) - (0.1 * col_2.z)) + _V);
          float3 tmpvar_8;
          tmpvar_8.x = (Y_5 + (1.14 * V_7));
          tmpvar_8.y = ((Y_5 - (0.395 * U_6)) - (0.581 * V_7));
          tmpvar_8.z = (Y_5 + (2.032 * U_6));
          col_2.xyz = float3(tmpvar_8);
          tmpvar_1 = col_2;
          out_f.color = tmpvar_1;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
